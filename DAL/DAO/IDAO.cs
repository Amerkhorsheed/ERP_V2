/*************************************************
* ERP System Version 1.0
* Copyright (c) 2024
* 
* Data Access Layer Components
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Erp_V1.DAL.DTO;

namespace Erp_V1.DAL
{
    /// <summary>
    /// Interface defining common database operations for ERP entities
    /// </summary>
    /// <typeparam name="T">Database entity type (e.g., Product, Customer)</typeparam>
    /// <typeparam name="K">Data Transfer Object (DTO) type, which is the data model returned from queries</typeparam>
    internal interface IDAO<T, K> where T : class where K : class
    {
        /// <summary>
        /// Retrieves all active entities from the database
        /// </summary>
        /// <returns>List of data transfer objects representing the active entities</returns>
        List<K> Select();

        /// <summary>
        /// Creates a new entity in the database
        /// </summary>
        /// <param name="entity">Entity to create (e.g., a new product)</param>
        /// <returns>True if creation succeeded, otherwise false</returns>
        bool Insert(T entity);

        /// <summary>
        /// Updates an existing entity in the database with new values
        /// </summary>
        /// <param name="entity">Entity with updated values (e.g., an updated product)</param>
        /// <returns>True if update succeeded, otherwise false</returns>
        bool Update(T entity);

        /// <summary>
        /// Marks an entity as deleted in the database (soft delete)
        /// </summary>
        /// <param name="entity">Entity to delete (e.g., a product to mark as deleted)</param>
        /// <returns>True if deletion succeeded, otherwise false</returns>
        bool Delete(T entity);

        /// <summary>
        /// Restores a soft-deleted entity, making it active again in the database
        /// </summary>
        /// <param name="ID">ID of entity to restore (e.g., product ID)</param>
        /// <returns>True if restoration succeeded, otherwise false</returns>
        bool GetBack(int ID);
    }
}
