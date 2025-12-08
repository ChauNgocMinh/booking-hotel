using AutoMapper;

using BookingRoomHotel.Models;
using BookingRoomHotel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingRoomHotel.Controllers
{
    public class LoaiPhongsController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly IUploadFileService _uploadFileService;
		private readonly IMapper _mapper;
		public LoaiPhongsController(ApplicationDbContext context, IUploadFileService uploadFileService, IMapper mapper)
		{
			_context = context;
			_uploadFileService = uploadFileService;
			_mapper = mapper;
		}

		// GET: LoaiPhongs
		public async Task<IActionResult> Index()
		{
			DanhSachLoaiPhongViewModel listLoaiPhongViewModel = new DanhSachLoaiPhongViewModel();
			listLoaiPhongViewModel.DanhSachLoaiPhong = await _context.LoaiPhongs.Take(6).ToListAsync();
			int total = await _context.LoaiPhongs.CountAsync();
			listLoaiPhongViewModel.Count = total % 6 == 0 ? total / 6 : total / 6 + 1;
			return _context.LoaiPhongs != null ?
						  PartialView(listLoaiPhongViewModel) :
						  Problem("Entity set 'ApplicationDbContext.LoaiPhongs'  is null.");
		}

		[HttpPost]
		public async Task<IActionResult> Index(string id)
		{
            DanhSachLoaiPhongViewModel listLoaiPhongViewModel = new DanhSachLoaiPhongViewModel();
			listLoaiPhongViewModel.DanhSachLoaiPhong = await _context.LoaiPhongs.Skip(6 * (int.Parse(id) - 1)).Take(6).ToListAsync();
			int total = await _context.LoaiPhongs.CountAsync();
			listLoaiPhongViewModel.Count = total % 6 == 0 ? total / 6 : total / 6 + 1;
			return _context.LoaiPhongs != null ?
						  PartialView(listLoaiPhongViewModel) :
						  Problem("Entity set 'ApplicationDbContext.LoaiPhongs'  is null.");
		}

		// GET: LoaiPhongs/Details/5
		public async Task<IActionResult> Details(string id)
		{
			if (id == null || _context.LoaiPhongs == null)
			{
				return NotFound();
			}

			var Loai = await _context.LoaiPhongs
				.FirstOrDefaultAsync(m => m.MaLoaiPhong == int.Parse(id));
			if (Loai == null)
			{
				return NotFound();
			}

			return PartialView(Loai);
		}

		// GET: LoaiPhongs/Create
		public IActionResult Create()
		{
			return PartialView();
		}

		// POST: LoaiPhongs/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		public async Task<IActionResult> Create([FromForm][Bind("LoaiName,SoLuongToiDa,Bed,Size,View,MoTa1,MoTa2,MoTa3,GiaFrom,GiaTo,Images,VideoID")] TaoLoaiPhongViewModel Loai)
		{
			if (ModelState.IsValid)
			{
				LoaiPhong LoaiPhong = ConvertTaoLoaiPhongViewModelToLoaiPhong(Loai);
				_context.Add(LoaiPhong);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return PartialView(Loai);
		}

		// GET: LoaiPhongs/Edit/5
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null || _context.LoaiPhongs == null)
			{
				return NotFound();
			}

			var staff = await _context.LoaiPhongs.FindAsync(int.Parse(id));
			if (staff == null)
			{
				return NotFound();
			}
			return PartialView(staff);
		}

		// POST: LoaiPhongs/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        public async Task<IActionResult> Edit(string id, [FromForm][Bind("MaLoaiPhong,LoaiName,SoLuongToiDa,Bed,Size,View,MoTa1,MoTa2,MoTa3,GiaFrom,GiaTo,Images,VideoID")] TaoLoaiPhongViewModel LoaiPhong)
		{
			if (int.Parse(id) != LoaiPhong.MaLoaiPhong)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
                    LoaiPhong Loai = ConvertTaoLoaiPhongViewModelToLoaiPhong(LoaiPhong);
                    _context.Update(Loai);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					
				}
				return RedirectToAction(nameof(Index));
			}
			return PartialView(LoaiPhong);
		}

		// GET: LoaiPhongs/Delete/5
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null || _context.LoaiPhongs == null)
			{
				return NotFound();
			}

			var staff = await _context.LoaiPhongs
				.FirstOrDefaultAsync(m => m.MaLoaiPhong == int.Parse(id));
			if (staff == null)
			{
				return NotFound();
			}

			return PartialView(staff);
		}

		// POST: LoaiPhongs/Delete/5
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed([FromForm] string id)
		{
			if (_context.LoaiPhongs == null)
			{
				return Problem("Entity set 'ApplicationDbContext.LoaiPhongs'  is null.");
			}
			var staff = await _context.LoaiPhongs.FindAsync(id);
			if (staff != null)
			{
				_context.LoaiPhongs.Remove(staff);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
        public LoaiPhong ConvertTaoLoaiPhongViewModelToLoaiPhong(TaoLoaiPhongViewModel model)
        {
            List<Media> listMedia = new List<Media>();
            if (model.HinhAnh != null)
            {
                listMedia = _uploadFileService.uploadListImage(model.HinhAnh, "images/Admin/LoaiPhongs");
            }

            if (model.VideoID != null)
			{
                Media video = new Media
                {
                    DungCho = "LoaiPhong",
                    Loai = "video",
                    URL = model.VideoID
                };
                listMedia.Add(video);
            }
			
            LoaiPhong LoaiPhong = _mapper.Map<LoaiPhong>(model);
			if (model.MaLoaiPhong != null) LoaiPhong.MaLoaiPhong = (int) model.MaLoaiPhong;
			if (listMedia.Count > 0)
			{
				LoaiPhong.Media = listMedia;
			}
			return LoaiPhong;
        }

    }

	
}
