using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;

namespace Erp_V1.DAL.DAO
{

    public class CategoryDAO : StockContext, IDAO<CATEGORY, CategoryDetailDTO>
    {
        #region Database Operations

        public virtual bool Delete(CATEGORY entity)
        {
            try
            {
                var category = DbContext.CATEGORY.First(x => x.ID == entity.ID);
                category.isDeleted = true;
                category.DeletedDate = DateTime.Today;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Category deletion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Category deletion failed", ex);
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                var category = DbContext.CATEGORY.First(x => x.ID == ID);
                category.isDeleted = false;
                category.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Category restoration failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Category restoration failed", ex);
            }
        }

        public virtual bool Insert(CATEGORY entity)
        {
            try
            {
                DbContext.CATEGORY.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Category insertion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Category insertion failed", ex);
            }
        }

        protected List<CategoryDetailDTO> GetCategories(bool isDeleted)
        {
            try
            {
                return DbContext.CATEGORY
                    .Where(x => x.isDeleted == isDeleted)
                    .Select(item => new CategoryDetailDTO
                    {
                        ID = item.ID,
                        CategoryName = item.CategoryName
                    }).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Category retrieval failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Category retrieval failed", ex);
            }
        }

        public virtual List<CategoryDetailDTO> Select()
        {
            return GetCategories(false);
        }

        public virtual List<CategoryDetailDTO> Select(bool isDeleted)
        {
            return GetCategories(isDeleted);
        }

        public virtual bool Update(CATEGORY entity)
        {
            try
            {
                var category = DbContext.CATEGORY.FirstOrDefault(x => x.ID == entity.ID);
                if (category == null)
                    return false;

                category.CategoryName = entity.CategoryName;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Category update failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Category update failed", ex);
            }
        }

        #endregion
    }
}
