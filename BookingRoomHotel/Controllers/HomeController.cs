using AutoMapper;
using BookingRoomHotel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using BookingRoomHotel.Models;

namespace BookingRoomHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private KhachHangViewModel customerViewModel;
        public HomeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: HomeController
        public async Task<ActionResult> Index()
        {
            customerViewModel = new KhachHangViewModel();
            TrangChuViewModel homeViewModel = new TrangChuViewModel();
            try
            {
                homeViewModel.PhongNgauNhien = await _context.Phongs.OrderBy(x => new Guid()).Take(3).ToListAsync();
                customerViewModel.TrangChu = homeViewModel;
                return View(customerViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Contact()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> LoaiPhongs()
        {
            var listLoaiPhongs = await _context.LoaiPhongs.ToListAsync();
            return View(listLoaiPhongs);
        }

        public async Task<IActionResult> ChiTietLoaiPhong(string id)
        {
            var LoaiPhong = await _context.LoaiPhongs.Include(rt => rt.Media).FirstOrDefaultAsync(rt => rt.MaLoaiPhong == int.Parse(id));
            ChiTietLoaiPhong ChiTietLoaiPhong = new ChiTietLoaiPhong();
            ChiTietLoaiPhong.DanhSachMedia = LoaiPhong.Media.ToList();
            ChiTietLoaiPhong.LoaiPhong = LoaiPhong;
            return View(ChiTietLoaiPhong);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm][Bind("CheckInDate,CheckOutDate,LoaiPhong,AreaFrom,GiaFrom")] TimKiemPhongViewModel search)
        {
            if (!ModelState.IsValid)
            {
                customerViewModel = new KhachHangViewModel();
                TrangChuViewModel trangChuViewModel = new TrangChuViewModel();
                try
                {
                    trangChuViewModel.PhongNgauNhien = await _context.Phongs.OrderBy(x => new Guid()).Take(3).ToListAsync();
                    customerViewModel.TrangChu = trangChuViewModel;
                    customerViewModel.TimKiemPhong = search;
                    return View(customerViewModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error");
                }
            }
            return RedirectToAction("Phongs", search);
        }

        public async Task<PhongViewModel> searchDanhSachPhong(TimKiemPhongViewModel search)
        {
            PhongViewModel listPhongViewModel = new PhongViewModel();
            var searchCheckInDate = search.NgayNhanPhong == null ? search.NgayTraPhong : search.NgayNhanPhong;
            var searchCheckOutDate = search.NgayTraPhong == null ? search.NgayNhanPhong : search.NgayTraPhong;

            var bookedMaPhongs = _context.DatPhongs.Where(booking => booking.TrangThai == "Booked" && (searchCheckInDate <= booking.NgayTraPhong && searchCheckOutDate >= booking.NgayNhanPhong)).Select(booking => booking.MaPhong).Distinct();

            var query = _context.Phongs.Where(room => !bookedMaPhongs.Contains(room.MaPhong));

            if (search.LoaiPhong.HasValue)
            {
                query = query.Include(r => r.LoaiPhong).Where(room => room.MaLoaiPhong == search.LoaiPhong);
                if (search.SoNguoiLon.HasValue)
                {
                    query = query.Where(room => room.LoaiPhong.SoLuongToiDa >= search.SoNguoiLon.Value);
                }

                if (search.DienTichTu.HasValue)
                {
                    query = query.Where(room => room.LoaiPhong.DienTich >= search.DienTichTu.Value);
                }

                if (search.DienTichDen.HasValue)
                {
                    query = query.Where(room => room.LoaiPhong.DienTich <= search.DienTichDen.Value);
                }
            }

            if (search.GiaTu.HasValue)
            {
                query = query.Where(room => room.Gia >= search.GiaTu.Value);
            }

            if (search.GiaDen.HasValue)
            {
                query = query.Where(room => room.Gia <= search.GiaDen.Value);
            }

            listPhongViewModel.DanhSachPhong = query.Take(6).ToList();
            int total = query.Count();
            listPhongViewModel.SoLuong = total % 6 == 0 ? total / 6 : total / 6 + 1;

            return listPhongViewModel;
        }

        public async Task<IActionResult> SearchRoom([FromForm] TimKiemPhongViewModel search)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PhongViewModel PhongViewModel = await searchDanhSachPhong(search);
                    return PartialView(PhongViewModel);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error");
                }
            }
            return BadRequest(search);
        }
        public async Task<IActionResult> Phongs(TimKiemPhongViewModel search)
        {
            TimKiemDanhSachPhongViewModel PhongViewModel = new TimKiemDanhSachPhongViewModel
            {
                PhongViewModel = await searchDanhSachPhong(search),
                TimKiemPhongViewModel = search
        };
            return View(PhongViewModel);
        }

        public async Task<IActionResult> Booking(DatPhong2ViewModel booking)
        {
             if (ModelState.IsValid)
            {
                try
                {
                    DatPhong book = _mapper.Map<DatPhong>(booking);
                    Phong room = _context.Phongs.Find(booking.MaKhachHang);
                    book.TrangThai = "Pending";
                    book.TongTien = room.Gia;
                    await _context.AddAsync(book);
                    ThongBaoKhachHang noti = new ThongBaoKhachHang
                    {
                        NgayTao = DateTime.Now,
                        TieuDe = "Booking Room Successful!",
                        NoiDung = $"Room number : {room.SoPhong}, Total Gia: {book.TongTien}$",
                        MaKhachHang = booking.MaKhachHang
                    };
                    await _context.AddAsync(noti);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, NoiDung = "Booking Successful! Check your notification...", noti = ObjectToJson(noti)});
                }catch (Exception ex) {
                    return Json(new { BookingError = "Network was not OK!" });
                }
            }
            return BadRequest(ModelState);
        }

        public string ObjectToJson(Object obj)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(obj, options);
        }

    }
}
