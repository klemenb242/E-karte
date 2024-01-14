using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class vpNul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_ApplicationUser_UserID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Performer_PerformerID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Venue_VenueID",
                table: "Event");

            migrationBuilder.AlterColumn<int>(
                name: "VenueID",
                table: "Event",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Event",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "PerformerID",
                table: "Event",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_ApplicationUser_UserID",
                table: "Event",
                column: "UserID",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Performer_PerformerID",
                table: "Event",
                column: "PerformerID",
                principalTable: "Performer",
                principalColumn: "PerformerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Venue_VenueID",
                table: "Event",
                column: "VenueID",
                principalTable: "Venue",
                principalColumn: "VenueID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_ApplicationUser_UserID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Performer_PerformerID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Venue_VenueID",
                table: "Event");

            migrationBuilder.AlterColumn<int>(
                name: "VenueID",
                table: "Event",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Event",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PerformerID",
                table: "Event",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_ApplicationUser_UserID",
                table: "Event",
                column: "UserID",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Performer_PerformerID",
                table: "Event",
                column: "PerformerID",
                principalTable: "Performer",
                principalColumn: "PerformerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Venue_VenueID",
                table: "Event",
                column: "VenueID",
                principalTable: "Venue",
                principalColumn: "VenueID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
