using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmExpenses : Form
    {
        public FrmExpenses()
        {
            InitializeComponent();
        }

        // Use the ExpensesBLL and ExpensesDetailDTO for the Expenses module.
        ExpensesBLL bll = new ExpensesBLL();
        public ExpensesDetailDTO detail = new ExpensesDetailDTO();
        public bool isUpdate = false;

        private void FrmExpenses_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                txtExpensesName.Text = detail.ExpensesName;
                // Convert the decimal cost to a string format.
                txtCostOfExpenses.Text = detail.CostOfExpenses.ToString("N2");
                txtExpensesNote.Text = detail.Note;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate the expense name.
            if (string.IsNullOrWhiteSpace(txtExpensesName.Text))
            {
                MessageBox.Show("Expense Name is empty. Please enter a valid name.");
                return;
            }

            // Optionally, use a validation method if available.
            if (!GeneralN.IsValidCustomerName(txtExpensesName.Text)) // Consider renaming to IsValidExpenseName if applicable.
            {
                MessageBox.Show("Invalid Expense Name. It should only contain letters and spaces.");
                return;
            }

            // Validate and parse the cost of expenses.
            if (!decimal.TryParse(txtCostOfExpenses.Text, out decimal cost))
            {
                MessageBox.Show("Invalid Cost Of Expenses. Please enter a valid number.");
                return;
            }

            if (!isUpdate)
            {
                // Add new expense.
                ExpensesDetailDTO expense = new ExpensesDetailDTO
                {
                    ExpensesName = txtExpensesName.Text,
                    CostOfExpenses = cost,
                    Note = txtExpensesNote.Text
                };

                if (bll.Insert(expense))
                {
                    MessageBox.Show("Expense was added successfully.");
                    txtExpensesName.Clear();
                    txtCostOfExpenses.Clear();
                    txtExpensesNote.Clear();
                }
            }
            else
            {
                // Update existing expense (update only if the field value has changed).
                if (detail.ExpensesName != txtExpensesName.Text)
                {
                    detail.ExpensesName = txtExpensesName.Text;
                }

                if (detail.CostOfExpenses != cost)
                {
                    detail.CostOfExpenses = cost;
                }

                if (detail.Note != txtExpensesNote.Text)
                {
                    detail.Note = txtExpensesNote.Text;
                }

                if (bll.Update(detail))
                {
                    MessageBox.Show("Expense was updated successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update expense.");
                }
            }
        }

        private void FrmExpenses_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the default 'ding' sound.
                btnSave_Click(sender, e);  // Simulate a save when Enter is pressed.
            }
        }

        private void btnSave_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        
    }
}
