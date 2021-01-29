using Microsoft.EntityFrameworkCore.Migrations;

namespace BaraholkaTeam.DAL.Migrations
{
    public partial class addcolumFullDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "tblProducts",
                type: "character varying(5000)",
                maxLength: 5000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "tblProducts");
        }
    }
}
