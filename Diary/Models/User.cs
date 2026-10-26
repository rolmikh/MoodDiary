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

        public bool EmailConfirmed { get; private set; } = false;

        [JsonIgnore]
        public ICollection<Post>? Posts { get;private set; }

        [JsonIgnore]
        public ICollection<UserVerificationToken>? UserVerificationTokens { get; private set; }

        public User(string email)
        {
            Email = email;
            CreatedAt = DateTime.UtcNow;

        }

        public void SetPasswordHash(string passwordHash)
        {
            Password = passwordHash;
        }

        public void SetEmailConfirmed()
        {
            EmailConfirmed = true;
        }

    }
}
