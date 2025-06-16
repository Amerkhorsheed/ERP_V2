using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmCustomer : DevExpress.XtraEditors.XtraForm
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        CustomerBLL bll = new CustomerBLL();
        public CustomerDetailDTO detail = new CustomerDetailDTO();
        public bool isUpdate = false;

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                txtCustomerName.Text = detail.CustomerName;
                txtCustomeradd.Text = detail.Cust_Address;
                txtCustomerphone.Text = detail.Cust_Phone;
                txtCustomernote.Text = detail.Notes;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate the customer name (still important!)
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Customer Name is empty. Please enter a valid name.");
                return;
            }

            if (!GeneralN.IsValidCustomerName(txtCustomerName.Text))
            {
                MessageBox.Show("Invalid Customer Name. It should only contain letters and spaces.");
                return;
            }

            if (!isUpdate)
            {
                // Add new customer (remains the same)
                CustomerDetailDTO customer = new CustomerDetailDTO
                {
                    CustomerName = txtCustomerName.Text,
                    Cust_Phone = txtCustomerphone.Text,
                    Cust_Address = txtCustomeradd.Text,
                    Notes = txtCustomernote.Text,
                };

                if (bll.Insert(customer))
                {
                    MessageBox.Show("Customer was added successfully.");
                    txtCustomerName.Clear();
                    txtCustomerphone.Clear();
                    txtCustomeradd.Clear();
                    txtCustomernote.Clear();
                }
            }
            else
            {
                // Update existing customer (only changed fields)

                // Check each field and update only if different
                if (detail.CustomerName != txtCustomerName.Text)
                {
                    detail.CustomerName = txtCustomerName.Text;
                }

                if (detail.Cust_Phone != txtCustomerphone.Text)
                {
                    detail.Cust_Phone = txtCustomerphone.Text;
                }

                if (detail.Cust_Address != txtCustomeradd.Text)
                {
                    detail.Cust_Address = txtCustomeradd.Text;
                }

                if (detail.Notes != txtCustomernote.Text)
                {
                    detail.Notes = txtCustomernote.Text;
                }

                if (bll.Update(detail))
                {
                    MessageBox.Show("Customer was updated successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update customer.");
                }
            }
        }

        private void FrmCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the 'ding' sound when Enter is pressed
                btnSave_Click(sender, e); // Simulate a save when Enter is pressed
            }
        }
        private void btnSave_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }
    }
}
