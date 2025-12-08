//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.Models
//{
//    public class ThongBaoKhachHang
//    {
//        [Key]
//        public int CustomerNotificationId { get; set; }

//        [Required]
//        public string NoiDung { get; set; }
//        public string MaKhachHang { get; set; }

//        [DataType(DataType.DateTime)]
//        public DateTime NgayTao { get; set; }
//        public string TieuDe { get; set; }
//    }
//}

using System;
using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class ThongBaoKhachHang
    {
        [Key]
        public int MaThongBaoKhachHang { get; set; } // CustomerNotificationId => MaThongBaoKhachHang

        [Required(ErrorMessage = "Vui lòng nhập nội dung thông báo")]
        public string NoiDung { get; set; } // NoiDung => NoiDung

        [Required(ErrorMessage = "Vui lòng nhập mã khách hàng")]
        public string MaKhachHang { get; set; } // MaKhachHang => MaKhachHang

        [DataType(DataType.DateTime)]
        public DateTime NgayTao { get; set; } // NgayTao => NgayTao

        public string TieuDe { get; set; } // TieuDe => TieuDe
    }
}
