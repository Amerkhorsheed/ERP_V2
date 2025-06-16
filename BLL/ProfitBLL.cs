using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;


namespace Erp_V1.BLL
{
    public class ProfitBLL
    {
        private readonly SalesDAO _salesDao = new SalesDAO();
        private readonly ExpensesDAO _expensesDao = new ExpensesDAO();
        private readonly ProductDAO _productDao = new ProductDAO();

        /// <summary>
        /// Calculates final profit based on the formula:
        /// (Sales Price - Product Sale Price) * Sales Amount - Total Expenses
        /// where Product Sale Price is the Sale_Price from the Product entity.
        /// </summary>
        /// <returns>The final profit as a decimal value.</returns>
        public decimal GetFinalProfit()
        {
            var sales = _salesDao.Select();
            var expenses = _expensesDao.Select();
            // Fetch all products to avoid fetching product details in each sale iteration
            var products = _productDao.Select();
            // Create a dictionary for quick product lookup by ProductID
            var productLookup = products.ToDictionary(p => p.ProductID, p => p);


            decimal totalSalesProfit = 0;

            foreach (var sale in sales)
            {
                if (productLookup.TryGetValue(sale.ProductID, out var product))
                {
                    // Calculate profit per unit sold:
                    // (Sales Price - Product Sale Price)
                    decimal profitPerUnit = (decimal)(sale.Price - product.price - sale.MaxDiscount);
                    totalSalesProfit += profitPerUnit * sale.SalesAmount;
                }
                else
                {
                    // Handle case where product is not found (optional - depends on data integrity)
                    // Log an error, throw an exception, or assign a default profit, etc.
                    // For now, let's assume 0 profit for missing products in sales
                    Console.WriteLine($"Warning: Product with ID {sale.ProductID} not found for sale ID {sale.SalesID}. Profit calculation may be inaccurate.");
                }
            }

            decimal totalExpenses = expenses.Sum(e => e.CostOfExpenses);
            return totalSalesProfit - totalExpenses;
        }
    }
}