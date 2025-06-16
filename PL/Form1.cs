using DevExpress.XtraBars;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Ribbon;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ApplyPermissions()
        {
            if (UserSession.RoleId == 1)
            {
                // Admin: full access
                return;
            }

            // Iterate through all Ribbon pages and groups
            foreach (RibbonPage page in ribbon.Pages)
            {
                foreach (RibbonPageGroup group in page.Groups)
                {
                    // Each ItemLink is of type BarItemLink
                    foreach (BarItemLink itemLink in group.ItemLinks)
                    {
                        if (itemLink.Item is BarButtonItem button)
                        {
                            // Toggle visibility based on permission name (Name must match permission key)
                            button.Visibility =
                                UserSession.HasPermission(button.Name)
                                ? BarItemVisibility.Always
                                : BarItemVisibility.Never;
                        }
                    }
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmCategory category = new FrmCategory();
            category.ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmCategoryList list = new FrmCategoryList();
            list.ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmAddStock frmAddStock = new FrmAddStock();
            frmAddStock.ShowDialog();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmStockAlert frmStockAlert = new FrmStockAlert();
            frmStockAlert.ShowDialog();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmCustomer customer = new FrmCustomer();
            customer.ShowDialog();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmCustomerList list = new FrmCustomerList();
            list.ShowDialog();
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmProduct product = new FrmProduct();
            product.ShowDialog();
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmProductList list = new FrmProductList();
            list.ShowDialog();
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmSales frmSales = new FrmSales();
            frmSales.ShowDialog();
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmDeleted frmDeleted = new FrmDeleted();
            frmDeleted.ShowDialog();
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmSalesList list = new FrmSalesList();
            list.ShowDialog();
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmStockCal frmStockCal = new FrmStockCal();
            frmStockCal.ShowDialog();
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Instantiate the renamed form class:
            var recommendationsForm = new FrmRecommendations();

            // Show it as a modal dialog:
            recommendationsForm.ShowDialog();
            recommendationsForm.Dispose();
        }
    

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            test1 test = new test1();
            test.ShowDialog();
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSearchProduct searchProduct = new frmSearchProduct();
            searchProduct.ShowDialog();
        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMultiSales frmMultiSales = new frmMultiSales();
            frmMultiSales.ShowDialog();
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMultiSales frmMultiSales = new frmMultiSales();
            frmMultiSales.ShowDialog();
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSalesChart frmSalesChart = new frmSalesChart();
            frmSalesChart.ShowDialog();
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSalesChart frmSalesChart = new frmSalesChart();
            frmSalesChart.ShowDialog();
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmProfitReport frmProfitReport = new frmProfitReport();
            frmProfitReport.ShowDialog();
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmProductRecommendations frmProductRecommendations = new frmProductRecommendations();
            frmProductRecommendations.ShowDialog();
        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmRecommendation frmRecommendation = new frmRecommendation();
            frmRecommendation.ShowDialog();
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmProductShortagePredictor frmProductShortagePredictor = new frmProductShortagePredictor();
            frmProductShortagePredictor.ShowDialog();
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            RecommendationForm frmRecommendation = new RecommendationForm();
            frmRecommendation.ShowDialog();
        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyPermissions();
        }

        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmStockFilter frmStockFilter = new FrmStockFilter();
            frmStockFilter.ShowDialog();
        }


        private void barButtonItem34_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            FrmSupplier supplier = new FrmSupplier();
            supplier.ShowDialog();
        }

        private void barButtonItem35_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            frmSupplierList frmSupplierList = new frmSupplierList();
            frmSupplierList.ShowDialog();
        }

        private void barButtonItem27_ItemClick_2(object sender, ItemClickEventArgs e)
        {
            frmPurchase frmPurchase = new frmPurchase();
            frmPurchase.ShowDialog();
        }

        private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPurchaseList frmPurchaseList = new FrmPurchaseList();
            frmPurchaseList.ShowDialog();
        }

        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPurchaseChart frmPurchaseChart = new FrmPurchaseChart();
            frmPurchaseChart.ShowDialog();
        }

        private void barButtonItem30_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            FrmSupplierPurchaseSummary frmSupplierPurchaseSummary = new FrmSupplierPurchaseSummary();
            frmSupplierPurchaseSummary.ShowDialog();
        }

        private void barButtonItem31_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            frmSupplierPurchaseReport frmSupplierPurchaseReport = new frmSupplierPurchaseReport();
            frmSupplierPurchaseReport.ShowDialog();
        }

        private void barButtonItem32_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            frmReturn frmReturn = new frmReturn();
            frmReturn.ShowDialog();
        }

        private void barButtonItem33_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            frmReturnAnalysis frmReturnAnalysis = new frmReturnAnalysis();
            frmReturnAnalysis.ShowDialog();
        }

        private void barButtonItem36_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            FrmExpenses frmExpense = new FrmExpenses();
            frmExpense.ShowDialog();
        }

        private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmOverallProfitReport frmOverallProfits = new frmOverallProfitReport();
            frmOverallProfits.ShowDialog();
        }

        private void barButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmExpensesList frmExpensesList = new FrmExpensesList();
            frmExpensesList.ShowDialog();
        }

        private void barButtonItem39_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProfitDashboard frmFinalProfit = new ProfitDashboard();
            frmFinalProfit.ShowDialog();
        }

        private void barButtonItem40_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProfitForm frmFinalProfit1 = new ProfitForm();
            frmFinalProfit1.ShowDialog();
        }

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmMagic frmMagic = new FrmMagic();
            frmMagic.ShowDialog();
        }

        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {
            CrudChatbotForm frm = new CrudChatbotForm();
            frm.ShowDialog();
        }

        private void barButtonItem43_ItemClick(object sender, ItemClickEventArgs e)
        {
            cvvrank cvvrank = new cvvrank();
            cvvrank.ShowDialog();
        }

        private void barButtonItem44_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmchatbot1 frmchatbot1 = new frmchatbot1();
            frmchatbot1.ShowDialog();
        }

        private void barButtonItem45_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmAi frmAi = new FrmAi();
            frmAi.ShowDialog();
        }

        private void barButtonItem46_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPayment frmPayment = new FrmPayment();
            frmPayment.ShowDialog();
        }

        private void barButtonItem47_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPartialPayment frmPartialPayment = new FrmPartialPayment();
            frmPartialPayment.ShowDialog();
        }

        private void barButtonItem48_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPayment frmPayment = new FrmPayment();
            frmPayment.ShowDialog();
        }

        private void barButtonItem49_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPartialPayment frmPartialPayment = new FrmPartialPayment();
            frmPartialPayment.ShowDialog();
        }

        private void barButtonItem50_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmCustomerSearchExport frmCustomerSearchExport = new FrmCustomerSearchExport();
            frmCustomerSearchExport.ShowDialog();
        }

        private void barButtonItem51_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmSalesExport frmSalesExport = new FrmSalesExport();
            frmSalesExport.ShowDialog();
        }

        private void barButtonItem52_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmReturnSearch frmReturnSearch = new frmReturnSearch();
            frmReturnSearch.ShowDialog();
        }

        private void barButtonItem53_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmScanSales frmScanSales = new FrmScanSales();
            frmScanSales.ShowDialog();
        }

        private void barButtonItem54_ItemClick(object sender, ItemClickEventArgs e)
        {
            ForecastForm frmForecastForm = new ForecastForm();
            frmForecastForm.ShowDialog();
        }

        private void barButtonItem55_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmProductPredictorModern frmProductPredictorModern = new FrmProductPredictorModern();
            frmProductPredictorModern.ShowDialog();
        }

        private void tileItem2_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {

        }

        private void barButtonItem56_ItemClick(object sender, ItemClickEventArgs e)
        {
            DepartmentForm frmDepartmentForm = new DepartmentForm();    
            frmDepartmentForm.ShowDialog();
        }

        private void barButtonItem57_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDepartmentSearch frmDepartmentSearch = new frmDepartmentSearch();
            frmDepartmentSearch.ShowDialog();
        }

        private void barButtonItem58_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPosition frmPosition=new FrmPosition();
            frmPosition.ShowDialog();
        }

        private void barButtonItem59_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPositionList frmPositionList = new FrmPositionList();
            frmPositionList.ShowDialog();
        }

        private void barButtonItem60_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmEmployee frmEmployee=new FrmEmployee();
            frmEmployee.ShowDialog();
        }

        private void barButtonItem61_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmEmployeeList frmEmployeeList = new FrmEmployeeList();
            frmEmployeeList.ShowDialog();
        }

        private void barButtonItem62_ItemClick(object sender, ItemClickEventArgs e)
        {
            //FrmRole frmRole=new FrmRole();
            //frmRole.ShowDialog();
            FrmRoleAdd frmRoleAdd = new FrmRoleAdd();
            frmRoleAdd.ShowDialog();
        }

        //private void barButtonItem63_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //  FrmRolePermission frmRolePermission=new FrmRolePermission();
        //    frmRolePermission.ShowDialog();
        //}
        private void barButtonItem63_ItemClick(object sender, ItemClickEventArgs e)
        {
            //MessageBox.Show(
            //    "Please open the Permissions editor from the Roles list, after selecting a role.",
            //    "No Role Selected",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Warning);
            FrmRolesList frmRolesList = new FrmRolesList();
            frmRolesList.ShowDialog();
        }

        private void barButtonItem64_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserSession.EndSession();

            // Close every open form except the login form
            foreach (Form f in Application.OpenForms.Cast<Form>().ToList())
                if (!(f is FrmLogin))
                    f.Close();

            // Now show the login form
            new FrmLogin().Show();
        }

        private void barButtonItem65_ItemClick(object sender, ItemClickEventArgs e)
        {
            //FrmRolePermissions2 frmRolePermissions2 = new FrmRolePermissions2();
            //frmRolePermissions2.ShowDialog();
        }

        private void barButtonItem66_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItem67_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmLoginHistory frmLoginHistory = new FrmLoginHistory();
            frmLoginHistory.ShowDialog();
        }
    }
}
