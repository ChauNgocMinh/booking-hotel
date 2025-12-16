using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagement.Migrations
{
    public partial class UpdatePhong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaPhong",
                table: "Loai_Phong");

            migrationBuilder.AddColumn<string>(
                name: "Anh",
                table: "Phong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Gia",
                table: "Phong",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "Order_Phong_Dich_Vu",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GiaDichVu",
                table: "Dich_Vu",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anh",
                table: "Phong");

            migrationBuilder.DropColumn(
                name: "Gia",
                table: "Phong");

            migrationBuilder.AlterColumn<float>(
                name: "DonGia",
                table: "Order_Phong_Dich_Vu",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "GiaPhong",
                table: "Loai_Phong",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<float>(
                name: "GiaDichVu",
                table: "Dich_Vu",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
