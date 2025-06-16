using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;

namespace Erp_V1.DAL.DAO
{
    public class EmployeeDAO : StockContext, IDAO<EMPLOYEE, EmployeeDetailDTO>
    {
        public bool Insert(EMPLOYEE e)
        {
            try
            {
                DbContext.EMPLOYEE.Add(e);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                             .SelectMany(x => x.ValidationErrors)
                             .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Employee insert failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public bool Delete(EMPLOYEE e)
        {
            try
            {
                var emp = DbContext.EMPLOYEE.First(x => x.ID == e.ID);
                emp.IsDeleted = true;
                emp.DeletedDate = DateTime.UtcNow;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Employee deletion failed.", ex);
            }
        }

        public bool GetBack(int id)
        {
            try
            {
                var emp = DbContext.EMPLOYEE.First(x => x.ID == id);
                emp.IsDeleted = false;
                emp.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Employee restoration failed.", ex);
            }
        }

        public bool Update(EMPLOYEE e)
        {
            try
            {
                var emp = DbContext.EMPLOYEE.First(x => x.ID == e.ID);
                emp.UserNo = e.UserNo;
                emp.Name = e.Name;
                emp.Surname = e.Surname;
                emp.Password = e.Password;
                emp.BirthDay = e.BirthDay;
                emp.Address = e.Address;
                emp.ImagePath = e.ImagePath;
                emp.Salary = e.Salary;
                emp.DepartmentID = e.DepartmentID;
                emp.PositionID = e.PositionID;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                             .SelectMany(x => x.ValidationErrors)
                             .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Employee update failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        /// <summary>
        /// Assigns exactly one role to an employee by manipulating the navigation collection.
        /// </summary>
        public bool AssignRole(int employeeId, int roleId)
        {
            try
            {
                // Eagerly load the ROLE nav property
                var emp = DbContext.EMPLOYEE
                                   .Include(e => e.ROLE)
                                   .First(e => e.ID == employeeId);

                // Clear existing roles
                emp.ROLE.Clear();

                // Find and add the new role
                var role = DbContext.ROLE.Find(roleId);
                if (role != null)
                {
                    emp.ROLE.Add(role);
                }

                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Role assignment failed.", ex);
            }
        }

        /// <summary>
        /// Updates the current Salary field on the EMPLOYEE row.
        /// Called from SalaryBLL after inserting/updating a SALARY record.
        /// </summary>
        public bool UpdateSalary(int employeeId, int newSalary)
        {
            try
            {
                var emp = DbContext.EMPLOYEE.First(e => e.ID == employeeId);
                emp.Salary = newSalary;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Employee salary update failed.", ex);
            }
        }

        public List<EmployeeDetailDTO> Select() => Select(false);

        public List<EmployeeDetailDTO> Select(bool includeDeleted)
        {
            var q = from e in DbContext.EMPLOYEE
                    where includeDeleted || !e.IsDeleted
                    join d in DbContext.DEPARTMENT on e.DepartmentID equals d.ID
                    join p in DbContext.POSITION on e.PositionID equals p.ID
                    from ri in e.ROLE.DefaultIfEmpty()
                    orderby e.UserNo
                    select new EmployeeDetailDTO
                    {
                        EmployeeID = e.ID,
                        UserNo = e.UserNo,
                        Name = e.Name,
                        Surname = e.Surname,
                        DepartmentID = d.ID,
                        DepartmentName = d.DepartmentName,
                        PositionID = p.ID,
                        PositionName = p.PositionName,
                        Salary = e.Salary,
                        Password = e.Password,
                        ImagePath = e.ImagePath,
                        Address = e.Address,
                        BirthDay = e.BirthDay,
                        RoleID = ri != null ? ri.ID : 0,
                        RoleName = ri != null ? ri.RoleName : string.Empty
                    };
            return q.ToList();
        }
        /// <summary>
        /// Retrieve the stored (hashed) password for lock-step rehashing.
        /// </summary>
        public string GetPasswordHash(int employeeId)
        {
            return DbContext.EMPLOYEE
                .Where(e => e.ID == employeeId)
                .Select(e => e.Password)
                .FirstOrDefault();
        }
    }
}
