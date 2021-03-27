using Microsoft.EntityFrameworkCore.Migrations;

namespace NecessaryDrugs.Web.Migrations
{
    public partial class MoreImageOptionRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineImages_Medicines_MedicineId",
                table: "MedicineImages");

            migrationBuilder.DropIndex(
                name: "IX_MedicineImages_MedicineId",
                table: "MedicineImages");

            migrationBuilder.AlterColumn<int>(
                name: "MedicineId",
                table: "MedicineImages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicineImages_MedicineId",
                table: "MedicineImages",
                column: "MedicineId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineImages_Medicines_MedicineId",
                table: "MedicineImages",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineImages_Medicines_MedicineId",
                table: "MedicineImages");

            migrationBuilder.DropIndex(
                name: "IX_MedicineImages_MedicineId",
                table: "MedicineImages");

            migrationBuilder.AlterColumn<int>(
                name: "MedicineId",
                table: "MedicineImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_MedicineImages_MedicineId",
                table: "MedicineImages",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineImages_Medicines_MedicineId",
                table: "MedicineImages",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
