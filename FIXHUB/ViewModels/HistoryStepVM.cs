namespace FIXHUB.ViewModels
{
    public class HistoryStepVM
    {
        public int HistoryStepId { get; set; }
        public int StepId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public int StepNumber { get; set; } 
        public DateTime CreatedAt { get; set; }
       
    }
}
