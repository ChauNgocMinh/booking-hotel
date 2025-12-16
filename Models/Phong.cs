using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
    public partial class Phong
    {
        public Phong()
        {
            OrderPhongs = new HashSet<OrderPhong>();
        }

        public string MaPhong { get; set; } = null!;
        public string MaKhachSan { get; set; }
        public string? TenPhong { get; set; }
        public string? MoTaPhong { get; set; }
        public string MaTrangThai { get; set; } = null!;
        public string MaLoaiPhong { get; set; } = null!;
        public decimal Gia { get; set; }
        public string Anh { get; set; }
        public virtual KhachSan KhachSan { get; set; } = null!;
        public virtual LoaiPhong MaLoaiPhongNavigation { get; set; } = null!;
        public virtual ICollection<OrderPhong> OrderPhongs { get; set; }
    }
}