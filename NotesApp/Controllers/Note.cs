using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Controllers
{
    public class Note : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
