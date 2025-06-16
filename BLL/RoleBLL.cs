using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;

namespace Erp_V1.BLL
{
    public class RoleBLL : IBLL<RoleDetailDTO, RoleDTO>
    {
        private readonly RoleDAO _dao = new RoleDAO();

        public bool Insert(RoleDetailDTO dto)
        {
            var entity = new ROLE
            {
                RoleName = dto.RoleName,
                Description = dto.Description
            };
            return _dao.Insert(entity);
        }

        public bool Delete(RoleDetailDTO dto)
        {
            return _dao.Delete(new ROLE { ID = dto.RoleID });
        }

        public bool GetBack(RoleDetailDTO dto)
        {
            // No restoration support
            throw new NotSupportedException("Role restoration is not supported.");
        }

        public bool Update(RoleDetailDTO dto)
        {
            var entity = new ROLE
            {
                ID = dto.RoleID,
                RoleName = dto.RoleName,
                Description = dto.Description
            };
            return _dao.Update(entity);
        }

        public RoleDTO Select()
        {
            return new RoleDTO
            {
                Roles = _dao.Select()
            };
        }
    }
}
