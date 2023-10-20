using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinaryClinic.Web.JorgePinto.Migrations
{
    public partial class ChangeAppointmentModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicId",
                table: "AppointmentDetailsTemps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicId",
                table: "AppointmentDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AnimalId",
                table: "Appointments",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicId",
                table: "Appointments",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_OwnerId",
                table: "Appointments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetailsTemps_MedicId",
                table: "AppointmentDetailsTemps",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetails_MedicId",
                table: "AppointmentDetails",
                column: "MedicId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDetails_Medics_MedicId",
                table: "AppointmentDetails",
                column: "MedicId",
                principalTable: "Medics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDetailsTemps_Medics_MedicId",
                table: "AppointmentDetailsTemps",
                column: "MedicId",
                principalTable: "Medics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Animals_AnimalId",
                table: "Appointments",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Medics_MedicId",
                table: "Appointments",
                column: "MedicId",
                principalTable: "Medics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Owners_OwnerId",
                table: "Appointments",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDetails_Medics_MedicId",
                table: "AppointmentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDetailsTemps_Medics_MedicId",
                table: "AppointmentDetailsTemps");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Animals_AnimalId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Medics_MedicId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Owners_OwnerId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AnimalId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_MedicId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_OwnerId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentDetailsTemps_MedicId",
                table: "AppointmentDetailsTemps");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentDetails_MedicId",
                table: "AppointmentDetails");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MedicId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MedicId",
                table: "AppointmentDetailsTemps");

            migrationBuilder.DropColumn(
                name: "MedicId",
                table: "AppointmentDetails");
        }
    }
}
