using DhlLab5.Data;
using DhlLab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DhlLab5.Controllers;

public sealed class DhlLearnerController(DhlSchoolContext db) : Controller
{
    public async Task<IActionResult> Index(int? mid, CancellationToken cancellationToken)
    {
        var learners = await db.Learners
            .AsNoTracking()
            .Include(learner => learner.Major)
            .Where(learner => mid == null || learner.MajorID == mid.Value)
            .OrderBy(learner => learner.LearnerID)
            .ToListAsync(cancellationToken);

        ViewBag.MajorID = mid;
        return View(learners);
    }

    [HttpGet]
    public async Task<IActionResult> LearnerByMajorID(int? id, CancellationToken cancellationToken)
    {
        var learners = await db.Learners
            .AsNoTracking()
            .Include(learner => learner.Major)
            .Where(learner => id == null || learner.MajorID == id.Value)
            .OrderBy(learner => learner.LearnerID)
            .ToListAsync(cancellationToken);

        return PartialView("LearnerTable", learners);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        await LoadMajorsAsync(null, cancellationToken);
        return View(new DhlLearner { EnrollmentDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] DhlLearner learner,
        CancellationToken cancellationToken)
    {
        if (ModelState.IsValid && !await db.Majors.AnyAsync(major => major.MajorID == learner.MajorID, cancellationToken))
        {
            ModelState.AddModelError(nameof(learner.MajorID), "Ngành học không tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            await LoadMajorsAsync(learner.MajorID, cancellationToken);
            return View(learner);
        }

        db.Learners.Add(learner);
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var learner = await db.Learners.FindAsync([id.Value], cancellationToken);
        if (learner is null)
        {
            return NotFound();
        }

        await LoadMajorsAsync(learner.MajorID, cancellationToken);
        return View(learner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] DhlLearner learner,
        CancellationToken cancellationToken)
    {
        if (id != learner.LearnerID)
        {
            return NotFound();
        }

        if (ModelState.IsValid && !await db.Majors.AnyAsync(major => major.MajorID == learner.MajorID, cancellationToken))
        {
            ModelState.AddModelError(nameof(learner.MajorID), "Ngành học không tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            await LoadMajorsAsync(learner.MajorID, cancellationToken);
            return View(learner);
        }

        db.Update(learner);
        try
        {
            await db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await LearnerExistsAsync(learner.LearnerID, cancellationToken))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var learner = await db.Learners
            .AsNoTracking()
            .Include(item => item.Major)
            .Include(item => item.Enrollments)
            .FirstOrDefaultAsync(item => item.LearnerID == id.Value, cancellationToken);

        if (learner is null)
        {
            return NotFound();
        }

        if (learner.Enrollments.Count > 0)
        {
            TempData["DeleteError"] = $"Không thể xóa {learner.FirstMidName} {learner.LastName} vì còn đăng ký khóa học.";
            return RedirectToAction(nameof(Index));
        }

        return View(learner);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var learner = await db.Learners
            .Include(item => item.Enrollments)
            .FirstOrDefaultAsync(item => item.LearnerID == id, cancellationToken);

        if (learner is null)
        {
            return NotFound();
        }

        if (learner.Enrollments.Count > 0)
        {
            TempData["DeleteError"] = $"Không thể xóa {learner.FirstMidName} {learner.LastName} vì còn đăng ký khóa học.";
            return RedirectToAction(nameof(Index));
        }

        db.Learners.Remove(learner);
        await db.SaveChangesAsync(cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadMajorsAsync(int? selectedMajorId, CancellationToken cancellationToken)
    {
        ViewBag.MajorID = new SelectList(
            await db.Majors.AsNoTracking().OrderBy(major => major.MajorName).ToListAsync(cancellationToken),
            nameof(DhlMajor.MajorID),
            nameof(DhlMajor.MajorName),
            selectedMajorId);
    }

    private Task<bool> LearnerExistsAsync(int id, CancellationToken cancellationToken) =>
        db.Learners.AnyAsync(learner => learner.LearnerID == id, cancellationToken);
}
