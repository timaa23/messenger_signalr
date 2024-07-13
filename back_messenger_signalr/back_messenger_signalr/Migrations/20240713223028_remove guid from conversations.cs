using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_messenger_signalr.Migrations
{
    /// <inheritdoc />
    public partial class removeguidfromconversations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Conversations_tbl_Guid",
                table: "Conversations_tbl");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Conversations_tbl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Conversations_tbl",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_tbl_Guid",
                table: "Conversations_tbl",
                column: "Guid",
                unique: true);
        }
    }
}
