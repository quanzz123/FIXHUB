namespace FIXHUB.Areas.Admin.ViewModel
{
    public class TechnicianVM
    {
        public int TechnicianID { get; set; }

        public int? UserID { get; set; }

        public string? Expertise { get; set; }

        public double? Rating { get; set; }

        public string? UserName { get; set; }
        public bool IsActive { get; set; } = true;

        public string? ProfilePicture
        {
            get; set;

        }
    }
}
