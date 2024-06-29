using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_messenger_signalr.Migrations
{
    /// <inheritdoc />
    public partial class addisReadtomessageandimagetoconversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Messages_tbl",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Conversations_tbl",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Messages_tbl");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Conversations_tbl");
        }
    }
}
