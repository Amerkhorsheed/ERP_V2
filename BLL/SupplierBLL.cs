using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{
    public class SupplierBLL : IBLL<SupplierDetailDTO, SupplierDTO>
    {
        private readonly SupplierDAO _supplierDao = new SupplierDAO();
        private readonly ProductDAO _productDao = new ProductDAO();
        private readonly CategoryDAO _categoryDao = new CategoryDAO();

        public SupplierDTO Select()
        {
            return new SupplierDTO
            {
                Suppliers = _supplierDao.Select(),
                Products = _productDao.Select(),
                Categories = _categoryDao.Select()
            };
        }
        public bool Insert(SupplierDetailDTO entity)
        {
            var supplier = new SUPPLIER
            {
                SupplierName = entity.SupplierName,
                PhoneNumber = entity.PhoneNumber,
            };
            return _supplierDao.Insert(supplier);
        }

        public bool Delete(SupplierDetailDTO entity)
        {
            var supplier = new SUPPLIER { ID = entity.SupplierID };
            return _supplierDao.Delete(supplier);
        }

        public bool GetBack(SupplierDetailDTO entity)
        {
            return _supplierDao.GetBack(entity.SupplierID);
        }

        public bool Update(SupplierDetailDTO entity)
        {
            var supplier = new SUPPLIER
            {
                ID = entity.SupplierID,
                SupplierName = entity.SupplierName,
                PhoneNumber = entity.PhoneNumber,
            };
            return _supplierDao.Update(supplier);
        }

       
        public bool UpdateProductStock(int productId, int newStock)
        {
            return _productDao.UpdateStock(productId, newStock);
        }
    }
}
