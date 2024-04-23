using ServiziMeteo;
using ServizioMeteo.Modelli;
using System.ServiceModel;
using System.Globalization;

namespace SOAPMeteo.MeteoLogic
{

    [ServiceContract]
    public interface ISOAPServices
    {
        [OperationContract]
        Rootobject Visualizza();

        [OperationContract]
        Previsione[] Ricerca(DateTime time);
    }

    public class SOAPServices : ISOAPServices
    {
        public Rootobject Visualizza()
        {
            //Visualizza il bollettino meteo nella sua interezza
            return RichiestaBollettino.Richiesta().Result;
        }

        public Previsione[] Ricerca(DateTime time)
        {
            var bollettino = RichiestaBollettino.Richiesta().Result;

            if (bollettino != null && bollettino.previsione != null)
            {
                time = DateParseHandling(time);

                // Filtra le previsioni in base alla data specificata
                bollettino.previsione = bollettino.previsione
                    .Select(p =>
                    {
                        p.giorni = p.giorni
                            .Where(g => DateTime.TryParse(g.giorno, out DateTime giorno) && giorno == time)
                            .ToArray();


                        return p;
                    })
                    .Where(p => p.giorni.Any())
                    .ToArray();

                return bollettino.previsione;
            }

            // Restituisce un bollettino vuoto se non ci sono dati disponibili
            return null;
        }

        //Metodo per la gestione delle date
        private DateTime DateParseHandling(DateTime time)
        {
            string dateString = time.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate;
            }
            else
            {
                throw new ArgumentException("Il formato della data non è valido. Utilizzare il formato 'yyyy-MM-dd'.");
            }
        }
    }
}
