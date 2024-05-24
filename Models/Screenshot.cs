namespace GameStore.Models
{
    public class Screenshot
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public Instrument Game{ get; set; }
        public string Image { get; set; }
    }
}
