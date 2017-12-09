namespace Stations.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class SeatingClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //[MinLength(2)]
        //[MaxLength(2)]
        [StringLength(2, MinimumLength = 2)]
        public string Abbreviation { get; set; }

        // public ICollection<TrainSeat> TrainSeats { get; set; } = new HashSet<TrainSeat>()
    }
}   