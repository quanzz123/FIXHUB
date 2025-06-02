using BCrypt.Net;
namespace FIXHUB.Services
{
    public class func
    {
        // Hàm mã hóa mật khẩu
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Hàm kiểm tra mật khẩu so với hash
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
