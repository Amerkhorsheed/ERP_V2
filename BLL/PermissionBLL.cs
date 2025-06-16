using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;

namespace Erp_V1.BLL
{
    public class PermissionBLL : IBLL<PermissionDetailDTO, PermissionDTO>
    {
        private readonly PermissionDAO _dao = new PermissionDAO();

        public bool Insert(PermissionDetailDTO dto)
            => _dao.Insert(new PERMISSION
            {
                EmployeeID = dto.EmployeeID,
                PermissionState = dto.State,
                PermissionStartDate = dto.StartDate,
                PermissionEndDate = dto.EndDate,
                PermissionDay = dto.PermissionDayAmount,
                PermissionExplanation = dto.Explanation
            });

        public bool Delete(PermissionDetailDTO dto)
            => _dao.Delete(new PERMISSION { ID = dto.PermissionID });

        public bool Update(PermissionDetailDTO dto)
            => _dao.Update(new PERMISSION
            {
                ID = dto.PermissionID,
                PermissionState = dto.State,
                PermissionStartDate = dto.StartDate,
                PermissionEndDate = dto.EndDate,
                PermissionDay = dto.PermissionDayAmount,
                PermissionExplanation = dto.Explanation
            });

        public PermissionDTO Select()
            => new PermissionDTO
            {
                States = _dao.GetStates(),
                Permissions = _dao.Select()
            };

        public bool GetBack(PermissionDetailDTO dto)
            => throw new NotSupportedException("Permission restoration is not supported.");
    }
}
