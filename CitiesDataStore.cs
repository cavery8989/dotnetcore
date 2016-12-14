using System.Collections.Generic;
using cityInfo.Models;

namespace cityInfo
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current {get;} = new CitiesDataStore();
        public List<CityDto> Cities {get; set;}

        public CitiesDataStore ()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "London",
                    Description = "Capital Of England",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto(){
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visited park in the united states"
                        },
                        new PointOfInterestDto(){
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "Used to be the biggest building ever!"
                        }
                    }

                },
                new CityDto()
                {
                    Id = 2,
                    Name = "New York",
                    Description = "Where Friends Was Set",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto(){
                            Id = 3,
                            Name = "Old church",
                            Description = "Big church that was never finished"
                        },
                        new PointOfInterestDto(){
                            Id = 4,
                            Name = "The mystery Duck",
                            Description = "The Duck with the blacked out sunglasses"
                        }
                    }

                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Las Vegas",
                    Description = "Where Elvis Lives",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto(){
                            Id = 5,
                            Name = "Ceasers Palace",
                            Description = "Where lots of gangesters used to live"
                        },
                        new PointOfInterestDto(){
                            Id = 6,
                            Name = "Monkey Island",
                            Description = "The last resing place of Elvis"
                        }
                    }
                }
            };
        }

    }
}