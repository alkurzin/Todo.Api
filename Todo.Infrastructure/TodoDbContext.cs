using Microsoft.EntityFrameworkCore;
using System;
using Todo.Domain.Task;

namespace Todo.Infrastructure
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                @"server=37.140.192.136;port=3306;database=u1680936_TodoDb;user=u1680936_Todo;password=4!Qq07uw2;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }
    }
}