using System.Text.Json.Serialization;

namespace Diary.Models
{
    public class User
    {
        public int IdUser { get; private set; }

        public string? UserName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? BirthdayDate { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        [JsonIgnore]
        public ICollection<User>? Users { get;private set; }

    }
}
