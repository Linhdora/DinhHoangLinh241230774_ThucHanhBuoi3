using DhlLab5.Models;
using Microsoft.AspNetCore.Mvc;

namespace DhlLab5.ViewComponents;

public sealed class DhlRenderViewComponent : ViewComponent
{
    private static readonly IReadOnlyList<DhlMenuItem> MenuItems =
    [
        new() { Id = 1, Name = "Branches", Link = "#", Icon = "bi bi-folder2" },
        new() { Id = 2, Name = "Students", Link = "/DhlStudent/Index", Icon = "bi bi-folder2" },
        new() { Id = 3, Name = "Subjects", Link = "#", Icon = "bi bi-folder2" },
        new() { Id = 4, Name = "Courses", Link = "#", Icon = "bi bi-folder2" }
    ];

    public IViewComponentResult Invoke() => View("DhlRenderLeftMenu", MenuItems);
}
