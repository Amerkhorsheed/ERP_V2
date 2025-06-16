using Erp_V1.DAL; // for ROLEPERMISSION entity
using System;
using System.Collections.Generic;
using System.Linq;

namespace Erp_V1.DAL.DAO
{
    public class RolePermissionDAO : StockContext
    {
        /// <summary>
        /// Retrieves all permissions assigned to a specific role.
        /// </summary>
        /// <param name="roleId">The ID of the role.</param>
        /// <returns>A list of permission names.</returns>
        public List<string> GetPermissionsByRole(int roleId)
        {
            try
            {
                // Use DbContext.Set<ROLEPERMISSION>() to avoid relying on DbSet property names
                return DbContext
                    .Set<ROLEPERMISSION>()
                    .Where(p => p.RoleID == roleId)
                    .Select(p => p.PermissionName)
                    .ToList();
            }
            catch (Exception ex)
            {
                // TODO: Replace with professional logging
                throw new Exception("Failed to retrieve role permissions.", ex);
            }
        }

        /// <summary>
        /// Updates the permissions for a specific role. It deletes all existing
        /// permissions for the role and then inserts the new ones.
        /// </summary>
        /// <param name="roleId">The ID of the role to update.</param>
        /// <param name="newPermissions">A list of permission names to assign.</param>
        /// <returns>True if the operation succeeds, false otherwise.</returns>
        public bool UpdatePermissionsForRole(int roleId, List<string> newPermissions)
        {
            try
            {
                using (var transaction = DbContext.Database.BeginTransaction())
                {
                    var set = DbContext.Set<ROLEPERMISSION>();

                    // Delete existing permissions for this role
                    var existing = set.Where(p => p.RoleID == roleId);
                    set.RemoveRange(existing);

                    // Add the new permissions
                    foreach (var permName in newPermissions ?? Enumerable.Empty<string>())
                    {
                        var entity = new ROLEPERMISSION
                        {
                            RoleID = roleId,
                            PermissionName = permName
                        };
                        set.Add(entity);
                    }

                    DbContext.SaveChanges();
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // TODO: Replace with professional logging
                throw new Exception("Failed to update role permissions.", ex);
            }
        }
    }
}
