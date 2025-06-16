using System;
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;

namespace Erp_V1.BLL
{
    public class TaskBLL : IBLL<TaskDetailDTO, TaskDTO>
    {
        private readonly TaskDAO _dao = new TaskDAO();
        private readonly EmployeeDAO _emp = new EmployeeDAO();
        private readonly DepartmentDAO _dept = new DepartmentDAO();
        private readonly PositionDAO _pos = new PositionDAO();

        public bool Insert(TaskDetailDTO dto)
        {
            var entity = new TASK
            {
                TaskTitle = dto.Title,
                TaskContent = dto.Content,
                TaskStartDate = dto.TaskStartDate ?? DateTime.Today,
                TaskState = dto.taskStateID,
                EmployeeID = dto.EmployeeID
            };
            return _dao.Insert(entity);
        }

        public bool Delete(TaskDetailDTO dto)
            => _dao.Delete(new TASK { ID = dto.TaskID });

        public bool GetBack(TaskDetailDTO dto)
            => _dao.GetBack(dto.TaskID);

        public bool Update(TaskDetailDTO dto)
        {
            var entity = new TASK
            {
                ID = dto.TaskID,
                TaskTitle = dto.Title,
                TaskContent = dto.Content,
                TaskState = dto.taskStateID,
                EmployeeID = dto.EmployeeID
            };
            return _dao.Update(entity);
        }

        public TaskDTO Select()
        {
            return new TaskDTO
            {
                Employees = _emp.Select(),
                Departments = _dept.Select(),  
                Positions = _pos.Select(),       
                TaskStates = _dao.GetStates(),
                Tasks = _dao.Select()
            };
        }

        public bool Approve(TaskDetailDTO dto, bool isAdmin)
            => _dao.Approve(dto.TaskID, isAdmin);
    }
}
