using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;

namespace Erp_V1.DAL.DAO
{
    public class SalaryDAO : StockContext, IDAO<SALARY, SalaryDetailDTO>
    {
        public bool Insert(SALARY entity)
        {
            try
            {
                DbContext.SALARY.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                            .SelectMany(e => e.ValidationErrors)
                            .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Salary insertion failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public bool Delete(SALARY entity)
        {
            try
            {
                var sal = DbContext.SALARY.First(x => x.ID == entity.ID);
                DbContext.SALARY.Remove(sal);
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Salary deletion failed.", ex);
            }
        }

        public bool GetBack(int id)
        {
            // Not supported for salary entries
            throw new NotSupportedException("Restoring deleted salary records is not supported.");
        }

        public bool Update(SALARY entity)
        {
            try
            {
                var sal = DbContext.SALARY.First(x => x.ID == entity.ID);
                sal.Amount = entity.Amount;
                sal.Year = entity.Year;
                sal.MonthID = entity.MonthID;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                            .SelectMany(e => e.ValidationErrors)
                            .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Salary update failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public List<SalaryDetailDTO> Select() => Select(false);

        public List<SalaryDetailDTO> Select(bool includeDeleted)
        {
            var q = from s in DbContext.SALARY
                    join e in DbContext.EMPLOYEE on s.EmployeeID equals e.ID
                    join d in DbContext.DEPARTMENT on e.DepartmentID equals d.ID
                    join p in DbContext.POSITION on e.PositionID equals p.ID
                    join m in DbContext.MONTH on s.MonthID equals m.ID
                    orderby s.Year, s.MonthID
                    select new SalaryDetailDTO
                    {
                        SalaryID = s.ID,
                        EmployeeID = e.ID,
                        UserNo = e.UserNo,
                        Name = e.Name,
                        Surname = e.Surname,
                        DepartmentID = d.ID,
                        DepartmentName = d.DepartmentName,
                        PositionID = p.ID,
                        PositionName = p.PositionName,
                        MonthID = m.ID,
                        MonthName = m.MonthName,
                        Year = s.Year,
                        Amount = s.Amount,
                        PreviousAmount = s.Amount
                    };

            return q.ToList();
        }

        public List<MONTH> GetMonths()
        {
            return DbContext.MONTH.OrderBy(m => m.ID).ToList();
        }
    }
}
