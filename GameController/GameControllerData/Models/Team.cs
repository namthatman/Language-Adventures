using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameControllerData.Models
{
    //Model for Team table
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

    }
}
