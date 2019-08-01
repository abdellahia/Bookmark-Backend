using System;
using launchpad_backend.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace launchpad_backend.Models
{
    public partial class LoggerContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public LoggerContext()
        {
        }

        public LoggerContext(DbContextOptions<LoggerContext> options)
            : base(options)
        {
        }

        // Unable to generate entity type for table 'dbo.BACKEND_LOG'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($"Server={InputVariableHelper.GetServernode()};Database={InputVariableHelper.LoggerGetDb()};User Id={InputVariableHelper.LoggerGetUid()}; Password={InputVariableHelper.LoggerGetPwd()};");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");            
        }
    }
}
