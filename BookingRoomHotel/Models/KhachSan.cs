//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.Models
//{
//	public class KhachSan
//	{
//		[Key]
//		public int HotelID { get; set; }

//		[Required]
//		public string Name { get; set; }

//		public string DiaChi { get; set; }
//		public string Description { get; set; }
//		[SoDienThoai]
//		public string SoDienThoaiNumber { get; set; }
//		[Range(1, 5)]
//		public int Rating { get; set; }
//		public virtual ICollection<Phong> Phongs { get; set; }
//	}
//}

using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class KhachSan
    {
        [Key]
        public int MaKhachSan { get; set; } // HotelID => MaKhachSan

        [Required(ErrorMessage = "Vui lòng nhập tên khách sạn")]
        public string TenKhachSan { get; set; } // Name => TenKhachSan

        public string DiaChi { get; set; } // DiaChi => DiaChi
        public string MoTa { get; set; } // Description => MoTa

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; } // SoDienThoaiNumber => SoDienThoai

        [Range(1, 5, ErrorMessage = "Đánh giá từ 1 đến 5 sao")]
        public int DanhGia { get; set; } // Rating => DanhGia

        public virtual ICollection<Phong> Phongs { get; set; } // Phongs => Phongs
    }
}
