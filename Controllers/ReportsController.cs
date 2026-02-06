using Microsoft.AspNetCore.Mvc;
using LibraryProject.Business;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly LibraryManager _manager;
        public ReportsController(LibraryManager manager) => _manager = manager;

        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var report = _manager.GetLibraryStatusReport();
            return Ok(report);
        }

        [HttpGet("overdue")]
        public IActionResult GetOverdue()
        {
            var overdueList = _manager.GetOverdueLoans();
            var result = overdueList.Select(l => new
            {
                Kitap = l.Book.Title,
                Uye = l.Member.FirstName + " " + l.Member.LastName,
                SonTarih = l.DueDate,
                GecikmeGunu = (DateTime.Now - l.DueDate).Days
            });

            return Ok(result);
        }
    }
}