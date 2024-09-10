using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAveragePriceForAllCategories
                AS
                BEGIN
                    SELECT Category, CAST(AVG(Price) AS DECIMAL(18, 2)) AS AveragePrice
                    FROM Products
                    GROUP BY Category;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetCategoriesWithHighestStock
                AS
                BEGIN
                    SELECT Category, CAST(SUM(Stock) AS DECIMAL(18, 2)) AS TotalStock
                    FROM Products
                    GROUP BY Category
                    ORDER BY TotalStock DESC;
                END
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE GetAveragePriceByCategory");
            migrationBuilder.Sql(@"DROP PROCEDURE GetCategoriesWithHighestStock");
        }
    }
}
