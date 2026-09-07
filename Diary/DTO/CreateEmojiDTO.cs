using System.Text.Json.Serialization;

namespace Diary.DTO
{
    public class CreateEmojiDTO
    {

        public string? NameEmoji { get; set; }
        
        public string? CodeEmoji { get; set; }
        
        public bool IsPositive { get; set; }
    }
}
