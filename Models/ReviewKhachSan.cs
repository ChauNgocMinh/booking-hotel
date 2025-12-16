namespace HotelManagement.Models
{
    public class ReviewKhachSan
    {
        public int Id { get; set; } 
        public string MaPhong { get; set; } = null!;
        public string PersonId { get; set; } = null!; 
        public int Rating { get; set; } 
        public string Comment { get; set; } = null!; 
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
        public virtual KhachSan KhachSan { get; set; } = null!;
        public virtual Person Person { get; set; } = null!;
    }
}
