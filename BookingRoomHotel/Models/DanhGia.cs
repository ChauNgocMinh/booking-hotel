//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.Models
//{
//	public class DanhGia
//	{
//		[Key]
//		public int RatingID { get; set; }

//		[Range(1, 5)]
//		public int Stars { get; set; }

//		[Required]
//		[StringLength(500)]
//		public string Comment { get; set; }
//		public int MaPhong { get; set; }
//		public int HotelID { get; set; }
//		public int MaKhachHang { get; set; }

//		public DateTime DateCreated { get; set; }
//	}

//}
using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class DanhGia
    {
        [Key]
        public int MaDanhGia { get; set; } // RatingID => MaDanhGia

        [Range(1, 5, ErrorMessage = "Số sao từ 1 đến 5")]
        public int Sao { get; set; } // Stars => Sao

        [Required(ErrorMessage = "Vui lòng nhập nhận xét")]
        [StringLength(500, ErrorMessage = "Độ dài tối đa 500 ký tự")]
        public string NhanXet { get; set; } // Comment => NhanXet

        public int MaDatPhong { get; set; } // MaPhong => MaDatPhong
        public int MaKhachSan { get; set; } // HotelID => MaKhachSan
        public int MaKhachHang { get; set; } // MaKhachHang => MaKhachHang

        public DateTime NgayTao { get; set; } // DateCreated => NgayTao
    }
}
