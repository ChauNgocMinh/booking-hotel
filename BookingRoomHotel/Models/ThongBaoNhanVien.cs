//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace BookingPhongHotel.Models
//{
//    public class StaffNotification
//    {
//        [Key]
//        public int StaffNotificationId { get; set; }

//        [Required]
//        public string NoiDung { get; set; }

//        [DataType(DataType.DateTime)]
//        public DateTime NgayTao { get; set; }
//        public string StaffId { get; set; }
//        public string TieuDe { get; set; }

//    }

//}

using System;
using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class ThongBaoNhanVien
    {
        [Key]
        public int MaThongBaoNhanVien { get; set; } // StaffNotificationId => MaThongBaoNhanVien

        [Required(ErrorMessage = "Vui lòng nhập nội dung thông báo")]
        public string NoiDung { get; set; } // NoiDung => NoiDung

        [DataType(DataType.DateTime)]
        public DateTime NgayTao { get; set; } // NgayTao => NgayTao

        [Required(ErrorMessage = "Vui lòng nhập mã nhân viên")]
        public string MaNhanVien { get; set; } // StaffId => MaNhanVien

        public string TieuDe { get; set; } // TieuDe => TieuDe
    }
}
