using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Model
{
    public class ProjectionHall
    {
        public Guid Id { get; set; }
        public Guid ProjectionId { get; set; }
        public Guid HallId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }

        public Projection? Projection { get; set; }
        public Hall Hall { get; set; }
    }
}
