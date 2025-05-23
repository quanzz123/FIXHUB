namespace FIXHUB.ViewModels
{
    public class TechnicianVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public string AvatarUrl { get; set; }
        public List<string> Expertise { get; set; }
        public double? Rating { get; set; }
    }
}
