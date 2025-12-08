//using BookingPhongHotel.Models;
//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.ViewModels
//{
//	public class ListLoaiPhongViewModel
//	{
//		public List<LoaiPhong> ListLoaiPhongs { get; set; }
//		public int Count { get; set; }
//	}

//    public class ChiTietLoaiPhong
//    {
//        public List<Media> ListMedia { get; set; }
//        public LoaiPhong LoaiPhong { get; set; }
//    }

//	public class TaoLoaiPhongViewModel
//	{
//        public int? LoaiPhongID { get; set; }
//        [Required]
//        public string LoaiName { get; set; }
//        [Required]
//        public int Bed { get; set; }
//        [Required]
//        public int SoLuongToiDa { get; set; }
//        [Required]
//        public string View { get; set; }
//        [Required]
//        public int Size { get; set; }
//        [Required]
//        public int GiaFrom { get; set; }
//        [Required]
//        public int GiaTo { get; set; }
//        [Required]
//        public string MoTa1 { get; set; }
//        public string? MoTa2 { get; set; }
//		public string? MoTa3 { get; set; }
//		public IFormFileCollection? Images { get; set; }
//        public string? VideoID { get; set; }
//	}
//}

using BookingRoomHotel.Models;
using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.ViewModels
{
    public class DanhSachLoaiPhongViewModel
    {
        public List<LoaiPhong> DanhSachLoaiPhong { get; set; }
        public int Count { get; set; }
    }

    public class ChiTietLoaiPhong
    {
        public List<Media> DanhSachMedia { get; set; }
        public LoaiPhong LoaiPhong { get; set; }
    }

    public class TaoLoaiPhongViewModel
    {
        public int? MaLoaiPhong { get; set; }
        [Required]
        public string TenLoaiPhong { get; set; } // LoaiName => TenLoaiPhong
        [Required]
        public int SoGiuong { get; set; } // Bed => SoGiuong
        [Required]
        public int SoNguoiToiDa { get; set; } // SoLuongToiDa => SoNguoiToiDa
        [Required]
        public string View { get; set; }
        [Required]
        public int DienTich { get; set; } // Size => DienTich
        [Required]
        public int GiaTu { get; set; } // GiaFrom => GiaTu
        [Required]
        public int GiaDen { get; set; } // GiaTo => GiaDen
        [Required]
        public string MoTa1 { get; set; } // MoTa1 => MoTa1
        public string? MoTa2 { get; set; } // MoTa2 => MoTa2
        public string? MoTa3 { get; set; } // MoTa3 => MoTa3
        public IFormFileCollection? HinhAnh { get; set; } // Images => HinhAnh
        public string? VideoID { get; set; }
    }
}
