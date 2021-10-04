using Microsoft.EntityFrameworkCore;
using ProjectListData.Models.ModelConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectListData.Models
{
    public class ProjectListContext : DbContext
    {
        public ProjectListContext(DbContextOptions<ProjectListContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
            => builder.ApplyConfiguration(new ProjectListConfiguration());

        public DbSet<Project> Project { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<ProjectMembers> ProjectMembers { get; set; }
    }
}
