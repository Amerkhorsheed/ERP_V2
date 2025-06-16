//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Text;
//using System.Linq;
//using System.Windows.Forms;
//using DevExpress.XtraEditors;
//using Erp_V1;
//using static System.Data.Entity.Infrastructure.Design.Executor;
//using System.Data.Entity;
//using System.util;

//namespace Erp_V1
//{
//    public partial class Frm_Sales : DevExpress.XtraEditors.XtraForm
//    {


//        private static Frm_Sales frm;
//        static void frm_FormClosed(object sender, FormClosedEventArgs e)
//        {
//            frm = null;
//        }
//        public static Frm_Sales GetFormSales
//        {
//            get
//            {
//                if (frm == null)
//                {
//                    frm = new Frm_Sales();
//                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
//                }
//                return frm;
//            }
//        }

//        public Frm_Sales()
//        {
//            InitializeComponent();
//            if (frm == null)
//                frm = this;
//        }
//        Database db = new Database();
//        DataTable tbl = new DataTable();


//        private void OpenSaleQtyForm()
//        {
//            Frm_SaleQty saleQtyForm = new Frm_SaleQty();
//            saleQtyForm.UpdateQtyEvent += SaleQtyForm_UpdateQtyEvent;
//            saleQtyForm.ShowDialog();
//        }
//        private void FillItems()
//        {

//            cbxItems.DataSource = db.readData("select * from Products", "");
//            cbxItems.DisplayMember = "Pro_Name";
//            cbxItems.ValueMember = "Pro_ID";
//            UpdateItemCount();
//        }

//        public void FillCustomer()
//        {

//            cbxCustomer.DataSource = db.readData("select * from Customers", "");
//            cbxCustomer.DisplayMember = "Cust_Name";
//            cbxCustomer.ValueMember = "Cust_ID";
//        }
//        private void Frm_Sales_Load(object sender, EventArgs e)
//        {
//            FillItems();
//            FillCustomer();
//            DtpDate.Text = DateTime.Now.ToShortDateString();
//            DtpReminder.Text = DateTime.Now.ToShortDateString();
//            rbtnCustNakdy_CheckedChanged_1(null, null);


//            try
//            {

//                AutoNumber();
//            }
//            catch (Exception) { }
//            DgvSale.RowsAdded += DgvSale_RowsAdded;
//            DgvSale.RowsRemoved += DgvSale_RowsRemoved;
//        }
//        private void AutoNumber()
//        {
//            tbl.Clear();
//            tbl = db.readData("select max (Order_ID) from Sales", "");

//            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
//            {

//                txtID.Text = "1";
//            }
//            else
//            {

//                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
//            }
//            DtpDate.Text = DateTime.Now.ToShortDateString();
//            DtpReminder.Text = DateTime.Now.ToShortDateString();
//            try
//            {
//                cbxItems.SelectedIndex = 0;
//                cbxCustomer.SelectedIndex = 0;
//            }
//            catch (Exception) { };
//            cbxItems.Text = "اختر منتج";
//            DgvSale.Rows.Clear();
//            rbtnCustNakdy.Checked = true;
//            txtbarcode.Clear();
//            txtbarcode.Focus();
//            txtTotal.Clear();

//        }
//        private void rbtnCustNakdy_CheckedChanged_1(object sender, EventArgs e)
//        {
//            try
//            {

//                cbxCustomer.Enabled = false;
//                btnCustomerBrowes.Enabled = false;
//                DtpReminder.Enabled = false;

//            }
//            catch (Exception) { }
//        }

//        private void rbtnCustAagel_CheckedChanged_1(object sender, EventArgs e)
//        {
//            try
//            {

//                cbxCustomer.Enabled = true;
//                btnCustomerBrowes.Enabled = true;
//                DtpReminder.Enabled = true;

//            }
//            catch (Exception) { }
//        }

//        private void btnCustomerBrowes_Click_1(object sender, EventArgs e)
//        {
//            Frm_Customer frm = new Frm_Customer();
//            frm.ShowDialog();
//        }

//        private void btnItems_Click_1(object sender, EventArgs e)
//        {
//            if (cbxItems.Text == "اختر منتج")
//            {
//                MessageBox.Show("من فضلك اختر منتج اولا", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }
//            if (cbxItems.Items.Count <= 0)
//            {
//                MessageBox.Show("من فضلك ادخل المنتجات اولا", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }

