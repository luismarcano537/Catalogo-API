using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorys(Name, ImageURL) Values('Bebidas','bebidas.jpg')");
            mb.Sql("Insert into Categorys(Name, ImageURL) Values('Lanches','lanches.jpg')");
            mb.Sql("Insert into Categorys(Name, ImageURL) Values('Sobremesas','sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorys");
        }
    }
}
