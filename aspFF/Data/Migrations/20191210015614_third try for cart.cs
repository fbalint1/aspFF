using Microsoft.EntityFrameworkCore.Migrations;

namespace aspFF.Data.Migrations
{
    public partial class thirdtryforcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ownerID",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ownerID",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
