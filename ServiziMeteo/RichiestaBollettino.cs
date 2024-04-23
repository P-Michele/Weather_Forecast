using Newtonsoft.Json;

namespace ServiziMeteo
{
    public class RichiestaBollettino
    {

        public static async Task<ServizioMeteo.Modelli.Rootobject> Richiesta()
        {
            string uri = "https://www.meteotrentino.it/protcivtn-meteo/api/front/previsioneOpenDataLocalita";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        // Deserializza la risposta JSON
                        ServizioMeteo.Modelli.Rootobject rootObject = JsonConvert.DeserializeObject<ServizioMeteo.Modelli.Rootobject>(result);

                        // Ritorna l'oggetto deserializzato
                        return rootObject;
                    }
                    else
                    {
                        throw new HttpRequestException($"Errore nella richiesta HTTP. Codice: {response.StatusCode}");
                    }
                }  
            }
        }

    }
}
