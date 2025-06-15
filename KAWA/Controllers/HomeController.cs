using System.Web.Mvc;

namespace CoffeeAppMvc5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // To będzie nasza strona startowa z logo
            return View();
        }

        public ActionResult Dashboard()
        {
            // To będzie strona z linkami do CRUD
            ViewBag.Title = "Panel Zarządzania Kawą"; // Ustaw tytuł dla widoku Dashboard
            return View();
        }

        // Pozostałe domyślne akcje About i Contact możesz usunąć, jeśli nie są potrzebne
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}