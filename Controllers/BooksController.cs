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

    [HttpGet("search")]
    public IActionResult SearchBooks(string? title, string? author)
    {
        var results = _manager.GetAllBooks()
            .Where(b => (string.IsNullOrEmpty(title) || b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) &&
                        (string.IsNullOrEmpty(author) || b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)))
            .ToList();
        return Ok(results);
    }

}