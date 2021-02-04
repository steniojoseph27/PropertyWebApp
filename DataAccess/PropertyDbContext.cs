using System.Data.Entity;
using PropertyWebApp.Models;

namespace PropertyWebApp.DataAccess
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext() : base("PropertyDatabase")
        {

        }
    }
}
