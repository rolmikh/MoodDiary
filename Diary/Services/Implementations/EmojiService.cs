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

        public async Task<List<EmojiDTO>> SelectEmoji()
        {
            var query = from emoji in _context.Emoji
                        select new EmojiDTO
                        {
                            NameEmoji = emoji.NameEmoji,
                            CodeEmoji = emoji.CodeEmoji,
                            IsPositive = emoji.IsPositive,
                        };

            var result = await query.ToListAsync();
            return result;
        }


        public async Task<EmojiDTO?> GetEmoji(int id)
        {

            var result = await _context.Emoji
                .Where(emoji => emoji.IdEmoji == id)
                .Select(emoji => new EmojiDTO{
                    NameEmoji = emoji.NameEmoji,
                    CodeEmoji = emoji.CodeEmoji,
                    IsPositive = emoji.IsPositive,
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Emoji> PostNewEmoji(CreateEmojiDTO emoji)
        {
            Emoji newEmoji = new Emoji(emoji.NameEmoji, emoji.CodeEmoji, emoji.IsPositive);
            _context.Emoji.Add(newEmoji);
            await _context.SaveChangesAsync();

            return newEmoji;
        }


        public async Task<bool> PutEmoji(int id, UpdateEmojiDTO updateEmoji)
        {

            var emoji = await _context.Emoji.FindAsync(id);

            if (emoji == null) { return false; }

            emoji.Update(updateEmoji.NameEmoji, updateEmoji.CodeEmoji, updateEmoji.IsPositive);

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
