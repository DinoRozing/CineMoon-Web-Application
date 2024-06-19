using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Model
{
    public class SeatReserved
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public Guid ProjectionId { get; set; }
        public Guid SeatId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public Ticket? Ticket { get; set; }
        public Projection? Projection { get; set; }
        public Seat? Seat { get; set; }
    }
}
