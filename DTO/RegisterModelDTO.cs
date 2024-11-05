using System.ComponentModel.DataAnnotations;

namespace To_Do_Api.DTO
{
    public class RegisterModelDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirm { get; set; }
    }
}
