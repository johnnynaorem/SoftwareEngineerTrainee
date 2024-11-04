namespace MovieRentWebAPI.Models
{
    public enum UserRole
    {
        Admin,
        user
    }
    public class User
    {
        public int UserId {  get; set; } 
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; }
        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }
        public UserRole Role { get; set; }

        //Navigate
        public Customer Customer { get; set; }
    }
}
