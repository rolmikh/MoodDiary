using System.Text.Json.Serialization;

namespace Diary.Models
{
    public class Post
    {
        
        public int IdPost { get; private set; }

        public string? PostText { get; set; }

        public int EmojiId { get; set; }

        public int? UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public Emoji? Emoji { get; private set; }

        [JsonIgnore]
        public User? User { get; private set; }
    }
}
