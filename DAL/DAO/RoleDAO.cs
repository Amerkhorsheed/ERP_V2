using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;

namespace Erp_V1.DAL.DAO
{
    public class RoleDAO : StockContext, IDAO<ROLE, RoleDetailDTO>
    {
        public bool Insert(ROLE entity)
        {
            try
            {
                DbContext.ROLE.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Role insertion failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public bool Delete(ROLE entity)
        {
            try
            {
                var role = DbContext.ROLE.First(r => r.ID == entity.ID);
                DbContext.ROLE.Remove(role);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbUpdateException dbEx)
            {
                // Check if it's a foreign‑key violation on EMPLOYEE_ROLE.RoleID
                if (dbEx.InnerException?.InnerException is SqlException sqlEx
                    && sqlEx.Number == 547)   // SQL Server FK violation
                {
                    // Here you know the role is still referenced by one or more employees
                    throw new Exception(
                        $"Cannot delete role “{entity.RoleName}” because it is still assigned to one or more employees. " +
                        "Please unassign this role from all employees (e.g. via the Employee → Roles screen) before deleting it."
                    );
                }
                // fallback for any other update exception
                throw new Exception("Role deletion failed. " + dbEx.Message, dbEx);
            }
            catch (Exception ex)
            {
                // any other unexpected error
                throw new Exception("Role deletion failed: " + ex.Message, ex);
            }
        }


        // Parameterless required by IDAO
        public List<RoleDetailDTO> Select() => Select(false);

        // Internal with optional flag (not used here)
        public List<RoleDetailDTO> Select(bool includeDeleted = false)
        {
            try
            {
                return DbContext.ROLE
                    .Select(r => new RoleDetailDTO
                    {
                        RoleID = r.ID,
                        RoleName = r.RoleName,
                        Description = r.Description
                    })
                    .OrderBy(r => r.RoleID)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Role retrieval failed.", ex);
            }
        }

        public bool Update(ROLE entity)
        {
            try
            {
                var role = DbContext.ROLE.First(r => r.ID == entity.ID);
                role.RoleName = entity.RoleName;
                role.Description = entity.Description;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Role update failed.", ex);
            }
        }
        public bool GetBack(int id)
        => throw new NotSupportedException("GetBack is not supported for roles.");
    }
}
