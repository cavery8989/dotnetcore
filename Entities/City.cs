using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cityInfo.Entities
{
    public class City 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        [Required]
        [MaxLengthAttribute(50)]
        public string Name {get; set;}
        [MaxLengthAttribute(200)]
        public string Description {get; set;}

        public ICollection<PointOfInterest> PointsOfInterest {get; set;}
            = new List<PointOfInterest>();
    }
}