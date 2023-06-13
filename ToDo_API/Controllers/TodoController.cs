using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo_API.Models;
using ToDo_API.Services;

namespace ToDo_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        [Authorize(Roles = "USER,ADMIN")]
        public IActionResult GetItems()
        {
            var items = _todoService.GetAllItems();
            return Ok(items);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult AddItem(TodoItem item)
        {
            string username = User.Identity.Name;
            var newItem = _todoService.AddItem(item, username);

            if (newItem == null)
                return Unauthorized();

            return Ok(newItem);
        }


        [HttpGet]
        [Route("test")]
        //[Authorize()]
        public IActionResult Test()
        {
            return Ok("Ok");
        }

      




    }

}
