using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;

namespace Erp_V1.DAL.DAO
{
    public class ProductDAO : StockContext, IDAO<PRODUCT, ProductDetailDTO>
    {
        #region Database Operations

        public virtual bool Delete(PRODUCT entity)
        {
            try
            {
                if (entity.ID != 0)
                {
                    var product = DbContext.PRODUCT.First(x => x.ID == entity.ID);
                    product.isDeleted = true;
                    product.DeletedDate = DateTime.Today;
                }
                else if (entity.CategoryID != 0)
                {
                    var products = DbContext.PRODUCT
                        .Where(x => x.CategoryID == entity.CategoryID)
                        .ToList();

                    foreach (var product in products)
                    {
                        product.isDeleted = true;
                        product.DeletedDate = DateTime.Today;

                        var sales = DbContext.SALES
                            .Where(x => x.ProductID == product.ID)
                            .ToList();

                        foreach (var sale in sales)
                        {
                            sale.isDeleted = true;
                            sale.DeletedDate = DateTime.Today;
                        }
                    }
                }

                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Product deletion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Product deletion failed", ex);
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                var product = DbContext.PRODUCT.First(x => x.ID == ID);
                product.isDeleted = false;
                product.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Product restoration failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Product restoration failed", ex);
            }
        }

        public virtual bool Insert(PRODUCT entity)
        {
            try
            {
                DbContext.PRODUCT.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Product insertion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Product insertion failed", ex);
            }
        }

        protected List<ProductDetailDTO> GetProducts(bool isDeleted)
        {
            try
            {
                return (from p in DbContext.PRODUCT.Where(x => x.isDeleted == isDeleted)
                        join c in DbContext.CATEGORY on p.CategoryID equals c.ID
                        select new ProductDetailDTO
                        {
                            ProductName = p.ProductName,
                            CategoryName = c.CategoryName,
                            stockAmount = p.StockAmount,
                            price = p.Price,
                            Sale_Price = p.Sale_Price ?? 0,
                            MinQty = p.MinQty ?? 0,
                            MaxDiscount = p.MaxDiscount ?? 0,
                            ProductID = p.ID,
                            CategoryID = c.ID,
                            isCategoryDeleted = c.isDeleted
                        })
                        .OrderBy(x => x.ProductID)
                        .ToList();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Product retrieval failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Product retrieval failed", ex);
            }
        }

        public virtual List<ProductDetailDTO> Select()
        {
            return GetProducts(false);
        }

        public virtual List<ProductDetailDTO> Select(bool isDeleted)
        {
            return GetProducts(isDeleted);
        }

        public virtual bool Update(PRODUCT entity)
        {
            try
            {
                // Retrieve the existing product from the database.
                var product = DbContext.PRODUCT.First(x => x.ID == entity.ID);

                // Update all fields with the values from the entity.
                product.ProductName = entity.ProductName;
                product.Price = entity.Price;
                product.StockAmount = entity.StockAmount;
                product.CategoryID = entity.CategoryID;
                product.Sale_Price = entity.Sale_Price;
                product.MinQty = entity.MinQty;
                product.MaxDiscount = entity.MaxDiscount;

                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Product update failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Product update failed", ex);
            }
        }

        // New method to update the stock amount for a given product.
        public bool UpdateStock(int productId, int newStock)
        {
            try
            {
                var product = DbContext.PRODUCT.FirstOrDefault(x => x.ID == productId);
                if (product == null)
                    return false;
                product.StockAmount = newStock;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Product stock update failed", ex);
            }
        }

        #endregion
    }
}
