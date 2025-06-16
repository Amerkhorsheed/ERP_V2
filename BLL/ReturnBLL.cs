using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using System.Linq;
using System.Transactions;
using System.Collections.Generic;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Business Logic Layer for processing product returns.
    /// This class handles return insertion, deletion, update and query operations
    /// while ensuring transactional consistency between return records, product stock, and sales data.
    /// </summary>
    public class ReturnBLL : IBLL<ReturnDetailDTO, ReturnDTO>
    {
        #region Data Access Dependencies
        private readonly ReturnDAO _returnDao = new ReturnDAO();
        private readonly SalesDAO _salesDao = new SalesDAO();
        private readonly ProductDAO _productDao = new ProductDAO();
        private readonly CategoryDAO _categoryDao = new CategoryDAO(); // Add Category DAO
        private readonly CustomerDAO _customerDao = new CustomerDAO(); // Add Customer DAO
        #endregion

        #region CRUD Operations

        /// <summary>
        /// Inserts a new return record and updates related product and sales data atomically.
        /// </summary>
        /// <param name="entity">The return detail data transfer object.</param>
        /// <returns>True if the entire operation succeeds; otherwise, false.</returns>
        public bool Insert(ReturnDetailDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Return detail cannot be null.");

            // Use TransactionScope to ensure atomicity across multiple data updates.
            using (var scope = new TransactionScope())
            {
                try
                {
                    // Create the return record.
                    var returnRecord = new PRODUCT_RETURN
                    {
                        SalesID = entity.SalesID,
                        ProductID = entity.ProductID,
                        CustomerID = entity.CustomerID,
                        ReturnQuantity = entity.ReturnQuantity,
                        ReturnDate = entity.ReturnDate,
                        ReturnReason = entity.ReturnReason,
                        isDeleted = false
                    };

                    if (!_returnDao.Insert(returnRecord))
                        throw new Exception("Failed to insert return record.");

                    // Update product stock (add returned quantity).
                    var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
                    if (productDTO == null)
                        throw new Exception($"Product with ID {entity.ProductID} not found.");

                    int updatedStock = productDTO.stockAmount + entity.ReturnQuantity;
                    var product = new PRODUCT
                    {
                        ID = productDTO.ProductID,
                        ProductName = productDTO.ProductName,
                        Price = productDTO.price,
                        StockAmount = updatedStock,
                        CategoryID = productDTO.CategoryID,
                        Sale_Price = productDTO.Sale_Price,
                        MinQty = productDTO.MinQty,
                        MaxDiscount = productDTO.MaxDiscount
                    };

                    if (!_productDao.Update(product))
                        throw new Exception("Failed to update product stock.");

                    // Update the corresponding sales record (subtract the returned quantity).
                    var sale = _salesDao.Select().FirstOrDefault(s => s.SalesID == entity.SalesID);
                    if (sale == null)
                        throw new Exception($"Sale with ID {entity.SalesID} not found.");

                    int updatedSalesAmount = sale.SalesAmount - entity.ReturnQuantity;
                    if (updatedSalesAmount < 0)
                        throw new Exception("Return quantity exceeds sold quantity.");

                    var updatedSale = new SALES
                    {
                        ID = sale.SalesID,
                        ProductSalesAmout = updatedSalesAmount,
                        ProductSalesPrice = sale.Price,
                        SalesDate = sale.SalesDate,
                        MaxDiscount = sale.MaxDiscount
                    };

                    if (!_salesDao.Update(updatedSale))
                        throw new Exception("Failed to update sales record.");

                    // Commit all changes.
                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    // Optionally log the exception here.
                    throw new Exception("Insert operation failed.", ex);
                }
            }
        }

        /// <summary>
        /// Marks a return record as deleted.
        /// </summary>
        public bool Delete(ReturnDetailDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Return detail cannot be null.");

            return _returnDao.Delete(new PRODUCT_RETURN { ID = entity.ReturnID });
        }

        /// <summary>
        /// Restores a previously deleted return record.
        /// </summary>
        public bool GetBack(ReturnDetailDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Return detail cannot be null.");

            return _returnDao.GetBack(entity.ReturnID);
        }

        /// <summary>
        /// Updates an existing return record.
        /// </summary>
        public bool Update(ReturnDetailDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Return detail cannot be null.");

            using (var scope = new TransactionScope())
            {
                try
                {
                    // Retrieve the original return record.
                    var originalReturn = _returnDao.Select(false)
                                                    .FirstOrDefault(r => r.ReturnID == entity.ReturnID);
                    if (originalReturn == null)
                        throw new Exception($"Return record with ID {entity.ReturnID} not found.");

                    int oldReturnQuantity = originalReturn.ReturnQuantity;
                    int newReturnQuantity = entity.ReturnQuantity;
                    int quantityDiff = newReturnQuantity - oldReturnQuantity; // Negative if reducing return quantity.

                    // Update Product Stock:
                    // If quantityDiff is negative, subtracting a negative effectively removes items from stock.
                    var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
                    if (productDTO == null)
                        throw new Exception($"Product with ID {entity.ProductID} not found.");

                    // Adjust stock: originally, when inserted, productDTO.stockAmount was increased by oldReturnQuantity.
                    // Now we adjust it by the difference.
                    int updatedStock = productDTO.stockAmount + quantityDiff;
                    var product = new PRODUCT
                    {
                        ID = productDTO.ProductID,
                        ProductName = productDTO.ProductName,
                        Price = productDTO.price,
                        StockAmount = updatedStock,
                        CategoryID = productDTO.CategoryID,
                        Sale_Price = productDTO.Sale_Price,
                        MinQty = productDTO.MinQty,
                        MaxDiscount = productDTO.MaxDiscount
                    };

                    if (!_productDao.Update(product))
                        throw new Exception("Failed to update product stock.");

                    // Update Sales Record:
                    // Adjust sales amount by subtracting the quantity difference.
                    // (If return is reduced, sales amount increases accordingly.)
                    var sale = _salesDao.Select().FirstOrDefault(s => s.SalesID == entity.SalesID);
                    if (sale == null)
                        throw new Exception($"Sale with ID {entity.SalesID} not found.");

                    int updatedSalesAmount = sale.SalesAmount - quantityDiff;
                    if (updatedSalesAmount < 0)
                        throw new Exception("Return quantity exceeds sold quantity.");

                    var updatedSale = new SALES
                    {
                        ID = sale.SalesID,
                        ProductSalesAmout = updatedSalesAmount,
                        ProductSalesPrice = sale.Price,
                        SalesDate = sale.SalesDate,
                        MaxDiscount = sale.MaxDiscount
                    };

                    if (!_salesDao.Update(updatedSale))
                        throw new Exception("Failed to update sales record.");

                    // Update the return record itself.
                    var returnRecord = new PRODUCT_RETURN
                    {
                        ID = entity.ReturnID,
                        SalesID = entity.SalesID,
                        ProductID = entity.ProductID,
                        CustomerID = entity.CustomerID,
                        ReturnQuantity = newReturnQuantity,
                        ReturnDate = entity.ReturnDate,
                        ReturnReason = entity.ReturnReason,
                        isDeleted = false
                    };

                    if (!_returnDao.Update(returnRecord))
                        throw new Exception("Failed to update return record.");

                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    // Optionally log the exception details.
                    throw new Exception("Return update operation failed.", ex);
                }
            }
        }



        /// <summary>
        /// Retrieves all return records along with categories and customers.
        /// </summary>
        public ReturnDTO Select()
        {
            return new ReturnDTO
            {
                Returns = _returnDao.Select(),
                Categories = _categoryDao.Select(), // Fetch categories
                Customers = _customerDao.Select()   // Fetch customers
            };
        }
        #endregion
    }
}