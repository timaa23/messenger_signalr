using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_messenger_signalr.Migrations
{
    /// <inheritdoc />
    public partial class addtypesinconversation_tblandremovetitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Conversations_tbl");

            migrationBuilder.AddColumn<string>(
                name: "ConversationType",
                table: "Conversations_tbl",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConversationType",
                table: "Conversations_tbl");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Conversations_tbl",
                type: "text",
                nullable: true);
        }
    }
}
