using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;

namespace Erp_V1.DAL.DAO
{
    /// <summary>
    /// Data access object for EXPENSES operations.
    /// </summary>
    public class ExpensesDAO : StockContext, IDAO<EXPENSES, ExpensesDetailDTO>
    {
        /// <summary>
        /// Inserts a new expense record into the database.
        /// </summary>
        public bool Insert(EXPENSES entity)
        {
            try
            {
                DbContext.EXPENSES.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception("Expense insertion failed. Validation errors:\n" + string.Join("\n", errorMessages));
            }
            catch (Exception ex)
            {
                throw new Exception("Expense insertion failed", ex);
            }
        }

        /// <summary>
        /// Updates an existing expense record.
        /// </summary>
        public bool Update(EXPENSES entity)
        {
            try
            {
                var expense = DbContext.EXPENSES.First(x => x.ID == entity.ID);
                expense.ExpensesName = entity.ExpensesName;
                expense.CostOfExpenses = entity.CostOfExpenses;
                expense.Note = entity.Note;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception("Expense update failed. Validation errors:\n" + string.Join("\n", errorMessages));
            }
            catch (Exception ex)
            {
                throw new Exception("Expense update failed", ex);
            }
        }

        /// <summary>
        /// Soft deletes an expense record.
        /// </summary>
        public bool Delete(EXPENSES entity)
        {
            try
            {
                if (entity.ID != 0)
                {
                    var expense = DbContext.EXPENSES.First(x => x.ID == entity.ID);
                    expense.isDeleted = true;
                    expense.DeletedDate = DateTime.Today;
                }
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception("Expense deletion failed. Validation errors:\n" + string.Join("\n", errorMessages));
            }
            catch (Exception ex)
            {
                throw new Exception("Expense deletion failed", ex);
            }
        }

        /// <summary>
        /// Restores a soft-deleted expense record.
        /// </summary>
        public bool GetBack(int ID)
        {
            try
            {
                var expense = DbContext.EXPENSES.First(x => x.ID == ID);
                expense.isDeleted = false;
                expense.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception("Expense restoration failed. Validation errors:\n" + string.Join("\n", errorMessages));
            }
            catch (Exception ex)
            {
                throw new Exception("Expense restoration failed", ex);
            }
        }

        /// <summary>
        /// Retrieves all non-deleted expenses.
        /// </summary>
        public List<ExpensesDetailDTO> Select()
        {
            try
            {
                var query = from e in DbContext.EXPENSES.Where(x => x.isDeleted == false)
                            select new ExpensesDetailDTO
                            {
                                ID = e.ID,
                                ExpensesName = e.ExpensesName,
                                CostOfExpenses = e.CostOfExpenses,
                                Note = e.Note
                            };

                return query.OrderBy(x => x.ID).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception("Expense retrieval failed. Validation errors:\n" + string.Join("\n", errorMessages));
            }
            catch (Exception ex)
            {
                throw new Exception("Expense retrieval failed", ex);
            }
        }
    }
}
