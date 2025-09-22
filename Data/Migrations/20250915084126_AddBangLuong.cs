using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBangLuong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BangLuong",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NhanVienId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    LuongCoBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuCap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Thuong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KhauTru = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaoHiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThueTNCN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThucLinh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangLuong", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BangLuong");
        }
    }
}
