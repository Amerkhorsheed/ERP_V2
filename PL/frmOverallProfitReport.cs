using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class frmOverallProfitReport : DevExpress.XtraEditors.XtraForm
    {
        private readonly ProductBLL productBLL = new ProductBLL();
        private readonly SalesBLL salesBLL = new SalesBLL();

        public frmOverallProfitReport()
        {
            InitializeComponent();
        }

        private void frmOverallProfitReport_Load(object sender, EventArgs e)
        {
            try
            {
                LoadProfitData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading overall profit report: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProfitData()
        {
            // Retrieve all products and sales records.
            var products = productBLL.Select().Products;
            var salesRecords = salesBLL.Select().Sales;

            // Join sales with products to compute profit per sale.
            var salesWithProfit = from sale in salesRecords
                                  join prod in products on sale.ProductID equals prod.ProductID
                                  select new
                                  {
                                      sale.SalesID,
                                      sale.ProductID,
                                      prod.ProductName,
                                      prod.CategoryName,
                                      // Original cost price from product.
                                      CostPrice = prod.price,
                                      // Sale price from sale record.
                                      SalePrice = sale.Price,
                                      // Discount is set to sale.MaxDiscount, defaulting to 0.
                                      Discount = sale.MaxDiscount,
                                      // Quantity sold.
                                      Quantity = sale.SalesAmount,
                                      // Profit = (SalePrice - CostPrice - Discount) * Quantity.
                                      Profit = (((decimal)sale.Price) - ((decimal)prod.price) - ((decimal)(sale.MaxDiscount))) * sale.SalesAmount,
                                      sale.SalesDate
                                  };

            // Calculate overall profit.
            decimal overallProfit = salesWithProfit.Sum(x => x.Profit);
            lblOverallProfit.Text = "Overall Profit: " + overallProfit.ToString("C");

            // Bind aggregated profit details per product to the grid.
            var productProfitData = salesWithProfit
                .GroupBy(x => new { x.ProductID, x.ProductName, x.CategoryName, x.CostPrice })
                .Select(g => new
                {
                    g.Key.ProductID,
                    g.Key.ProductName,
                    g.Key.CategoryName,
                    CostPrice = g.Key.CostPrice,
                    TotalQuantity = g.Sum(x => x.Quantity),
                    TotalProfit = g.Sum(x => x.Profit)
                })
                .OrderByDescending(x => x.TotalProfit)
                .ToList();
            dgvProfitDetails.DataSource = productProfitData;

            // Group by category to get overall profit per category.
            var categoryProfitData = salesWithProfit
                .GroupBy(x => x.CategoryName)
                .Select(g => new
                {
                    CategoryName = g.Key,
                    TotalProfit = g.Sum(x => x.Profit)
                })
                .OrderByDescending(x => x.TotalProfit)
                .ToList();

            UpdateProfitChart(categoryProfitData);
        }

        private void UpdateProfitChart(IEnumerable<dynamic> categoryProfitData)
        {
            // Retrieve the series by name (explicitly cast to Series).
            Series profitSeries = chartControlCategory.Series
                .FirstOrDefault(s => s.Name == "CategoryProfitSeries") as Series;
            if (profitSeries == null)
            {
                profitSeries = new Series("CategoryProfitSeries", ViewType.Bar);
                chartControlCategory.Series.Add(profitSeries);
            }
            else
            {
                profitSeries.Points.Clear();
            }

            // Add a data point for each category.
            foreach (var entry in categoryProfitData)
            {
                profitSeries.Points.Add(new SeriesPoint(entry.CategoryName, (double)entry.TotalProfit));
            }

            // Set a chart title.
            chartControlCategory.Titles.Clear();
            chartControlCategory.Titles.Add(new ChartTitle { Text = "Profit by Category" });
        }
    }
}
