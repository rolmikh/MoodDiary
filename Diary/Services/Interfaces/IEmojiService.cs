using Diary.DTO;
using Diary.Models;

namespace Diary.Services.Interfaces
{
    public interface IEmojiService
    {

        Task<List<EmojiDTO>> SelectEmoji();

        Task<EmojiDTO?> GetEmoji(int id);

        Task<Emoji> PostNewEmoji(CreateEmojiDTO emoji);

        Task<bool> PutEmoji(int id, UpdateEmojiDTO updateEmoji);

        Task<bool> DeleteEmoji(int id);


    }
}
