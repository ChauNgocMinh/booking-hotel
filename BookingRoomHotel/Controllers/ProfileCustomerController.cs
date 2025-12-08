using Microsoft.AspNetCore.Mvc;
using BookingRoomHotel.Models;
using System.Data;
using BookingRoomHotel.ViewModels;


namespace BookingRoomHotel.Controllers
{
    public class ProfileCustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUploadFileService _uploadFileService;

        public ProfileCustomerController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IUploadFileService uploadFileService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _uploadFileService = uploadFileService;
        }

        // GET: ProfileCustomer
        public async Task<IActionResult> Index()
        {
            var CustomerId = HttpContext.Session.GetString("CustomerId");
            var Customer = await _context.KhachHangs.FindAsync(CustomerId);
            return (Customer == null) ?
                RedirectToAction("Error","Home") : View(CustomerToProfileView(Customer));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ThongTinKhachHangViewModel model)
        {
            var CustomerId = HttpContext.Session.GetString("CustomerId");
            var CustomertoUpdate = await _context.KhachHangs.FindAsync(CustomerId);

            if (ModelState.IsValid)
            {
                string profileFileName = _uploadFileService.uploadImage(model.AnhDaiDien, "images/customer/profile");
                string AnhCMND1 = _uploadFileService.uploadImage(model.AnhCMND1, "images/customer/AnhCMND1");
                string AnhCMND2 = _uploadFileService.uploadImage(model.AnhCMND2, "images/customer/AnhCMND2");
                CustomertoUpdate.HoTen = model.HoTen;
                CustomertoUpdate.Email = model.Email;
                CustomertoUpdate.SoDienThoai = model.SoDienThoai;
                CustomertoUpdate.DiaChi = model.DiaChi;
                CustomertoUpdate.NgaySinh = model.NgaySinh;
                CustomertoUpdate.AnhDaiDien = profileFileName == null? CustomertoUpdate.AnhDaiDien: profileFileName;
                CustomertoUpdate.AnhCMND1 = AnhCMND1 == null ? CustomertoUpdate.AnhCMND1 : AnhCMND1;
                CustomertoUpdate.AnhCMND2 = AnhCMND2 == null ? CustomertoUpdate.AnhCMND2 : AnhCMND2;
                _context.KhachHangs.Update(CustomertoUpdate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            return View(CustomerToProfileView(CustomertoUpdate));
        }

        private ProfileView CustomerToProfileView(KhachHang Customer)
        {
            ProfileView cus = new ProfileView();
            cus.Name = Customer.HoTen;
            cus.Email = Customer.Email;
            cus.SoDienThoai = Customer.SoDienThoai;
            cus.DiaChi = Customer.DiaChi;
            cus.NgaySinh = Customer.NgaySinh;
            cus.AnhDaiDien = Customer.AnhDaiDien;
            cus.AnhCMND1 = Customer.AnhCMND1;
            cus.AnhCMND2 = Customer.AnhCMND2;
            cus.TrangThai = Customer.TrangThai;
            return cus;
        }

    }
}