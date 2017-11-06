using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessTournamentsApi.Models
{
    public class TournamentPlayer
    {
        public long Id { get; set; }
        public long TournamentId { get; set; }
        public long PlayerId { get; set; }
        public double Points { get; set; }

        public TournamentPlayer(long TournamentId, long PlayerId, double Points)
        {
            this.TournamentId = TournamentId;
            this.PlayerId = PlayerId;
            this.Points = Points;
        }
        public TournamentPlayer() { }
    }
}
