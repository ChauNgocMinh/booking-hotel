using HotelManagement.Models;

namespace HotelManagement.DataAccess
{
    public interface IRepository
    {
        Task<List<KhachSan>> getListKhachSan();
        Task<KhachSan> getKhachSan(string id);

        IEnumerable<Person> getPeople { get; }

        bool CreateAccount(TaiKhoan a);

        TaiKhoan CheckAccount(TaiKhoan a);


        string CreateMaTaiKhoan();

        IEnumerable<LoaiPhong> getLoaiPhong { get; }

        IEnumerable<Phong> getPhongByLoaiPhong(string id);

        Phong getChiTietPhong(string id);
        IEnumerable<Phong> FilterPhong(string loaiphong,
            DateTime? ngayden,
            DateTime? ngaydi,
            string khachsan);

        void AddReview(ReviewKhachSan model);

        void removeLoaiPhong(string id);

        void themLoaiPhong(LoaiPhong newloaiphong);

        void suaLoaiPhong(LoaiPhong phongcuasua);

        void themPhong(Phong newphong);

        void xoaPhong(string id);

        void suaPhong(Phong phongcansua);

        public IEnumerable<Phong> getPhongByMaTrangThai(string trangthai);

        public Task<DichVu> getDichvu(string id);

        public Task<List<DichVu>> getDichVuByIds(List<string> ids);

        string createOrderPhongId();

        void updateTrangThaiPhong(string maphong, string maTrangThai);

        void addKhachHang(KhachHang kh);

        void addOrderPhong(OrderPhong orderPhong);

        void addOrderPhongDichVu(List<OrderPhongDichVu> orderphongdichvu);

        Phong getPhongByMaPhong(string id);


        IEnumerable<OrderPhong> getOrderPhongByMaPhong(string id);

        string createHoaDonId();

        void updateTrangThaiOrderPhong(string orderPhongId, string status);
        //0 là chưa thanh toán
        //1 là đã thanh toán
        //2 là phòng đặt trước

        bool addHoaDon(HoaDon hoaDon);

        Person getPersonByUserName(string username);
    
        public IEnumerable<OrderPhong> getOrderPhongByPerson(string personid);

        public int funcGetLastIndex(List<string> maid,int vt);

        public void removeOrderPhong(string maorder);

        IQueryable<HoaDon> GetHoaDonQueryable();

        IEnumerable<HoaDon> getChiTietHoaDon(string mahoadon);

        IEnumerable<KhachHang> getKhachHang { get; }

        IEnumerable<BaiViet> getDanhSachBaiViet { get; }
        public Task<BaiViet> getBaiViet(string id);

        void updateBaiViet(BaiViet baiViet);
        void addBaiViet(BaiViet baiViet);

        void removeBaiViet(string maBaiViet);

        public void removeKhachHang(string makhachhang);

        IEnumerable<LoaiTaiKhoan> getLoaiTaiKhoan {  get; }

        IEnumerable<TaiKhoan> getTaiKhoan { get; }

        IEnumerable<NhanVien> getTaiKhoanNhanVien { get; }

        IEnumerable<KhachHang> getTaiKhoanKhachHang { get; }

        IEnumerable<VaiTro> GetVaiTros { get; }

        bool checkTonTaiUserName(string username);


        bool checkTonTaiMaNhanVien(string manhanvien);
        
        bool addNhanVien(NhanVien nhanvien);

        bool addTaiKhoanNhanVien(TaiKhoan taiKhoan);

        void updateThongTinKhachHang(Person newperson);

        void updateTrangThaiPhongs(IEnumerable<Phong> phongs);

        IEnumerable<DichVu> getDichVus { get; }

        void updateDichVu(DichVu dichvu);

        bool xoaDichVu(string madichvu);

        bool themDichVu(DichVu dichvu);

        void updateThongTinNhanVien(Person nhanvien,NhanVien vaitro);

        void updateLoaiTaiKhoanOfPerson(string personID, string loaitaikhoan);

        bool removeNhanVien(string manhanvien);

        void updateLoaiTaiKhoan(LoaiTaiKhoan loaitaikhoancansua);

        bool updateTaiKhoan(string mataikhoan,string username, string password);
        IEnumerable<OrderPhong> getOrderPhong();
        void updateOrderPhong(OrderPhong order);
        TaiKhoan GetAccountByUserName(string username);

    }
}
