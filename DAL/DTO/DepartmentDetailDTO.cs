using System;

namespace Erp_V1.DAL.DTO
{
    public class DepartmentDetailDTO
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
