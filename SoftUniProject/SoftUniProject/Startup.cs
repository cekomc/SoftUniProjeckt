using SoftUniProject.Migrations;
using SoftUniProject.Models;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(SoftUniProject.Startup))]
namespace SoftUniProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<SoftUniDbContext, Configuration>());
            ConfigureAuth(app);
        }
    }
}
