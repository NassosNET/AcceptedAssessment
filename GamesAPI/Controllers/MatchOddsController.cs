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
    public class MatchOddsController : ControllerBase
    {
        private readonly GamesContext _context;
        private readonly AutoMapper.IMapper _mapper;


        public MatchOddsController(GamesContext context, AutoMapper.IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: MatchOdds

        [HttpGet("[action]")]
        public async Task<ActionResult<List<MatchOddsDto>>> GetAll()
        {
            var gamesContext = _context.MatchOdds.Include(m => m.Match);
            return Ok(_mapper.Map<List<MatchOddsDto>>(await gamesContext.ToListAsync()));
        }

        // GET: MatchOdds/Details/5
        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult<MatchOddsDto>> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchOdds = await _context.MatchOdds
                .Include(m => m.Match)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (matchOdds == null)
            {
                return NotFound();
            }

            return _mapper.Map<MatchOddsDto>(matchOdds);
        }

        // GET: MatchOdds/Create
        [HttpGet("[action]")]
        public ActionResult<MatchOddsDto> Create()
        {
            return Ok(new MatchOddsDto());
        }

        // POST: MatchOdds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("[action]")]
        public async Task<ActionResult<MatchOddsDto>> Create([Bind("ID,MatchId,Specifier,Odd")] MatchOddsDto matchOdds)
        {
            try
            {


                var match = _mapper.Map<MatchOdds>(matchOdds);
                if (ModelState.IsValid)
                {
                    match.ID = Guid.NewGuid();
                    _context.Add(match);
                    await _context.SaveChangesAsync();
                }
                return Ok(_mapper.Map<MatchDto>(match));
            }
            catch (Exception e)
            {
            }
            return Problem("Unexpected Error.");
        }

        // GET: MatchOdds/Edit/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<MatchOddsDto>> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchOdds = await _context.MatchOdds.FindAsync(id);
            if (matchOdds == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<MatchOddsDto>(matchOdds));
        }

        // POST: MatchOdds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("[action]")]
        public async Task<ActionResult<MatchOddsDto>> Edit(Guid id, [Bind("ID,MatchId,Specifier,Odd")] MatchOddsDto matchOddsDto)
        {
            if (id != matchOddsDto.ID)
            {
                return NotFound();
            }
            var matchOdds = _mapper.Map<MatchOddsDto>(matchOddsDto);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchOdds);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchOddsExists(matchOdds.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok(_mapper.Map<MatchOddsDto>(matchOdds));
        }

        // GET: MatchOdds/Delete/5

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<MatchOddsDto>> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchOdds = await _context.MatchOdds
                .Include(m => m.Match)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (matchOdds == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MatchOddsDto>(matchOdds));
        }

        // POST: MatchOdds/Delete/5
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var matchOdds = await _context.MatchOdds.FindAsync(id);
            _context.MatchOdds.Remove(matchOdds);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("[action]/{id}")]
        private bool MatchOddsExists(Guid id)
        {
            return _context.MatchOdds.Any(e => e.ID == id);
        }
    }
}
