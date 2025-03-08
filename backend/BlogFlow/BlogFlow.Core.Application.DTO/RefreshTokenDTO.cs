using Microsoft.Win32.SafeHandles;

namespace BlogFlow.Core.Application.DTO
{
    public class RefreshTokenDTO
    {
        public int Id { get; set; }
        public string AccessToken { get; set; } 
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? UserId { get; set; }
    }
}
