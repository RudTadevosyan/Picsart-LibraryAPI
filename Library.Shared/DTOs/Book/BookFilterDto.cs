using System.ComponentModel.DataAnnotations;

namespace Library.Shared.DTOs.FilterDtos;

public class BookFilterDto
{
    public string? BookTitle { get; set; }
    public string? AuthorName { get; set; }
    public string? Genre { get; set; }
    public DateTime? PublishedAfter { get; set; }
    public bool? IsAvailable { get; set; }
    
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    [Range(1, 100)] 
    public int PageSize { get; set; } = 10;
    
    public string? SortBy { get; set; }
}