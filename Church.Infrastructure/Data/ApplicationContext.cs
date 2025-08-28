using Church.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Church.Infrastructure.Data
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : IdentityDbContext(options)
    {
        public DbSet<Event> Events { get; protected set; }
        public DbSet<Address> Addresses { get; protected set; }
        public DbSet<UserData> UsersData { get; protected set; }
    }
}
