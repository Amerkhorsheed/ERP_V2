using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Business logic for managing expenses.
    /// </summary>
    public class ExpensesBLL : IBLL<ExpensesDetailDTO, ExpensesDTO>
    {
        #region Data Access Dependencies
        private readonly ExpensesDAO _expensesDao = new ExpensesDAO();
        #endregion

        #region CRUD Operations

        /// <summary>
        /// Inserts a new expense record.
        /// </summary>
        /// <param name="entity">The expense details.</param>
        /// <returns>True if inserted successfully.</returns>
        public bool Insert(ExpensesDetailDTO entity)
        {
            var expense = new EXPENSES
            {
                ExpensesName = entity.ExpensesName,
                CostOfExpenses = entity.CostOfExpenses,
                Note = entity.Note,
                isDeleted = false
            };
            return _expensesDao.Insert(expense);
        }

        /// <summary>
        /// Updates an existing expense record.
        /// </summary>
        /// <param name="entity">The expense details.</param>
        /// <returns>True if updated successfully.</returns>
        public bool Update(ExpensesDetailDTO entity)
        {
            var expense = new EXPENSES
            {
                ID = entity.ID,
                ExpensesName = entity.ExpensesName,
                CostOfExpenses = entity.CostOfExpenses,
                Note = entity.Note
            };
            return _expensesDao.Update(expense);
        }

        /// <summary>
        /// Soft deletes an expense record.
        /// </summary>
        /// <param name="entity">The expense details.</param>
        /// <returns>True if deleted successfully.</returns>
        public bool Delete(ExpensesDetailDTO entity)
        {
            var expense = new EXPENSES { ID = entity.ID };
            return _expensesDao.Delete(expense);
        }

        /// <summary>
        /// Restores a soft-deleted expense record.
        /// </summary>
        /// <param name="entity">The expense details.</param>
        /// <returns>True if restored successfully.</returns>
        public bool GetBack(ExpensesDetailDTO entity)
        {
            return _expensesDao.GetBack(entity.ID);
        }

        /// <summary>
        /// Retrieves all non-deleted expenses.
        /// </summary>
        /// <returns>A DTO containing the list of expense details.</returns>
        public ExpensesDTO Select()
        {
            return new ExpensesDTO
            {
                Expenses = _expensesDao.Select()
            };
        }

        #endregion
    }
}
