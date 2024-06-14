﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReviewModel
{
    public class PutReviewRest
    {
        public string Description { get; set; }
        public int Rating { get; set; }
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid UpdatedByUserId { get; set; }
    }
}
