using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using BookingRoomHotel.Models.ModelsInterface;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;
using BookingRoomHotel.ViewModels;
using BookingRoomHotel.Models;

namespace BookingRoomHotel.Controllers
{
    public class KhachHangsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public KhachHangsController(ApplicationDbContext context, IEmailService emailService, ITokenService tokenService, IConfiguration configuration)
        {
            _context = context;
            _emailService = emailService;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        // GET: KhachHangs
        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> Index()
         {
            DanhSachKhachHangViewModel listCustomerViewModel = new DanhSachKhachHangViewModel();
            listCustomerViewModel.DanhSachKhachHang = await _context.KhachHangs.OrderByDescending(x => x.TrangThai).Take(6).ToListAsync();
            int total = await _context.KhachHangs.CountAsync();
            listCustomerViewModel.SoLuong = total % 6 == 0? total /6 : total / 6 + 1;
            return _context.KhachHangs != null ?
                        PartialView(listCustomerViewModel):
                        Problem("Entity set 'ApplicationDbContext.KhachHangs'  is null.");
        }

        [HttpPost, ActionName("Index")]
        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> Index(string id)
        {
            DanhSachKhachHangViewModel listCustomerViewModel = new DanhSachKhachHangViewModel();
            listCustomerViewModel.DanhSachKhachHang = await _context.KhachHangs.OrderByDescending(x => x.TrangThai).Skip(6 * (int.Parse(id) - 1)).Take(6).ToListAsync();
            int total = await _context.KhachHangs.CountAsync();
            listCustomerViewModel.SoLuong = total % 6 == 0 ? total / 6 : total / 6 + 1;
            return _context.KhachHangs != null ?
                        PartialView(listCustomerViewModel) :
                        Problem("Entity set 'ApplicationDbContext.KhachHangs'  is null.");
        }

        // GET: KhachHangs/Details/5
        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.KhachHangs == null)
            {
                return NotFound();
            }

            var customer = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKhachHang == id);
            if (customer == null)
            {
                return NotFound();
            }

            return PartialView(customer);
        }

        // GET: KhachHangs/Create
        [HttpGet]
        [Authorize(Policy = "AdminAndReceptPolicy")]
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> Create([FromForm][Bind("Id,Name,Email,SoDienThoai,NgaySinh,DiaChi,MatKhau")] KhachHang customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(customer);
        }

        // GET: KhachHangs/Edit/5
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.KhachHangs == null)
            {
                return NotFound();
            }

            var customer = await _context.KhachHangs.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return PartialView(customer);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Edit(string id, [FromForm][Bind("Id,Name,Email,SoDienThoai,NgaySinh,DiaChi,MatKhau,TrangThai,AnhCMND1,AnhCMND2,AnhDaiDien")] KhachHang customer)
        {
            if (ModelState.IsValid )
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.MaKhachHang))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView(customer);
        }

        // GET: KhachHangs/Delete/5
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(string id)
        {
            if (_context.KhachHangs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.KhachHangs'  is null.");
            }
            var customer = await _context.KhachHangs.FindAsync(id);
            if (customer != null)
            {
                _context.KhachHangs.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return (_context.KhachHangs?.Any(e => e.MaKhachHang == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] DangKyKhachHangViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.MatKhau.Equals(model.MatKhauXacNhan))
                {
                    if (await _context.KhachHangs.FindAsync(model.MaKhachHang) != null)
                    {
                        throw new Exception("ID already existed!");
                    }
                    else
                    {
                        _emailService.SendRegisterMail(model.Email, model.HoTen, model.MaKhachHang, model.MatKhau);
                        var cus = new KhachHang
                        {
                            MaKhachHang = model.MaKhachHang,
                            MatKhau = model.MatKhau,
                            HoTen = model.HoTen,
                            Email = model.Email,
                            DiaChi = model.DiaChi,
                            NgaySinh = model.NgaySinh,
                            SoDienThoai = model.SoDienThoai,
                            TrangThai = "Unverified"
                        };
                        HttpContext.Session.SetString("MaKhachHang", model.MaKhachHang);
                        await _context.KhachHangs.AddAsync(cus);
                        await _context.SaveChangesAsync();
                        return Json(new { success = true, NoiDung = "Register Successful! Please check your email!", accessToken = _tokenService.GenerateAccessToken(cus.MaKhachHang, cus.HoTen, "customer"), role = "customer", name = cus.HoTen });
                    }
                }
                throw new Exception("Your password does not match!");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Register Failed! Error: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] DangNhapKhachHangViewModel model)
        {
            try
            {
                var cus = await _context.KhachHangs.Include(r => r.ThongBaoKhachHang).FirstOrDefaultAsync(x => x.MaKhachHang == model.TenDangNhap);
                if (cus != null && cus.MatKhau.Equals(model.MatKhau))
                {
                    HttpContext.Session.SetString("MaKhachHang", model.TenDangNhap);
                    JsonSerializerOptions options = new()
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles,
                        WriteIndented = true
                    };
                    string listNoti = JsonSerializer.Serialize(cus.ThongBaoKhachHang.ToList(), options);
                    return Json(new { success = true, NoiDung = "Login Successful!", accessToken = _tokenService.GenerateAccessToken(cus.MaKhachHang, cus.HoTen, "customer"), role = "customer", name = cus.HoTen, avt = cus.AnhDaiDien, id = cus.MaKhachHang, listNoti = listNoti });
                }
                else
                {
                    throw new Exception("ID or Password not correct!");
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Login Failed! Error: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromForm] DoiMatKhauKhachHangViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.MatKhauMoi.Equals(model.XacNhanMatKhauMoi))
                {
                    var cus = _context.KhachHangs.Find(model.MaKhachHang);
                    if (cus != null && cus.MatKhau.Equals(model.MatKhauCu))
                    {
                        cus.MatKhau = model.MatKhauMoi;
                        await _context.SaveChangesAsync();
                        _emailService.SendChangePasswordMail(cus.Email, cus.HoTen, cus.MatKhau);
                        return Json(new { success = true, NoiDung = "Change Password successful! Please check your email!" });
                    }
                    else
                    {
                        throw new Exception("Your information not correctly!");
                    }
                }
                else
                {
                    throw new Exception("Your input not correct!");
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Change Password Failed! Error: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromForm] QuenMatKhauKhachHangViewModel model)
        {
            try
            {
                var cus = await _context.KhachHangs.FindAsync(model.MaKhachHang);
                if (cus != null && cus.Email.Equals(model.Email))
                {
                    _emailService.SendForgotPasswordMail(cus.Email, cus.HoTen, cus.MatKhau);
                    return Json(new { success = true, NoiDung = "Your password has been sent via email. Please check your email!" });
                    }
                else
                {
                    throw new Exception("Your ID or Email not correct!");
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Get password Failed! Error: " + ex.Message });
            }
        }


        
    }
}