
namespace JobOpenings.Models
{
    public class Location
    {
        public int Id { get; set; }                    // Unique identifier for the location
        public string Title { get; set; }              // Location title
        public string City { get; set; }               // City
        public string State { get; set; }              // State
        public string Country { get; set; }            // Country
        public int Zip { get; set; }                   // Zip code
    }

}