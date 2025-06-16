using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{
    public class PositionBLL : IBLL<PositionDetailDTO, PositionDTO>
    {
        private readonly PositionDAO _dao = new PositionDAO();
        public bool Insert(PositionDetailDTO dto)
            => _dao.Insert(new POSITION
            {
                PositionName = dto.PositionName,
                DepartmentID = dto.DepartmentID
            });

        public bool Delete(PositionDetailDTO dto)
            => _dao.Delete(new POSITION { ID = dto.PositionID });

        public PositionDTO Select()
            => new PositionDTO { Positions = _dao.Select() };

        public bool Update(PositionDetailDTO dto)
            => _dao.Update(new POSITION
            {
                ID = dto.PositionID,
                PositionName = dto.PositionName,
                DepartmentID = dto.DepartmentID
            });

        public bool GetBack(PositionDetailDTO dto)
            => _dao.GetBack(dto.PositionID);
    }
}
