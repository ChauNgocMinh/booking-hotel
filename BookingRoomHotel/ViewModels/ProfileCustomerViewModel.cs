using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.ViewModels
{
    public class ProfileCustomerViewModel
    {
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name")]
        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Length between 6 - 50")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email")]
        public string Email { get; set; }

        [Phone]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your SoDienThoai")]
        public string SoDienThoai { get; set; }

        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your BirthDay")]
        public DateTime NgaySinh { get; set; }

        [DataType(DataType.Text)]
        public string DiaChi { get; set; }

        public IFormFile? AnhDaiDien { get; set; }
        public IFormFile? AnhCMND1 { get; set; }
        public IFormFile? AnhCMND2 { get; set; }
    }

    public class ProfileView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string AnhDaiDien { get; set; }
        public string AnhCMND1 { get; set; }
        public string AnhCMND2 { get; set; }
        public string TrangThai { get; set; }
    }
}