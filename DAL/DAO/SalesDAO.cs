using Erp_V1.DAL.DTO;
using Erp_V1.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Erp_V1.DAL.DAO
{
    public class SalesDAO : StockContext, IDAO<SALES, SalesDetailDTO>
    {
        #region Database Operations

        public virtual bool Delete(SALES entity)
        {
            try
            {
                if (entity.ID != 0)
                {
                    var sale = DbContext.SALES.First(x => x.ID == entity.ID);
                    sale.isDeleted = true;
                    sale.DeletedDate = DateTime.Today;
                }
                else if (entity.ProductID != 0)
                {
                    var sales = DbContext.SALES.Where(x => x.ProductID == entity.ProductID).ToList();
                    sales.ForEach(item =>
                    {
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Today;
                    });
                }
                else if (entity.CustomerID != 0)
                {
                    var sales = DbContext.SALES.Where(x => x.CustomerID == entity.CustomerID).ToList();
                    sales.ForEach(item =>
                    {
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Today;
                    });
                }
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Sales deletion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Sales deletion failed", ex);
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                var sale = DbContext.SALES.First(x => x.ID == ID);
                sale.isDeleted = false;
                sale.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Sales restoration failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Sales restoration failed", ex);
            }
        }

        public virtual bool Insert(SALES entity)
        {
            try
            {
                DbContext.SALES.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Sales insertion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Sales insertion failed", ex);
            }
        }

        public virtual List<SalesDetailDTO> Select()
        {
            return ExecuteSalesQuery(false);
        }

        public virtual List<SalesDetailDTO> Select(bool isDeleted)
        {
            return ExecuteSalesQuery(isDeleted);
        }

        public virtual bool Update(SALES entity)
        {
            try
            {
                var sale = DbContext.SALES.FirstOrDefault(x => x.ID == entity.ID);
                if (sale == null)
                    return false;

                // Update fields based on the passed entity.
                sale.ProductSalesAmout = entity.ProductSalesAmout;
                sale.ProductSalesPrice = entity.ProductSalesPrice;
                sale.SalesDate = entity.SalesDate;
                sale.MaxDiscount = entity.MaxDiscount;

                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Sales update failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Sales update failed", ex);
            }
        }

        #endregion

        #region Helper Methods

        private List<SalesDetailDTO> ExecuteSalesQuery(bool isDeleted)
        {
            try
            {
                var query = from s in DbContext.SALES.Where(x => x.isDeleted == isDeleted)
                            join p in DbContext.PRODUCT on s.ProductID equals p.ID
                            join c in DbContext.CUSTOMER on s.CustomerID equals c.ID
                            join cat in DbContext.CATEGORY on s.CategoryID equals cat.ID
                            select new SalesDetailDTO
                            {
                                ProductName = p.ProductName,
                                CustomerName = c.CustomerName,
                                CategoryName = cat.CategoryName,
                                ProductID = s.ProductID,
                                CustomerID = s.CustomerID,
                                CategoryID = s.CategoryID,
                                SalesID = s.ID,
                                Price = s.ProductSalesPrice,
                                SalesAmount = s.ProductSalesAmout,
                                SalesDate = s.SalesDate,
                                isCategoryDeleted = cat.isDeleted,
                                isCustomerDeleted = c.isDeleted,
                                isProductDeleted = p.isDeleted,
                                MinQty = p.MinQty ?? 0,
                                MaxDiscount = s.MaxDiscount ?? 0
                            };

                return query.OrderBy(x => x.SalesDate).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Sales query failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Sales query failed", ex);
            }
        }


        #endregion
    }
}
