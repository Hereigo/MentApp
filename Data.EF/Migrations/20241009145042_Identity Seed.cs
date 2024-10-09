using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class IdentitySeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf7", "02174cf0–9412–4cfe-afbf-59f706d72cf7", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "22e4598c-d509-4d72-9cb4-dce110115dcf", 0, "105b7382-37a9-4ce3-bc47-b4294cb9261c", "User", null, true, "Admin", "SuperAdmin", false, null, null, "ADMIN@AA.AA", "AQAAAAIAAYagAAAAEFjeoZ+7zzT4nrpqy4byeBQIk7qB+FEYTzTyH+E0ucCPGeF07blFFrz7MKpNiF5OrA==", null, false, "495f408f-c462-41ec-9c8b-35a6e7851fcc", false, "admin@aa.aa" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf7", "02174cf0–9412–4cfe-afbf-59f706d72cf6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf7", "02174cf0–9412–4cfe-afbf-59f706d72cf6" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e4598c-d509-4d72-9cb4-dce110115dcf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf7");
        }
    }
}
