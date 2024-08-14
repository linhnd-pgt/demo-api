using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblauthor",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    biography = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dob = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    active_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    delete_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_date = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_date = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP"),
                    updated_by = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblauthor", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblcategory",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    delete_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblcategory", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbluser",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    delete_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbluser", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblbook",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    published_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    author_id = table.Column<long>(type: "bigint", nullable: false),
                    active_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    delete_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblbook", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblbook_tblauthor_author_id",
                        column: x => x.author_id,
                        principalTable: "tblauthor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_category_book",
                columns: table => new
                {
                    book_id = table.Column<long>(type: "bigint", nullable: false),
                    category_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_category_book", x => new { x.book_id, x.category_id });
                    table.ForeignKey(
                        name: "FK_tbl_category_book_tblbook_book_id",
                        column: x => x.book_id,
                        principalTable: "tblbook",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_category_book_tblcategory_category_id",
                        column: x => x.category_id,
                        principalTable: "tblcategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_category_book_category_id",
                table: "tbl_category_book",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblbook_author_id",
                table: "tblbook",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblbook_title",
                table: "tblbook",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblcategory_name",
                table: "tblcategory",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbluser_email",
                table: "tbluser",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbluser_username",
                table: "tbluser",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_category_book");

            migrationBuilder.DropTable(
                name: "tbluser");

            migrationBuilder.DropTable(
                name: "tblbook");

            migrationBuilder.DropTable(
                name: "tblcategory");

            migrationBuilder.DropTable(
                name: "tblauthor");
        }
    }
}
