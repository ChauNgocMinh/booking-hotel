using HotelManagement.DataAccess;
using HotelManagement.Models;
using HotelManagement.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    public class BookingHistoryOfUser : Controller
    {
        private IRepository repo;
        private IHttpContextAccessor accessor;
        public BookingHistoryOfUser(IRepository repo, IHttpContextAccessor accessor)
        {
            this.repo = repo;
            this.accessor = accessor;
        }

        [Authentication]
        public IActionResult Index()
        {
            var dichvus = repo.getDichVus.ToList();
            ViewData["DichVus"] = dichvus;
            string userName = accessor.HttpContext.Session.GetString("UserName");
            Person p = repo.getPersonByUserName(userName);
            IEnumerable<OrderPhong> oderPhongs = repo.getOrderPhongByPerson(p.PersonId);
            return View(oderPhongs);
        }

        [Authentication]
        public IActionResult removeOrder(string maorder, string maphong)
        {
            repo.removeOrderPhong(maorder);
            repo.updateTrangThaiPhong(maphong, "MTT1");
            return RedirectToAction("Index");
        }

        [Authentication]
        public IActionResult HuyPhong(string id)
        {
            repo.updateTrangThaiOrderPhong(id, "Cancel");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authentication]
        public async Task<IActionResult> DatThemDichVu(
            string orderPhongId,
            List<OrderPhongDichVu> items)
        {
            var validItems = new List<OrderPhongDichVu>();

            foreach (var item in items)
            {
                if (!item.SoLuong.HasValue || item.SoLuong <= 0)
                    continue;

                item.MaOrderPhong = orderPhongId;

                var dv = await repo.getDichvu(item.MaDichVu);
                item.DonGia = dv.GiaDichVu;

                validItems.Add(item);
            }

            if (validItems.Any())
            {
                repo.addOrderPhongDichVu(validItems);
            }
            return RedirectToAction("Index");
        }

    }
}
