namespace DhlLab6.Models;

public sealed class LearnerListViewModel
{
    public IReadOnlyList<DhlLearner> Learners { get; init; } = Array.Empty<DhlLearner>();
    public IReadOnlyList<DhlMajor> Majors { get; init; } = Array.Empty<DhlMajor>();
    public int? Mid { get; init; }
    public string Keyword { get; init; } = string.Empty;
    public int PageNum { get; init; } = 1;
    public int PageCount { get; init; } = 1;
    public int TotalCount { get; init; }
}