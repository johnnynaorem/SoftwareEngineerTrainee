namespace MovieRentWebAPI.Models.DTOs
{
    public class CustomerResponseDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}
