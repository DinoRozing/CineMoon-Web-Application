using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Model
{
    public class Hall
    {
        public Guid Id { get; set; }
        public string? HallNumber { get; set; }
        public ICollection<Seat>? Seats { get; set; }
        public ICollection<Projection>? Projections { get; set; }
    }
}
