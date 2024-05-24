using System.Collections.Generic;

namespace GameStore.Models
{
    public class Genre
    {
        public int ID { get; set; } 
        public string Name { get; set; }
        ICollection<Instrument> Games { get; set; }
    }
}
