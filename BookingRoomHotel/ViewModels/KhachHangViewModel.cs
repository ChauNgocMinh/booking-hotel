//using BookingPhongHotel.Models;
//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.ViewModels
//{
//    public class CustomerViewModel
//    {
//        public CusRegisterViewModel CusRegisterViewModel { get; set; }
//        public CusLoginViewModel CusLoginViewModel { get; set; }
//        public CusChangeMatKhauViewModel CusChangeMatKhauViewModel { get; set; }
//        public CusForgotPasswordViewModel CusForgotPasswordViewModel { get; set; }
//        public CusViewModel CusViewModel { get; set; }
//        public HomeViewModel HomeViewModel { get; set; }
//        public SearchPhongViewModel SearchPhongViewModel { get; set; }

//    }
//    public class CusRegisterViewModel
//    {
//        public string Id { get; set; }
//        public string Name { get; set; }
//        public string Email { get; set; }
//        public string SoDienThoai { get; set; }
//        public DateTime NgaySinh { get; set; }
//        public string DiaChi { get; set; }
//        [Compare("MatKhauCf")]
//        public string MatKhau { get; set; }
//        [Compare("MatKhau")]
//        public string MatKhauCf { get; set; }
//    }

//    public class CusLoginViewModel
//    {

//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your TenDangNhap")]
//        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Length between 5 - 20")]
//        public string TenDangNhap { get; set; }

//        [DataType(DataType.Password)]
//        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Length between 6 - 30")]
//        public string Password { get; set; }
//    }

//    public class CusChangeMatKhauViewModel
//    {
//        public string Id { get; set; }
//        public string OldMatKhau { get; set; }
//        [Compare("ConfirmMatKhauMoi")]
//        public string MatKhauMoi { get; set; }
//        [Compare("MatKhauMoi")]
//        public string ConfirmMatKhauMoi { get; set; }
//    }

//    public class CusViewModel
//    {
//        public int Name { get; set; }
//    }

//    public class CusForgotPasswordViewModel
//    {
//        public string Id { get; set; }
//        public string Email { get; set; }
//    }
//    public class ListCustomerViewModel
//    {
//        public List<Customer> ListCus { get; set; }
//        public int Count { get; set; }
//    }

//    public class HomeViewModel
//    {
//        public List<Phong> ListPhongsRandom { get; set; }
//    }

//    public class SearchPhongViewModel : IValidatableObject
//    {
//        public DateTime? CheckInDate { get; set; }
//        public DateTime? TenDangNhap { get; set; }
//        public int? LoaiPhong { get; set; }
//        public int? AreaFrom { get; set; }
//        public int? DienTichDen { get; set; }
//        public int? GiaFrom { get; set; }
//        public int? GiaTo { get; set; }
//        public int? SoNguoiLon { get; set; }
//        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
//        {
//            if (CheckInDate > TenDangNhap)
//            {
//                yield return new ValidationResult("Check Out Date must be greater than or equal to the Check In Date.", new[] { "CheckInDate" });
//            }
//        }
//    }

//    public class BookViewModel : IValidatableObject
//    {
//        [Required(ErrorMessage = "Check-in Date is a required field!")]
//        public DateTime CheckInDate { get; set; }
//        [Required(ErrorMessage = "Check-out Date is a required field!")]
//        public DateTime TenDangNhap { get; set; }
//        [Required]
//        public int PhongID { get; set; }

//        [Required(ErrorMessage = "Login before Booking Phong!")]
//        public string MaKhachHang { get; set; }
//        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
//        {
//            if (CheckInDate > TenDangNhap)
//            {
//                yield return new ValidationResult("Check Out Date must be greater than or equal to the Check In Date.", new[] { "CheckInDate", "TenDangNhap" });
//            }


//        }
//    }

//}

using BookingRoomHotel.Models;
using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.ViewModels
{
    public class KhachHangViewModel
    {
        public DangKyKhachHangViewModel DangKy { get; set; }
        public DangNhapKhachHangViewModel DangNhap { get; set; }
        public DoiMatKhauKhachHangViewModel DoiMatKhau { get; set; }
        public QuenMatKhauKhachHangViewModel QuenMatKhau { get; set; }
        public ThongTinKhachHangViewModel ThongTin { get; set; }
        public TrangChuViewModel TrangChu { get; set; }
        public TimKiemPhongViewModel TimKiemPhong { get; set; }
    }

    public class DangKyKhachHangViewModel
    {
        public string MaKhachHang { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        [Compare("MatKhauXacNhan")]
        public string MatKhau { get; set; }
        [Compare("MatKhau")]
        public string MatKhauXacNhan { get; set; }
    }

    public class DangNhapKhachHangViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Độ dài từ 5 - 20 ký tự")]
        public string TenDangNhap { get; set; }

        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Độ dài từ 6 - 30 ký tự")]
        public string MatKhau { get; set; }
    }

    public class DoiMatKhauKhachHangViewModel
    {
        public string MaKhachHang { get; set; }
        public string MatKhauCu { get; set; }
        [Compare("XacNhanMatKhauMoi")]
        public string MatKhauMoi { get; set; }
        [Compare("MatKhauMoi")]
        public string XacNhanMatKhauMoi { get; set; }
    }

    public class ThongTinKhachHangViewModel
    {
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public IFormFile? AnhDaiDien { get; set; }
        public IFormFile? AnhCMND1 { get; set; }
        public IFormFile? AnhCMND2 { get; set; }
    }

    public class QuenMatKhauKhachHangViewModel
    {
        public string MaKhachHang { get; set; }
        public string Email { get; set; }
    }

    public class DanhSachKhachHangViewModel
    {
        public List<KhachHang> DanhSachKhachHang { get; set; }
        public int SoLuong { get; set; }
    }

    public class TrangChuViewModel
    {
        public List<Phong> PhongNgauNhien { get; set; }
    }

    public class TimKiemPhongViewModel : IValidatableObject
    {
        public DateTime? NgayNhanPhong { get; set; }
        public DateTime? NgayTraPhong { get; set; }
        public int? LoaiPhong { get; set; }
        public int? DienTichTu { get; set; }
        public int? DienTichDen { get; set; }
        public int? GiaTu { get; set; }
        public int? GiaDen { get; set; }
        public int? SoNguoiLon { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NgayNhanPhong > NgayTraPhong)
            {
                yield return new ValidationResult("Ngày trả phòng phải lớn hơn hoặc bằng ngày nhận phòng.", new[] { "NgayNhanPhong" });
            }
        }
    }

    public class DatPhong2ViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Ngày nhận phòng là bắt buộc!")]
        public DateTime NgayNhanPhong { get; set; }

        [Required(ErrorMessage = "Ngày trả phòng là bắt buộc!")]
        public DateTime NgayTraPhong { get; set; }

        [Required]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Vui lòng đăng nhập trước khi đặt phòng!")]
        public string MaKhachHang { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NgayNhanPhong > NgayTraPhong)
            {
                yield return new ValidationResult("Ngày trả phòng phải lớn hơn hoặc bằng ngày nhận phòng.", new[] { "NgayNhanPhong", "NgayTraPhong" });
            }
        }
    }
}
