using Diary.DTO;
using Diary.Models;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diary.Services.Implementations
{
    public class EmojiService: IEmojiService
    {
        private readonly MoodDiaryDBContext _context;

        public EmojiService(MoodDiaryDBContext context)
        {
            _context = context;
        }

        public async Task<List<Emoji>> SelectEmoji()
        {
            var result = await _context.Emoji.ToListAsync();
            return result;
        }


        public async Task<Emoji> GetEmoji(int id)
        {
            var emoji = await _context.Emoji.FindAsync(id);

            if (emoji == null)
            {
                return null;
            }

            return emoji;
        }

        public async Task<Emoji> PostNewEmoji(CreateEmojiDTO emoji)
        {
            Emoji newEmoji = new Emoji
            {
                NameEmoji = emoji.NameEmoji,
                CodeEmoji = emoji.CodeEmoji,
                IsPositive = emoji.IsPositive,
            };
            _context.Emoji.Add(newEmoji);
            await _context.SaveChangesAsync();

            return newEmoji;
        }


        public async Task<bool> PutEmoji(int id, UpdateEmojiDTO updateEmoji)
        {

            var emoji = await _context.Emoji.FindAsync(id);

            if (emoji == null) { return false; }

            if (updateEmoji.NameEmoji != null)
            {
                emoji.NameEmoji = updateEmoji.NameEmoji;
            }

            if (updateEmoji.CodeEmoji != null)
            {
                emoji.CodeEmoji = updateEmoji.CodeEmoji;
            }

            if (updateEmoji.IsPositive != null)
            {
                emoji.IsPositive = updateEmoji.IsPositive;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmojiExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }


        public async Task<bool> DeleteEmoji(int id)
        {
            var emoji = await _context.Emoji.FindAsync(id);
            if (emoji == null)
            {
                return false;
            }

            _context.Emoji.Remove(emoji);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool EmojiExists(int id)
        {
            return _context.Emoji.Any(e => e.IdEmoji == id);
        }
    }
}
