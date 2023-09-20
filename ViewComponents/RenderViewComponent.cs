using Lab_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Lab_02.ViewComponents;

public class RenderViewComponent : ViewComponent
{
    private List<MenuItem> menuItems = new List<MenuItem>();

    public RenderViewComponent() 
    {
        menuItems.Add(new MenuItem() { Id = 1, Name = "Branches", Link = "Branches/List" });
        menuItems.Add(new MenuItem() { Id = 2, Name = "Students", Link = "Students/List" });
        menuItems.Add(new MenuItem() { Id = 3, Name = "Subjects", Link = "Subjects/List" });
        menuItems.Add(new MenuItem() { Id = 4, Name = "Courses", Link = "Courses/List" });
    }
    public async Task<ViewViewComponentResult> InvokeAsync()
    {
        return View("RenderLeftMenu", menuItems);
    }
}