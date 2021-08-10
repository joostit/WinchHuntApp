using Microsoft.EntityFrameworkCore.Migrations;

namespace WinchHuntApp.Server.Migrations
{
    public partial class fixedAdminEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bec81c59-b51e-4ef0-9516-6950963880f5",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b4549fb-0981-40db-93e4-d4c597b90f74", "admin@winchhunt.net", "AQAAAAEAACcQAAAAEJPHe759t7IVg2PIH560AC6kXFwUe/C6pMVduakGtLdffLHBlyKppRaaqc25bLH2rQ==", "bf918999-f152-493c-843a-7ccaa37e6ca4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bec81c59-b51e-4ef0-9516-6950963880f5",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8fb512b8-8a44-4fde-9370-67d7b6fcb5de", null, "AQAAAAEAACcQAAAAEJcFofKwh+r8/rMTw5n2eKMA+k4UpRIN0OLYwxHi04++GFwb9COjE15JBFYrcmnDrw==", "90312383-b89d-4232-8448-0a6efb0fd736" });
        }
    }
}
