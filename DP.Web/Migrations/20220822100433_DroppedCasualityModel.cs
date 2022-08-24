using Microsoft.EntityFrameworkCore.Migrations;

namespace DP.Web.Migrations
{
    public partial class DroppedCasualityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cauality");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cauality",
                columns: table => new
                {
                    CasualityReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplainerId = table.Column<int>(type: "int", nullable: false),
                    IncidentId = table.Column<int>(type: "int", nullable: true),
                    IsVideoFootageAvailable = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfInjuredPeople = table.Column<int>(type: "int", nullable: false),
                    NumberOfNonInjuredPeople = table.Column<int>(type: "int", nullable: false),
                    NumberOfPeopleDied = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cauality", x => x.CasualityReportId);
                    table.ForeignKey(
                        name: "FK_Cauality_Complainer_ComplainerId",
                        column: x => x.ComplainerId,
                        principalTable: "Complainer",
                        principalColumn: "ComplainerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cauality_Incident_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incident",
                        principalColumn: "IncidentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cauality_ComplainerId",
                table: "Cauality",
                column: "ComplainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cauality_IncidentId",
                table: "Cauality",
                column: "IncidentId");
        }
    }
}
