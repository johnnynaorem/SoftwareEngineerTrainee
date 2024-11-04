using System.ComponentModel.DataAnnotations;

namespace PolicyClaimWebApi.Models
{
    public enum Roles
    {
        Admin,
        Claimant
    }

    public class User
    {
        [Key]
        public string Username { get; set; } = string.Empty;
        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }
        public Roles Role { get; set; }

    }
}

