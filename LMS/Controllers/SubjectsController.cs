using LMS.Mappings;
using LMS.Stores;
using LMS.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;
using University.Domain.Entities;

namespace LMS.Controllers;

public class SubjectsController : Controller
{
    private readonly SubjectsStore _subjectsStore;

    public SubjectsController()
    {
        _subjectsStore = new SubjectsStore();
    }

    // GET: SubjectsController
    public ActionResult Index(string? search)
    {
        var subjects = _subjectsStore.Get(search);
        var subjectViews = subjects.Select(x => x.ToView());

        ViewBag.Search = search;

        return View(subjectViews);
    }

    // GET: SubjectsController/Details/5
    public ActionResult Details(int id)
    {
        var subject = _subjectsStore.GetById(id);
        var subjectView = subject.ToView();

        return View();
    }

    // GET: SubjectsController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: SubjectsController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(CreateCourseView course)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var entity = course.ToEntity();
            _subjectsStore.Add(entity);
            return RedirectToAction(nameof(Details), new { id = entity.Id });
        }
        catch
        {
            return View();
        }
    }

    // GET: SubjectsController/Edit/5
    public ActionResult Edit(int id)
    {
        var subject = _subjectsStore.GetById(id);
        var subjectView = subject.ToUpdateView();

        return View(subjectView);
    }

    // POST: SubjectsController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, UpdateCourseView view)
    {
        try
        {
            var entity = view.ToEntity();
            _subjectsStore.Update(entity);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: SubjectsController/Delete/5
    public ActionResult Delete(int id)
    {
        var subject = _subjectsStore.GetById(id);
        return View(subject);
    }

    // POST: SubjectsController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            _subjectsStore.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
