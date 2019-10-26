using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameControllerData.Models
{
    //Model for Message table
    public class Message
	{
        [Key]
		public int MessageID { get; set; }

		public DateTime Time { get; set; }

		public string MessageContent { get; set; }
        [ForeignKey("Team")]
        public int TeamID { get; set; }
		public virtual Team Team { get; set; }
	}
}
