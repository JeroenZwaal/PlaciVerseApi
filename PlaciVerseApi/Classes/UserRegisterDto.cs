using System.ComponentModel.DataAnnotations;

namespace PlaciVerseApi.Classes
{
    public class UserRegisterDto
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Het wachtwoord moet minimaal 10 karakters lang zijn.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Wachtwoord moet minstens 1 kleine letter, 1 hoofdletter, 1 cijfer en 1 speciaal teken bevatten.")]
        public string Password { get; set; }
    }
}
