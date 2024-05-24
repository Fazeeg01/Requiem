using GameStore.Models;
using System.Linq;

namespace GameStore.Data
{
    public class DBInitializer
    {
        public static void Initialize(GameStoreContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }


            //Ролі
            var roles = new Role[]
            {
                new Role{ Name = "Модератор"},
                new Role{ Name = "Користувач"}
            };
            
            foreach (Role r in roles)
            {
                context.Roles.Add(r);
            }
            context.SaveChanges();


            //Ролі
            var genres = new Genre[]
            {
                new Genre{ Name = "Електромеханічні"},
                new Genre{ Name = "Електронні"},
                new Genre{ Name = "Клавішні"},
                new Genre{ Name = "Духові"},
                new Genre{ Name = "Самозвучні ударно-клавішні"}
            };

            foreach (Genre g in genres)
            {
                context.Genres.Add(g);
            }
            context.SaveChanges();


           

        }
    }
}
