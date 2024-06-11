using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Model
{
    public class Seat
    {
        public Guid Id { get; set; }
        public string? SeatNumber { get; set; }
        public Guid HallId { get; set; }

        public Hall Hall { get; set; }
        public ICollection<SeatReserved>? SeatsReserved { get; set; }
    }
}
