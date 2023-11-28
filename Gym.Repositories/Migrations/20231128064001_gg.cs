using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class gg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPackage_AspNetUsers_ApplicationUserId",
                table: "CustomerPackage");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPackage_ApplicationUserId",
                table: "CustomerPackage");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CustomerPackage");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "CustomerPackage",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnlimitedAccess = table.Column<bool>(type: "bit", nullable: false),
                    SpecialClassesIncluded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.PackageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPackage_CustomerId",
                table: "CustomerPackage",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPackage_PackageId",
                table: "CustomerPackage",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPackage_AspNetUsers_CustomerId",
                table: "CustomerPackage",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPackage_Package_PackageId",
                table: "CustomerPackage",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPackage_AspNetUsers_CustomerId",
                table: "CustomerPackage");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPackage_Package_PackageId",
                table: "CustomerPackage");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPackage_CustomerId",
                table: "CustomerPackage");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPackage_PackageId",
                table: "CustomerPackage");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "CustomerPackage",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CustomerPackage",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPackage_ApplicationUserId",
                table: "CustomerPackage",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPackage_AspNetUsers_ApplicationUserId",
                table: "CustomerPackage",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
