using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetNinja.Identity.Data.Migrations.IdentityModel.UserDb
{
    public partial class InitialUserDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 64, nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 256, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2020, 2, 15, 21, 14, 20, 271, DateTimeKind.Unspecified).AddTicks(2821), new TimeSpan(0, -6, 0, 0, 0))),
                    DateModified = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2020, 2, 15, 21, 14, 20, 276, DateTimeKind.Unspecified).AddTicks(3814), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_UserAccount_Subject", x => x.Subject);
                });

            migrationBuilder.CreateIndex(
                name: "UK_UserAccount_UserName",
                table: "UserAccounts",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
