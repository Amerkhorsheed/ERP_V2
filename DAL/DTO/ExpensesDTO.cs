using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
    /// <summary>
    /// Data transfer object that encapsulates a list of expense details.
    /// </summary>
    public class ExpensesDTO
    {
        public List<ExpensesDetailDTO> Expenses { get; set; }
    }
}