//            DataTable tblItems = db.readData("select * from Products where Pro_ID=" + cbxItems.SelectedValue + "", "");
//            if (tblItems.Rows.Count >= 1)
//            {
//                try
//                {
//                    string productID = tblItems.Rows[0][0].ToString();
//                    string productName = tblItems.Rows[0][1].ToString();
//                    decimal productQty = 1; // Default quantity
//                    decimal productPrice = Convert.ToDecimal(tblItems.Rows[0][3]);
//                    decimal discount = 0;
//                    decimal total = Convert.ToDecimal(productQty) * Convert.ToDecimal(tblItems.Rows[0][3]);




//                    bool productExists = false;
//                    foreach (DataGridViewRow row in DgvSale.Rows)
//                    {
//                        if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == productID)
//                        {
//                            // Update existing row
//                            row.Cells[2].Value = Convert.ToDecimal(row.Cells[2].Value) + productQty; // Increment quantity
//                            row.Cells[3].Value = productPrice; // Update price
//                            row.Cells[4].Value = discount; // Update discount
//                            row.Cells[5].Value = Convert.ToDecimal(row.Cells[2].Value) * productPrice - discount; // Update total
//                            productExists = true;
//                            break;
//                        }
//                    }

//                    if (!productExists)
//                    {
//                        DgvSale.Rows.Add(productID, productName, productQty, productPrice, discount, total);
//                    }

//                    UpdateTotal();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void UpdateQty()
//        {
//            if (DgvSale.Rows.Count >= 1)
//            {
//                int index = DgvSale.SelectedRows[0].Index;

//                Properties.Settings.Default.Item_Qty = Convert.ToDecimal(DgvSale.Rows[index].Cells[2].Value);
//                Properties.Settings.Default.Item_SalePrice = Convert.ToDecimal(DgvSale.Rows[index].Cells[3].Value);
//                Properties.Settings.Default.Item_Discount = Convert.ToDecimal(DgvSale.Rows[index].Cells[4].Value);

//                Properties.Settings.Default.Save();

//                Frm_SaleQty frm = new Frm_SaleQty();
//                frm.UpdateQtyEvent += SaleQtyForm_UpdateQtyEvent; // Use the correct event handler name
//                frm.ShowDialog();
//            }
//        }

//        private void UpdateTotal()
//        {
//            decimal totalOrder = 0;
//            foreach (DataGridViewRow row in DgvSale.Rows)
//            {
//                if (row.Cells[5].Value != null)
//                {
//                    totalOrder += Convert.ToDecimal(row.Cells[5].Value);
//                }
//            }
//            txtTotal.Text = Math.Round(totalOrder, 2).ToString();
//            lblItemsCount.Text = DgvSale.Rows.Count.ToString();
//        }


//        private void SaleQtyForm_UpdateQtyEvent(object sender, UpdateQtyEventArgs e)
//        {
//            try
//            {
//                int index = DgvSale.SelectedRows[0].Index;

//                DgvSale.Rows[index].Cells[2].Value = e.ItemQty;
//                DgvSale.Rows[index].Cells[3].Value = e.ItemSalePrice;
//                DgvSale.Rows[index].Cells[4].Value = e.ItemDiscount;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }


//        private void UpdateItemCount()
//        {
//            textBox1.Text = DgvSale.Rows.Count.ToString();
//        }

//        private void PayOrder()
//        {

//            if (DgvSale.Rows.Count >= 1)
//            {
//                string d = DtpDate.Value.ToString("dd/MM/yyyy");
//                string dreminder = DtpReminder.Value.ToString("dd/MM/yyyy");
//                string Cust_Name = "";
//                if (rbtnCustAagel.Checked == true)
//                { Cust_Name = cbxCustomer.Text; }
//                else
//                {
//                    if (txtCustomer.Text == "")
//                    {
//                        MessageBox.Show("من فضلك ادخل اسم العميل أولا", "تاكيد");
//                        return;
//                        //Cust_Name = "عميل نقدى";
//                    }

//                    else if (txtCustomer.Text != "")
//                    {
//                        Cust_Name = txtCustomer.Text;
//                    }

//                }
//                Properties.Settings.Default.TotalOrder = Convert.ToDecimal(txtTotal.Text);
//                Properties.Settings.Default.Madfou3 = 0;
//                Properties.Settings.Default.Bakey = 0;
//                Properties.Settings.Default.Save();



