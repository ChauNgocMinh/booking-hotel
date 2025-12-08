//using System.ComponentModel.DataAnnotations;
//namespace BookingPhongHotel.Models
//{
//	public class DatDichVu
//	{
//		[Key]
//		public int ServiceMaPhong { get; set; }

//		public int MaPhong { get; set; }
//		public int ServiceID { get; set; }

//		public int Quantity { get; set; }
//		public int Subtotal { get; set; }
//		public virtual DatPhong Booking { get; set; }
//		public virtual DichVu Service { get; set; }
//	}
//}


using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class DatDichVu
    {
        [Key]
        public int MaDatDichVu { get; set; } // ServiceMaPhong => MaDatDichVu

        public int MaDatPhong { get; set; } // MaPhong => MaDatPhong
        public int MaDichVu { get; set; } // ServiceID => MaDichVu

        public int SoLuong { get; set; } // Quantity => SoLuong
        public int ThanhTien { get; set; } // Subtotal => ThanhTien

        public virtual DatPhong DatPhong { get; set; } // Booking => DatPhong
        public virtual DichVu DichVu { get; set; } // Service => DichVu
    }
}
