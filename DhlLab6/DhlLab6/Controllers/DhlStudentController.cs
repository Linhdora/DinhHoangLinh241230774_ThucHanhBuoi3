using DhlLab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;

namespace DhlLab6.Controllers;

public sealed class DhlStudentController(IWebHostEnvironment webHostEnvironment) : Controller
{
    private const int PageSize = 2;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    private static readonly List<DhlStudent> Students =
    [
        new() { Id = 101, Name = "Hải Nam", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "A1-2018", Email = "nam@gmail.com" },
        new() { Id = 102, Name = "Minh Tú", Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Address = "A1-2019", Email = "tu@gmail.com" },
        new() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Address = "A1-2020", Email = "phong@gmail.com" },
        new() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "A1-2021", Email = "mai@gmail.com" }
    ];

    [HttpGet("Admin/Student/List")]
    public IActionResult Index(int? mid, string? keyword, int pageIndex = 1) =>
        View(BuildListModel(mid, keyword, pageIndex));

    [HttpGet("Admin/Student/LearnerFilter", Name = "StudentLearnerFilter")]
    public IActionResult LearnerFilter(int? mid, string? keyword, int pageIndex = 1) =>
        PartialView("_StudentTable", BuildListModel(mid, keyword, pageIndex));

    [HttpGet("Admin/Student/Add")]
    public IActionResult Create()
    {
        PopulateCreateOptions();
        return View();
    }

    [HttpPost("Admin/Student/Add")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DhlStudent student, IFormFile? avatarFile, CancellationToken cancellationToken)
    {
        if (avatarFile is { Length: > 0 })
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            Directory.CreateDirectory(uploadsFolder);

            var safeFileName = Path.GetFileName(avatarFile.FileName);
            var uniqueFileName = $"{Guid.NewGuid():N}_{safeFileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await using var fileStream = new FileStream(filePath, FileMode.CreateNew);
            await avatarFile.CopyToAsync(fileStream, cancellationToken);
            student.AvatarUrl = uniqueFileName;
        }

        if (!ModelState.IsValid)
        {
            PopulateCreateOptions();
            return View(student);
        }

        student.Id = Students.Count == 0 ? 101 : Students.Max(item => item.Id) + 1;
        Students.Add(student);
        return RedirectToAction(nameof(Index));
    }

    private static StudentListViewModel BuildListModel(int? mid, string? keyword, int pageIndex)
    {
        var normalizedKeyword = keyword?.Trim() ?? string.Empty;
        IEnumerable<DhlStudent> filteredStudents = Students;

        if (mid.HasValue && Enum.IsDefined(typeof(Branch), mid.Value))
        {
            filteredStudents = filteredStudents.Where(student => (int?)student.Branch == mid.Value);
        }
        else if (mid.HasValue)
        {
            filteredStudents = [];
        }

        if (normalizedKeyword.Length > 0)
        {
            filteredStudents = filteredStudents.Where(student =>
                (student.Name ?? string.Empty).Contains(normalizedKeyword, StringComparison.CurrentCultureIgnoreCase));
        }

        var matchingStudents = filteredStudents.OrderBy(student => student.Id).ToList();
        var pageCount = Math.Max(1, (int)Math.Ceiling(matchingStudents.Count / (double)PageSize));
        var currentPage = Math.Clamp(pageIndex, 1, pageCount);

        return new StudentListViewModel
        {
            Students = matchingStudents.Skip((currentPage - 1) * PageSize).Take(PageSize).ToList(),
            PageNum = currentPage,
            PageCount = pageCount,
            Mid = mid,
            Keyword = normalizedKeyword,
            TotalCount = matchingStudents.Count
        };
    }

    private void PopulateCreateOptions()
    {
        ViewBag.AllGenders = Enum.GetValues<Gender>()
            .Select(gender => new SelectListItem(gender.ToString(), ((int)gender).ToString()))
            .ToList();
        ViewBag.AllBranches = Enum.GetValues<Branch>()
            .Select(branch => new SelectListItem(branch.ToString(), ((int)branch).ToString()))
            .ToList();
    }
}