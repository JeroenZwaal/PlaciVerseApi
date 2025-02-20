using System.ComponentModel.DataAnnotations;

namespace PlaciVerseApi.Classes
{
    public class UserLoginDto
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
