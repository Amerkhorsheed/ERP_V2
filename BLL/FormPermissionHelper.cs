using Erp_V1.DAL.DTO;
using System;
using System.Linq;

namespace Erp_V1.BLL
{
    public static class FormPermissionHelper
    {
        public static bool HasFormAccess(EmployeeDetailDTO user, string formName)
        {
            if (user == null) return false;

            // Admin role has access to all forms
            if (user.RoleName.Equals("admin", StringComparison.OrdinalIgnoreCase))
                return true;

            // Check if user's role has permission for the form
            var roleBLL = new RoleBLL();
            var role = roleBLL.Select().Roles.FirstOrDefault(r => r.RoleID == user.RoleID);
            if (role == null) return false;

            var permission = role.FormPermissions?.FirstOrDefault(p => 
                p.FormName.Equals(formName, StringComparison.OrdinalIgnoreCase));
            
            return permission?.HasAccess ?? false;
        }

        public static void RequireFormAccess(EmployeeDetailDTO user, string formName)
        {
            if (!HasFormAccess(user, formName))
            {
                throw new UnauthorizedAccessException($"User does not have access to {formName}");
            }
        }
    }
} 