using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageAdventures.Models
{
    // represents the object of a challenge
    // stores the data retrieve from the web server
    public class Challenge
    {
        public int ChallengeID { get; set; } // id of this challenge
        public string ChallengeType { get; set; } // type of this challenge, can be MCQ/Text/Photo/Audio
        public string ChallengeDetail { get; set; } // content of this challenge
        public string CorrectAnswer { get; set; } // correct answer of this challenge

        public override string ToString()
        {
            return this.ChallengeDetail;
        }
    }
}
