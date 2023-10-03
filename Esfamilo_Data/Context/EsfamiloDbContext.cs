using Esfamilo_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Data.Context
{
    public class EsfamiloDbContext : DbContext
    {
        public EsfamiloDbContext(DbContextOptions<EsfamiloDbContext> options):base(options)
        {

        }

        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Friend> friends { get; set; }
        public DbSet<UserInLobby> userInLobbies { get; set; }
        public DbSet<WordForCategory> WordForCategories { get; set; }
        public DbSet<CategoryInLobby> categoryInLobbies { get; set; }
    }
}
