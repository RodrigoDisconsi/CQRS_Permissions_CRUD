using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDCleanArchitecture.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTypes_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpleado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApellidoEmpleado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaPermiso = table.Column<DateTime>(type: "datetime", nullable: false),
                    TipoPermisoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contactos_TelefonoTipos_TelefonoTipoId",
                        column: x => x.TipoPermisoId,
                        principalTable: "PermissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_TipoPermisoId",
                table: "Permissions",
                column: "TipoPermisoId");

            migrationBuilder.Sql("INSERT INTO PermissionTypes (Descripcion) VALUES ('Admin')");
            migrationBuilder.Sql("INSERT INTO PermissionTypes (Descripcion) VALUES ('Usuario')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PermissionTypes");
        }
    }
}
