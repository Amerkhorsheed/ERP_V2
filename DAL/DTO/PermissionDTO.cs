using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
    public class PermissionDTO
    {
        public List<PermissionDetailDTO> Permissions { get; set; }
        public List<PERMISSIONSTATE> States { get; set; }
    }
}
