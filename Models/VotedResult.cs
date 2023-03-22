using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace election_api.Models
{
    public partial class VotedResult
    {
        public Guid VreId { get; set; }
        public Guid VreFkUprId { get; set; }
        public Guid VreFkCaId { get; set; }

        public virtual UserProflie VreFkUpr { get; set; }
        public virtual Candidate VreFkCa { get; set; }
    }
}
