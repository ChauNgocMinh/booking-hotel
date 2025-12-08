using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingRoomHotel.Migrations
{
    public partial class CreateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CauHois",
                columns: table => new
                {
                    MaCauHoi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuDe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhanHoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHois", x => x.MaCauHoi);
                });

            migrationBuilder.CreateTable(
                name: "DanhGias",
                columns: table => new
                {
                    MaDanhGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sao = table.Column<int>(type: "int", nullable: false),
                    NhanXet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MaDatPhong = table.Column<int>(type: "int", nullable: false),
                    MaKhachSan = table.Column<int>(type: "int", nullable: false),
                    MaKhachHang = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGias", x => x.MaDanhGia);
                });

            migrationBuilder.CreateTable(
                name: "DichVus",
                columns: table => new
                {
                    MaDichVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVus", x => x.MaDichVu);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    MaKhachHang = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhCMND1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhCMND2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.MaKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "KhachSans",
                columns: table => new
                {
                    MaKhachSan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhachSan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DanhGia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachSans", x => x.MaKhachSan);
                });

            migrationBuilder.CreateTable(
                name: "LoaiPhongs",
                columns: table => new
                {
                    MaLoaiPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaTu = table.Column<int>(type: "int", nullable: false),
                    GiaDen = table.Column<int>(type: "int", nullable: false),
                    SoLuongToiDa = table.Column<int>(type: "int", nullable: false),
                    DienTich = table.Column<int>(type: "int", nullable: false),
                    Huong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoGiuong = table.Column<int>(type: "int", nullable: false),
                    MoTa1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiPhongs", x => x.MaLoaiPhong);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    MaNhanVien = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.MaNhanVien);
                });

            migrationBuilder.CreateTable(
                name: "ThongBaoKhachHangs",
                columns: table => new
                {
                    MaThongBaoKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhachHangMaKhachHang = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoKhachHangs", x => x.MaThongBaoKhachHang);
                    table.ForeignKey(
                        name: "FK_ThongBaoKhachHangs_KhachHangs_KhachHangMaKhachHang",
                        column: x => x.KhachHangMaKhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKhachHang");
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    MaMedia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLoaiPhong = table.Column<int>(type: "int", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DungCho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiPhongMaLoaiPhong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.MaMedia);
                    table.ForeignKey(
                        name: "FK_Medias_LoaiPhongs_LoaiPhongMaLoaiPhong",
                        column: x => x.LoaiPhongMaLoaiPhong,
                        principalTable: "LoaiPhongs",
                        principalColumn: "MaLoaiPhong");
                });

            migrationBuilder.CreateTable(
                name: "Phongs",
                columns: table => new
                {
                    MaPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhachSan = table.Column<int>(type: "int", nullable: true),
                    MaLoaiPhong = table.Column<int>(type: "int", nullable: true),
                    SoPhong = table.Column<int>(type: "int", nullable: false),
                    AnhPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhachSanMaKhachSan = table.Column<int>(type: "int", nullable: true),
                    LoaiPhongMaLoaiPhong = table.Column<int>(type: "int", nullable: true),
                    DichVuMaDichVu = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phongs", x => x.MaPhong);
                    table.ForeignKey(
                        name: "FK_Phongs_DichVus_DichVuMaDichVu",
                        column: x => x.DichVuMaDichVu,
                        principalTable: "DichVus",
                        principalColumn: "MaDichVu");
                    table.ForeignKey(
                        name: "FK_Phongs_KhachSans_KhachSanMaKhachSan",
                        column: x => x.KhachSanMaKhachSan,
                        principalTable: "KhachSans",
                        principalColumn: "MaKhachSan");
                    table.ForeignKey(
                        name: "FK_Phongs_LoaiPhongs_LoaiPhongMaLoaiPhong",
                        column: x => x.LoaiPhongMaLoaiPhong,
                        principalTable: "LoaiPhongs",
                        principalColumn: "MaLoaiPhong");
                });

            migrationBuilder.CreateTable(
                name: "ThongBaoNhanVien",
                columns: table => new
                {
                    MaThongBaoNhanVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhanVienMaNhanVien = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoNhanVien", x => x.MaThongBaoNhanVien);
                    table.ForeignKey(
                        name: "FK_ThongBaoNhanVien_NhanViens_NhanVienMaNhanVien",
                        column: x => x.NhanVienMaNhanVien,
                        principalTable: "NhanViens",
                        principalColumn: "MaNhanVien");
                });

            migrationBuilder.CreateTable(
                name: "DatPhongs",
                columns: table => new
                {
                    MaDatPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhong = table.Column<int>(type: "int", nullable: false),
                    MaKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayNhanPhong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTraPhong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KhachHangMaKhachHang = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    PhongMaPhong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatPhongs", x => x.MaDatPhong);
                    table.ForeignKey(
                        name: "FK_DatPhongs_KhachHangs_KhachHangMaKhachHang",
                        column: x => x.KhachHangMaKhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatPhongs_Phongs_PhongMaPhong",
                        column: x => x.PhongMaPhong,
                        principalTable: "Phongs",
                        principalColumn: "MaPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatDichVus",
                columns: table => new
                {
                    MaDatDichVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatPhong = table.Column<int>(type: "int", nullable: false),
                    MaDichVu = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<int>(type: "int", nullable: false),
                    DatPhongMaDatPhong = table.Column<int>(type: "int", nullable: false),
                    DichVuMaDichVu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatDichVus", x => x.MaDatDichVu);
                    table.ForeignKey(
                        name: "FK_DatDichVus_DatPhongs_DatPhongMaDatPhong",
                        column: x => x.DatPhongMaDatPhong,
                        principalTable: "DatPhongs",
                        principalColumn: "MaDatPhong",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatDichVus_DichVus_DichVuMaDichVu",
                        column: x => x.DichVuMaDichVu,
                        principalTable: "DichVus",
                        principalColumn: "MaDichVu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatDichVus_DatPhongMaDatPhong",
                table: "DatDichVus",
                column: "DatPhongMaDatPhong");

            migrationBuilder.CreateIndex(
                name: "IX_DatDichVus_DichVuMaDichVu",
                table: "DatDichVus",
                column: "DichVuMaDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_DatPhongs_KhachHangMaKhachHang",
                table: "DatPhongs",
                column: "KhachHangMaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DatPhongs_PhongMaPhong",
                table: "DatPhongs",
                column: "PhongMaPhong");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_LoaiPhongMaLoaiPhong",
                table: "Medias",
                column: "LoaiPhongMaLoaiPhong");

            migrationBuilder.CreateIndex(
                name: "IX_Phongs_DichVuMaDichVu",
                table: "Phongs",
                column: "DichVuMaDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_Phongs_KhachSanMaKhachSan",
                table: "Phongs",
                column: "KhachSanMaKhachSan");

            migrationBuilder.CreateIndex(
                name: "IX_Phongs_LoaiPhongMaLoaiPhong",
                table: "Phongs",
                column: "LoaiPhongMaLoaiPhong");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoKhachHangs_KhachHangMaKhachHang",
                table: "ThongBaoKhachHangs",
                column: "KhachHangMaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoNhanVien_NhanVienMaNhanVien",
                table: "ThongBaoNhanVien",
                column: "NhanVienMaNhanVien");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauHois");

            migrationBuilder.DropTable(
                name: "DanhGias");

            migrationBuilder.DropTable(
                name: "DatDichVus");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "ThongBaoKhachHangs");

            migrationBuilder.DropTable(
                name: "ThongBaoNhanVien");

            migrationBuilder.DropTable(
                name: "DatPhongs");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "Phongs");

            migrationBuilder.DropTable(
                name: "DichVus");

            migrationBuilder.DropTable(
                name: "KhachSans");

            migrationBuilder.DropTable(
                name: "LoaiPhongs");
        }
    }
}
