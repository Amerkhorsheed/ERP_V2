// File: BLL/ClusteringDiagnostics.cs
using System;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Holds the silhouette score result for a given K in K-means clustering.
    /// </summary>
    public sealed class ClusteringDiagnostics
    {
        public int K { get; set; }
        public double SilhouetteScore { get; set; }

        public ClusteringDiagnostics(int k, double silhouetteScore)
        {
            K = k;
            SilhouetteScore = silhouetteScore;
        }
    }
}
