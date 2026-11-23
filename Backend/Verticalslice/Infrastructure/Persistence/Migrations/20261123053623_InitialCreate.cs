using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShowRegistration.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                CREATE TABLE IF NOT EXISTS "Registrations" (
                    "Id" TEXT NOT NULL CONSTRAINT "PK_Registrations" PRIMARY KEY,
                    "RegistrationNumber" TEXT NOT NULL,
                    "RegistrationDate" TEXT NOT NULL,
                    "ParticipantName" TEXT NOT NULL,
                    "ContactNumber" TEXT NOT NULL,
                    "Email" TEXT NOT NULL,
                    "Address" TEXT NOT NULL,
                    "CarMakeModel" TEXT NOT NULL,
                    "PlateNumber" TEXT COLLATE NOCASE NOT NULL,
                    "RegistrationType" TEXT NOT NULL,
                    "Status" TEXT NOT NULL,
                    "RegistrationFee" TEXT NOT NULL,
                    "AmountPaid" TEXT NOT NULL
                );
                CREATE UNIQUE INDEX IF NOT EXISTS "IX_Registrations_RegistrationNumber"
                    ON "Registrations" ("RegistrationNumber");
                DROP INDEX IF EXISTS "IX_Registrations_PlateNumber";
                CREATE UNIQUE INDEX "IX_Registrations_PlateNumber"
                    ON "Registrations" ("PlateNumber" COLLATE NOCASE);
                CREATE INDEX IF NOT EXISTS "IX_Registrations_Email"
                    ON "Registrations" ("Email");
                CREATE INDEX IF NOT EXISTS "IX_Registrations_RegistrationType"
                    ON "Registrations" ("RegistrationType");
                CREATE INDEX IF NOT EXISTS "IX_Registrations_Status"
                    ON "Registrations" ("Status");
                CREATE TABLE IF NOT EXISTS "RegistrationNumberSequences" (
                    "Id" INTEGER NOT NULL CONSTRAINT "PK_RegistrationNumberSequences" PRIMARY KEY,
                    "LastNumber" INTEGER NOT NULL
                );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "RegistrationNumberSequences");
            migrationBuilder.DropTable(name: "Registrations");
        }
    }
}
