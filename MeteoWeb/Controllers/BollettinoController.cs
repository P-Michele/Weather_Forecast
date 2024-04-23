using MeteoWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiziMeteo;
using ServizioMeteo.Modelli;

namespace MeteoWeb.Controllers
{
    public class BollettinoController : Controller
    {

        public async Task<IActionResult> Visualizza()
        {
            try
            {
                // Ottiene i dati più recenti
                var bollettino = await RichiestaBollettino.Richiesta();


                if (bollettino != null)
                {
                    //Crea il viewmodel coi dati del bollettino medio
                    BollettinoVisualizzaViewModel vm = new BollettinoVisualizzaViewModel(bollettino);

                    // Passa il view model alla vista
                    return View(vm);
                }
                else
                {
                    return View("Errore");
                }
            }
            catch (Exception ex)
            {
                return View("Errore");
            }
        }
    }
}
