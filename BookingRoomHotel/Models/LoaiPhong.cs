//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.Models
//{

//	public class LoaiPhong
//	{
//		[Key]
//		public int LoaiPhongID { get; set; }

//		[Required]  
//		public string LoaiName { get; set; }

//        [Required]
//        public int GiaFrom { get; set; }
//        [Required]

//        public int GiaTo { get; set; }
//        [Required]
//        public int SoLuongToiDa { get; set; }
//        [Required]
//        public int Size { get; set; }
//        [Required]
//        public string View { get; set; }
//        [Required]
//        public int Bed { get; set; }
//        [Required]
//        public string MoTa1 { get; set; }
//		public string? MoTa2 { get; set; }
//		public string? MoTa3 { get; set; }
//		public virtual ICollection<Phong>? Phongs { get; set; }
//		public virtual ICollection<Media>? Media { get; set; }
//	}
//}

using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class LoaiPhong
    {
        [Key]
        public int MaLoaiPhong { get; set; } // LoaiPhongID => MaLoaiPhong

        [Required(ErrorMessage = "Vui lòng nhập tên loại phòng")]
        public string TenLoaiPhong { get; set; } // LoaiName => TenLoaiPhong

        [Required(ErrorMessage = "Vui lòng nhập giá từ")]
        public int GiaTu { get; set; } // GiaFrom => GiaTu

        [Required(ErrorMessage = "Vui lòng nhập giá đến")]
        public int GiaDen { get; set; } // GiaTo => GiaDen

        [Required(ErrorMessage = "Vui lòng nhập số lượng tối đa khách")]
        public int SoLuongToiDa { get; set; } // SoLuongToiDa => SoLuongToiDa

        [Required(ErrorMessage = "Vui lòng nhập diện tích phòng")]
        public int DienTich { get; set; } // Size => DienTich

        [Required(ErrorMessage = "Vui lòng nhập hướng phòng")]
        public string Huong { get; set; } // View => Huong

        [Required(ErrorMessage = "Vui lòng nhập số giường")]
        public int SoGiuong { get; set; } // Bed => SoGiuong

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string MoTa1 { get; set; } // MoTa1 => MoTa1

        public string? MoTa2 { get; set; } // MoTa2 => MoTa2
        public string? MoTa3 { get; set; } // MoTa3 => MoTa3

        public virtual ICollection<Phong>? Phongs { get; set; } // Phongs => Phongs
        public virtual ICollection<Media>? Media { get; set; } // Media => Media
    }
}

