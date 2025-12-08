//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.Models
//{
//    public class NhanVien
//    {
//        [Key]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your TenDangNhap")]
//        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Length between 5 - 20")]
//        public string Id { get; set; }
//        [DataType(DataType.Text)]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name")]
//        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Length between 6 - 50")]
//        public string Name { get; set; }
//        [EmailDiaChi]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email")]
//        public string Email { get; set; }
//        [SoDienThoai]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your SoDienThoai")]
//        public string SoDienThoai { get; set; }
//        [DataType(DataType.Date)]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your BirthDay")]
//        public DateTime NgaySinh { get; set; }
//        [DataType(DataType.Text)]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your DiaChi")]
//        public string DiaChi { get; set; }
//        [DataType(DataType.Password)]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your password")]
//        public string MatKhau { get; set; }
//        [StringLength(maximumLength: 20, ErrorMessage = "SoLuongToiDa Length 20")]
//        public string Role { get; set; }
//        [DataType(DataType.Text)]
//        [StringLength(10, ErrorMessage = "Error TrangThai")]
//        public string? TrangThai { get; set; }
//        public ICollection<StaffNotification>? StaffNotifications { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class NhanVien
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập mã nhân viên")]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Độ dài từ 5 - 20 ký tự")]
        public string MaNhanVien { get; set; } // Id => MaNhanVien

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập họ và tên")]
        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Độ dài từ 6 - 30 ký tự")]
        public string HoTen { get; set; } // Name => HoTen

        [EmailAddress]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }

        [Phone]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SoDienThoai { get; set; } // SoDienThoai => SoDienThoai

        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập ngày sinh")]
        public DateTime NgaySinh { get; set; } // NgaySinh => NgaySinh

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string DiaChi { get; set; } // DiaChi => DiaChi

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string MatKhau { get; set; } // MatKhau => MatKhau

        [StringLength(maximumLength: 20, ErrorMessage = "Độ dài tối đa 20 ký tự")]
        public string VaiTro { get; set; } // Role => VaiTro

        [DataType(DataType.Text)]
        [StringLength(10, ErrorMessage = "Trạng thái không hợp lệ")]
        public string? TrangThai { get; set; } // TrangThai => TrangThai

        public ICollection<ThongBaoNhanVien>? ThongBaoNhanVien { get; set; } // StaffNotifications => ThongBaoNhanVien
    }
}
