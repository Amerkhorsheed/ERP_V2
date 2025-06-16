using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;

namespace Erp_V1.DAL.DAO
{
    public class PositionDAO : StockContext, IDAO<POSITION, PositionDetailDTO>
    {
        public bool Insert(POSITION entity)
        {
            try
            {
                DbContext.POSITION.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Position insert failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public bool Delete(POSITION entity)
        {
            try
            {
                var pos = DbContext.POSITION.First(p => p.ID == entity.ID);
                pos.IsDeleted = true;
                pos.DeletedDate = DateTime.UtcNow;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Position delete failed.", ex);
            }
        }

        public bool GetBack(int id)
        {
            try
            {
                var pos = DbContext.POSITION.First(p => p.ID == id);
                pos.IsDeleted = false;
                pos.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Position restoration failed.", ex);
            }
        }

        public List<PositionDetailDTO> Select() => Select(false);

        public List<PositionDetailDTO> Select(bool includeDeleted)
        {
            try
            {
                return (from p in DbContext.POSITION
                        join d in DbContext.DEPARTMENT on p.DepartmentID equals d.ID
                        where includeDeleted || !p.IsDeleted
                        orderby p.ID
                        select new PositionDetailDTO
                        {
                            PositionID = p.ID,
                            PositionName = p.PositionName,
                            DepartmentID = p.DepartmentID,
                            DepartmentName = d.DepartmentName
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Position retrieval failed.", ex);
            }
        }

        public bool Update(POSITION entity)
        {
            try
            {
                var pos = DbContext.POSITION.First(p => p.ID == entity.ID);
                pos.PositionName = entity.PositionName;
                pos.DepartmentID = entity.DepartmentID;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Position update failed.", ex);
            }
        }
    }
}
