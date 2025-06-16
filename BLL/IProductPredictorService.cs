using System.Collections.Generic;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{
    public interface IProductPredictorService
    {
        List<ProductPredictionDTO> GenerateProductForecasts(PredictionParameters parameters);
    }
}
