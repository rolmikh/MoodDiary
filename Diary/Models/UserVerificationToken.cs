using System.Text.Json.Serialization;

namespace Diary.Models
{
    public class UserVerificationToken
    {
        public int IdUserVerificationToken { get; private set; }

        public int UserId { get; private set; }

        public DateTime ExpirationDate { get; private set; }

        public DateTime? UsedAt { get; private set; }

        public string HashData { get; private set; }

        public TypeOperationEnum OperationType { get; private set; }

        [JsonIgnore]
        public User? User { get; private set; }
        
        private UserVerificationToken() { }

        public UserVerificationToken(int UserId, string HashData, TypeOperationEnum OperationType)
        {
            this.UserId = UserId;
            ExpirationDate = DateTime.UtcNow + TimeSpan.FromHours(2);
            this.HashData = HashData;
            this.OperationType = OperationType;

        }

    }
}
