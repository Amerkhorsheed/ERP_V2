using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL;
using System.Linq;

namespace Erp_V1.BLL
{
    public class PurchasesBLL : IBLL<PurchasesDetailDTO, PurchasesDTO>
    {
        #region Data Access Dependencies
        private readonly PurchasesDAO _purchasesDao = new PurchasesDAO();
        private readonly ProductDAO _productDao = new ProductDAO();
        private readonly CategoryDAO _categoryDao = new CategoryDAO();
        private readonly SupplierDAO _supplierDao = new SupplierDAO();
        #endregion

        #region CRUD Operations

        public bool Delete(PurchasesDetailDTO entity)
        {
            // Mark the purchase record as deleted.
            var purchase = new PURCHASES { ID = entity.PurchaseID };
            _purchasesDao.Delete(purchase);

            // Retrieve the complete product details.
            var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
            if (productDTO != null)
            {
                // When deleting a purchase, reduce the product stock by subtracting the purchase amount.
                var updatedStock = productDTO.stockAmount - entity.PurchaseAmount;
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

        public bool GetBack(PurchasesDetailDTO entity)
        {
            // Restore the purchase record.
            _purchasesDao.GetBack(entity.PurchaseID);

            // When restoring a purchase, add the purchase amount back to product stock.
            var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
            if (productDTO != null)
            {
                var updatedStock = productDTO.stockAmount + entity.PurchaseAmount;
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

        public bool Insert(PurchasesDetailDTO entity)
        {
            // Insert the new purchase record.
            var purchase = new PURCHASES
            {
                CategoryID = entity.CategoryID,
                ProductID = entity.ProductID,
                SupplierID = entity.SupplierID,
                PurchaseSalesPrice = entity.Price,
                PurchaseSalesAmout = entity.PurchaseAmount,
                PurchaseDate = entity.PurchaseDate,
            };
            _purchasesDao.Insert(purchase);

            // Retrieve the complete product details.
            var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
            if (productDTO == null)
                throw new System.Exception("Product not found.");

            // Update product stock by adding the purchase amount.
            var updatedStock = productDTO.stockAmount + entity.PurchaseAmount;
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

        public PurchasesDTO Select()
        {
            return new PurchasesDTO
            {
                Products = _productDao.Select(),
                Suppliers = _supplierDao.Select(),
                Categories = _categoryDao.Select(),
                Purchases = _purchasesDao.Select()
            };
        }

        public bool Update(PurchasesDetailDTO entity)
        {
            // Update the purchase record.
            var purchase = new PURCHASES
            {
                ID = entity.PurchaseID,
                PurchaseSalesAmout = entity.PurchaseAmount,
                PurchaseSalesPrice = entity.Price,
                PurchaseDate = entity.PurchaseDate,
            };
            _purchasesDao.Update(purchase);

            // Retrieve the complete product details.
            var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
            if (productDTO == null)
                throw new System.Exception("Product not found.");

            // Update product stock with the new stock value provided in the entity.
            var product = new PRODUCT
            {
                ID = productDTO.ProductID,
                ProductName = productDTO.ProductName,
                Price = productDTO.price,
                StockAmount = entity.StockAmount, // Pre-calculated stock amount must be supplied.
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

        public PurchasesDTO Select(bool isDeleted)
        {
            return new PurchasesDTO
            {
                Products = _productDao.Select(isDeleted),
                Suppliers = _supplierDao.Select(isDeleted),
                Categories = _categoryDao.Select(isDeleted),
                Purchases = _purchasesDao.Select(isDeleted)
            };
        }
        #endregion
    }
}
