namespace DhlLab5.Models;

public sealed class DhlMenuItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Link { get; init; } = string.Empty;
    public string Icon { get; init; } = "bi bi-circle";
}
