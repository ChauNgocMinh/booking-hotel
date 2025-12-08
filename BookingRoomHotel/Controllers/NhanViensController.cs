using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingRoomHotel.Models;
using Microsoft.AspNetCore.Authorization;
using BookingRoomHotel.ViewModels;


namespace BookingRoomHotel.Controllers
{
    public class NhanViensController : Controller
    {
        private readonly ApplicationDbContext _context;
        public NhanViensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NhanViens
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Index()
        {
            return _context.NhanViens != null ?
                          PartialView(await getListViewStaff("1")) :
                          Problem("Entity set 'ApplicationDbContext.NhanViens'  is null.");
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Index(string id)
        {
            return _context.NhanViens != null ?
                          PartialView(await getListViewStaff(id)) :
                          Problem("Entity set 'ApplicationDbContext.NhanViens'  is null.");
        }

        // GET: NhanViens/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var staff = await _context.NhanViens
                .FirstOrDefaultAsync(m => m.MaNhanVien == id);
            if (staff == null)
            {
                return NotFound();
            }

            return PartialView(staff);
        }

        // GET: NhanViens/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] [Bind("Id,Name,Email,SoDienThoai,NgaySinh,DiaChi,MatKhau,Role")] NhanVien staff)
        {
            if (ModelState.IsValid)
            {
                staff.TrangThai = "Action";
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(staff);
        }

        // GET: NhanViens/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var staff = await _context.NhanViens.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return PartialView(staff);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [FromForm] [Bind("Id,Name,Email,SoDienThoai,NgaySinh,DiaChi,MatKhau,Role")] NhanVien staff)
        {
            if (id != staff.MaNhanVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.MaNhanVien))
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
            return PartialView(staff);
        }

        // GET: NhanViens/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var staff = await _context.NhanViens
                .FirstOrDefaultAsync(m => m.MaNhanVien == id);
            if (staff == null)
            {
                return NotFound();
            }

            return PartialView(staff);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromForm] string id)
        {
            if (_context.NhanViens == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NhanViens'  is null.");
            }
            var staff = await _context.NhanViens.FindAsync(id);
            if (staff != null)
            {
                _context.NhanViens.Remove(staff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(string id)
        {
          return (_context.NhanViens?.Any(e => e.MaNhanVien == id)).GetValueOrDefault();
        }

        public async Task<ListsStaffViewModel> getListViewStaff(string id)
        {
            ListsStaffViewModel listStaffViewModel = new ListsStaffViewModel();
            listStaffViewModel.ListStaff = await _context.NhanViens.OrderByDescending(x => x.TrangThai).Skip(6 * (int.Parse(id) - 1)).Take(6).ToListAsync();
            int total = await _context.NhanViens.CountAsync();
            listStaffViewModel.Count = total % 6 == 0 ? total / 6 : total / 6 + 1;
            return listStaffViewModel;
        }
    }
}
