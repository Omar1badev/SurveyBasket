using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SurveyBasket.Migrations
{
    /// <inheritdoc />
    public partial class addrolesandroleclaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77B96C5D-F502-47TF-EE95-ABVN14A3CA22", "d75719b8-c437-4c33-9003-3b818cc62e98", true, false, "Member", "MEMBER" },
                    { "77B96CED-F902-47EF-AE95-ABBE14A8CA22", "6b10a125-58cf-4201-913c-bafa65cd5034", false, false, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "59724D2D-E2B5-4C67-AB6F-D93478347B03", 0, "B4555410-F5B0-45B1-B963-1B2351A0723C", "admin@survay-basket.com", true, "Survay Basket", "Admin", false, null, "ADMIN@SURVAY-BASKET.COM", "ADMIN@SURVAY-BASKET.COM", "AQAAAAIAAYagAAAAEIEv2BZU4NbT21m87/rgu/oA7dE3BSa/3oXgERki2Szjip4o0z0yAyAR8L5yH3LIDA==", null, false, "9FABB58491024B7BB140E4D6658B5BDA", false, "admin@survay-basket.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "polls:read", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 2, "permissions", "polls:add", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 3, "permissions", "polls:update", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 4, "permissions", "polls:delete", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 5, "permissions", "questions:read", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 6, "permissions", "questions:add", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 7, "permissions", "questions:update", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 8, "permissions", "users:read", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 9, "permissions", "users:add", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 10, "permissions", "users:update", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 11, "permissions", "roles:read", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 12, "permissions", "roles:add", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 13, "permissions", "roles:update", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" },
                    { 14, "permissions", "results:read", "77B96CED-F902-47EF-AE95-ABBE14A8CA22" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "77B96CED-F902-47EF-AE95-ABBE14A8CA22", "59724D2D-E2B5-4C67-AB6F-D93478347B03" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77B96C5D-F502-47TF-EE95-ABVN14A3CA22");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "77B96CED-F902-47EF-AE95-ABBE14A8CA22", "59724D2D-E2B5-4C67-AB6F-D93478347B03" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77B96CED-F902-47EF-AE95-ABBE14A8CA22");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59724D2D-E2B5-4C67-AB6F-D93478347B03");
        }
    }
}
