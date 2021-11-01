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
        }

        // GET: Matches

        [HttpGet("[action]")]
        public async Task<ActionResult<List<MatchDto>>> GetAll()
        {
            try
            {

            var dbItems = await _context.Match.Include(r => r.MatchOdds).ToListAsync();
            var retItems = _mapper.Map<List<MatchDto>>(dbItems);
            return Ok(retItems);
            }
            catch (Exception ed)
            {
            }
            return Problem("Unexpected Error.");
        }

        // GET: Matches/Details/5
        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult<MatchDto>> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match.Include(r=>r.MatchOdds)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (match == null)
            {
                return NotFound();
            }

            return _mapper.Map<MatchDto>(match);
        }

        // GET: Matches/Create
        [HttpGet("[action]")]
        public ActionResult<MatchDto> Create()
        {
            return Ok(new MatchDto());
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("[action]")]
        public async Task<ActionResult<MatchDto>> Create([Bind("ID,Description,MatchDate,MatchTime,TeamA,TeamB,Sport")] MatchDto matchdto)
        {
            try
            {

            
            var match = _mapper.Map<Match>(matchdto);
            if (ModelState.IsValid)
            {
                match.ID = Guid.NewGuid();
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(_mapper.Map<MatchDto>(match));
            }
            catch (Exception e)
            {
            }
            return Problem("Unexpected Error.");
        }

        // GET: Matches/Edit/5

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<MatchDto>> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<MatchDto>(match));
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("[action]")]
        public async Task<ActionResult<MatchDto>> Edit(Guid id, [Bind("ID,Description,MatchDate,MatchTime,TeamA,TeamB,Sport")] MatchDto matchdto)
        {
            if (id != matchdto.ID)
            {
                return NotFound();
            }
            var match = _mapper.Map<Match>(matchdto);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return Ok(_mapper.Map<MatchDto>(match));
        }

        // GET: Matches/Delete/5

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<MatchDto>> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .FirstOrDefaultAsync(m => m.ID == id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MatchDto>(match));
        }

        // POST: Matches/Delete/5
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var match = await _context.Match.Where(r=>r.ID==id).Include(r=>r.MatchOdds).FirstOrDefaultAsync();
            if (match != null)
            {
                match.MatchOdds.Select(r =>
                {
                    _context.MatchOdds.Remove(r);
                    return r;
                }).ToList();
                _context.Match.Remove(match);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Problem("not Found!!");
        }

        [HttpGet("[action]/{id}")]
        private bool MatchExists(Guid id)
        {
            return _context.Match.Any(e => e.ID == id);
        }
    }
}
