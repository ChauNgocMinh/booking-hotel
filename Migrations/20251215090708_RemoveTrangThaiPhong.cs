using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagement.Migrations
{
    public partial class RemoveTrangThaiPhong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKPhong128242",
                table: "Phong");

            migrationBuilder.DropTable(
                name: "Trang_Thai_Phong");

            migrationBuilder.DropIndex(
                name: "IX_Phong_MaTrangThai",
                table: "Phong");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trang_Thai_Phong",
                columns: table => new
                {
                    MaTrangThai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TenTrangThai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Trang_Th__AADE41383344BB34", x => x.MaTrangThai);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phong_MaTrangThai",
                table: "Phong",
                column: "MaTrangThai");

            migrationBuilder.AddForeignKey(
                name: "FKPhong128242",
                table: "Phong",
                column: "MaTrangThai",
                principalTable: "Trang_Thai_Phong",
                principalColumn: "MaTrangThai");
        }
    }
}
