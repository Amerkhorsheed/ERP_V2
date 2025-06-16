using System;
using System.Collections.Generic;
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{

    public class CategoryBLL : IBLL<CategoryDetailDTO, CategoryDTO>
    {
        #region Data Access Dependencies
        private readonly CategoryDAO _categoryDao = new CategoryDAO();
        private readonly ProductDAO _productDao = new ProductDAO();
        #endregion

        #region CRUD Operations
    
        public bool Delete(CategoryDetailDTO entity)
        {
            var category = new CATEGORY { ID = entity.ID };
            _categoryDao.Delete(category);

            var product = new PRODUCT { CategoryID = entity.ID };
            _productDao.Delete(product);

            return true;
        }

        public bool GetBack(CategoryDetailDTO entity)
        {
            return _categoryDao.GetBack(entity.ID);
        }

        public bool Insert(CategoryDetailDTO entity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(entity.CategoryName) || entity.CategoryName.Length > 255)
                {
                    // Reject input that is empty or too long.
                    throw new Exception("Category insertion failed");
                }

                // Proceed with insertion (e.g., sanitize input if necessary)...
                var category = new CATEGORY { CategoryName = entity.CategoryName };
                return _categoryDao.Insert(category);
            }
            catch (Exception)
            {
                // Log the exception if needed and rethrow a generic error.
                throw new Exception("Category insertion failed");
            }
        }

        public CategoryDTO Select()
        {
            return new CategoryDTO
            {
                Categories = _categoryDao.Select()
            };
        }

        public bool Update(CategoryDetailDTO entity)
        {
            var category = new CATEGORY
            {
                ID = entity.ID,
                CategoryName = entity.CategoryName
            };
            return _categoryDao.Update(category);
        }
        #endregion
    }
}