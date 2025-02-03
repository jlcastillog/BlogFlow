using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogFlow.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostRelationToContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Post_PostId",
                table: "Content");

            migrationBuilder.DropIndex(
                name: "IX_Content_PostId",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Content");

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Post_ContentId",
                table: "Post",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Content_ContentId",
                table: "Post",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Content_ContentId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_ContentId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Post");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Content",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Content_PostId",
                table: "Content",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Post_PostId",
                table: "Content",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
