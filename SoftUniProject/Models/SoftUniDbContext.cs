using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SoftUniProject.Models
{
    public class SoftUniDbContext : IdentityDbContext<ApplicationUser>
    {
        public SoftUniDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Product> Products { get; set; }

        public static SoftUniDbContext Create()
        {
            return new SoftUniDbContext();
        }
    }
}