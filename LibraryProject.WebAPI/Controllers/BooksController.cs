using Microsoft.AspNetCore.Mvc;
using LibraryProject.Application;
using LibraryProject.Domain;

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
    public IActionResult GetAllBooks([FromQuery] PaginationParameters @params)
    {
        var books = _manager.GetAllBooksPaged(@params);
        return Ok(books);
    }

    [HttpGet("search")]
    public IActionResult SearchBooks(string? title, string? author, [FromQuery] PaginationParameters @params)
    {
        var results = _manager.SearchBooksPaged(title, author, @params);
        return Ok(results);
    }

}