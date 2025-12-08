
using BookingRoomHotel.Models;
using BookingRoomHotel.Models.ModelsInterface;
using BookingRoomHotel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoomHotel.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        public AdminController(ApplicationDbContext context, ITokenService tokenService, IConfiguration configuration){ 
            _context = context;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string NoiDung = TempData["NoiDung"] as string;
            if (!string.IsNullOrEmpty(NoiDung))
            {
                ViewBag.NoiDung = NoiDung;
            }
            return View();
        }

        public ViewResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([FromForm] StaffLoginViewModel model)
        {
            var staff = _context.NhanViens.Find(model.TenDangNhap);
            if (staff != null && staff.MatKhau.Equals(model.Password))
            {
                TempData["NoiDung"] = "Login Successful!";
                return RedirectToAction("Dashboard", "Admin");
            }
            TempData["NoiDung"] = "Login Failed!";
            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
            return View(model); 
        }

        public ViewResult Dashboard()
        {
            return View();
        }
    }
}
