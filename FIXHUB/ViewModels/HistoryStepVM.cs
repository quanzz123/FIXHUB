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
        public string ImgUrl { get; set; }
        public string OldImage { get; set; }  //

        public string? OldContent { get; set; }  // Nội dung cũ
        public string? NewContent { get; set; }  // Nội dung hiện tại
        public string? HtmlDiff { get; set; }    // Kết quả highlight

    }
}
