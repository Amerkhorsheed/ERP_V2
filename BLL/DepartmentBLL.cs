using DocumentFormat.OpenXml.Bibliography;
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{
    public class DepartmentBLL : IBLL<DepartmentDetailDTO, DepartmentDTO>
    {
        private readonly DepartmentDAO _departmentDao = new DepartmentDAO();

        public bool Insert(DepartmentDetailDTO entity)
        {
            var dept = new DEPARTMENT
            {
                DepartmentName = entity.DepartmentName
            };
            return _departmentDao.Insert(dept);
        }

        public bool Delete(DepartmentDetailDTO entity)
        {
            var dept = new DEPARTMENT { ID = entity.DepartmentID };
            return _departmentDao.Delete(dept);
        }

        public bool GetBack(DepartmentDetailDTO entity)
        {
            return _departmentDao.GetBack(entity.DepartmentID);
        }

        public DepartmentDTO Select()
        {
            return new DepartmentDTO
            {
                Departments = _departmentDao.Select()
            };
        }

        public bool Update(DepartmentDetailDTO entity)
        {
            var dept = new DEPARTMENT
            {
                ID = entity.DepartmentID,
                DepartmentName = entity.DepartmentName
            };
            return _departmentDao.Update(dept);
        }
    }
}
