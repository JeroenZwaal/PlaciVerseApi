using System.ComponentModel.DataAnnotations;

namespace PlaciVerseApi.Classes
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required]   
        public string Password { get; set; }

        //public ICollection<Environment2D> Environments { get; set; }
    }
}
