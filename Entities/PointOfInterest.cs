
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cityInfo.Entities
{
    public class PointOfInterest
    {
        public int Id {get; set;}
        [Required]
        [MaxLengthAttribute(50)]
        public string Name {get;set;}
        [ForeignKey("CityId")]
        public City city {get; set;}
        public int CityId{get; set;}
        public string Description {get; set;}
    }
}