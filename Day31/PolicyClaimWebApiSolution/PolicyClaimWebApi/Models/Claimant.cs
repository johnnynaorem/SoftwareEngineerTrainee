namespace PolicyClaimWebApi.Models
{
    public class Claimant
    {
        public int ClaimantId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}
