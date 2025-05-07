using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AR_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class renamesintroduced : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RenameItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OldName = table.Column<string>(type: "TEXT", nullable: false),
                    NewName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenameItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RenameItems");
        }
    }
}
