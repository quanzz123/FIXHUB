namespace FIXHUB.Areas.Admin.ViewModel
{
    public class UsersVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public string Bio { get; set; }
        public string Role { get; set; } 
        public bool IsActive { get; set; } = true;
        public int points { get; set; } =  0;    
        public DateTime CreatedAt { get; set; }
        
    }
}
