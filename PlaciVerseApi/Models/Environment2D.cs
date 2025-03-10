using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaciVerseApi.Models
{
    public class Environment2D
    {
        [Key]
        public int EnvironmentId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int MaxLenght { get; set; }

        [Required]
        public int MaxHeight { get; set; }

    }
}
