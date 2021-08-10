using Microsoft.EntityFrameworkCore.Migrations;

namespace WinchHuntApp.Server.Migrations
{
    public partial class defaultUserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3181998d-0b28-490d-9619-ca3f35d0cf83", "1", "Admin", "Admin" },
                    { "0ecc57b3-7919-46ff-a8cd-f40df6fdccbf", "2", "SiteManager", "Site Manager" },
                    { "552fba04-f975-4329-ac6c-0e744e25abeb", "3", "LoggedInUser", "Logged In User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bec81c59-b51e-4ef0-9516-6950963880f5", 0, "8fb512b8-8a44-4fde-9370-67d7b6fcb5de", "admin@winchhunt.net", true, false, null, null, "admin@winchhunt.net", "AQAAAAEAACcQAAAAEJcFofKwh+r8/rMTw5n2eKMA+k4UpRIN0OLYwxHi04++GFwb9COjE15JBFYrcmnDrw==", "", false, "90312383-b89d-4232-8448-0a6efb0fd736", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3181998d-0b28-490d-9619-ca3f35d0cf83", "bec81c59-b51e-4ef0-9516-6950963880f5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ecc57b3-7919-46ff-a8cd-f40df6fdccbf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "552fba04-f975-4329-ac6c-0e744e25abeb");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3181998d-0b28-490d-9619-ca3f35d0cf83", "bec81c59-b51e-4ef0-9516-6950963880f5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3181998d-0b28-490d-9619-ca3f35d0cf83");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bec81c59-b51e-4ef0-9516-6950963880f5");
        }
    }
}
