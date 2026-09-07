using System.Text.Json.Serialization;

namespace Diary.Models
{
    public class Emoji
    {
        public int IdEmoji { get; private set; }

        public string? NameEmoji { get; private set; }
        
        public string? CodeEmoji { get; private set; }

        public bool? IsPositive { get; private set; }

        [JsonIgnore]
        public ICollection<Post>? Posts { get; private set; }  

        private Emoji() { }

        public Emoji(string name, string code, bool isPositive) 
        {
            NameEmoji = name;
            CodeEmoji = code;
            IsPositive = isPositive;
        }
        
        public void Add(string name, string code, bool isPositive)
        {
            NameEmoji = name;
            CodeEmoji = code;
            IsPositive = isPositive;
            
        }

        public void Update(string? name, string? code, bool? isPositive)
        {
            if (name != null)
            {
                NameEmoji = name;
            }

            if (code != null)
            {
                CodeEmoji = code;
            }

            if (isPositive != null)
            {
                IsPositive = isPositive;
            }
        }
    }
}
