using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChessTournamentsApi.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessTournamentsApi.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        // GET: /<controller>/
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(long id)
        {
            var user = _context.Players.FirstOrDefault(t => t.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        [HttpGet(Name = "GetAll")]
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        [HttpPost("create/{usr}/{pass}")]
        public IActionResult Create(string usr, string pass)
        {
            int tempuser=0;
            tempuser = _context.Users.Where(t => t.UserName == usr).Count();
            
            if (tempuser != 0)
            {
                return BadRequest("User exists");
            }

            User user = new User(usr, pass, "user");

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtRoute("GetUser", new { Id = user.Id }, user);
        }
    }
}
