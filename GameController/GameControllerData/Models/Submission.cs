using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameControllerData.Models
{
    //Model for Submission table
    public class Submission
	{
        [Key]
		public int SubmissionID { get; set; }

		public string Text { get; set; }

		public string Image { get; set; }
        [ForeignKey("Team")]
        public int TeamID { get; set; }
        [ForeignKey("Challenge")]
        public int ChallengeID { get; set; }
		public virtual Team Team { get; set; }
		public virtual Challenge Challenge { get; set; }
	}
}
