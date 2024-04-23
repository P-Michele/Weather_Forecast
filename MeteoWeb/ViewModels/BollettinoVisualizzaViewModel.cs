using ServizioMeteo.Modelli;  // Assicurati di includere il namespace corretto

namespace MeteoWeb.ViewModels
{
    public class BollettinoVisualizzaViewModel
    {
        public List<string> Localita { get; set; }
        public List<int> Quota { get; set; }
        public List<string> Giorno { get; set; }
        public List<int> TemperaturaMinima { get; set; }
        public List<int> TemperaturaMassima { get; set; }
        public List<string> DescrizioneCondizioni { get; set; }
        public List<string> Icona { get; set; }

        public BollettinoVisualizzaViewModel(Rootobject rootObject)
        {
            if (rootObject != null && rootObject.previsione != null)
            {
                Localita = rootObject.previsione.SelectMany(p => p.giorni.Select(g => p.localita)).ToList();
                Quota = rootObject.previsione.SelectMany(p => p.giorni.Select(g => p.quota)).ToList();
                Giorno = rootObject.previsione.SelectMany(p => p.giorni.Select(g => g.giorno)).ToList();
                TemperaturaMinima = rootObject.previsione.SelectMany(p => p.giorni.Select(g => g.tMinGiorno)).ToList();
                TemperaturaMassima = rootObject.previsione.SelectMany(p => p.giorni.Select(g => g.tMaxGiorno)).ToList();
                DescrizioneCondizioni = rootObject.previsione.SelectMany(p => p.giorni.Select(g => g.testoGiorno)).ToList();
                Icona = rootObject.previsione.SelectMany(p => p.giorni.Select(g => g.icona)).ToList();
            }
        }
    }
}