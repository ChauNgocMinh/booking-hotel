using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    public class CuDanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
