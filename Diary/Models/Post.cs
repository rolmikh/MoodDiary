using System.Text.Json.Serialization;

namespace Diary.Models
{
    public class Post
    {
        
        public int IdPost { get; private set; }

        public string? PostText { get; private set; }

        public int EmojiId { get; private set; }

        public int? UserId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        [JsonIgnore]
        public Emoji? Emoji { get; private set; }

        [JsonIgnore]
        public User? User { get; private set; }

        private Post() { }

        public Post(string? postText, int emoji, int userId) 
        { 
            PostText = postText;
            EmojiId = emoji;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        
        }

        public void Update(string? postText, int emoji)
        {
            if(postText != null)
            {
                PostText = postText;
            }
            if (emoji != 0)
            { 
                EmojiId = emoji;
            }

        }


    }
}
