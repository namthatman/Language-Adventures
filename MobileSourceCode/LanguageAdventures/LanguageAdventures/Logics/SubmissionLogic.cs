using LanguageAdventures.Models;
using LanguageAdventures.URLs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LanguageAdventures.Logics
{
    // handles logic for sending http request to web server via api uri
    // requests for Submission stuff
    class SubmissionLogic
    {
        // expects web server to receive and save the submission for the required challenge
        public async static void PostSubmission(object submission) // TODO: implement the method
        {

        }

        // expects web server to receive and update the submission for the required challenge
        public async static void PutSubmission(object submission) // TODO: implement the method
        {

        }
    }
}
