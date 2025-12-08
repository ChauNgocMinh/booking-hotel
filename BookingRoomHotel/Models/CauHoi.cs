//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.Models
//{
//    public class CauHoi
//    {
//        [Key] 
//        public int Id { get; set; }
//        [DataType(DataType.Text)]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name")]
//        public string Name { get; set; }
//        [EmailDiaChi]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email")]
//        public string Email { get; set; }
//        [DataType(DataType.Text)]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your ChuDe")]
//        [StringLength(50, ErrorMessage = "Less than 50 characters")]
//        public string ChuDe { get; set; }
//        [DataType(DataType.Text)]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your NoiDung")]
//        [StringLength(300, ErrorMessage = "Less than 300 characters")]
//        public string NoiDung { get; set; }
//        [DataType(DataType.Text)]
//        public string? PhanHoi { get; set; }
//        [DataType(DataType.Text)]
//        [StringLength(10, ErrorMessage = "Error TrangThai")]
//        public string? TrangThai { get; set; }
//    }
//}
using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class CauHoi
    {
        [Key]
        public int MaCauHoi { get; set; } // Id => MaCauHoi

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập họ và tên")]
        public string HoTen { get; set; } // Name => HoTen

        [EmailAddress]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập chủ đề")]
        [StringLength(50, ErrorMessage = "Độ dài tối đa 50 ký tự")]
        public string ChuDe { get; set; } // ChuDe => ChuDe

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập nội dung")]
        [StringLength(300, ErrorMessage = "Độ dài tối đa 300 ký tự")]
        public string NoiDung { get; set; } // NoiDung => NoiDung

        [DataType(DataType.Text)]
        public string? PhanHoi { get; set; } // PhanHoi => PhanHoi

        [DataType(DataType.Text)]
        [StringLength(10, ErrorMessage = "Trạng thái không hợp lệ")]
        public string? TrangThai { get; set; } // TrangThai => TrangThai
    }
}
