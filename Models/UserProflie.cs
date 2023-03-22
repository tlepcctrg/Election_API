using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace election_api.Models
{
    public partial class UserProflie
    {
        public UserProflie()
        {
            VotedResult = new HashSet<VotedResult>();
        }

        public Guid UprId { get; set; }
        public string UprUserId { get; set; }
        public string UprGender { get; set; }
        public int? UprAge { get; set; }

        public virtual ICollection<VotedResult> VotedResult { get; set; }
    }
}
