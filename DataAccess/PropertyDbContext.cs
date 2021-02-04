using System.Data.Entity;
using PropertyWebApp.Models;

namespace PropertyWebApp.DataAccess
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext() : base("PropertyDatabase")
        {

        }

        //public DbSet<Property> Properties { get; set; }
    }
}
