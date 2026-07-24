using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginForm.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Dep_id = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Createdby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Editedby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EmployeeCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    DateOfJoining = table.Column<DateOnly>(type: "date", nullable: false),
                    OfficeTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "educationDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Institution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearOfPassing = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<DateOnly>(type: "date", nullable: true),
                    To = table.Column<DateOnly>(type: "date", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_educationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_educationDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_educationDetails_EmployeeId",
                table: "educationDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Department_Id",
                table: "Employees",
                column: "Department_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "educationDetails");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
