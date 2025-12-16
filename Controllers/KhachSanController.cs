using HotelManagement.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    public class KhachSanController : Controller
    {
        //public IActionResult Index()
        //{

        //    return View();
        //}
        private IRepository repo;
        public KhachSanController(IRepository repository)
        {
            this.repo = repository;
        }

        public IActionResult Index()
        {
            var khachSans = repo.getListKhachSan().Result;
            return View(khachSans);
        }

        public IActionResult Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var khachSan = repo.getListKhachSan().Result.FirstOrDefault(ks => ks.MaKhachSan == id);

            if (khachSan == null)
                return NotFound();

            return View(khachSan);
        }
    }
}