//                Frm_PaySale frm = new Frm_PaySale();
//                frm.ShowDialog();
//                if (Properties.Settings.Default.CheckButton == true)
//                {

//                    try
//                    {
//                        db.exceuteData("insert into Sales values (" + txtID.Text + " , '" + d + "' , N'" + Cust_Name + "')", "");

//                        for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
//                        {

//                            db.exceuteData("insert into Sales_Detalis values (" + txtID.Text + " , N'" + Cust_Name + "' , " + DgvSale.Rows[i].Cells[0].Value + " , '" + d + "' , " + DgvSale.Rows[i].Cells[2].Value + " , '123', " + DgvSale.Rows[i].Cells[3].Value + " , " + DgvSale.Rows[i].Cells[4].Value + " ," + DgvSale.Rows[i].Cells[5].Value + " , " + Properties.Settings.Default.TotalOrder + " , " + Properties.Settings.Default.Madfou3 + " , " + Properties.Settings.Default.Bakey + ")", "");
//                            db.exceuteData("update Products set Qty = Qty - " + DgvSale.Rows[i].Cells[2].Value + " where Pro_ID=" + DgvSale.Rows[i].Cells[0].Value + "", "");

//                            try
//                            {

//                                decimal buyPrice = 0;

//                                buyPrice = Convert.ToDecimal(db.readData("select * from Products where Pro_ID=" + DgvSale.Rows[i].Cells[0].Value + "", "").Rows[0][3]);

//                                db.exceuteData("insert into Sales_Rb7h values (" + txtID.Text + " , N'" + Cust_Name + "' , " + DgvSale.Rows[i].Cells[0].Value + " , '" + d + "' , " + DgvSale.Rows[i].Cells[2].Value + " , '123', " + DgvSale.Rows[i].Cells[3].Value + " , " + DgvSale.Rows[i].Cells[4].Value + " ," + DgvSale.Rows[i].Cells[5].Value + " , " + Properties.Settings.Default.TotalOrder + " , " + Properties.Settings.Default.Madfou3 + " , " + Properties.Settings.Default.Bakey + " , " + buyPrice + ")", "");

//                            }
//                            catch (Exception) { }

//                        }

//                        if (rbtnCustNakdy.Checked == true)
//                        {
//                            db.exceuteData("insert into Customer_Report values (" + txtID.Text + " ," + Properties.Settings.Default.Madfou3 + " , '" + d + "' , N'" + Cust_Name + "')", "");

//                        }
//                        else if (rbtnCustAagel.Checked == true)
//                        {
//                            db.exceuteData("insert into Customer_Money values (" + txtID.Text + " , N'" + Cust_Name + "' , " + Properties.Settings.Default.Bakey + " ,'" + d + "' ,'" + dreminder + "')", "");

//                            if (Properties.Settings.Default.Madfou3 >= 1)
//                            {
//                                db.exceuteData("insert into Customer_Report values (" + txtID.Text + " ," + Properties.Settings.Default.Madfou3 + " , '" + d + "' , N'" + Cust_Name + "')", "");
//                            }
//                        }

//                        //Print();
//                        AutoNumber();
//                    }
//                    catch (Exception) { }
//                }
//            }

//        }
//        private void txtbarcode_KeyPress_1(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == 13)
//            {

//                DataTable tblItems = new DataTable();
//                tblItems.Clear();

//                tblItems = db.readData("select * from Products where Barcode='" + txtbarcode.Text + "'", "");
//                if (tblItems.Rows.Count >= 1)
//                {
//                    try
//                    {
//                        string Product_ID = tblItems.Rows[0][0].ToString();
//                        string Product_Name = tblItems.Rows[0][1].ToString();
//                        string Product_Qty = "1";
//                        string Product_Price = tblItems.Rows[0][4].ToString();
//                        decimal Discount = 0;

//                        decimal total = Convert.ToDecimal(Product_Qty) * Convert.ToDecimal(tblItems.Rows[0][4]);

//                        DgvSale.Rows.Add(1);
//                        int rowindex = DgvSale.Rows.Count - 1;

//                        DgvSale.Rows[rowindex].Cells[0].Value = Product_ID;
//                        DgvSale.Rows[rowindex].Cells[1].Value = Product_Name;
//                        DgvSale.Rows[rowindex].Cells[2].Value = Product_Qty;
//                        DgvSale.Rows[rowindex].Cells[3].Value = Product_Price;
//                        DgvSale.Rows[rowindex].Cells[4].Value = Discount;
//                        DgvSale.Rows[rowindex].Cells[5].Value = total;
//                    }
//                    catch (Exception) { }


