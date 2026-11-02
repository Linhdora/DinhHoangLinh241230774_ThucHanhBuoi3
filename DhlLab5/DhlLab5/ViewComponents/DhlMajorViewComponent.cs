using DhlLab5.Data;
using DhlLab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DhlLab5.ViewComponents;

public sealed class DhlMajorViewComponent(DhlSchoolContext db) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int? selectedMajorId = null)
    {
        var majors = await db.Majors
            .AsNoTracking()
            .OrderBy(major => major.MajorName)
            .ToListAsync(HttpContext.RequestAborted);

        ViewBag.SelectedMajorId = selectedMajorId;
        return View("RenderMajor", majors);
    }
}