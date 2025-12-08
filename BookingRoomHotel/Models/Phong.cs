//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.Models
//{
//	public class Phong
//	{
//		[Key]
//		public int PhongID { get; set; }

//		public int? HotelID { get; set; }
//		public int? LoaiPhongID { get; set; }

//		public int PhongNumber { get; set; }
//		public string? PhongImage { get; set; }

//		[Range(0, double.SoLuongToiDaValue)]
//		public int Gia { get; set; }

//		public string Describe { get; set; }
//		public virtual KhachSan? Hotel { get; set; }
//		public virtual ICollection<DatPhong>? DatPhongs { get; set; }

//		public virtual LoaiPhong? LoaiPhong { get; set; }
//	}
//}

using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class Phong
    {
        [Key]
        public int MaPhong { get; set; } // PhongID => MaPhong

        public int? MaKhachSan { get; set; } // HotelID => MaKhachSan
        public int? MaLoaiPhong { get; set; } // LoaiPhongID => MaLoaiPhong

        public int SoPhong { get; set; } // PhongNumber => SoPhong
        public string? AnhPhong { get; set; } // PhongImage => AnhPhong

        [Range(0, double.MaxValue, ErrorMessage = "Giá không hợp lệ")]
        public int Gia { get; set; } // Gia => Gia

        public string MoTa { get; set; } // Describe => MoTa

        public virtual KhachSan? KhachSan { get; set; } // Hotel => KhachSan
        public virtual ICollection<DatPhong>? DatPhongs { get; set; } // DatPhongs => DatPhongs

        public virtual LoaiPhong? LoaiPhong { get; set; } // LoaiPhong => LoaiPhong
    }
}
