using DP.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DP.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Complainer> Complainers { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<ComplaintAssignment> ComplaintAssignments { get; set; }

        public DbSet<PolicemenDetail> PolicemenDetails  { get; set; }

        public DbSet<Department> Departments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
