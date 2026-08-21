namespace RajvLearning.API.DTOs.JWTAuth
{
    public class TokenResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }
    }
}
