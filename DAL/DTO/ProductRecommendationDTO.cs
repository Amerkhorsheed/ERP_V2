using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp_V1.DAL.DTO
{

    public class ProductRecommendationDTO
    {

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }
        public double PredictedRating { get; set; }

    }
}
