using System.Text.Json.Serialization;

namespace Diary.DTO
{
    public class CreatePostDTO
    {

        public string? PostText { get; set; }

        public int EmojiId { get; set; }

    }
}
