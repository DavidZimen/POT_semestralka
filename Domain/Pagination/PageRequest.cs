namespace Domain.Pagination;

public class PageRequest
{
    /// <summary>
    /// Number of the page to be retrieved.
    /// </summary>
    public int PageNumber { get; set; }
    
    /// <summary>
    /// Number of elements that should be inside the PageResponse.
    /// </summary>
    public int PageSize { get; set; }
}