using System.Text.Json.Serialization;

namespace Diary
{
    public class Post
    {
        public int IdPost { get; set; }
        public string? PostText { get; set; }
        public int EmojiId { get; set; }

        [JsonIgnore] 
        public Emoji? Emoji { get; set; }
    }
}
