using System.Collections.Generic;

namespace GameStore.Models
{
    public class User
    {
        public int ID { get; set; } 
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public string FullName { get => FirstName + " " + LastName; }
        public ICollection<Instrument> Games { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Raiting> Raitings { get; set; }
    }
}
