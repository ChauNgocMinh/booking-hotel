using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingRoomHotel.Models;
using Microsoft.AspNetCore.Authorization;
using BookingRoomHotel.Models.ModelsInterface;
using BookingRoomHotel.ViewModels;


namespace BookingRoomHotel.Controllers
{
    public class CauHoisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public CauHoisController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: CauHois
        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> Index()
        {
            return _context.CauHois != null ?
                          PartialView(await getListViewCauHoi("1")) :
                          Problem("Entity set 'ApplicationDbContext.CauHoi'  is null.");
        }

        [HttpPost]
        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> Index(string id)
        {
            return _context.CauHois != null ?
                          PartialView(await getListViewCauHoi(id)) :
                          Problem("Entity set 'ApplicationDbContext.CauHoi'  is null.");
        }

        // GET: CauHois/Details/5
        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CauHois == null)
            {
                return NotFound();
            }

            var CauHoi = await _context.CauHois
                .FirstOrDefaultAsync(m => m.MaCauHoi == id);
            if (CauHoi == null)
            {
                return NotFound();
            }

            return PartialView(CauHoi);
        }

        // GET: CauHois/Create

        [Authorize(Policy = "AdminAndReceptPolicy")]
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: CauHois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromForm][Bind("Name,Email,ChuDe,NoiDung")] CauHoi CauHoi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CauHoi.TrangThai = "Pending";
                    _context.Add(CauHoi);
                    await _context.SaveChangesAsync();
                    _emailService.SendConfirmQ(CauHoi.Email, CauHoi.HoTen, CauHoi.ChuDe);
                    return RedirectToAction(nameof(Index));
                }else
                {
                    throw new Exception("Input not valid!");
                }
            }catch (Exception ex)
            {
                return Json(new { error = true, NoiDung = ex.Message});
            }

        }

        // GET: CauHois/Edit/5
        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CauHois == null)
            {
                return NotFound();
            }

            var CauHoi = await _context.CauHois.FindAsync(id);
            if (CauHoi == null)
            {
                return NotFound();
            }
            return PartialView(CauHoi);
        }

        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> ListEdit(string id)
        {
            return _context.CauHois != null ?
                          PartialView(await getListViewCauHoi(id)) :
                          Problem("Entity set 'ApplicationDbContext.CauHoi'  is null.");
        }

        [HttpPost]
        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> ListEdit(int id, [Bind("Id,Email,Name,ChuDe,NoiDung,PhanHoi,TrangThai")] CauHoi CauHoi)
        {
            if (id != CauHoi.MaCauHoi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(CauHoi);
                    await _context.SaveChangesAsync();
                    if (CauHoi.TrangThai.Equals("Complete"))
                    {
                        _emailService.SendPhanHoiQ(CauHoi.Email, CauHoi.HoTen, CauHoi.ChuDe, CauHoi.NoiDung, CauHoi.PhanHoi);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauHoiExists(CauHoi.MaCauHoi))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return _context.CauHois != null ?
                          PartialView(await getListViewCauHoi("1")) :
                          Problem("Entity set 'ApplicationDbContext.CauHoi'  is null.");
            }
            return PartialView(CauHoi);
        }

        // POST: CauHois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Name,ChuDe,NoiDung,PhanHoi,TrangThai")] CauHoi CauHoi)
        {
            if (id != CauHoi.MaCauHoi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(CauHoi);
                    await _context.SaveChangesAsync();
                    if (CauHoi.TrangThai.Equals("Complete"))
                    {
                        _emailService.SendPhanHoiQ(CauHoi.Email, CauHoi.HoTen, CauHoi.ChuDe, CauHoi.NoiDung, CauHoi.PhanHoi);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauHoiExists(CauHoi.MaCauHoi))
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
            return PartialView(CauHoi);
        }

        [Authorize(Policy = "AdminAndReceptPolicy")]
        public async Task<IActionResult> Delete(string id)
         {
            if (_context.CauHois == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CauHoi'  is null.");
            }
            var CauHoi = await _context.CauHois.FindAsync(id);
            if (CauHoi != null)
            {
                _context.CauHois.Remove(CauHoi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CauHoiExists(int id)
        {
          return (_context.CauHois?.Any(e => e.MaCauHoi == id)).GetValueOrDefault();
        }

        public async Task<DanhSachCauHoiViewModel> getListViewCauHoi(string id)
        {
            DanhSachCauHoiViewModel listCauHoiViewModel = new DanhSachCauHoiViewModel();
            listCauHoiViewModel.DanhSachCauHoi = await _context.CauHois.OrderBy(x => x.TrangThai == "Pending" ? 1 : x.TrangThai == "Processing" ? 2 : x.TrangThai == "Complete" ? 3 : 4).Skip(6 * (int.Parse(id) - 1)).Take(6).ToListAsync();
            int total = await _context.CauHois.CountAsync();
            listCauHoiViewModel.Count = total % 6 == 0 ? total / 6 : total / 6 + 1;
            return listCauHoiViewModel;
        }
    }
}
