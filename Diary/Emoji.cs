using System.Text.Json.Serialization;

namespace Diary
{
    public class Emoji
    {
        public int IdEmoji { get; set; }
        public string? NameEmoji { get; set; }

        [JsonIgnore]
        public ICollection<Post>? Posts { get; set; }  
    }
}
