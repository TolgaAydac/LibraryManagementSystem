using Microsoft.AspNetCore.Mvc;
using LibraryProject.Application;
using LibraryProject.Domain;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly LibraryManager _manager;
    public CategoriesController(LibraryManager manager) => _manager = manager;

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _manager.GetAllCategories();
        return Ok(categories);
    }
}