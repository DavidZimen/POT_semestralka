namespace Domain.Pagination;

/// <summary>
/// Class that represents paged response to the client.
/// </summary>
/// <typeparam name="T">Type of the data the page holds inside.</typeparam>
public class PageResponse<T> where T : class
{
    /// <summary>
    /// Order number of the page.
    /// </summary>
    public int PageNumber { get; init; }
    
    /// <summary>
    /// Number of elements the page holds.
    /// </summary>
    public int PageSize { get; init; }
    
    /// <summary>
    /// Total records with given predicates in the DB.
    /// </summary>
    public int TotalRecords { get; init; }
    
    /// <summary>
    /// Total pages with given predicates.
    /// </summary>
    public int TotalPages { get; init; }
    
    /// <summary>
    /// Data to be transferred.
    /// </summary>
    public List<T> Data { get; init; }
    
    public PageResponse(List<T> data, int pageNumber, int pageSize, int totalRecords)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (decimal)pageSize);
    }
}