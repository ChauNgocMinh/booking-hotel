using HotelManagement.DataAccess;
using HotelManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository repo;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountController(IRepository repo, IHttpContextAccessor accessor)
        {
            this.repo = repo;
            this.httpContextAccessor = accessor;
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(new TaiKhoan());
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(TaiKhoan account)
        {
            httpContextAccessor.HttpContext.Session.Clear();
            TaiKhoan check = repo.CheckAccount(account);
            if (check != null)
            {
                switch (check.LoaiTaiKhoan)
                {
                    case "LTK1":
                        httpContextAccessor.HttpContext.Session.SetString("admin", account.UserName);
                        break;
                    case "LTK2":
                        httpContextAccessor.HttpContext.Session.SetString("nhanvien", account.UserName);
                        break;
                    default:
                        httpContextAccessor.HttpContext.Session.SetString("UserName", account.UserName);
                        break;
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác");
            return View(account);
        }

        // GET: SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new PersonAndAccount());
        }

        // POST: SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(PersonAndAccount paa)
        {
            if (paa.confirm != paa.a.Password)
            {
                ModelState.AddModelError("", "Mật khẩu không giống nhau");
            }
            else
            {
                bool taoTaiKhoan = repo.CreateAccount(new TaiKhoan
                {
                    MaTaiKhoan = repo.CreateMaTaiKhoan(),
                    UserName = paa.a.UserName,
                    Password = paa.a.Password,
                    LoaiTaiKhoan = "LTK3",
                    Person = paa.p
                });

                if (!taoTaiKhoan)
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại");
                }
                else
                {
                    ModelState.AddModelError("", "Tạo tài khoản thành công");
                }
            }

            return View(paa);
        }

        // GET: Logout
        public IActionResult Logout()
        {
            httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
       

    }

    public class PersonAndAccount
    {
        public Person p { get; set; } = new Person();
        public TaiKhoan a { get; set; } = new TaiKhoan();
        public string confirm { get; set; }
    }
}
