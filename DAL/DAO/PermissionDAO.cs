using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;

namespace Erp_V1.DAL.DAO
{
    public class PermissionDAO : StockContext, IDAO<PERMISSION, PermissionDetailDTO>
    {
        public bool Insert(PERMISSION entity)
        {
            try
            {
                DbContext.PERMISSION.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Permission insert failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public bool Delete(PERMISSION entity)
        {
            try
            {
                var pr = DbContext.PERMISSION.First(p => p.ID == entity.ID);
                DbContext.PERMISSION.Remove(pr);
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Permission delete failed.", ex);
            }
        }

        public bool GetBack(int id)
        {
            // Permissions aren’t soft‐deleted; restoring isn’t supported
            throw new NotSupportedException("Permission restoration is not supported.");
        }

        public bool Update(PERMISSION entity)
        {
            try
            {
                var pr = DbContext.PERMISSION.First(p => p.ID == entity.ID);
                pr.PermissionState = entity.PermissionState;
                pr.PermissionStartDate = entity.PermissionStartDate;
                pr.PermissionEndDate = entity.PermissionEndDate;
                pr.PermissionDay = entity.PermissionDay;
                pr.PermissionExplanation = entity.PermissionExplanation;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Permission update failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public List<PermissionDetailDTO> Select() => Select(false);

        public List<PermissionDetailDTO> Select(bool includeDeleted)
        {
            // includeDeleted unused—permissions aren’t soft‐deleted
            return (from p in DbContext.PERMISSION
                    join s in DbContext.PERMISSIONSTATE on p.PermissionState equals s.ID
                    join e in DbContext.EMPLOYEE on p.EmployeeID equals e.ID
                    orderby p.PermissionStartDate
                    select new PermissionDetailDTO
                    {
                        PermissionID = p.ID,
                        EmployeeID = p.EmployeeID,
                        UserNo = e.UserNo,
                        Name = e.Name,
                        Surname = e.Surname,
                        DepartmentID = e.DepartmentID,
                        PositionID = e.PositionID,
                        State = p.PermissionState,
                        StateName = s.StateName,
                        StartDate = p.PermissionStartDate,
                        EndDate = p.PermissionEndDate,
                        PermissionDayAmount = p.PermissionDay,
                        Explanation = p.PermissionExplanation
                    })
                   .ToList();
        }

        public List<PERMISSIONSTATE> GetStates()
            => DbContext.PERMISSIONSTATE.OrderBy(st => st.ID).ToList();
    }
}
