using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseSchool.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseSQL",
                columns: table => new
                {
                    CourseID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Credits = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSQL", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "StudentSQL",
                columns: table => new
                {
                    StudentID = table.Column<Guid>(nullable: false),
                    Grade = table.Column<int>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    EnrollmentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSQL", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "UserSQL",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSQL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentSQL",
                columns: table => new
                {
                    EnrollmentID = table.Column<Guid>(nullable: false),
                    CourseID = table.Column<Guid>(nullable: false),
                    StudentID = table.Column<Guid>(nullable: false),
                    Grade = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentSQL", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_EnrollmentSQL_CourseSQL_CourseID",
                        column: x => x.CourseID,
                        principalTable: "CourseSQL",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentSQL_StudentSQL_StudentID",
                        column: x => x.StudentID,
                        principalTable: "StudentSQL",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentSQL_CourseID",
                table: "EnrollmentSQL",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentSQL_StudentID",
                table: "EnrollmentSQL",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentSQL");

            migrationBuilder.DropTable(
                name: "UserSQL");

            migrationBuilder.DropTable(
                name: "CourseSQL");

            migrationBuilder.DropTable(
                name: "StudentSQL");
        }
    }
}
