using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BaraholkaTeam.DAL.Migrations
{
    public partial class CreatetablesinDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblUsersTeamProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Surname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Login = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsersTeamProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblProductsTeamProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    FullDescription = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    image = table.Column<byte[]>(type: "bytea", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductsTeamProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductsTeamProject_tblUsersTeamProject_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUsersTeamProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProductsTeamProject_UserId",
                table: "tblProductsTeamProject",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblProductsTeamProject");

            migrationBuilder.DropTable(
                name: "tblUsersTeamProject");
        }
    }
}
