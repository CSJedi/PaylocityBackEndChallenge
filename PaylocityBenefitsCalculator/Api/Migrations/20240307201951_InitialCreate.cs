using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Salary = table.Column<decimal>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dependents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Relationship = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependents_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Salary" },
                values: new object[] { 1, new DateTime(1984, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "LeBron", "James", 75420.99m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Salary" },
                values: new object[] { 2, new DateTime(1999, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ja", "Morant", 92365.22m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Salary" },
                values: new object[] { 3, new DateTime(1963, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael", "Jordan", 143211.12m });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 1, new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Spouse", "Morant", 1 });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 2, new DateTime(2020, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Child1", "Morant", 3 });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 3, new DateTime(2021, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Child2", "Morant", 3 });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "FirstName", "LastName", "Relationship" },
                values: new object[] { 4, new DateTime(1974, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "DP", "Jordan", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_EmployeeId",
                table: "Dependents",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependents");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
