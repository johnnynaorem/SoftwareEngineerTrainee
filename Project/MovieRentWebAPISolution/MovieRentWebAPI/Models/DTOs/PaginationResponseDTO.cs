namespace MovieRentWebAPI.Models.DTOs
{
    public class PaginatedResponseDTO<T>
    {
        public int TotalRecords { get; set; }  
        public int TotalPages { get; set; }      
        public int PageSize { get; set; }         
        public int PageNumber { get; set; }       
        public IEnumerable<T> Data { get; set; }  
        
    }
}
