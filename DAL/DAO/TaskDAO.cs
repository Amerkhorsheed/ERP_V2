using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;

namespace Erp_V1.DAL.DAO
{
    public class TaskDAO : StockContext, IDAO<TASK, TaskDetailDTO>
    {
        public bool Insert(TASK entity)
        {
            try
            {
                DbContext.TASK.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Task insertion failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public bool Delete(TASK entity)
        {
            try
            {
                var t = DbContext.TASK.First(x => x.ID == entity.ID);
                DbContext.TASK.Remove(t);
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Task deletion failed.", ex);
            }
        }

        public bool GetBack(int id)
        {
            // No soft-delete on tasks
            throw new NotSupportedException("Task restoration is not supported.");
        }

        public bool Update(TASK entity)
        {
            try
            {
                var t = DbContext.TASK.First(x => x.ID == entity.ID);
                t.TaskTitle = entity.TaskTitle;
                t.TaskContent = entity.TaskContent;
                t.TaskState = entity.TaskState;
                t.EmployeeID = entity.EmployeeID;
                // preserve dates or update delivery if needed
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Task update failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public List<TaskDetailDTO> Select() => Select(false);

        public List<TaskDetailDTO> Select(bool includeDeleted)
        {
            // includeDeleted unused; tasks aren’t soft-deleted
            var q = from t in DbContext.TASK
                    join s in DbContext.TASKSTATE on t.TaskState equals s.ID
                    join e in DbContext.EMPLOYEE on t.EmployeeID equals e.ID
                    join d in DbContext.DEPARTMENT on e.DepartmentID equals d.ID
                    join p in DbContext.POSITION on e.PositionID equals p.ID
                    orderby t.TaskStartDate
                    select new TaskDetailDTO
                    {
                        TaskID = t.ID,
                        Title = t.TaskTitle,
                        Content = t.TaskContent,
                        TaskStartDate = t.TaskStartDate,
                        TaskDeliveryDate = t.TaskDeliveryDate,
                        taskStateID = t.TaskState,
                        TaskStateName = s.StateName,
                        EmployeeID = e.ID,
                        UserNo = e.UserNo,
                        Name = e.Name,
                        Surname = e.Surname,
                        DepartmentID = d.ID,
                        DepartmentName = d.DepartmentName,
                        PositionID = p.ID,
                        PositionName = p.PositionName
                    };
            return q.ToList();
        }

        public List<TASKSTATE> GetStates()
        {
            return DbContext.TASKSTATE.OrderBy(s => s.ID).ToList();
        }

        /// <summary>
        /// Approve or mark delivered.
        /// </summary>
        public bool Approve(int taskId, bool isAdmin)
        {
            try
            {
                var t = DbContext.TASK.First(x => x.ID == taskId);
                t.TaskState = isAdmin ? TaskStates.Approved : TaskStates.Delivered;
                t.TaskDeliveryDate = DateTime.Today;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Task approval failed.", ex);
            }
        }
    }
}
