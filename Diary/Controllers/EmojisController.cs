using Diary.DTO;
using Diary.Models;
using Diary.Services.Implementations;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmojisController : ControllerBase
    {
        private readonly IEmojiService _emojiService;

        public EmojisController(IEmojiService emojiService)
        {
            _emojiService = emojiService;
        }


        [HttpGet]
        public async Task<List<Emoji>> GetEmoji()
        {
            return await _emojiService.SelectEmoji();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Emoji>> GetEmoji(int id)
        {
            var result = await _emojiService.GetEmoji(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmoji(int id, UpdateEmojiDTO updateEmoji)
        {
            var result = await _emojiService.PutEmoji(id, updateEmoji);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<Emoji>> PostNewEmoji(CreateEmojiDTO emoji)
        {
            await _emojiService.PostNewEmoji(emoji);

            return CreatedAtAction("GetEmoji", new { id = emoji.IdEmoji }, emoji);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmoji(int id)
        {
            var result = await _emojiService.DeleteEmoji(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
