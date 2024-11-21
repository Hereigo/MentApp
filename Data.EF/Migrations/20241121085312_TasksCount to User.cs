using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class TasksCounttoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TasksCount",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TasksCount" },
                values: new object[] { "f3bb39e5-38cd-4b97-bb89-4cd54c68e656", "AQAAAAIAAYagAAAAEPskl3950wuP0k+W49kFeanrfYLRMYEqFC5H/SDc4hisWZ93bv1KfeAI5u/VQ8RCGA==", "737cfe6c-6aa2-4c80-b535-f52fb576a48d", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TasksCount",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8ee83c9-590e-44ac-914e-ef58d729ea41", "AQAAAAIAAYagAAAAEOyr/kimPGekgUhhzz+O+0oPjk+fQL0SE27OM8YE4FyDe6XL4+d8nVYJgzTC+b3W3A==", "067a697d-c084-4af7-a083-8c8ac7e03f73" });
        }
    }
}
