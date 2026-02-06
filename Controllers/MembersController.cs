using Microsoft.AspNetCore.Mvc;
using LibraryProject.Business;
using LibraryProject.Models;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly LibraryManager _manager;
        public MembersController(LibraryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetMembers()
        {
            var members = _manager.GetAllMembers();
            return Ok(members);
        }

        [HttpPost]
        public IActionResult AddMember(string name, string surname, string phone)
        {
            _manager.AddMember(name, surname, phone);
            return Ok(new { Message = "Üye kaydı Başarılı." });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            _manager.DeleteMember(id);
            return Ok(new { Message = $"İd'si {id} olan üye pasife alındı." });
        }
    }
}