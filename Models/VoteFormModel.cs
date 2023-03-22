using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace election_api.Models
{
    public class VoteFormModel
    {
        public string userID { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public string candidateID { get; set; }
    }
}
