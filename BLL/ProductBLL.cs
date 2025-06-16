using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{
   
    public class ProductBLL : IBLL<ProductDetailDTO, ProductDTO>
    {
        #region Data Access Dependencies
        private readonly CategoryDAO _categoryDao = new CategoryDAO();
        private readonly ProductDAO _productDao = new ProductDAO();
        private readonly SalesDAO _salesDao = new SalesDAO();
        #endregion

        #region CRUD Operations
        
        
        
        public bool Delete(ProductDetailDTO entity)
        {
            var product = new PRODUCT { ID = entity.ProductID };
            _productDao.Delete(product);

            var sales = new SALES { ProductID = entity.ProductID };
            _salesDao.Delete(sales);

            return true;
        }

        
      
       
        public bool GetBack(ProductDetailDTO entity)
        {
            return _productDao.GetBack(entity.ProductID);
        }

       
        public bool Insert(ProductDetailDTO entity)
        {
            var product = new PRODUCT
            {
                ProductName = entity.ProductName,
                CategoryID = entity.CategoryID,
                Price = entity.price,
                Sale_Price = entity.Sale_Price,   
                MinQty = entity.MinQty,           
                MaxDiscount = entity.MaxDiscount  
            };
            return _productDao.Insert(product);
        }


       
        public ProductDTO Select()
        {
            return new ProductDTO
            {
                Categories = _categoryDao.Select(),
                Products = _productDao.Select()
            };
        }


        public bool Update(ProductDetailDTO entity)
        {
            var product = new PRODUCT
            {
                ID = entity.ProductID,
                ProductName = entity.ProductName,
                Price = entity.price,
                StockAmount = entity.stockAmount,
                CategoryID = entity.CategoryID,
                Sale_Price = entity.Sale_Price,
                MinQty = entity.MinQty,
                MaxDiscount = entity.MaxDiscount
            };
            return _productDao.Update(product);
        }



        #endregion
    }
}