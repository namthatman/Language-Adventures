using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameControllerData.Models
{
    //Model for Challenge table
    public class Challenge
	{
        [Key]
		public int ChallengeID { get; set; }

		public string ChallengeType { get; set; }
        public string ChallengeDetail { get; set; }
		//The type for correct answer is subject to change
		//Assume the answers are texts here
		public string CorrectAnswer { get; set; }

	}
}
