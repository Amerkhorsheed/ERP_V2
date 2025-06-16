
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{

    public class CustomerBLL : IBLL<CustomerDetailDTO, CustomerDTO>
    {
        #region Data Access Dependencies
        private readonly CustomerDAO _customerDao = new CustomerDAO();
        private readonly SalesDAO _salesDao = new SalesDAO();
        #endregion

        #region CRUD Operations

        public bool Delete(CustomerDetailDTO entity)
        {
            var customer = new CUSTOMER { ID = entity.ID };
            _customerDao.Delete(customer);

            var sales = new SALES { CustomerID = entity.ID };
            _salesDao.Delete(sales);

            return true;
        }

        public bool GetBack(CustomerDetailDTO entity)
        {
            return _customerDao.GetBack(entity.ID);
        }

        public bool Insert(CustomerDetailDTO entity)
        {
            var customer = new CUSTOMER
            {
                CustomerName = entity.CustomerName,
                Cust_Address = entity.Cust_Address,
                Cust_Phone = entity.Cust_Phone,
                Notes = entity.Notes,
                baky = entity.baky,
            };
            return _customerDao.Insert(customer);
        }

        public CustomerDTO Select()
        {
            return new CustomerDTO
            {
                Customers = _customerDao.Select()
            };
        }

        public bool Update(CustomerDetailDTO entity)
        {
            var customer = new CUSTOMER
            {
                ID = entity.ID,
                CustomerName = entity.CustomerName,
                Cust_Address = entity.Cust_Address,
                Cust_Phone = entity.Cust_Phone,
                Notes = entity.Notes,
                baky = entity.baky,
            };
            return _customerDao.Update(customer);
        }
        #endregion
    }
}