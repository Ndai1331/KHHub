using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KHHub.MasterDataService.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Place_26051516175503 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Places",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceUrl",
                table: "Places",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "SourceUrl",
                table: "Places");
        }
    }
}
