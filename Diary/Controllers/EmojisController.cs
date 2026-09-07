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


        [HttpGet("select")]
        public async Task<List<EmojiDTO>> SelectEmojis()
        {
            return await _emojiService.SelectEmoji();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EmojiDTO?>> GetEmoji(int id)
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
        public async Task<ActionResult> PostNewEmoji(CreateEmojiDTO emoji)
        {
            await _emojiService.PostNewEmoji(emoji);

            return Ok();
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
