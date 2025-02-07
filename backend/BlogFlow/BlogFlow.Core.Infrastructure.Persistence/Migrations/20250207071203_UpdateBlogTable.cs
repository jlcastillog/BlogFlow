using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogFlow.Core.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Blog",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Blog");
        }
    }
}
