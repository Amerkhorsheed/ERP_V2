using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;

namespace Erp_V1.DAL.DAO
{
    public class SupplierDAO : StockContext, IDAO<SUPPLIER, SupplierDetailDTO>
    {
        public virtual bool Insert(SUPPLIER entity)
        {
            try
            {
                DbContext.SUPPLIER.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Supplier insertion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Supplier insertion failed", ex);
            }
        }

        public virtual bool Delete(SUPPLIER entity)
        {
            try
            {
                if (entity.ID != 0)
                {
                    var supplier = DbContext.SUPPLIER.First(x => x.ID == entity.ID);
                    supplier.isDeleted = true;
                    supplier.DeletedDate = DateTime.Today;
                }
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Supplier deletion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Supplier deletion failed", ex);
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                var supplier = DbContext.SUPPLIER.First(x => x.ID == ID);
                supplier.isDeleted = false;
                supplier.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Supplier restoration failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Supplier restoration failed", ex);
            }
        }

        public virtual bool Update(SUPPLIER entity)
        {
            try
            {
                var supplier = DbContext.SUPPLIER.First(x => x.ID == entity.ID);
                supplier.SupplierName = entity.SupplierName;
                supplier.PhoneNumber = entity.PhoneNumber;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Supplier update failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Supplier update failed", ex);
            }
        }

        
        public virtual List<SupplierDetailDTO> Select()
        {
            
            return this.Select(false);
        }

        
        public virtual List<SupplierDetailDTO> Select(bool isDeleted)
        {
            try
            {
                
                var suppliers = (from s in DbContext.SUPPLIER.Where(s => s.isDeleted == isDeleted)
                                 select new SupplierDetailDTO
                                 {
                                     SupplierID = s.ID,
                                     SupplierName = s.SupplierName,
                                     PhoneNumber = s.PhoneNumber,
                                     isDeleted = s.isDeleted,
                                     DeletedDate = s.DeletedDate,
                                   
                                 }).ToList();

                return suppliers;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Supplier retrieval failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Supplier retrieval failed", ex);
            }
        }
    }
}
