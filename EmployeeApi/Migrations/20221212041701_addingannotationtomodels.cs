using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeApi.Migrations
{
    public partial class addingannotationtomodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeDepartments",
                table: "EmployeeDepartments");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDepartments_DepartmentId",
                table: "EmployeeDepartments");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeAddress",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DesignationName",
                table: "Designations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeDepartments",
                table: "EmployeeDepartments",
                columns: new[] { "DepartmentId", "EmployeeId" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDepartments_EmployeeId",
                table: "EmployeeDepartments",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeDepartments",
                table: "EmployeeDepartments");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDepartments_EmployeeId",
                table: "EmployeeDepartments");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeAddress",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DesignationName",
                table: "Designations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeDepartments",
                table: "EmployeeDepartments",
                columns: new[] { "EmployeeId", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDepartments_DepartmentId",
                table: "EmployeeDepartments",
                column: "DepartmentId");
        }
    }
}
