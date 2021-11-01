using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamesAPI.DB;
using GamesAPI.DTOs;

namespace GamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly GamesContext _context;
        private readonly AutoMapper.IMapper _mapper;

        public MatchesController(GamesContext context, AutoMapper.IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _context.Database.EnsureCreated();
        }

        #region Matches Endpoints
        // GET: api/Matches2
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatches()
        {
            try
            {
                var dbItems = await _context.Match.Include(r => r.MatchOdds).ToListAsync();
                var retItems = _mapper.Map<List<MatchDto>>(dbItems);
                return Ok(retItems);
            }
            catch (Exception ed)
            {
                return Problem(ed.Message,ed.GetType().Name);
            }
        }

        // GET: api/Matches2/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<MatchDto>> GetMatch(Guid id)
        {
            try
            {

           
            var match = await _context.Match.Include(r => r.MatchOdds)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MatchDto>(match));
            }
            catch (Exception ed)
            {
                return Problem(ed.Message, ed.GetType().Name);
            }
        }

        // PUT: api/Matches2/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> PutMatch(Guid id, MatchDto matchdto)
        {
            if (id != matchdto.ID)
            {
                return BadRequest();
            }
            var match = new Match();
            _context.Entry(_mapper.Map(matchdto,match)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Matches2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult<MatchDto>> PostMatch([FromBody]MatchDto matchdto)
        {
            try
            {
                var match = _mapper.Map<Match>(matchdto);

                _context.Add(match);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetMatch", new { id = match.ID }, _mapper.Map<MatchDto>(match));
            }
            catch (Exception ed)
            {
                return Problem(ed.Message, ed.GetType().Name);
            }
        }

        // DELETE: api/Matches2/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteMatch(Guid id)
        {
            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Match.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchExists(Guid id)
        {
            return _context.Match.Any(e => e.ID == id);
        }
        #endregion Matches Endpoints

        #region Odds Endpoints

        // GET: api/MatchOdds1
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<MatchOddsDto>>> GetMatchOdds()
        {
            return _mapper.Map<List<MatchOddsDto>>(await _context.MatchOdds.ToListAsync());
        }

        // GET: api/MatchOdds1/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<MatchOddsDto>> GetMatchOdds(Guid id)
        {
            var matchOdds = _mapper.Map<MatchOddsDto>(await _context.MatchOdds.FindAsync(id));

            if (matchOdds == null)
            {
                return NotFound();
            }

            return matchOdds;
        }

        // PUT: api/MatchOdds1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> PutMatchOdds(Guid id, MatchOddsDto matchOdds)
        {
            if (id != matchOdds.ID)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<MatchOddsDto>(matchOdds)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchOddsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MatchOdds1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult<MatchOddsDto>> PostMatchOdds([FromBody]MatchOddsDto matchOddstdo)
        {
            MatchOdds matchOdds = _mapper.Map<MatchOdds>(matchOddstdo);
             _context.MatchOdds.Add(matchOdds);
            await _context.SaveChangesAsync();
            matchOddstdo = _mapper.Map<MatchOddsDto>(matchOdds);
            return CreatedAtAction("GetMatchOdds", new { id = matchOddstdo.ID }, matchOddstdo);
        }

        // DELETE: api/MatchOdds1/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteMatchOdds(Guid id)
        {
            var matchOdds = await _context.MatchOdds.FindAsync(id);
            if (matchOdds == null)
            {
                return NotFound();
            }

            _context.MatchOdds.Remove(matchOdds);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchOddsExists(Guid id)
        {
            return _context.MatchOdds.Any(e => e.ID == id);
        }
        #endregion Odds Endpoints
    }
}
