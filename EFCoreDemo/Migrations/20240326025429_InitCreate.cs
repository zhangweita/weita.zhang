using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace EFCoreDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "作者")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                },
                comment: "作者信息");

            migrationBuilder.CreateTable(
                name: "Books",
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
                    table.PrimaryKey("PK_Books", x => x.Id);
                },
                comment: "书籍信息");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorName",
                table: "Books",
                column: "AuthorName");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title_PublishTime",
                table: "Books",
                columns: new[] { "Title", "PublishTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
