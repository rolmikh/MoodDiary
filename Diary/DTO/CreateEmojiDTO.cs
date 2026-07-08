using System.Text.Json.Serialization;

namespace Diary.DTO
{
    public class CreateEmojiDTO
    {

        [JsonIgnore]
        public int IdEmoji { get; set; }

        public string? NameEmoji { get; set; }
        
        public string? CodeEmoji { get; set; }
        
        public bool? IsPositive { get; set; }
    }
}
