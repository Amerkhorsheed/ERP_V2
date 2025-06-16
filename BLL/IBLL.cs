/*************************************************
* ERP System Version 1.0
* Copyright (c) 2024 
* 
* Business Logic Layer Components
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Defines the core business logic operations for ERP entities
    /// </summary>
    /// <typeparam name="T">The entity type for CRUD operations</typeparam>
    /// <typeparam name="K">The return type for data retrieval operations</typeparam>
    internal interface IBLL<T, K> where T : class where K : class
    {
        /// <summary>
        /// Creates a new entity in the system
        /// </summary>
        /// <param name="entity">The entity to create</param>
        /// <returns>True if creation succeeded, false otherwise</returns>
        bool Insert(T entity);

        /// <summary>
        /// Updates an existing entity in the system
        /// </summary>
        /// <param name="entity">The entity with updated values</param>
        /// <returns>True if update succeeded, false otherwise</returns>
        bool Update(T entity);

        /// <summary>
        /// Marks an entity as deleted using soft delete
        /// </summary>
        /// <param name="entity">The entity to mark as deleted</param>
        /// <returns>True if deletion succeeded, false otherwise</returns>
        bool Delete(T entity);

        /// <summary>
        /// Retrieves all active (non-deleted) entities
        /// </summary>
        /// <returns>A data transfer object containing results</returns>
        K Select();

        /// <summary>
        /// Restores a soft-deleted entity
        /// </summary>
        /// <param name="entity">The entity to restore</param>
        /// <returns>True if restoration succeeded, false otherwise</returns>
        bool GetBack(T entity);
    }
}