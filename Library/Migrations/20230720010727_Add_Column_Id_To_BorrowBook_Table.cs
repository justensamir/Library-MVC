using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_Id_To_BorrowBook_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowBooks",
                table: "BorrowBooks");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BorrowBooks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BorrowBooks",
                table: "BorrowBooks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowBooks_BorrowerCode",
                table: "BorrowBooks",
                column: "BorrowerCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowBooks",
                table: "BorrowBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowBooks_BorrowerCode",
                table: "BorrowBooks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BorrowBooks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BorrowBooks",
                table: "BorrowBooks",
                columns: new[] { "BorrowerCode", "BookId" });
        }
    }
}
