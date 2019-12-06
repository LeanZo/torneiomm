using Microsoft.EntityFrameworkCore.Migrations;

namespace matamata.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Torneio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Time",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    TorneioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Time_Torneio_TorneioId",
                        column: x => x.TorneioId,
                        principalTable: "Torneio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Partida",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time1Id = table.Column<int>(nullable: true),
                    Time2Id = table.Column<int>(nullable: true),
                    Gols1 = table.Column<int>(nullable: false),
                    Gols2 = table.Column<int>(nullable: false),
                    Penaltis1 = table.Column<int>(nullable: false),
                    Penaltis2 = table.Column<int>(nullable: false),
                    VencedorId = table.Column<int>(nullable: true),
                    TorneioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partida_Time_Time1Id",
                        column: x => x.Time1Id,
                        principalTable: "Time",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partida_Time_Time2Id",
                        column: x => x.Time2Id,
                        principalTable: "Time",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partida_Torneio_TorneioId",
                        column: x => x.TorneioId,
                        principalTable: "Torneio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partida_Time_VencedorId",
                        column: x => x.VencedorId,
                        principalTable: "Time",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partida_Time1Id",
                table: "Partida",
                column: "Time1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Partida_Time2Id",
                table: "Partida",
                column: "Time2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Partida_TorneioId",
                table: "Partida",
                column: "TorneioId");

            migrationBuilder.CreateIndex(
                name: "IX_Partida_VencedorId",
                table: "Partida",
                column: "VencedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Time_TorneioId",
                table: "Time",
                column: "TorneioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partida");

            migrationBuilder.DropTable(
                name: "Time");

            migrationBuilder.DropTable(
                name: "Torneio");
        }
    }
}
