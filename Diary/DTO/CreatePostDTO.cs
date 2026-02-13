using System.Text.Json.Serialization;

namespace Diary.DTO
{
    public class CreatePostDTO
    {
        [JsonIgnore]
        public int IdPost { get; set; }

        public string? PostText { get; set; }

        public int EmojiId { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

    }
}
