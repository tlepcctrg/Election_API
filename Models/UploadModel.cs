using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace election_api.Models
{
    public class UploadModel
    {
        public string Base64StringImage { get; set; }
        public Guid CandidateID { get; set; }
    }
}
