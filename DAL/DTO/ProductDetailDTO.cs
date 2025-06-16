using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
  
    public class ProductDetailDTO
    {
    
        public string ProductName { get; set; }

        
        public string CategoryName { get; set; }

       
        public int stockAmount { get; set; }

      
        public int price { get; set; }

        
        public int ProductID { get; set; }

       
        public int CategoryID { get; set; }

        
        public bool isCategoryDeleted { get; set; }
        public float Sale_Price { get; set; }
        public float MinQty { get;set; }
        public float MaxDiscount { get;set; }
    }
}