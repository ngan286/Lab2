using Lab_02.Controllers.Interfaces;
using Lab_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_02.Controllers;

public class StudentController : Controller
{
    private List<Student> listStudents = new List<Student>();
    readonly IBufferedFileUploadService _bufferedFileUploadService;

    public StudentController(IBufferedFileUploadService bufferedFileUploadService)
    {
        _bufferedFileUploadService = bufferedFileUploadService;
        listStudents = new List<Student>()
        {
            new Student()
            {
                Id = 101, Name = "Hải Nam", Branch = Branch.IT,
                Gender = Gender.Male, IsRegular = true,
                Address = "A1-2018", Email = "nam@g.com"
            },
            new Student()
            {
                Id = 102, Name = "Minh Tú", Branch = Branch.BE,
                Gender = Gender.Female, IsRegular = true,
                Address = "A1-2019", Email = "tu@g.com"
            },
            new Student()
            {
                Id = 103, Name = "Hoàng phong", Branch = Branch.CE,
                Gender = Gender.Male, IsRegular = false,
                Address = "A1-2020", Email = "phong@g.com"
            },
            new Student()
            {
                Id = 104, Name = "Xuân Mai", Branch = Branch.CE,
                Gender = Gender.Female, IsRegular = false,
                Address = "A1-2021", Email = "mai@g.com"
            }
        };
    }


    [HttpGet]
    [Route("/Admin/Student/List", Name = "list")]
    public IActionResult Index()
    {
        return View(listStudents);
    }

    [HttpGet]
    [Route("Admin/Student/Add", Name = "add")]
    public IActionResult Create()
    {
//lấy danh sách các giá trị Gender để hiển thị radio button trên form
        ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
//lấy danh sách các giá trị Branch để hiển thị select-option trên form
//Để hiển thị select-option trên View cần dùng List<SelectListItem>
        ViewBag.AllBranches = new List<SelectListItem>()
        {
            new SelectListItem { Text = "IT", Value = "1" },
            new SelectListItem { Text = "BE", Value = "2" },
            new SelectListItem { Text = "CE", Value = "3" },
            new SelectListItem { Text = "EE", Value = "4" }
        };
        return View();
    }


    [HttpPost]
    [Route("Admin/Student/Add")]
    public async Task<ActionResult> Create(Student s, IFormFile file)
    {
        s.Id = listStudents.Last<Student>().Id + 1;
        listStudents.Add(s);
        try
        {
            if (await _bufferedFileUploadService.UploadFile(file))
            {
                ViewBag.Message = "File Upload Successful";
            }
            else
            {
                ViewBag.Message = "File Upload Failed";
            }
        }
        catch (Exception ex)
        {
            //Log ex
            ViewBag.Message = "File Upload Failed";
        }

        return View("Index", listStudents);
    }
}