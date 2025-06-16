using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Erp_V1.DAL.DAO
{
    public class FormPermissionDAO : StockContext, IDAO<FORM_PERMISSION, FormPermissionDetailDTO>
    {
        public bool Insert(FORM_PERMISSION entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                DbContext.FORM_PERMISSION.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Form permission insertion failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public bool Delete(FORM_PERMISSION entity)
        {
            try
            {
                var permission = DbContext.FORM_PERMISSION.First(p => p.ID == entity.ID);
                DbContext.FORM_PERMISSION.Remove(permission);
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Form permission deletion failed.", ex);
            }
        }

        public List<FormPermissionDetailDTO> Select()
        {
            try
            {
                return DbContext.FORM_PERMISSION
                    .Select(p => new FormPermissionDetailDTO
                    {
                        PermissionID = p.ID,
                        RoleID = p.RoleID,
                        FormName = p.FormName,
                        HasAccess = p.HasAccess,
                        CreatedDate = p.CreatedDate,
                        ModifiedDate = p.ModifiedDate
                    })
                    .OrderBy(p => p.RoleID)
                    .ThenBy(p => p.FormName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Form permission retrieval failed.", ex);
            }
        }

        public bool Update(FORM_PERMISSION entity)
        {
            try
            {
                var permission = DbContext.FORM_PERMISSION.First(p => p.ID == entity.ID);
                permission.HasAccess = entity.HasAccess;
                permission.ModifiedDate = DateTime.Now;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Form permission update failed.", ex);
            }
        }

        public bool GetBack(int id)
        {
            throw new NotSupportedException("GetBack is not supported for form permissions.");
        }

        public List<FormPermissionDetailDTO> GetPermissionsByRole(int roleId)
        {
            try
            {
                return DbContext.FORM_PERMISSION
                    .Where(p => p.RoleID == roleId)
                    .Select(p => new FormPermissionDetailDTO
                    {
                        PermissionID = p.ID,
                        RoleID = p.RoleID,
                        FormName = p.FormName,
                        HasAccess = p.HasAccess,
                        CreatedDate = p.CreatedDate,
                        ModifiedDate = p.ModifiedDate
                    })
                    .OrderBy(p => p.FormName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Form permission retrieval by role failed.", ex);
            }
        }
    }
} 