using HotelManagement.DataAccess;
using HotelManagement.Models;
using HotelManagement.Models.Authentication;
using HotelManagement.ModelsView;
using HotelManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelManagement.Controllers
{
    public class RoomController : Controller
    {
        private IRepository repo;
        IHttpContextAccessor accessor;
        private readonly IVnPayService _vnPayservice;

        public RoomController(IRepository repo, IHttpContextAccessor accessor, IVnPayService vnPayservice)
        {
            this.repo = repo;
            this.accessor = accessor;
            _vnPayservice = vnPayservice;
        }

        LoaiPhongPhongTrangThaiPhong treetable = new LoaiPhongPhongTrangThaiPhong();

        [HttpGet]
        [HttpPost]
        public IActionResult Index(string loaiphong = null, string trangthaiphong = null, bool error = true)
        {
            if (loaiphong == null && trangthaiphong == null) treetable.phongs = repo.getPhongByLoaiPhong(null);
            else if (loaiphong == null) treetable.phongs = repo.getPhongByMaTrangThai(trangthaiphong);
            else if (trangthaiphong == null) treetable.phongs = repo.getPhongByLoaiPhong(loaiphong);

            treetable.trangthaiphongs = repo.getTrangThaiPhong;
            treetable.loaiphongs = repo.getLoaiPhong;
            treetable.dichvus = repo.getDichvu;
            treetable.error = error;
            if (accessor.HttpContext.Session.GetString("UserName") != null)
            {
                treetable.Person = repo.getPersonByUserName(accessor.HttpContext.Session.GetString("UserName"));
            }
            return View(treetable);
        }

        public IActionResult ChiTietPhong(string maphong)
        {
            var phong = repo.getChiTietPhong(maphong);
            return View(phong);
        }

        [HttpPost]
        [Authentication]
        public IActionResult AddReview(ReviewPhong review)
        {
            repo.AddReview(review);
            return RedirectToAction("ChiTietPhong", new { id = review.MaPhong });
        }

        [Authentication]
        public IActionResult datPhongVaDichVu(string hoten, int tuoi, int gioitinh, string cccd, string sdt, DateTime? ngayden, DateTime? ngaydi, string maphong, string selectedServiceIds, string servicePrice,
            string selectedQuantities, int trangThaiThanhToan, double tongTien)
        {
            if(trangThaiThanhToan == 1)
            {
                var vnPayModel = new VnPaymentRequestModel
                {
                    Amount = tongTien,
                    CreatedDate = DateTime.Now,
                    Description = $"{hoten} {sdt}",
                    FullName = hoten,
                    OrderId = new Random().Next(1000, 100000)
                };
                return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
            }
            var userName = accessor.HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrEmpty(hoten) || tuoi <= 0 || gioitinh < 0 || string.IsNullOrEmpty(cccd)
              || string.IsNullOrEmpty(sdt) || !ngayden.HasValue || !ngaydi.HasValue)
            {
                return RedirectToAction("Index", "Room", new { error = false });
            }


            Person person = repo.getPersonByUserName(userName);

            string maorderphong = repo.createOrderPhongId();
            OrderPhong orderphong = new OrderPhong
            {
                MaOrderPhong = maorderphong,
                NgayDen = ngayden,
                NgayDi = ngaydi,
                PersonId = person.PersonId,
                MaPhong = maphong,
                Person = person,
                TrangThaiThanhToan = trangThaiThanhToan,
                MaPhongNavigation = repo.getPhongByMaPhong(maphong)
            };
            repo.addOrderPhong(orderphong);

            // Thêm dịch vụ nếu có
            if (!string.IsNullOrEmpty(selectedServiceIds)
                && !string.IsNullOrEmpty(selectedQuantities)
                && !string.IsNullOrEmpty(servicePrice))
            {
                var madichvu = selectedServiceIds.Split(',').ToList();
                var soLuongMoiDichVu = selectedQuantities.Split(',').Select(int.Parse).ToList();
                var giaMoiDichVu = servicePrice.Split(',').Select(float.Parse).ToList();

                var orderphongdichvu = new List<OrderPhongDichVu>();
                for (int i = 0; i < madichvu.Count; i++)
                {
                    orderphongdichvu.Add(new OrderPhongDichVu
                    {
                        MaOrderPhong = maorderphong,
                        MaDichVu = madichvu[i],
                        SoLuong = soLuongMoiDichVu[i],
                        DonGia = giaMoiDichVu[i]
                    });
                }
                repo.addOrderPhongDichVu(orderphongdichvu);
            }

            repo.updateTrangThaiPhong(maphong, "MTT3"); // đặt trước
            return RedirectToAction("Index", "Room", new { error = true });
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
            HoaDon hd = new HoaDon
            {
                MaHoaDon = repo.createHoaDonId(),
                NgayIn = DateTime.Now,
                TongTien = float.Parse(tongtien),
                MaOrderPhong = maorder,
            };

            bool checkHoaDonOrderPHong = repo.addHoaDon(hd);
            if (checkHoaDonOrderPHong)
            {
                //sao khi thanh toán thì cập nhật trạng thái phòng lại
                repo.updateTrangThaiPhong(maphong, "MTT1");

                //cập nhật lại trạng thái thanh toán của order phòng
                repo.updateTrangThaiOrderPhong(maorder);

                return View("ThanhToanThanhCong");
            }
            else
            {
                return RedirectToAction("thanhToan", "Room", new { maphong = maphong });
            }

        }

        [AdminOrNhanVienAuthentication]
        public IActionResult xacNhanDatPhong(string maphong)
        {
            //khi xác nhận đặt phòng thì phòng chuyển sang trạng thái đã đặt nghĩa là khách đang ở
            repo.updateTrangThaiPhong(maphong, "MTT2");
            return RedirectToAction("Index");
        }


    }

    public class LoaiPhongPhongTrangThaiPhong
    {
        public LoaiPhongPhongTrangThaiPhong()
        {

        }
        public IEnumerable<LoaiPhong> loaiphongs { get; set; }
        public IEnumerable<Phong> phongs { get; set; }
        public IEnumerable<TrangThaiPhong> trangthaiphongs { get; set; }
        public IEnumerable<DichVu> dichvus { get; set; }
        public Person Person { get; set; } = null;
        public bool error { get; set; } = true;
    }

}
