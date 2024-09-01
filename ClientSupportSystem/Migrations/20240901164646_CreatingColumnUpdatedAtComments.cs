using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientSupportSystem.Migrations
{
    /// <inheritdoc />
    public partial class CreatingColumnUpdatedAtComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TicketComment",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TicketComment");
        }
    }
}
