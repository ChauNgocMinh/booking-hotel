//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.Models
//{
//	public class DichVu
//	{
//		[Key]
//		public int ServiceID { get; set; }

//		[Required]
//		public string ServiceName { get; set; }
//		public string Description { get; set; }
//		[Range(0, double.SoLuongToiDaValue)]
//		public int Gia { get; set; }

//		public virtual ICollection<Phong> Phongs { get; set; }
//	}
//}

using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class DichVu
    {
        [Key]
        public int MaDichVu { get; set; } // ServiceID => MaDichVu

        [Required(ErrorMessage = "Vui lòng nhập tên dịch vụ")]
        public string TenDichVu { get; set; } // ServiceName => TenDichVu

        public string MoTa { get; set; } // Description => MoTa

        [Range(0, double.MaxValue, ErrorMessage = "Giá không hợp lệ")]
        public int Gia { get; set; } // Gia => Gia

        public virtual ICollection<Phong> Phongs { get; set; } // Phongs => Phongs
    }
}
