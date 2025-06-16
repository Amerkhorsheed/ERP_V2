using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace Erp_V1.DAL.DAO
{
    /// <summary>
    /// Database context wrapper with essential features.
    /// This class wraps the Entity Framework DbContext (erp_v1Entities) to perform
    /// CRUD operations and manage database interactions with optimized settings.
    /// </summary>
    public class StockContext : IDisposable
    {
        #region Core Components
        /// <summary>
        /// The main Entity Framework DbContext that provides access to the database.
        /// It holds references to entity models and is used to perform database operations.
        /// </summary>
        protected erp_v2Entities DbContext { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the StockContext class and sets up the DbContext.
        /// </summary>
        public StockContext()
        {
            DbContext = new erp_v2Entities();
            ConfigureContext();
        }
        #endregion

        #region Configuration
        /// <summary>
        /// Configures the DbContext with optimized settings for performance.
        /// Disables lazy loading, proxy creation, and sets a command timeout.
        /// </summary>
        private void ConfigureContext()
        {
            // Disable performance-heavy features
            DbContext.Configuration.LazyLoadingEnabled = false;
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Set command timeout (in seconds)
            DbContext.Database.CommandTimeout = 30;
        }
        #endregion

        #region Data Operations
        /// <summary>
        /// Saves all changes made to the DbContext to the underlying database.
        /// </summary>
        /// <returns>The number of affected rows in the database.</returns>
        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
        #endregion

        #region Disposal
        /// <summary>
        /// A flag to determine whether the object has been disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Disposes of the resources used by the StockContext.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the resources used by the StockContext.
        /// </summary>
        /// <param name="disposing">True if called explicitly, false if called by the garbage collector.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && DbContext != null)
            {
                // Dispose of the DbContext instance
                DbContext.Dispose();
                DbContext = null;
            }

            _disposed = true;
        }
        #endregion
    }
}
