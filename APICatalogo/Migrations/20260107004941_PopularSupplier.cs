using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopularSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Suppliers(Name,Document,IsActive,ImageURL,DateCreation)" +
                "Values('Bom Pastor','01.354.198/0001-02',true,'bompastor.jpg',now())");

            mb.Sql("Insert into Suppliers(Name,Document,IsActive,ImageURL,DateCreation)" +
                "Values('Padaria Bread','02.951.173/0001-01',true,'padariabread.jpg',now())");

            mb.Sql("Insert into Suppliers(Name,Document,IsActive,ImageURL,DateCreation)" +
                "Values('Refris Coca','05.364.171/0045-07',true,'refriscoca.jpg',now())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Suppliers");
        }
    }
}
