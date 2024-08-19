using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Migrations
{
    /// <inheritdoc />
    public partial class V7_UpdateAuthorToHaveBookList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblbook_tblauthor_author_id",
                table: "tblbook");

            migrationBuilder.AddForeignKey(
                name: "FK_tblbook_tblauthor_author_id",
                table: "tblbook",
                column: "author_id",
                principalTable: "tblauthor",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblbook_tblauthor_author_id",
                table: "tblbook");

            migrationBuilder.AddForeignKey(
                name: "FK_tblbook_tblauthor_author_id",
                table: "tblbook",
                column: "author_id",
                principalTable: "tblauthor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
