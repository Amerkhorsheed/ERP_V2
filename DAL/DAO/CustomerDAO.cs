
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;
using MathNet.Numerics.Statistics.Mcmc;

namespace Erp_V1.DAL.DAO
{

    public class CustomerDAO : StockContext, IDAO<CUSTOMER, CustomerDetailDTO>
    {
       
        public virtual bool Delete(CUSTOMER entity)
        {
            try
            {
                var customer = DbContext.CUSTOMER.First(x => x.ID == entity.ID);
                customer.isDeleted = true;
                customer.DeletedDate = DateTime.Today;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                     .SelectMany(e => e.ValidationErrors)
                                     .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Customer deletion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Customer deletion failed", ex);
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                var customer = DbContext.CUSTOMER.First(x => x.ID == ID);
                customer.isDeleted = false;
                customer.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                     .SelectMany(e => e.ValidationErrors)
                                     .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Customer restoration failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Customer restoration failed", ex);
            }
        }

        public virtual bool Insert(CUSTOMER entity)
        {
            try
            {
                DbContext.CUSTOMER.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                     .SelectMany(e => e.ValidationErrors)
                                     .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Customer insertion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Customer insertion failed", ex);
            }
        }

        protected List<CustomerDetailDTO> GetCustomers(bool isDeleted)
        {
            try
            {
                return DbContext.CUSTOMER
                    .Where(x => x.isDeleted == isDeleted)
                    .Select(item => new CustomerDetailDTO
                    {
                        ID = item.ID,
                        CustomerName = item.CustomerName,
                        Cust_Address = item.Cust_Address,
                        Cust_Phone = item.Cust_Phone,
                        Notes = item.Notes,
                        baky = item.baky,
                    }).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                     .SelectMany(e => e.ValidationErrors)
                                     .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Customer retrieval failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Customer retrieval failed", ex);
            }
        }

        public virtual List<CustomerDetailDTO> Select()
        {
            return GetCustomers(false);
        }

        public virtual List<CustomerDetailDTO> Select(bool isDeleted)
        {
            return GetCustomers(isDeleted);
        }

        public virtual bool Update(CUSTOMER entity)
        {
            try
            {
                var customer = DbContext.CUSTOMER.First(x => x.ID == entity.ID);
                bool isChanged = false;

                if (customer.CustomerName != entity.CustomerName)
                {
                    customer.CustomerName = entity.CustomerName;
                    isChanged = true;
                }
                if (customer.Cust_Address != entity.Cust_Address)
                {
                    customer.Cust_Address = entity.Cust_Address;
                    isChanged = true;
                }
                if (customer.Cust_Phone != entity.Cust_Phone)
                {
                    customer.Cust_Phone = entity.Cust_Phone;
                    isChanged = true;
                }
                if (customer.Notes != entity.Notes)
                {
                    customer.Notes = entity.Notes;
                    isChanged = true;
                }
                if (customer.baky != entity.baky)
                {
                    customer.baky = entity.baky;
                    isChanged = true;
                }

                // If nothing changed, consider it a successful update.
                if (!isChanged)
                    return true;

                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Customer update failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Customer update failed", ex);
            }
        }

    }
}
