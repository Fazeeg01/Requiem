using System;

namespace GameStore.Models
{
    public class Raiting
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public Instrument Game { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public Double Value { get; set; }
    }
}
