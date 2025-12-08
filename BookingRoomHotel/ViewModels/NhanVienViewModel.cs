using BookingRoomHotel.Models;
using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.ViewModels
{
    public class StaffLoginViewModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your TenDangNhap")]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Length between 5 - 20")]
        public string TenDangNhap { get; set; }

        [DataType(DataType.Password)]
        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Length between 6 - 30")]
        public string Password { get; set; }
    }

    public class ListsStaffViewModel
    {
        public List<NhanVien> ListStaff { get; set; }
        public int Count { get; set; }
    }
}
