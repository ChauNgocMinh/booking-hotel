using HotelManagement.DataAccess;
using HotelManagement.Models;
using HotelManagement.Models.Authentication;
using HotelManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    public class RoomController : Controller
    {
        private IRepository repo;
        IHttpContextAccessor accessor;
        private readonly PayPalService _payPalService;

        public RoomController(IRepository repo, IHttpContextAccessor accessor, PayPalService payPalService)
        {
            this.repo = repo;
            this.accessor = accessor;
            _payPalService = payPalService;
        }

        LoaiPhongPhongTrangThaiPhong treetable = new LoaiPhongPhongTrangThaiPhong();

        [HttpGet]
        public async Task<IActionResult> Index(
            string loaiphong = null,
            DateTime? ngayden = null,
            DateTime? ngaydi = null,
            string khachsan = null,
            bool error = true)
        {
            treetable.phongs = repo.FilterPhong(
                loaiphong,
                ngayden,
                ngaydi,
                khachsan
            );

            treetable.loaiphongs = repo.getLoaiPhong;
            treetable.dichvus = repo.getDichVus;
            treetable.khachSans = await repo.getListKhachSan();
            treetable.error = error;

            if (accessor.HttpContext.Session.GetString("UserName") != null)
                treetable.Person = repo.getPersonByUserName(
                    accessor.HttpContext.Session.GetString("UserName"));

            return View(treetable);
        }


        public IActionResult ChiTietPhong(string maphong)
        {
            var dichvus = repo.getDichVus.ToList();

            ViewData["DichVus"] = dichvus;
            var phong = repo.getChiTietPhong(maphong);
            return View(phong);
        }

        [HttpPost]
        public async Task<IActionResult> DatPhong(
            string MaPhong,
            DateTime NgayDen,
            DateTime NgayDi,
            int trangThaiThanhToan,
            List<string> DichVuIds)
        {
            var userName = accessor.HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
                return RedirectToAction("Login", "Account");

            var person = repo.getPersonByUserName(userName);
            var phong = repo.getPhongByMaPhong(MaPhong);

            int soNgay = (NgayDi - NgayDen).Days;
            decimal tongTien = soNgay * phong.Gia;

            if (DichVuIds != null && DichVuIds.Any())
            { 
                var dichvus = await repo.getDichVuByIds(DichVuIds);
                tongTien += dichvus.Sum(d => d.GiaDichVu);
            }

            if (trangThaiThanhToan == 1)
            {
                const decimal USD_EXCHANGE_RATE = 24500m;

                decimal tongTienUsd = Math.Round(
                    tongTien / USD_EXCHANGE_RATE,
                    2,
                    MidpointRounding.AwayFromZero
                );

                TempData["MaPhong"] = MaPhong;
                TempData["NgayDen"] = NgayDen.ToString();
                TempData["NgayDi"] = NgayDi.ToString();
                TempData["PersonId"] = person.PersonId;
                TempData["trangThaiThanhToan"] = trangThaiThanhToan;
                TempData["DichVuIds"] = DichVuIds != null ? string.Join(',', DichVuIds) : "";

                var paypalUrl = await _payPalService.CreatePayment(
                    amount: tongTienUsd,
                    returnUrl: Url.Action("PayPalSuccess", "ThanhToan", null, Request.Scheme),
                    cancelUrl: Url.Action("PayPalCancel", "ThanhToan", null, Request.Scheme)
                );

                return Redirect(paypalUrl);
            }

            string maOrder = repo.createOrderPhongId();
            repo.addOrderPhong(new OrderPhong
            {
                MaOrderPhong = maOrder,
                NgayDen = NgayDen,
                NgayDi = NgayDi,
                PersonId = person.PersonId,
                MaPhong = MaPhong,
                TrangThaiThanhToan = trangThaiThanhToan,
                TrangThaiDatPhong = "InActive"
            });
            if (DichVuIds != null && DichVuIds.Any())
            {
                var dichVus = await repo.getDichVuByIds(DichVuIds);

                var orderDichVus = dichVus.Select(dv => new OrderPhongDichVu
                {
                    MaOrderPhong = maOrder,
                    MaDichVu = dv.MaDichVu,
                    SoLuong = 1,
                    DonGia = dv.GiaDichVu
                }).ToList();

                repo.addOrderPhongDichVu(orderDichVus);
            }

            return RedirectToAction("BookingSuccess", "ThanhToan");
        }


        


        [AdminOrNhanVienAuthentication]
        [Route("[controller]/[action]/{maphong}/{successOrFail?}")]
        public IActionResult thanhToan(string maphong, string successOrFail = "0")
        {
            //TrangThaiThanhToan == 0 : chưa thanh toán
            //TrangThaiThanhToan == 1: đã thanh toán
            OrderPhong order = repo.getOrderPhongByMaPhong(maphong).FirstOrDefault(od => od.TrangThaiThanhToan == 0);
            return View("thanhToan", order);
        }

        [AdminOrNhanVienAuthentication]
        [Route("[controller]/[action]/maorder")]
        public IActionResult addHoadon(string maorder, string tongtien, string maphong)
        {
            string maHd = repo.createHoaDonId();
            HoaDon hd = new HoaDon
            {
                MaHoaDon = maHd,
                NgayIn = DateTime.Now,
                TongTien = float.Parse(tongtien),
                MaOrderPhong = maorder,
            };

            bool checkHoaDonOrderPHong = repo.addHoaDon(hd);
            if (checkHoaDonOrderPHong)
            {
                //cập nhật lại trạng thái thanh toán của order phòng
                repo.updateTrangThaiOrderPhong(maorder, "Done");
                return RedirectToAction("ChiTietHoaDon", "Admin", new { maHoaDon = maHd });

            }
            else
            {
                return RedirectToAction("QLPhongDaDat", "Admin");
            }

        }

        [AdminOrNhanVienAuthentication]
        public IActionResult xacNhanDatPhong(string maorder, string maphong)
        {
            if (!string.IsNullOrEmpty(maorder) && !string.IsNullOrEmpty(maphong))
            {
                // 1. Update trạng thái phòng
                repo.updateTrangThaiPhong(maphong, "MTT2"); // Đang ở

                // 2. Update trạng thái đặt phòng chỉ cho order được chọn
                var order = repo.getOrderPhongByMaPhong(maphong)
                                .FirstOrDefault(o => o.MaOrderPhong == maorder); 
                if (order != null)
                {
                    order.TrangThaiDatPhong = "Active";
                    repo.updateOrderPhong(order);
                }
            }
            return RedirectToAction("QLPhongDaDat", "Admin");
        }
    }

    public class LoaiPhongPhongTrangThaiPhong
    {
        public LoaiPhongPhongTrangThaiPhong()
        {

        }
        public IEnumerable<LoaiPhong> loaiphongs { get; set; }
        public IEnumerable<Phong> phongs { get; set; }
        public IEnumerable<DichVu> dichvus { get; set; }
        public List<KhachSan> khachSans { get; set; }
        public Person Person { get; set; } = null;
        public bool error { get; set; } = true;
    }

}
