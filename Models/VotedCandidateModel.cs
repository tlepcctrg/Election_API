using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace election_api.Models
{
    public class VotedCandidateModel
    {
        public int CaNumber { get; set; }
        public int CaVoted { get; set; }
    }
}
