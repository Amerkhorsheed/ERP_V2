using Erp_V1.DAL.DTO;
using Erp_V1.DAL;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace Erp_V1.DAL.DAO
{
    public class PurchasesDAO : StockContext, IDAO<PURCHASES, PurchasesDetailDTO>
    {
        #region Database Operations

        public virtual bool Delete(PURCHASES entity)
        {
            try
            {
                if (entity.ID != 0)
                {
                    var purchase = DbContext.PURCHASES.First(x => x.ID == entity.ID);
                    purchase.isDeleted = true;
                    purchase.DeletedDate = System.DateTime.Today;
                }
                else if (entity.ProductID != 0)
                {
                    var purchases = DbContext.PURCHASES.Where(x => x.ProductID == entity.ProductID).ToList();
                    purchases.ForEach(item =>
                    {
                        item.isDeleted = true;
                        item.DeletedDate = System.DateTime.Today;
                    });
                }
                else if (entity.SupplierID != 0)
                {
                    var purchases = DbContext.PURCHASES.Where(x => x.SupplierID == entity.SupplierID).ToList();
                    purchases.ForEach(item =>
                    {
                        item.isDeleted = true;
                        item.DeletedDate = System.DateTime.Today;
                    });
                }
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new System.Exception($"Purchases deletion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Purchases deletion failed", ex);
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                var purchase = DbContext.PURCHASES.First(x => x.ID == ID);
                purchase.isDeleted = false;
                purchase.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new System.Exception($"Purchases restoration failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Purchases restoration failed", ex);
            }
        }

        public virtual bool Insert(PURCHASES entity)
        {
            try
            {
                DbContext.PURCHASES.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new System.Exception($"Purchases insertion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Purchases insertion failed", ex);
            }
        }

        public virtual List<PurchasesDetailDTO> Select()
        {
            return ExecutePurchasesQuery(false);
        }

        public virtual List<PurchasesDetailDTO> Select(bool isDeleted)
        {
            return ExecutePurchasesQuery(isDeleted);
        }

        public virtual bool Update(PURCHASES entity)
        {
            try
            {
                var purchase = DbContext.PURCHASES.FirstOrDefault(x => x.ID == entity.ID);
                if (purchase == null)
                    return false;

                purchase.PurchaseSalesAmout = entity.PurchaseSalesAmout;
                purchase.PurchaseSalesPrice = entity.PurchaseSalesPrice;
                purchase.PurchaseDate = entity.PurchaseDate;

                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new System.Exception($"Purchases update failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Purchases update failed", ex);
            }
        }

        #endregion

        #region Helper Methods

        private List<PurchasesDetailDTO> ExecutePurchasesQuery(bool isDeleted)
        {
            try
            {
                var query = from p in DbContext.PURCHASES.Where(x => x.isDeleted == isDeleted)
                            join prod in DbContext.PRODUCT on p.ProductID equals prod.ID
                            join sup in DbContext.SUPPLIER on p.SupplierID equals sup.ID
                            join cat in DbContext.CATEGORY on p.CategoryID equals cat.ID
                            select new PurchasesDetailDTO
                            {
                                ProductName = prod.ProductName,
                                SupplierName = sup.SupplierName,
                                CategoryName = cat.CategoryName,
                                ProductID = p.ProductID,
                                SupplierID = p.SupplierID,
                                CategoryID = p.CategoryID,
                                PurchaseID = p.ID,
                                Price = p.PurchaseSalesPrice,
                                PurchaseAmount = p.PurchaseSalesAmout,
                                PurchaseDate = p.PurchaseDate,
                                isCategoryDeleted = cat.isDeleted,
                                isSupplierDeleted = sup.isDeleted,
                                isProductDeleted = prod.isDeleted,
                                MinQty = prod.MinQty ?? 0
                            };

                return query.OrderBy(x => x.PurchaseDate).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new System.Exception($"Purchases query failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Purchases query failed", ex);
            }
        }

        #endregion
    }
}
