using System;
using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
    public class FormPermissionDTO
    {
        public List<FormPermissionDetailDTO> Permissions { get; set; }
        public List<string> AvailableForms { get; set; }
    }

    public class FormPermissionDetailDTO
    {
        public int PermissionID { get; set; }
        public int RoleID { get; set; }
        public string FormName { get; set; }
        public bool HasAccess { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
} 