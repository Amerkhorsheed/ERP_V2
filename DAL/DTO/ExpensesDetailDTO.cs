namespace Erp_V1.DAL.DTO
{
    /// <summary>
    /// Data transfer object for detailed expense information.
    /// </summary>
    public class ExpensesDetailDTO
    {
        public int ID { get; set; }
        public string ExpensesName { get; set; }
        public decimal CostOfExpenses { get; set; }
        public string Note { get; set; }
    }
}
