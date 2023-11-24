using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class updatepackagemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSchedule_AspNetUsers_TrainerId",
                table: "TrainingSchedule");

            migrationBuilder.DropIndex(
                name: "IX_TrainingSchedule_TrainerId",
                table: "TrainingSchedule");

            migrationBuilder.AlterColumn<int>(
                name: "TrainerId",
                table: "TrainingSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialClass",
                table: "TrainingSchedule",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TimeSlot",
                table: "TrainingSchedule",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrainerId1",
                table: "TrainingSchedule",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SpecialClassesIncluded",
                table: "Package",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TimeSlot",
                table: "Package",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "UnlimitedAccess",
                table: "Package",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSchedule_TrainerId1",
                table: "TrainingSchedule",
                column: "TrainerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSchedule_AspNetUsers_TrainerId1",
                table: "TrainingSchedule",
                column: "TrainerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSchedule_AspNetUsers_TrainerId1",
                table: "TrainingSchedule");

            migrationBuilder.DropIndex(
                name: "IX_TrainingSchedule_TrainerId1",
                table: "TrainingSchedule");

            migrationBuilder.DropColumn(
                name: "IsSpecialClass",
                table: "TrainingSchedule");

            migrationBuilder.DropColumn(
                name: "TimeSlot",
                table: "TrainingSchedule");

            migrationBuilder.DropColumn(
                name: "TrainerId1",
                table: "TrainingSchedule");

            migrationBuilder.DropColumn(
                name: "SpecialClassesIncluded",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "TimeSlot",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "UnlimitedAccess",
                table: "Package");

            migrationBuilder.AlterColumn<string>(
                name: "TrainerId",
                table: "TrainingSchedule",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSchedule_TrainerId",
                table: "TrainingSchedule",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSchedule_AspNetUsers_TrainerId",
                table: "TrainingSchedule",
                column: "TrainerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
