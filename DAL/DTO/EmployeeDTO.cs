using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
    public class EmployeeDTO
    {
        public List<DepartmentDetailDTO> Departments { get; set; }
        public List<PositionDetailDTO> Positions { get; set; }
        public List<EmployeeDetailDTO> Employees { get; set; }
        public List<RoleDetailDTO> Roles { get; set; }
    }
}

