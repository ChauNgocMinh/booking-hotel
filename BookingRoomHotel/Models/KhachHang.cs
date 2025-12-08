////using System.ComponentModel.DataAnnotations;

////namespace BookingPhongHotel.Models
////{
////    public class KhachHang
////    {
////        [Key]
////        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your TenDangNhap")]
////        [StringLength(maximumLength:20,MinimumLength =5,ErrorMessage ="Length between 5 - 20")]
////        public string Id { get; set; }
////        [DataType(DataType.Text)]
////        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name")]
////        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Length between 6 - 50")]
////        public string Name { get; set; }
////        [EmailDiaChi]
////        [Required(AllowEmptyStrings =false, ErrorMessage ="Please enter your email")]
////        public string Email { get; set; }

////        [SoDienThoai]
////        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your SoDienThoai")]
////        public string SoDienThoai { get; set; }
////        [DataType(DataType.Date)]
////        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your BirthDay")]
////        public DateTime NgaySinh { get; set; }
////        [DataType(DataType.Text)]
////        public string DiaChi { get; set; }
////        [DataType(DataType.Password)]
////        public string MatKhau { get; set; }
////        public string? AnhDaiDien { get; set; }
////        public string? AnhCMND1 { get; set; }
////        public string? AnhCMND2 { get; set; }

////        [DataType(DataType.Text)]
////        [StringLength(10, ErrorMessage = "Error TrangThai")]
////        public string TrangThai { get; set; }

////        public virtual ICollection<ThongBaoKhachHang>? CustomerNotifications { get; set; }
////    }
////}
//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.Models
//{
//    public class KhachHang
//    {
//        [Key]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập tên đăng nhập")]
//        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Độ dài từ 5 - 20 ký tự")]
//        public string MaKhachHang { get; set; }  // Id => MaKhachHang

//        [DataType(DataType.Text)]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập họ và tên")]
//        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Độ dài từ 6 - 30 ký tự")]
//        public string HoTen { get; set; } // Name => HoTen

//        [EmailDiaChi]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập email")]
//        public string Email { get; set; }

//        [SoDienThoai]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập số điện thoại")]
//        public string SoDienThoai { get; set; } // SoDienThoai => SoDienThoai

//        [DataType(DataType.Date)]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập ngày sinh")]
//        public DateTime NgaySinh { get; set; } // NgaySinh => NgaySinh

//        [DataType(DataType.Text)]
//        public string DiaChi { get; set; } // DiaChi => DiaChi

//        [DataType(DataType.Password)]
//        public string MatKhau { get; set; } // MatKhau => MatKhau

//        public string? AnhDaiDien { get; set; } // AnhDaiDien => AnhDaiDien
//        public string? AnhCMND1 { get; set; } // AnhCMND1 => AnhCMND1
//        public string? AnhCMND2 { get; set; } // AnhCMND2 => AnhCMND2

//        [DataType(DataType.Text)]
//        [StringLength(10, ErrorMessage = "Trạng thái không hợp lệ")]
//        public string TrangThai { get; set; } // TrangThai => TrangThai

//        public virtual ICollection<ThongBaoKhachHang>? ThongBaoKhachHang { get; set; } // CustomerNotifications => ThongBaoKhachHang
//    }
//}
using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class KhachHang
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập mã khách hàng")]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Độ dài từ 5 - 20 ký tự")]
        public string MaKhachHang { get; set; }

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập họ và tên")]
        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Độ dài từ 6 - 30 ký tự")]
        public string HoTen { get; set; }

        [EmailAddress]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }

        [Phone]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SoDienThoai { get; set; }

        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập ngày sinh")]
        public DateTime NgaySinh { get; set; }

        [DataType(DataType.Text)]
        public string DiaChi { get; set; }

        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        public string? AnhDaiDien { get; set; }
        public string? AnhCMND1 { get; set; }
        public string? AnhCMND2 { get; set; }

        [DataType(DataType.Text)]
        [StringLength(10, ErrorMessage = "Trạng thái không hợp lệ")]
        public string TrangThai { get; set; }

        public virtual ICollection<ThongBaoKhachHang>? ThongBaoKhachHang { get; set; }
    }
}
