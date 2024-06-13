using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Model
{
    public class Projection
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public Movie? Movie { get; set; }
        public Hall? Hall { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
        public ICollection<SeatReserved>? SeatsReserved { get; set; }
        public List<ProjectionHall> ProjectionHalls { get; set; }
    }
}
