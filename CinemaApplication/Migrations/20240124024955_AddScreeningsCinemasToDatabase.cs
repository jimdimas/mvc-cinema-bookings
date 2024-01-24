using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddScreeningsCinemasToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    ThreeDim = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Screenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CinemaName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContentAdminUsername = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenings_Cinemas_CinemaName",
                        column: x => x.CinemaName,
                        principalTable: "Cinemas",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_Screenings_ContentAdmins_ContentAdminUsername",
                        column: x => x.ContentAdminUsername,
                        principalTable: "ContentAdmins",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Screenings_Movies_MovieName",
                        column: x => x.MovieName,
                        principalTable: "Movies",
                        principalColumn: "MovieName");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_CinemaName",
                table: "Screenings",
                column: "CinemaName");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_ContentAdminUsername",
                table: "Screenings",
                column: "ContentAdminUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_MovieName",
                table: "Screenings",
                column: "MovieName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Screenings");

            migrationBuilder.DropTable(
                name: "Cinemas");
        }
    }
}
