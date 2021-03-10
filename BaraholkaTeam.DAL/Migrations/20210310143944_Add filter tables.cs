using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BaraholkaTeam.DAL.Migrations
{
    public partial class Addfiltertables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFilterNamesTeamProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFilterNamesTeamProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFilterValuesTeamProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFilterValuesTeamProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFilterNameValuesTeamProject",
                columns: table => new
                {
                    FilterNameId = table.Column<int>(type: "integer", nullable: false),
                    FilterValueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFilterNameValuesTeamProject", x => new { x.FilterNameId, x.FilterValueId });
                    table.ForeignKey(
                        name: "FK_tblFilterNameValuesTeamProject_tblFilterNamesTeamProject_Fi~",
                        column: x => x.FilterNameId,
                        principalTable: "tblFilterNamesTeamProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFilterNameValuesTeamProject_tblFilterValuesTeamProject_F~",
                        column: x => x.FilterValueId,
                        principalTable: "tblFilterValuesTeamProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFilterNameValuesTeamProject_FilterValueId",
                table: "tblFilterNameValuesTeamProject",
                column: "FilterValueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFilterNameValuesTeamProject");

            migrationBuilder.DropTable(
                name: "tblFilterNamesTeamProject");

            migrationBuilder.DropTable(
                name: "tblFilterValuesTeamProject");
        }
    }
}
