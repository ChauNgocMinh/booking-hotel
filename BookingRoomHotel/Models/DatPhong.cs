//using System.ComponentModel.DataAnnotations;

//namespace BookingPhongHotel.Models
//{
//	public class DatPhong
//	{
//		[Key]
//		public int MaPhong { get; set; }
//		public int PhongID { get; set; }
//		public string MaKhachHang { get; set; }

//		[DataType(DataType.Date)]
//		public DateTime CheckInDate { get; set; }

//		[DataType(DataType.Date)]
//        public DateTime CheckOutDate { get; set; }
//		public int? TongTien { get; set; }

//		public string? TrangThai { get; set; }

//		public virtual KhachHang Customer { get; set; }
//		public virtual Phong Phong { get; set; }
//		public virtual ICollection<DatDichVu> ServiceDatPhongs { get; set; }
//        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
//        {
//            if (CheckInDate <= CheckOutDate)
//            {
//                yield return new ValidationResult("Check Out Date must be greater than the start date.", new[] { "Check In Date" });
//            }
//        }
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace BookingRoomHotel.Models
{
    public class DatPhong
    {
        [Key]
        public int MaDatPhong { get; set; } // MaPhong => MaDatPhong

        public int MaPhong { get; set; } // PhongID => MaPhong
        public string MaKhachHang { get; set; } // MaKhachHang => MaKhachHang

        [DataType(DataType.Date)]
        public DateTime NgayNhanPhong { get; set; } // CheckInDate => NgayNhanPhong

        [DataType(DataType.Date)]
        public DateTime NgayTraPhong { get; set; } // CheckOutDate => NgayTraPhong

        public int? TongTien { get; set; } // TongTien => TongTien
        public string? TrangThai { get; set; } // TrangThai => TrangThai

        public virtual KhachHang KhachHang { get; set; } // Customer => KhachHang
        public virtual Phong Phong { get; set; } // Phong => Phong
        public virtual ICollection<DatDichVu> DatDichVus { get; set; } // ServiceDatPhongs => DatDichVus

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NgayTraPhong <= NgayNhanPhong)
            {
                yield return new ValidationResult(
                    "Ngày trả phòng phải lớn hơn ngày nhận phòng.",
                    new[] { "NgayTraPhong" });
            }
        }
    }
}
