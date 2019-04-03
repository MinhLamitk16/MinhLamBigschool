using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bigschool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbcontext_dbContext;
            public CoursesController()
        {
            _dbContext = new ApplicationDbcontext();
        }
        // GET: Courses
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.Tolist()

            };
            return View(viewModel);
        }
        [Authorize] 
        [HttpPost]
        public ActionResult Create(CoursesViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.Tolist();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Indentiny.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}