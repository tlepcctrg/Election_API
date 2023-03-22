using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace election_api.Models
{
    public class VotedResponse
    {
        public int Total { get; set; }
        public IEnumerable<VotedCandidateModel> VotedCandidate { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Other { get; set; }
    }
}
