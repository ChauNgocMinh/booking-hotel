//using Microsoft.EntityFrameworkCore;

//namespace BookingPhongHotel.Models
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions options) : base(options)
//        {
//        }

//        protected ApplicationDbContext()
//        {
//        }

//        public DbSet<Customer> KhachHangs { get; set; }
//        public DbSet<Staff> NhanViens { get; set; }

//        public DbSet<CauHoi> CauHoi { get; set; }
//        public DbSet<Hotel> Hotels { get; set; }
//        public DbSet<Booking> DatPhongs { get; set; }
//        public DbSet<Media> Media { get; set; }
//        public DbSet<Rating> Rating { get; set; }
//        public DbSet<Phong> Phongs { get; set; }
//        public DbSet<LoaiPhong> LoaiPhongs { get; set; }
//        public DbSet<Service> Service { get; set; }
//        public DbSet<ServiceBooking> ServiceBooking { get; set; }
//        public DbSet<CustomerNotification> CustomerNotification { get; set; }



//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            /*if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlServer("DefaultConnection");
//            }*/
//        }
//    }
//}
using BookingRoomHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingRoomHotel.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }

        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<KhachSan> KhachSans { get; set; }
        public DbSet<DatPhong> DatPhongs { get; set; }
        public DbSet<Media> Medias { get; set; } // nếu muốn giữ Media, hoặc đổi tên khác
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<LoaiPhong> LoaiPhongs { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<DatDichVu> DatDichVus { get; set; }
        public DbSet<ThongBaoKhachHang> ThongBaoKhachHangs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }*/
        }
    }
}