//                    try
//                    {
//                        decimal TotalOrder = 0;
//                        for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
//                        {

//                            TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells[5].Value);
//                            DgvSale.ClearSelection();
//                            DgvSale.FirstDisplayedScrollingRowIndex = DgvSale.Rows.Count - 1;
//                            DgvSale.Rows[DgvSale.Rows.Count - 1].Selected = true;
//                        }


//                        txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
//                        lblItemsCount.Text = (DgvSale.Rows.Count).ToString();

//                    }
//                    catch (Exception) { }
//                }

//            }
//        }

//        private void btnDeleteItem_Click_1(object sender, EventArgs e)
//        {
//            if (DgvSale.Rows.Count >= 1)
//            {

//                int index = DgvSale.SelectedRows[0].Index;

//                DgvSale.Rows.RemoveAt(index);


//                if (DgvSale.Rows.Count <= 0)
//                {

//                    txtTotal.Text = "0";
//                }

//                try
//                {
//                    decimal TotalOrder = 0;
//                    for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
//                    {

//                        TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells[5].Value);
//                        DgvSale.ClearSelection();
//                        DgvSale.FirstDisplayedScrollingRowIndex = DgvSale.Rows.Count - 1;
//                        DgvSale.Rows[DgvSale.Rows.Count - 1].Selected = true;
//                    }


//                    txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
//                    lblItemsCount.Text = (DgvSale.Rows.Count).ToString();

//                }
//                catch (Exception) { }
//            }
//        }

//        private void DgvSale_CellValueChanged(object sender, DataGridViewCellEventArgs e)
//        {
//            decimal Item_Qty = 0, Item_SalePrice = 0, Item_Discount = 0;
//            try
//            {

//                int index = DgvSale.SelectedRows[0].Index;

//                Item_Qty = Convert.ToDecimal(DgvSale.Rows[index].Cells[2].Value);
//                Item_SalePrice = Convert.ToDecimal(DgvSale.Rows[index].Cells[3].Value);
//                Item_Discount = Convert.ToDecimal(DgvSale.Rows[index].Cells[4].Value);

//                decimal Total = 0;

//                Total = (Item_Qty * Item_SalePrice) - Item_Discount;

//                DgvSale.Rows[index].Cells[5].Value = Total;



//                decimal TotalOrder = 0;
//                for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
//                {

//                    TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells[5].Value);

//                }


//                txtTotal.Text = Math.Round(TotalOrder, 2).ToString();

//            }
//            catch (Exception) { }

//        }


//        private void simpleButton3_Click(object sender, EventArgs e)
//        {

//        }

//        private void labelControl19_Click(object sender, EventArgs e)
//        {

//        }

//        private void btnPayment_Click(object sender, EventArgs e)
//        {
//            PayOrder();
//        }

//        private void simpleButton5_Click(object sender, EventArgs e)
//        {
//            UpdateQty();
//        }

//        private void labelControl11_Click(object sender, EventArgs e)
//        {
//            UpdateQty();
//        }

//        private void labelControl12_Click(object sender, EventArgs e)
//        {
//            PayOrder();
//        }

//        private void cbxItems_SelectedIndexChanged(object sender, EventArgs e)
//        {

//        }

//        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
//        {

//        }

//        private void DgvSale_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {

//        }

//        private void lblItems_Click(object sender, EventArgs e)
//        {
//            int itemCount = DgvSale.Rows.Count;

//        }
//        private void DgvSale_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
//        {
//            UpdateItemCount();
//        }

//        private void DgvSale_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
//        {
//            UpdateItemCount();
//        }

//        private void txtCustomer_TextChanged(object sender, EventArgs e)
//        {

//        }

//        private void txtID_TextChanged(object sender, EventArgs e)
//        {

//        }

//        private void Frm_Sales_KeyDown_1(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.F2)
//            {

//                btnItems_Click_1(null, null);

//            }
//            else if (e.KeyCode == Keys.F1)
//            {

//                txtbarcode.Clear();
//                txtbarcode.Focus();
//            }
//            else if (e.KeyCode == Keys.F11)

//            {
//                UpdateQty();
//            }
//            else if (e.KeyCode == Keys.F12)
//            {
//                PayOrder();
//            }
//        }
//    }
//}
