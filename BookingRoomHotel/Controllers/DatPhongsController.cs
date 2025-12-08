
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using BookingRoomHotel.ViewModels;
using BookingRoomHotel.Models;

namespace BookingPhongHotel.Controllers
{
    public class DatPhongsController : Controller
    {
        // GET: DatPhongsController
        private readonly ApplicationDbContext _context;
        public DatPhongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DatPhongs
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Index()
        {
            return _context.DatPhongs != null ?
                          PartialView(await getListViewBooking("1")) :
                          Problem("Entity set 'ApplicationDbContext.DatPhongs'  is null.");
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Index(string id)
        {
            return _context.DatPhongs != null ?
                          PartialView(await getListViewBooking(id)) :
                          Problem("Entity set 'ApplicationDbContext.DatPhongs'  is null.");
        }

        // GET: DatPhongs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DatPhongs == null)
            {
                return NotFound();
            }

            var booking = await _context.DatPhongs
                .FirstOrDefaultAsync(m => m.MaPhong == int.Parse(id));
            if (booking == null)
            {
                return NotFound();
            }

            return PartialView(booking);
        }

        // GET: DatPhongs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: DatPhongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromForm][Bind("Id,Name,Email,SoDienThoai,NgaySinh,DiaChi,MatKhau,Role")] DatPhong booking)
        {
            if (ModelState.IsValid)
            {
                booking.TrangThai = "Action";
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(booking);
        }

        // GET: DatPhongs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DatPhongs == null)
            {
                return NotFound();
            }

            var booking = await _context.DatPhongs.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return PartialView(booking);
        }

        // POST: DatPhongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [FromForm][Bind("Id,Name,Email,SoDienThoai,NgaySinh,DiaChi,MatKhau,Role")] DatPhong booking)
        {
            if (int.Parse(id) != booking.MaPhong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!bookingExists(booking.MaPhong.ToString()))
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
            return PartialView(booking);
        }

        // GET: DatPhongs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DatPhongs == null)
            {
                return NotFound();
            }

            var booking = await _context.DatPhongs
                .FirstOrDefaultAsync(m => m.MaPhong == int.Parse(id));
            if (booking == null)
            {
                return NotFound();
            }

            return PartialView(booking);
        }

        // POST: DatPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromForm] string id)
        {
            if (_context.DatPhongs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DatPhongs'  is null.");
            }
            var booking = await _context.DatPhongs.FindAsync(id);
            if (booking != null)
            {
                _context.DatPhongs.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool bookingExists(string id)
        {
            return (_context.DatPhongs?.Any(e => e.MaPhong == int.Parse(id))).GetValueOrDefault();
        }

        public async Task<DatPhongViewModel> getListViewBooking(string id)
        {
            DatPhongViewModel listBooking = new DatPhongViewModel();
            listBooking.DanhSachDatPhong = await _context.DatPhongs.OrderByDescending(x => x.MaPhong).Skip(6 * (int.Parse(id) - 1)).Take(6).ToListAsync();
            int total = await _context.DatPhongs.CountAsync();
            listBooking.SoLuong = total % 6 == 0 ? total / 6 : total / 6 + 1;
            return listBooking;
        }

        [HttpGet]
		public async Task<IActionResult> GetListBookingJson()
		{
			try
			{
				var DatPhongs = await _context.DatPhongs.Include(r => r.Phong.LoaiPhong).Select(booking => new
				{
					MaPhong = booking.MaPhong,
					CheckInDate = booking.NgayNhanPhong,
					TenDangNhap = booking.NgayTraPhong,
					PhongID = booking.Phong.SoPhong,
					MaKhachHang = booking.MaKhachHang,
					TongTien = booking.TongTien,
					TrangThai = booking.TrangThai,
                    LoaiPhong = booking.Phong.LoaiPhong.TenLoaiPhong
				}).ToListAsync();

				JsonSerializerOptions options = new JsonSerializerOptions
				{
					ReferenceHandler = ReferenceHandler.IgnoreCycles,
					WriteIndented = true
				};

				var json = JsonSerializer.Serialize(DatPhongs, options);

				return Content(json, "application/json");
			}
			catch (Exception ex)
			{
				return BadRequest("An error occurred while fetching DatPhongs.");
			}
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
