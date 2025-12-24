using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagement.Migrations
{
    public partial class UpdateReviewKS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewKhachSan_KhachSan_MaPhong",
                table: "ReviewKhachSan");

            migrationBuilder.RenameColumn(
                name: "MaPhong",
                table: "ReviewKhachSan",
                newName: "MaKhachSan");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewKhachSan_MaPhong",
                table: "ReviewKhachSan",
                newName: "IX_ReviewKhachSan_MaKhachSan");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewKhachSan_KhachSan_MaKhachSan",
                table: "ReviewKhachSan",
                column: "MaKhachSan",
                principalTable: "KhachSan",
                principalColumn: "MaKhachSan",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewKhachSan_KhachSan_MaKhachSan",
                table: "ReviewKhachSan");

            migrationBuilder.RenameColumn(
                name: "MaKhachSan",
                table: "ReviewKhachSan",
                newName: "MaPhong");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewKhachSan_MaKhachSan",
                table: "ReviewKhachSan",
                newName: "IX_ReviewKhachSan_MaPhong");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewKhachSan_KhachSan_MaPhong",
                table: "ReviewKhachSan",
                column: "MaPhong",
                principalTable: "KhachSan",
                principalColumn: "MaKhachSan",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
