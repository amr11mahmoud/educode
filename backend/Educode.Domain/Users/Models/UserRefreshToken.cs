using System.ComponentModel.DataAnnotations.Schema;

namespace Educode.Domain.Users.Models
{
    public class UserRefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public DateTime ExpirationDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
