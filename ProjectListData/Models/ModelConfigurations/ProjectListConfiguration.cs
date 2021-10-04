using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectListData.Models.ModelConfigurations
{
    class ProjectListConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Name).HasMaxLength(255);
            builder.Property(prop => prop.Description).HasMaxLength(255);
        }

        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.First_name).HasMaxLength(255);
            builder.Property(prop => prop.Last_name).HasMaxLength(255);
            builder.Property(prop => prop.Email).HasMaxLength(255);
        }
        public void Configure(EntityTypeBuilder<ProjectMembers> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Project_id);
            builder.Property(prop => prop.Member_id);
        }
    }
}
