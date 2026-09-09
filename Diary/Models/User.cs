using System.Text.Json.Serialization;

namespace Diary.Models
{
    public class User
    {
        public int IdUser { get; private set; }

        public string? UserName { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? BirthdayDate { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        [JsonIgnore]
        public ICollection<Post>? Posts { get;private set; }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;

        }

    }
}
