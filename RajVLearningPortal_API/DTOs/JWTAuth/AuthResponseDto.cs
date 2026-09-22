namespace RajvLearning.API.DTOs.JWTAuth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }
    }
}
