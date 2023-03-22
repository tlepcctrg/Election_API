using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace election_api.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            VotedResult = new HashSet<VotedResult>();
        }
        public Guid CaId { get; set; }
        public string CaName { get; set; }
        public int? CaNumer { get; set; }
        public byte[] CaImage { get; set; }
        public string CaMotto { get; set; }

        public virtual ICollection<VotedResult> VotedResult { get; set; }
    }
}
