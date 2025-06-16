using System;

namespace Erp_V1.DAL.DTO
{
    public class EmployeeDetailDTO
    {
        public int EmployeeID { get; set; }
        public int UserNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public int Salary { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
