using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    public partial class addRoles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO public.\"Roles\" (\"Title\") VALUES ('Admin'); INSERT INTO public.\"Roles\" (\"Title\") VALUES ('Student'); INSERT INTO public.\"Roles\"(\"Title\") VALUES ('Teacher')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
