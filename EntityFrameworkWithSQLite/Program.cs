using System.Collections.Generic;
using System.Data.Entity;

namespace SQLiteSample
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public User()
        {
            Name = string.Empty;
        }
    }

    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Crea la base de datos y la tabla 'Users' si no existen
            using (UserContext db = new UserContext())
            {
                db.Database.CreateIfNotExists();
            }

            // Inserta algunos datos en la tabla
            using (UserContext db = new UserContext())
            {
                db.Users.Add(new User { Name = "Alice", Age = 20 });
                db.Users.Add(new User { Name = "Bob", Age = 25 });
                db.SaveChanges();
            }

            // Consulta los datos de la tabla
            using (UserContext db = new UserContext())
            {
                foreach (User user in db.Users)
                {
                    Console.WriteLine($"{user.Id}: {user.Name} ({user.Age})");
                }
            }
        }
    }
}
