using System.ComponentModel.DataAnnotations;

namespace cityInfo.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "You should provide a valid name")]
        [MaxLengthAttribute(50)]
        public string Name {get; set;}

        [MaxLengthAttribute(200)]
        public string Description {get; set;}

    }
}