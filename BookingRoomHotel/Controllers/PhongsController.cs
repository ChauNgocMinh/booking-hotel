using AutoMapper;
using BookingRoomHotel.Models;
using BookingRoomHotel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingRoomHotel.Controllers
{
    public class PhongsController : Controller
    {
        // GET: PhongsController
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadFileService _uploadFileService;
        public PhongsController(ApplicationDbContext context, IMapper mapper, IUploadFileService uploadFileService)
        {
            _context = context;
            _mapper = mapper;
            _uploadFileService = uploadFileService;
        }

        // GET: Phongs
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Index()
        {
            return _context.Phongs != null ?
                          PartialView(await getListViewRoom("1")) :
                          Problem("Entity set 'ApplicationDbContext.Phongs'  is null.");
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Index(string id)
        {
            return _context.Phongs != null ?
                          PartialView(await getListViewRoom(id)) :
                          Problem("Entity set 'ApplicationDbContext.Phongs'  is null.");
        }

        // GET: Phongs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Phongs == null)
            {
                return NotFound();
            }

            var room = await _context.Phongs
                .FirstOrDefaultAsync(m => m.MaPhong == int.Parse(id));
            if (room == null)
            {
                return NotFound();
            }

            return PartialView(room);
        }

        // GET: Phongs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Phongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromForm][Bind("SoPhong,MaLoaiPhong,Gia,Describe,HotelID,Image")] TaoPhongViewModel roomC)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Phong room = _mapper.Map<Phong>(roomC);
                    string roomImg = _uploadFileService.uploadImage(roomC.Anh, "images/Admin/Phongs");
                    room.AnhPhong = roomImg == null ? room.AnhPhong : roomImg;
                    _context.Add(room);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return PartialView(roomC);
            }
        }

        // GET: Phongs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Phongs == null)
            {
                return NotFound();
            }

            var room = await _context.Phongs.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return PartialView(room);
        }

        // POST: Phongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [FromForm][Bind("Id,Name,Email,SoDienThoai,NgaySinh,DiaChi,MatKhau,Role")] Phong room)
        {
            if (int.Parse(id) != room.MaPhong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.MaPhong.ToString()))
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
            return PartialView(room);
        }

        // GET: Phongs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Phongs == null)
            {
                return NotFound();
            }

            var room = await _context.Phongs
                .FirstOrDefaultAsync(m => m.MaPhong == int.Parse(id));
            if (room == null)
            {
                return NotFound();
            }

            return PartialView(room);
        }

        // POST: Phongs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromForm] string id)
        {
            if (_context.Phongs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Phongs'  is null.");
            }
            var room = await _context.Phongs.FindAsync(id);
            if (room != null)
            {
                _context.Phongs.Remove(room);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(string id)
        {
            return (_context.Phongs?.Any(e => e.MaPhong == int.Parse(id))).GetValueOrDefault();
        }

        public async Task<PhongViewModel> getListViewRoom(string id)
        {
            PhongViewModel listPhongViewModel = new PhongViewModel();
            listPhongViewModel.DanhSachPhong = await _context.Phongs.OrderByDescending(x => x.MaPhong).Skip(6 * (int.Parse(id) - 1)).Take(6).ToListAsync();
            int total = await _context.Phongs.CountAsync();
            listPhongViewModel.SoLuong = total % 6 == 0 ? total / 6 : total / 6 + 1;
            return listPhongViewModel;
        }

        
    }
}
