using Erp_V1.DAL.DTO;
using System;

namespace Erp_V1.BLL
{
    public static class RoleAuthorization
    {
        public static bool HasRole(EmployeeDetailDTO user, string roleName)
        {
            if (user == null) return false;
            return string.Equals(user.RoleName, roleName, StringComparison.OrdinalIgnoreCase);
        }

        public static bool HasAnyRole(EmployeeDetailDTO user, params string[] roleNames)
        {
            if (user == null) return false;
            return Array.Exists(roleNames, role => string.Equals(user.RoleName, role, StringComparison.OrdinalIgnoreCase));
        }

        public static void RequireRole(EmployeeDetailDTO user, string roleName)
        {
            if (!HasRole(user, roleName))
            {
                throw new UnauthorizedAccessException($"User does not have the required role: {roleName}");
            }
        }

        public static void RequireAnyRole(EmployeeDetailDTO user, params string[] roleNames)
        {
            if (!HasAnyRole(user, roleNames))
            {
                throw new UnauthorizedAccessException($"User does not have any of the required roles: {string.Join(", ", roleNames)}");
            }
        }
    }
} 