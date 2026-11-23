using Microsoft.EntityFrameworkCore.Migrations;
namespace CarShowRegistration.Infrastructure.Persistence.Migrations;
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(name: "Registrations", columns: table => new
        {
            Id = table.Column<Guid>(type: "TEXT", nullable: false), RegistrationNumber = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false), RegistrationDate = table.Column<DateTime>(type: "TEXT", nullable: false), ParticipantName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false), ContactNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false), Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false), Address = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false), CarMakeModel = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false), PlateNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false), RegistrationType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false), Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false), RegistrationFee = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false), AmountPaid = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false)
        }, constraints: table => table.PrimaryKey("PK_Registrations", x => x.Id));
        migrationBuilder.CreateIndex(name: "IX_Registrations_Email", table: "Registrations", column: "Email");
        migrationBuilder.CreateIndex(name: "IX_Registrations_RegistrationNumber", table: "Registrations", column: "RegistrationNumber", unique: true);
        migrationBuilder.CreateIndex(name: "IX_Registrations_PlateNumber", table: "Registrations", column: "PlateNumber", unique: true);
        migrationBuilder.CreateIndex(name: "IX_Registrations_Status", table: "Registrations", column: "Status");
        migrationBuilder.CreateIndex(name: "IX_Registrations_RegistrationType", table: "Registrations", column: "RegistrationType");
    }
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable("Registrations");
}
