namespace HotelManagement.Models
{
    public class KhachSan
    {
        public string MaKhachSan { get; set;  }
        public string TenKhachSan { get; set; }
        public string AnhDaiDien { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<Phong> Phongs { get; set; }
        public virtual ICollection<ReviewKhachSan> ReviewKhachSans { get; set; }
    }
}
