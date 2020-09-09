﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Roommates.Models
{
    public class Roommate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RentPortion { get; set; }
        public DateTime MoveInDate { get; set; }
        public Room Room { get; set; }
    }
}
