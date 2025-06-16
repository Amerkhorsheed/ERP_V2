using DocumentFormat.OpenXml.Bibliography;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Erp_V1.DAL.DAO
{
    public class DepartmentDAO : StockContext, IDAO<DEPARTMENT, DepartmentDetailDTO>
    {
        public bool Insert(DEPARTMENT entity)
        {
            try
            {
                DbContext.DEPARTMENT.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Department insertion failed:\n{string.Join("\n", errors)}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Department insertion failed.", ex);
            }
        }

        public bool Delete(DEPARTMENT entity)
        {
            try
            {
                var dept = DbContext.DEPARTMENT.First(d => d.ID == entity.ID);
                dept.IsDeleted = true;
                dept.DeletedDate = DateTime.UtcNow;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Department deletion failed.", ex);
            }
        }

        public bool GetBack(int id)
        {
            try
            {
                var dept = DbContext.DEPARTMENT.First(d => d.ID == id);
                dept.IsDeleted = false;
                dept.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Department restoration failed.", ex);
            }
        }
        public List<DepartmentDetailDTO> Select()
        {
            
            return Select(false);
        }

        public List<DepartmentDetailDTO> Select(bool includeDeleted = false)
        {
            try
            {
                return DbContext.DEPARTMENT
                    .Where(d => includeDeleted || !d.IsDeleted)
                    .Select(d => new DepartmentDetailDTO
                    {
                        DepartmentID = d.ID,
                        DepartmentName = d.DepartmentName,
                        IsDeleted = d.IsDeleted,
                        DeletedDate = d.DeletedDate
                    })
                    .OrderBy(d => d.DepartmentID)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Department retrieval failed.", ex);
            }
        }

        public bool Update(DEPARTMENT entity)
        {
            try
            {
                var dept = DbContext.DEPARTMENT.First(d => d.ID == entity.ID);
                dept.DepartmentName = entity.DepartmentName;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Department update failed:\n{string.Join("\n", errors)}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Department update failed.", ex);
            }
        }
    }
}
