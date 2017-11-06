using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.IO;
using System;
using ChessTournamentsApi.Models;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace ChessTournamentsApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        
        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
            // string file = System.IO.Path.GetFullPath("Players.txt");
            string file = @"..\ChessTournamentsApi\Players.txt";




            if (_context.Players.Count() == 0)

            {
                Read(file);
                Console.WriteLine("context was empty");
                _context.SaveChanges();
            }
        }

        public void Read(string file)
        {
            string line = null;

            using (StreamReader reader = new StreamReader(@file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(' ');

                    Player player = new Player(values[0], int.Parse(values[1]), values[2]);
                    _context.Players.Add(player);
                    _context.SaveChanges();
                }
            }
        }

        [HttpGet]
        public IEnumerable<Player> GetAll()
        {


            return _context.Players.ToList();
        }
        [Authorize]
        [HttpGet("top{nr}", Name = "Top")]
        public IEnumerable<Player> GetTop(int nr)
        {
            return _context.Players.OrderByDescending(x => x.Rating).Take(nr).ToList();
        }

        [Authorize]
        [HttpGet("best", Name = "Best")]
        public IEnumerable<Player> GetBest()
        {
            List<Player> best = new List<Player>();
            List<Player> temp = new List<Player>();
            List<string> categories = _context.Players.OrderByDescending(x => x.Category).Select(x => x.Category).Distinct().ToList();

            foreach (string cat in categories)
            {
                temp = _context.Players.Where(x => x.Category.Equals(cat)).OrderByDescending(x => x.Rating).Take(3).ToList();
                best.AddRange(temp);

            }
            return best;


        }

        [HttpGet("s60", Name = "S60 Players")]
        public IEnumerable<Player> GetS60()
        {
            return _context.Players.Where(x => x.Category.Equals("S60")).OrderByDescending(x => x.Rating).ToList();
        }


        [HttpGet("s50", Name = "S50 Players")]
        public IEnumerable<Player> GetS50()
        {
            return _context.Players.Where(x => x.Category.Equals("S50")).OrderByDescending(x => x.Rating).ToList();
        }

        [HttpGet("u20", Name = "U20 Players")]
        public IEnumerable<Player> GetU20()
        {
            return _context.Players.Where(x => x.Category.Equals("U20")).OrderByDescending(x => x.Rating).ToList();
        }

        [HttpGet("u18", Name = "U18 Players")]
        public IEnumerable<Player> GetU18()
        {
            return _context.Players.Where(x => x.Category.Equals("U18")).OrderByDescending(x => x.Rating).ToList();
        }

        [HttpGet("u16", Name = "U16 Players")]
        public IEnumerable<Player> GetU16()
        {
            return _context.Players.Where(x => x.Category.Equals("U16")).OrderByDescending(x => x.Rating).ToList();
        }

        [HttpGet("u14", Name = "U14 Players")]
        public IEnumerable<Player> GetU14()
        {
            return _context.Players.Where(x => x.Category.Equals("U14")).OrderByDescending(x => x.Rating).ToList();
        }

        [HttpGet("women", Name = "Women Players")]
        public IEnumerable<Player> GetWomen()
        {
            return _context.Players.Where(x => x.Category.Equals("W")).OrderByDescending(x => x.Rating).ToList();
        }


        [HttpGet("{id}", Name = "GetPlayer")]
        public IActionResult GetById(long id)
        {
            var item = _context.Players.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] Player item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Players.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetPlayer", new { id = item.Id }, item);
        }
        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Player item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var player = _context.Players.FirstOrDefault(t => t.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            player.Rating = item.Rating;
            player.Name = item.Name;

            _context.SaveChanges();
            return new NoContentResult();
        }
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var player = _context.Players.FirstOrDefault(t => t.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}