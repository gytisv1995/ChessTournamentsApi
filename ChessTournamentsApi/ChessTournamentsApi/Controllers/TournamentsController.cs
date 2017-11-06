using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ChessTournamentsApi.Models;
using System.Linq;
using System.IO;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace ChessTournamentsApi.Controllers
{
    [Route("api/[controller]")]
    public class TournamentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TournamentsController(ApplicationDbContext context)
        {
            _context = context;

            string file = @"..\ChessTournamentsApi\Tournaments.txt";

            string file2 = @"..\ChessTournamentsApi\TournamentPlayers.txt";



            if (_context.Tournaments.Count() == 0)

            {
                Read(file);
                _context.SaveChanges();
            }

            if (_context.TournamentPlayers.Count() == 0)

            {
                Read2(file2);
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
                    string[] values = line.Split(',');

                    Tournament tournament = new Tournament(values[0], bool.Parse(values[1]), values[2]);
                    _context.Tournaments.Add(tournament);
                    _context.SaveChanges();
                }
            }
        }

        public void Read2(string file)
        {
            string line = null;

            using (StreamReader reader = new StreamReader(@file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(' ');

                    TournamentPlayer tournamentPlayer = new TournamentPlayer(long.Parse(values[0]), long.Parse(values[1]),double.Parse(values[2]));
                    _context.TournamentPlayers.Add(tournamentPlayer);
                    _context.SaveChanges();
                }
            }
        }

        [HttpGet]
        public IEnumerable<Tournament> GetAll()
        {
            return _context.Tournaments.ToList();
        }

        //[HttpGet("{country}", Name = "Get By Country")]
        //public IEnumerable<Tournament> GetByCountry(string country)
        //{
        //    return _context.Tournaments.ToList().Where(x=>x.Country.ToLower().Equals(country.ToLower()));
        //}

        [HttpGet("{id}", Name = "GetTournament")]
        public IActionResult GetById(long id)
        {
            var item = _context.Tournaments.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("{id}/players", Name = "Get Tournament Players")]
        public IEnumerable GetTournamentPlayers(long id)
        {

            var innerJoinQuery =
            from tournaments in _context.Tournaments
            join tplayers in _context.TournamentPlayers on tournaments.Id equals tplayers.TournamentId
            join players in _context.Players on tplayers.PlayerId equals players.Id
            where tournaments.Id == id
            select new {Name = players.Name, Category =players.Category, Points = tplayers.Points, Rating = players.Rating  };
            return innerJoinQuery.ToList();
        }

        [HttpGet("{tid}/players/{pid}", Name = "Tournament Player")]
        public IActionResult GetTournamentPlayerById(long tid, long pid)
        {
            var item =
           from tournaments in _context.Tournaments
           join tplayers in _context.TournamentPlayers on tournaments.Id equals tplayers.TournamentId
           join players in _context.Players on tplayers.PlayerId equals players.Id
           where tournaments.Id == tid && players.Id == pid
           orderby tplayers.Points descending
           select new { Name = players.Name, Points = tplayers.Points, Rating = players.Rating, Category = players.Category };

            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
            //return new ObjectResult(item);
        }

        [HttpGet("{id}/players/category/{categ}", Name = "Tournament Players Category")]
        public IEnumerable GetTournamentPlayersByCategory(int id,string categ)
        {
            var innerJoinQuery =
           from tournaments in _context.Tournaments
           join tplayers in _context.TournamentPlayers on tournaments.Id equals tplayers.TournamentId
           join players in _context.Players on tplayers.PlayerId equals players.Id
           where tournaments.Id == id && players.Category == categ
           orderby tplayers.Points descending
           select new {Name = players.Name, Points = tplayers.Points, Rating = players.Rating, Category = players.Category };
            return innerJoinQuery.ToList();
        }




        [Authorize]
        [HttpGet("current", Name = "Current")]
        public IEnumerable<Tournament> GetCurrent()
        {
            return _context.Tournaments.ToList().Where(x => x.hasEnded == false);
        }
        [Authorize]
        [HttpGet("previous", Name = "Previous")]
        public IEnumerable<Tournament> GetPrevious()
        {
            return _context.Tournaments.ToList().Where(x => x.hasEnded == true);
        }
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] Tournament item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Tournaments.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("Get Tournament", new { id = item.Id }, item);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Tournament item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var tournament = _context.Tournaments.FirstOrDefault(t => t.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }

            tournament.hasEnded = item.hasEnded;
            tournament.Name = item.Name;
            _context.SaveChanges();
            return new NoContentResult();
        }


    }
}