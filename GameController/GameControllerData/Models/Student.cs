using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameControllerData.Models
{
    //Model for Student table
    public class Student
	{
        [PrimaryKey]
		public int StudentID { get; set; }
        [ForeignKey("Team")]
        public int TeamID { get; set; }
		public virtual Team Team { get; set; }
	}
}
