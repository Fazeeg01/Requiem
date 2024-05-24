using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public class Instrument
    {
        public int ID { get; set; } 
        public string Name { get; set; }
        public DateTime ProductionDate { get; set; }    
        public string Developer { get; set; }
        public string Platforms { get; set; }
        public string Language { get; set; }    

        public int ModereatorID { get; set; }   
        public User Modereator { get; set; }
        public Double Raiting { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }

        public int GenreID { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Screenshot> Screenshots { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Raiting> Raitings{ get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Requirement> Requirements { get; set; }

    }
}
