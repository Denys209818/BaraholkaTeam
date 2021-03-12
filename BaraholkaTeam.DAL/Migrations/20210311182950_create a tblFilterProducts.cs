using Microsoft.EntityFrameworkCore.Migrations;

namespace BaraholkaTeam.DAL.Migrations
{
    public partial class createatblFilterProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFilterProductsTeamProject",
                columns: table => new
                {
                    FilterNameId = table.Column<int>(type: "integer", nullable: false),
                    FilterValueId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFilterProductsTeamProject", x => new { x.FilterNameId, x.FilterValueId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_tblFilterProductsTeamProject_tblFilterNamesTeamProject_Filt~",
                        column: x => x.FilterNameId,
                        principalTable: "tblFilterNamesTeamProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFilterProductsTeamProject_tblFilterValuesTeamProject_Fil~",
                        column: x => x.FilterValueId,
                        principalTable: "tblFilterValuesTeamProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFilterProductsTeamProject_tblProductsTeamProject_Product~",
                        column: x => x.ProductId,
                        principalTable: "tblProductsTeamProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFilterProductsTeamProject_FilterValueId",
                table: "tblFilterProductsTeamProject",
                column: "FilterValueId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFilterProductsTeamProject_ProductId",
                table: "tblFilterProductsTeamProject",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFilterProductsTeamProject");
        }
    }
}
