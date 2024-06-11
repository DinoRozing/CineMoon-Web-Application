using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Model
{
    public class Review
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }

        public User? User { get; set; }
        public Movie? Movie { get; set; }
    }
}