using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "标题"),
                    PublishTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "发布时间"),
                    Price = table.Column<double>(type: "float", nullable: false, comment: "价格"),
                    AuthorName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "作者")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                },
                comment: "书籍信息");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
