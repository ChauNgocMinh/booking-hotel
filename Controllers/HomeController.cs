//using HotelManagement.DataAccess;
//using HotelManagement.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Runtime.InteropServices;

//namespace HotelManagement.Controllers
//{
//    public class HomeController : Controller
//    {
//        private IRepository repo;
//        public HomeController(IRepository repository)
//        {
//            this.repo = repository;
//        }


//        [HttpGet]
//        [HttpPost]
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}


using HotelManagement.DataAccess;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repo;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HomeController(IRepository repository, IHttpContextAccessor accessor)
        {
            this.repo = repository;
            this.httpContextAccessor = accessor;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var loaiPhongs = repo.getLoaiPhong;       
            var khachSans = await repo.getListKhachSan(); 

            ViewData["LoaiPhongs"] = loaiPhongs;
            ViewData["KhachSans"] = khachSans;

            return View();
        }

        public IActionResult Profile()
        {
            var userName = httpContextAccessor.HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Account");
            }

            var account = repo.GetAccountByUserName(userName);
            if (account == null || account.Person == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Trả về view với model Person
            return View(account.Person);
        }
    }
}
