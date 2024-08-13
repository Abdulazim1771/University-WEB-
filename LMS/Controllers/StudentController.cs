using LMS.Mappings;
using LMS.Stores;
using LMS.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers;

public class StudentController : Controller
{
    private readonly StudentStore _store;

    public StudentController()
    {
        _store = new StudentStore();
    }

    // GET: StudentController
    public ActionResult Index(string? search)
    {
        var students = _store.Get(search);

        var studentView = students.Select(x => x.ToView());

        ViewBag.Students = studentView;

        return View(studentView);
    }

    // GET: StudentController/Details/5
    public ActionResult Details(int id)
    {
        var student = _store.GetById(id);

        var studentView = student.ToView();

        return View(studentView);
    }

    // GET: StudentController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: StudentController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(StudentCreateView student)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var entity = student.ToEntity();
            _store.Add(entity);

            return RedirectToAction(nameof(Index), new { id = entity.Id });
        }
        catch
        {
            return View();
        }
    }

    // GET: StudentController/Edit/5
    public ActionResult Edit(int id)
    {
        var student = _store.GetById(id);
        var studentView = student.ToUpdateView();

        return View(studentView);
    }

    // POST: StudentController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, StudentUpdateView view)
    {
        try
        {
            var entity = view.ToEntity();
            _store.Update(entity);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: StudentController/Delete/5
    public ActionResult Delete(int id)
    {
        var student = _store.GetById(id);

        return View(student.ToView());
    }

    // POST: StudentController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            _store.Delete(id);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
