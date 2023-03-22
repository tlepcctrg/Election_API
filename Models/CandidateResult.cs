using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace election_api.Models
{
    public class CandidateResult
    {
        public Guid CandidateID { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public string Motto { get; set; }
        public string Image { get; set; }
  
    }
}
