using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{
    public class SalaryBLL : IBLL<SalaryDetailDTO, SalaryDTO>
    {
        private readonly SalaryDAO _salDao = new SalaryDAO();
        private readonly EmployeeDAO _empDao = new EmployeeDAO();
        private readonly DepartmentDAO _deptDao = new DepartmentDAO();
        private readonly PositionDAO _posDao = new PositionDAO();

        public bool Insert(SalaryDetailDTO dto)
        {
            var entity = new SALARY
            {
                EmployeeID = dto.EmployeeID,
                MonthID = dto.MonthID,
                Year = dto.Year,
                Amount = dto.Amount
            };

            var ok = _salDao.Insert(entity);
            if (ok)
            {
                // Cascade update of employee's current salary
                _empDao.UpdateSalary(dto.EmployeeID, dto.Amount);
            }
            return ok;
        }

        public bool Delete(SalaryDetailDTO dto)
        {
            return _salDao.Delete(new SALARY { ID = dto.SalaryID });
        }

        public bool Update(SalaryDetailDTO dto)
        {
            var entity = new SALARY
            {
                ID = dto.SalaryID,
                MonthID = dto.MonthID,
                Year = dto.Year,
                Amount = dto.Amount
            };

            var ok = _salDao.Update(entity);
            if (ok)
            {
                _empDao.UpdateSalary(dto.EmployeeID, dto.Amount);
            }
            return ok;
        }

        public SalaryDTO Select()
        {
            return new SalaryDTO
            {
                Salaries = _salDao.Select(),
                Employees = _empDao.Select(),
                Departments = _deptDao.Select(),
                Positions = _posDao.Select(),
                Months = _salDao.GetMonths()
            };
        }

        public bool GetBack(SalaryDetailDTO dto)
        {
            return _salDao.GetBack(dto.SalaryID);
        }
    }
}
