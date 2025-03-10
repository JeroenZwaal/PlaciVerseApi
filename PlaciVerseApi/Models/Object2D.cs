using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlaciVerseApi.Models
{
    public class Object2D
    {
        [Key]
        public int ObjectId { get; set; }

        [Required, ForeignKey("EnvironmentId")]
        public int EnvironmentId { get; set; }

        [Required]
        public int PrefabId { get; set; }

        [Required]
        public double positionX { get; set; }

        [Required]
        public double positionY { get; set; }

        [Required]
        public double ScaleX { get; set; }

        [Required]
        public double ScaleY { get; set; }

        [Required]
        public double RotationZ { get; set; }

        [Required]
        public int SortingLayer { get; set; }

    }
}
