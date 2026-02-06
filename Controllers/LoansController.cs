using Microsoft.AspNetCore.Mvc;
using LibraryProject.Business;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly LibraryManager _manager;

        public LoansController(LibraryManager manager)
        {
            _manager = manager;
        }

        [HttpPost("issue")]
        public IActionResult IssueBook(int bookId, int memberId)
        {
            try
            {
                _manager.IssueBook(bookId, memberId);
                return Ok("Kitap Başarıyla ödünç verildi.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("return")]
        public IActionResult ReturnBook(int bookId)
        {
            try
            {
                _manager.ReturnBook(bookId);
                return Ok("Kitap Başarıyla iade edildi.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("all")]
        public IActionResult GetAllLoans()
        {
            var loans = _manager.GetAllLoans();
            return Ok(loans);
        }
    }
}