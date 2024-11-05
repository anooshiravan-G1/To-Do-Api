namespace To_Do_Api.DTO
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public string Email { get; set; } 
        public DateTime ExpiresAt { get; set; }
    }
}
