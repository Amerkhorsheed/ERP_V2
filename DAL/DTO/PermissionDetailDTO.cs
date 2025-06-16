using System;

namespace Erp_V1.DAL.DTO
{
    public class PermissionDetailDTO
    {
        public int PermissionID { get; set; }
        public int EmployeeID { get; set; }
        public int UserNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DepartmentID { get; set; }
        public int PositionID { get; set; }
        public int State { get; set; }
        public string StateName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PermissionDayAmount { get; set; }
        public string Explanation { get; set; }
    }
}
