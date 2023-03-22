using election_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace election_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteElectionController : ControllerBase
    {
        private ElectionContext _electionContext;

        public VoteElectionController(ElectionContext electionContext)
        {
            _electionContext = electionContext;
        }
        // GET: api/<VoteElectionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VoteElectionController>/5
        [HttpGet("GetCurrentVoted")]
        public async Task<IActionResult> GetCurrentVoted()
        {
            var result = await this.CurrentVotedAsync();
            return Ok(result);
        }
        // GET api/<VoteElectionController>/5
        [HttpGet("GetCandidate")]
        public IActionResult GetCandidate()
        {
            var candidate = _electionContext.Candidate.ToList();
            var result = candidate.Select(c => new CandidateResult { CandidateID = c.CaId, Name = c.CaName, Number = c.CaNumer, Image = (c.CaImage?.Length > 0?Convert.ToBase64String(c.CaImage):""), Motto = c.CaMotto });
            return Ok(result);
        }
        // POST api/<VoteElectionController>
        [HttpPost("votecandidate")]
        public async Task<IActionResult> VoteCandidate([FromBody] VoteFormModel value)
        {
            try
            {
                var userProfile = new UserProflie { UprUserId = value.userID, UprAge = value.age, UprGender = value.gender };
                var voteResult = new VotedResult { VreFkUprId = userProfile.UprId, VreFkCaId = new Guid(value.candidateID), VreFkUpr = userProfile };
                if (this.CheckUserVoted(userProfile))
                    return BadRequest(new { Message = "Duplicate vote" });

                _electionContext.UserProflie.Add(userProfile);
                _electionContext.VotedResult.Add(voteResult);
                await _electionContext.SaveChangesAsync();
                var result = await this.CurrentVotedAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
          
        }
        [HttpPost("UplodateImage")]
        public async Task<IActionResult> UplodateImage([FromBody] UploadModel value)
        {
            byte[] newBytes = Convert.FromBase64String(value.Base64StringImage);
            var candidate = _electionContext.Candidate.FirstOrDefault(c => c.CaId == value.CandidateID);
            candidate.CaImage = newBytes;
            await _electionContext.SaveChangesAsync();
            return Ok();


        }
        private bool CheckUserVoted(UserProflie user)
        {
            var isVoted = _electionContext.UserProflie.Any(c => c.UprUserId == user.UprUserId);
            return isVoted;

        }
        private async Task<VotedResponse> CurrentVotedAsync()
        {
            int total = _electionContext.VotedResult.Count();
            int male = _electionContext.VotedResult.Count(c => c.VreFkUpr.UprGender == "male");
            int female = _electionContext.VotedResult.Count(c => c.VreFkUpr.UprGender == "female");
            int other = _electionContext.VotedResult.Count(c => c.VreFkUpr.UprGender == "other");
            var votedCandidate = await _electionContext.Candidate.Select(c=>new VotedCandidateModel { CaNumber = c.CaNumer??0, CaVoted = 0}).ToListAsync();
            Dictionary<string, int> voteDict = _electionContext.VotedResult.Include(s => s.VreFkCa).ToList().GroupBy(c => c.VreFkCa.CaNumer, c => c).ToDictionary(c => (c.Key ?? 0).ToString(), c => c.Count());
            votedCandidate.ForEach(c =>
            {
                int count_voted = 0;
                if (voteDict.TryGetValue(c.CaNumber.ToString(), out count_voted))
                    c.CaVoted = count_voted;
            });
            return new VotedResponse { Total = total, VotedCandidate = votedCandidate.ToList(), Male = male, Female = female, Other = other };

        }
    }
}
