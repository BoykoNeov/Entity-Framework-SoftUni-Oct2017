namespace Stations.Models
{
    using Stations.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CustomerCard
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        public CardType Type { get; set; } = CardType.Normal;
        public ICollection<Ticket> BoughtTickets { get; set; }

        public CustomerCard()
        {
            this.BoughtTickets = new HashSet<Ticket>();
        }
    }
}