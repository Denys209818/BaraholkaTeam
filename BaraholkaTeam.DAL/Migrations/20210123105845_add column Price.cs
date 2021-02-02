using Microsoft.EntityFrameworkCore.Migrations;

namespace BaraholkaTeam.DAL.Migrations
{
    public partial class addcolumnPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "tblProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "tblProducts");
        }
    }
}
