using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Diary;

namespace Diary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmojisController : ControllerBase
    {
        private readonly MoodDiaryDBContext _context;

        public EmojisController(MoodDiaryDBContext context)
        {
            _context = context;
        }

        // GET: api/Emojis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emoji>>> GetEmojis()
        {
            return await _context.Emojis.ToListAsync();
        }

        // GET: api/Emojis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emoji>> GetEmoji(int id)
        {
            var emoji = await _context.Emojis.FindAsync(id);

            if (emoji == null)
            {
                return NotFound();
            }

            return emoji;
        }

        // PUT: api/Emojis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmoji(int id, Emoji emoji)
        {
            if (id != emoji.IdEmoji)
            {
                return BadRequest();
            }

            _context.Entry(emoji).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmojiExists(id))
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

        // POST: api/Emojis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Emoji>> PostEmoji(Emoji emoji)
        {
            _context.Emojis.Add(emoji);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmoji", new { id = emoji.IdEmoji }, emoji);
        }

        // DELETE: api/Emojis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmoji(int id)
        {
            var emoji = await _context.Emojis.FindAsync(id);
            if (emoji == null)
            {
                return NotFound();
            }

            _context.Emojis.Remove(emoji);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmojiExists(int id)
        {
            return _context.Emojis.Any(e => e.IdEmoji == id);
        }
    }
}
