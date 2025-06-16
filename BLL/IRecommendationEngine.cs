// File: BLL/IRecommendationEngine.cs
using System.Collections.Generic;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{
    public interface IRecommendationEngine
    {
        List<ProductDetailDTO> GetRecommendationsForCustomer(string customerName);
        List<ClusteringDiagnostics> EvaluateClusters(double[][] data, int maxK);
        int DetermineOptimalK(double[][] data);

        /// <summary>
        /// All available products (non‐deleted).
        /// </summary>
        List<ProductDetailDTO> AvailableProducts { get; }
    }
}
