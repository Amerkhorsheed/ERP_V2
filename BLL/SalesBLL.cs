using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL;
using System.Linq;

namespace Erp_V1.BLL
{
    public class SalesBLL : IBLL<SalesDetailDTO, SalesDTO>
    {
        #region Data Access Dependencies
        private readonly SalesDAO _salesDao = new SalesDAO();
        private readonly ProductDAO _productDao = new ProductDAO();
        private readonly CategoryDAO _categoryDao = new CategoryDAO();
        private readonly CustomerDAO _customerDao = new CustomerDAO();
        #endregion

        #region CRUD Operations

        public bool Delete(SalesDetailDTO entity)
        {
            // Delete the sales record.
            var sales = new SALES { ID = entity.SalesID };
            _salesDao.Delete(sales);

            // Retrieve the complete product details.
            var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
            if (productDTO != null)
            {
                // Restore product stock by adding back the sales amount.
                var updatedStock = productDTO.stockAmount + entity.SalesAmount;
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
                _productDao.Update(product);
            }

            return true;
        }

        public bool GetBack(SalesDetailDTO entity)
        {
            _salesDao.GetBack(entity.SalesID);

            var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
            if (productDTO != null)
            {
                var updatedStock = productDTO.stockAmount - entity.SalesAmount;
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
                _productDao.Update(product);
            }

            return true;
        }

        public bool Insert(SalesDetailDTO entity)
        {

            // Insert the new sales record.
            var sales = new SALES
            {
                CategoryID = entity.CategoryID,
                ProductID = entity.ProductID,
                CustomerID = entity.CustomerID,
                ProductSalesPrice = entity.Price,
                ProductSalesAmout = entity.SalesAmount,
                SalesDate = entity.SalesDate,
                MaxDiscount = entity.MaxDiscount,
            };
            _salesDao.Insert(sales);

            // Retrieve the complete product details.
            var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
            if (productDTO == null)
                throw new System.Exception("Product not found.");

            // Update product stock by subtracting the sales amount.
            var updatedStock = productDTO.stockAmount - entity.SalesAmount;
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

            _productDao.Update(product);

            return true;
        }

        public SalesDTO Select()
        {
            return new SalesDTO
            {
                Products = _productDao.Select(),
                Customers = _customerDao.Select(),
                Categories = _categoryDao.Select(),
                Sales = _salesDao.Select()
            };
        }

        public bool Update(SalesDetailDTO entity)
        {
            // Update the sales record.
            var sales = new SALES
            {
                ID = entity.SalesID,
                ProductSalesAmout = entity.SalesAmount,
                ProductSalesPrice = entity.Price,
                SalesDate = entity.SalesDate,
                MaxDiscount = entity.MaxDiscount,
            };
            _salesDao.Update(sales);

            // Retrieve the complete product details.
            var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
            if (productDTO == null)
                throw new System.Exception("Product not found.");

            // Update product stock with the new stock value from the entity.
            var product = new PRODUCT
            {
                ID = productDTO.ProductID,
                ProductName = productDTO.ProductName,
                Price = productDTO.price,
                StockAmount = entity.StockAmount, // This should be pre-calculated and provided.
                CategoryID = productDTO.CategoryID,
                Sale_Price = productDTO.Sale_Price,
                MinQty = productDTO.MinQty,
                MaxDiscount = productDTO.MaxDiscount
            };
            _productDao.Update(product);

            return true;
        }
        #endregion

        #region Filtered Retrieval

        public SalesDTO Select(bool isDeleted)
        {
            return new SalesDTO
            {
                Products = _productDao.Select(isDeleted),
                Customers = _customerDao.Select(isDeleted),
                Categories = _categoryDao.Select(isDeleted),
                Sales = _salesDao.Select(isDeleted)
            };
        }
        #endregion
    }
}
