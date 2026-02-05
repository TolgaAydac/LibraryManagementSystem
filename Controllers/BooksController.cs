using Microsoft.AspNetCore.Mvc;
using LibraryProject.Business;
using LibraryProject.Models;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly LibraryManager _manager;
    public BooksController(LibraryManager manager)
    {
        _manager = manager;
    }
    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var books = _manager.GetAllBooks();
        return Ok(books);
    }
}