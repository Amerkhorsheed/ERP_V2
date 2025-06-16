using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{

    public class CustomerDetailDTO
    {

        public int ID { get; set; }

        public string CustomerName { get; set; }
        public string Cust_Address { get; set; }
        public string Cust_Phone { get; set; }
        public string Notes { get; set; }
        public long? baky { get; set; } // Outstanding amount
    }
}