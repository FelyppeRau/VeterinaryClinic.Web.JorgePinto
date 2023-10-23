using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinaryClinic.Web.JorgePinto.Migrations
{
    public partial class AddTimeAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<DateTime>(
				name: "Time",
				table: "Appointments",
				type: "datetime2",
				nullable: false,
				defaultValue: DateTime.MinValue);

			migrationBuilder.AddColumn<DateTime>(
				name: "Time",
				table: "AppointmentDetails",
				type: "datetime2",
				nullable: false,
				defaultValue: DateTime.MinValue);

			migrationBuilder.AddColumn<DateTime>(
				name: "Time",
				table: "AppointmentDetailsTemps",
				type: "datetime2",
				nullable: false,
				defaultValue: DateTime.MinValue);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
			name: "Time",
			table: "Appointments");

			migrationBuilder.DropColumn(
				name: "Time",
				table: "AppointmentDetails");

			migrationBuilder.DropColumn(
				name: "Time",
				table: "AppointmentDetailsTemps");
		}
    }
}
