﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Event> Events { get; set; }
    }
}
