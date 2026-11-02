using DhlLab5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DhlLab5.Controllers;

public sealed class DhlStudentController : Controller
{
    private static readonly List<DhlStudent> Students =
    [
        new() { Id = 101, FullName = "Hai Nam", DateOfBirth = new(2001, 1, 1), Major = "CNTT", Gender = "Nam", Program = "Chính quy", Address = "A1-2018", Email = "nam@g.com" },
        new() { Id = 102, FullName = "Minh Tú", DateOfBirth = new(2001, 1, 1), Major = "Kinh tế", Gender = "Nữ", Program = "Chính quy", Address = "A1-2019", Email = "tu@g.com" },
        new() { Id = 103, FullName = "Hoàng Phong", DateOfBirth = new(2001, 1, 1), Major = "Công trình", Gender = "Nam", Program = "Phi chính quy", Address = "A1-2020", Email = "phong@g.com" },
        new() { Id = 104, FullName = "Xuân Mai", DateOfBirth = new(2001, 1, 1), Major = "Điện - Điện tử", Gender = "Nữ", Program = "Phi chính quy", Address = "A1-2021", Email = "mai@g.com" }
    ];

    public IActionResult Index(int? id)
    {
        var students = Students.OrderBy(student => student.Id);
        ViewData["SearchId"] = id;
        ViewData["NoSearchResults"] = id.HasValue && !Students.Any(student => student.Id == id.Value);
        return View(id.HasValue ? students.Where(student => student.Id == id.Value).ToList() : students.ToList());
    }

    [HttpGet]
    public IActionResult Create() => View(new DhlStudent());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(DhlStudent student)
    {
        if (!ModelState.IsValid)
        {
            return View(student);
        }

        var nextId = Students.Count == 0 ? 101 : Students.Max(item => item.Id) + 1;
        Students.Add(new DhlStudent
        {
            Id = nextId,
            FullName = student.FullName,
            DateOfBirth = student.DateOfBirth,
            Major = student.Major,
            Gender = student.Gender,
            Program = student.Program,
            Address = student.Address,
            Email = student.Email
        });

        return RedirectToAction(nameof(Index));
    }
}
