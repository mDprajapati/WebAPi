using System.Data.Entity;

namespace WebAPI.Models
{
    public class CFDB : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Desigantion> Desigantions { get; set; }
    }
}