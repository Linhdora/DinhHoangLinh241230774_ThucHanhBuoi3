namespace DhlLab6.Models;

public sealed class StudentListViewModel
{
    public IReadOnlyList<DhlStudent> Students { get; init; } = [];
    public int PageNum { get; init; } = 1;
    public int PageCount { get; init; } = 1;
    public int? Mid { get; init; }
    public string Keyword { get; init; } = string.Empty;
    public int TotalCount { get; init; }
}