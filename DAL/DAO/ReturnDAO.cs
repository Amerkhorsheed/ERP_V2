using Erp_V1.DAL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Erp_V1.DAL.DAO
{
    public class ReturnDAO : StockContext, IDAO<PRODUCT_RETURN, ReturnDetailDTO>
    {
        public virtual bool Insert(PRODUCT_RETURN entity)
        {
            try
            {
                DbContext.PRODUCT_RETURN.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Return insertion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Return insertion failed", ex);
            }
        }

        public virtual bool Delete(PRODUCT_RETURN entity)
        {
            try
            {
                if (entity.ID != 0)
                {
                    var ret = DbContext.PRODUCT_RETURN.First(x => x.ID == entity.ID);
                    ret.isDeleted = true;
                    ret.DeletedDate = DateTime.Today;
                }
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Return deletion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Return deletion failed", ex);
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                var ret = DbContext.PRODUCT_RETURN.First(x => x.ID == ID);
                ret.isDeleted = false;
                ret.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Return restoration failed", ex);
            }
        }

        public virtual bool Update(PRODUCT_RETURN entity)
        {
            try
            {
                var ret = DbContext.PRODUCT_RETURN.FirstOrDefault(x => x.ID == entity.ID);
                if (ret == null)
                    return false;

                ret.SalesID = entity.SalesID;
                ret.ProductID = entity.ProductID;
                ret.CustomerID = entity.CustomerID;
                ret.ReturnQuantity = entity.ReturnQuantity;
                ret.ReturnDate = entity.ReturnDate;
                ret.ReturnReason = entity.ReturnReason;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Return update failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Return update failed", ex);
            }
        }

        public virtual List<ReturnDetailDTO> Select()
        {
            return ExecuteReturnQuery(false);
        }

        public virtual List<ReturnDetailDTO> Select(bool isDeleted)
        {
            return ExecuteReturnQuery(isDeleted);
        }

        private List<ReturnDetailDTO> ExecuteReturnQuery(bool isDeleted)
        {
            try
            {
                var query = from r in DbContext.PRODUCT_RETURN.Where(x => x.isDeleted == isDeleted)
                            join p in DbContext.PRODUCT on r.ProductID equals p.ID
                            join s in DbContext.SALES on r.SalesID equals s.ID
                            join c in DbContext.CUSTOMER on s.CustomerID equals c.ID
                            join cat in DbContext.CATEGORY on s.CategoryID equals cat.ID
                            select new ReturnDetailDTO
                            {
                                ReturnID = r.ID,
                                SalesID = r.SalesID,
                                ProductID = r.ProductID,
                                CustomerID = r.CustomerID,
                                ReturnQuantity = r.ReturnQuantity,
                                ReturnDate = r.ReturnDate,
                                ReturnReason = r.ReturnReason,
                                ProductName = p.ProductName,
                                CustomerName = c.CustomerName,
                                CategoryName = cat.CategoryName,
                                CategoryID = cat.ID
                            };

                return query.OrderBy(x => x.ReturnDate).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Return query failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Return query failed", ex);
            }
        }
    }
}
