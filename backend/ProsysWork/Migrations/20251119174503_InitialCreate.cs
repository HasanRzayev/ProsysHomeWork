using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProsysWork.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dersler",
                columns: table => new
                {
                    DersKodu = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    DersAdi = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Sinifi = table.Column<short>(type: "smallint", nullable: false),
                    MuellimAdi = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    MuellimSoyadi = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersler", x => x.DersKodu);
                });

            migrationBuilder.CreateTable(
                name: "Shagirdler",
                columns: table => new
                {
                    Nomresi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Soyadi = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Sinifi = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shagirdler", x => x.Nomresi);
                });

            migrationBuilder.CreateTable(
                name: "Imtahanlar",
                columns: table => new
                {
                    DersKodu = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    ShagirdNomresi = table.Column<int>(type: "int", nullable: false),
                    ImtahanTarixi = table.Column<DateTime>(type: "date", nullable: false),
                    Qiymeti = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imtahanlar", x => new { x.DersKodu, x.ShagirdNomresi, x.ImtahanTarixi });
                    table.ForeignKey(
                        name: "FK_Imtahanlar_Dersler_DersKodu",
                        column: x => x.DersKodu,
                        principalTable: "Dersler",
                        principalColumn: "DersKodu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Imtahanlar_Shagirdler_ShagirdNomresi",
                        column: x => x.ShagirdNomresi,
                        principalTable: "Shagirdler",
                        principalColumn: "Nomresi",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Imtahanlar_ShagirdNomresi",
                table: "Imtahanlar",
                column: "ShagirdNomresi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imtahanlar");

            migrationBuilder.DropTable(
                name: "Dersler");

            migrationBuilder.DropTable(
                name: "Shagirdler");
        }
    }
}
