using System.ComponentModel.DataAnnotations;

namespace WebUI.DTOs
{
    public class RegisterDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
