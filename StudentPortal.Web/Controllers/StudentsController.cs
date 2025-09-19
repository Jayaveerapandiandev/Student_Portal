using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entity;

namespace StudentPortal.Web.Controllers
{
    public class StudentsController : Controller
    {
        public ApplicationDbContext DbContext { get; }
        public StudentsController(ApplicationDbContext dbContext)
        {
            DbContext=dbContext;
        }

       

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel ViewModel)
        {
            var student = new Student
            {
                Name = ViewModel.Name,
                Email = ViewModel.Email,
                IsSubscribed = ViewModel.IsSubscribed
            };
            await DbContext.Students.AddAsync(student);

            await DbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await DbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await DbContext.Students.FindAsync(id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var students = await DbContext.Students.FindAsync(viewModel.Id);

            if(students is not null)
            {
                students.Name = viewModel.Name;
                students.Email = viewModel.Email;   
                students.IsSubscribed = viewModel.IsSubscribed;

                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var students = await DbContext.Students
                .AsNoTracking().
                FirstOrDefaultAsync(x=> x.Id==viewModel.Id);

            if (students is not null)
            {
                DbContext.Students.Remove(viewModel);

                await DbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
    }
}
