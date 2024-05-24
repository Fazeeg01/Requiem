namespace GameStore.Models
{
    public class Requirement
    {
        public int ID { get; set; } 
        public string OS { get; set; }
        public string CPU { get; set; }
        public string GPU { get; set; }
        public string RAM { get; set; }
        public string ROM { get; set; }
        public int GameID { get; set; }
        public Instrument Game{ get; set; }

    }
}
