using BookingRoomHotel.Models;
using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.ViewModels
{
    public class PhongViewModel
    {
        public List<Phong> DanhSachPhong { get; set; }
        public int SoLuong { get; set; }
    }

    public class TimKiemDanhSachPhongViewModel
    {
        public PhongViewModel PhongViewModel { get; set; }
        public TimKiemPhongViewModel TimKiemPhongViewModel { get; set; }
    }

    public class TaoPhongViewModel
    {
        public int? PhongID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại phòng")]
        public int? MaLoaiPhong { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khách sạn")]
        public int? KhachSanID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số giường")]
        public int SoGiuong { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số phòng")]
        public int SoPhong { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá phòng")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phòng phải lớn hơn hoặc bằng 0")]
        public int Gia { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả phòng")]
        public string MoTa { get; set; }

        public IFormFile? Anh { get; set; }
    }
}
