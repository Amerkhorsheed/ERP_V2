using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmExpensesList : Form
    {
        private ExpensesBLL bll = new ExpensesBLL();
        private ExpensesDTO dto = new ExpensesDTO();
        private ExpensesDetailDTO detail = new ExpensesDetailDTO();

        public FrmExpensesList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the list of expenses into the DataGridView.
        /// </summary>
        private void LoadExpenses()
        {
            try
            {
                dto = bll.Select();
                dataGridView1.DataSource = dto.Expenses;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load expenses: " + ex.Message);
            }
        }

        private void FrmExpensesList_Load(object sender, EventArgs e)
        {
            LoadExpenses();

            // Adjust grid columns: hide ID and set header text for the expense name column.
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Expense Name";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Opens the Expense detail form to add a new expense.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            FrmExpenses frm = new FrmExpenses();
            frm.ShowDialog();
            LoadExpenses();
        }

        /// <summary>
        /// Filters the expenses list as the user types.
        /// </summary>
        private void txtExpenses_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                List<ExpensesDetailDTO> list = dto.Expenses;
                list = list.Where(x => x.ExpensesName.IndexOf(txtExpenses.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                dataGridView1.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering expenses: " + ex.Message);
            }
        }

        /// <summary>
        /// Opens the Expense detail form for updating the selected expense.
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.ID == 0)
            {
                MessageBox.Show("Please select an expense from the table.");
                return;
            }

            FrmExpenses frm = new FrmExpenses
            {
                detail = detail,
                isUpdate = true
            };

            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            LoadExpenses();
        }

        /// <summary>
        /// Updates the selected expense detail based on the current row in the DataGridView.
        /// </summary>
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = dataGridView1.Rows[e.RowIndex].DataBoundItem as ExpensesDetailDTO;
        }

        /// <summary>
        /// Deletes the selected expense after confirmation.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detail.ID == 0)
            {
                MessageBox.Show("Please select an expense from the table.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected expense?",
                                                  "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("Expense was deleted successfully.");
                        LoadExpenses();
                        txtExpenses.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete expense: " + ex.Message);
                }
            }
        }

       
    }
}
