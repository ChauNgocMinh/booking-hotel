//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.Models
//{
//	public class Media
//	{
//		[Key]
//		public int MediaID { get; set; }
//		public int? LoaiPhongID { get; set; }

//		[Required]
//		public string URL { get; set; }

//		public string For { get; set; }
//		public string? TieuDe { get; set; }
//		[Required]
//		public string? Loai { get; set; }
//	}

//}

using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class Media
    {
        [Key]
        public int MaMedia { get; set; } // MediaID => MaMedia

        public int? MaLoaiPhong { get; set; } // LoaiPhongID => MaLoaiPhong

        [Required(ErrorMessage = "Vui lòng nhập URL")]
        public string URL { get; set; }

        public string DungCho { get; set; } // For => DungCho
        public string? TieuDe { get; set; } // TieuDe => TieuDe

        [Required(ErrorMessage = "Vui lòng nhập loại media")]
        public string? Loai { get; set; } // Loai => Loai
    }
}
