using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WpfCore.Models;

namespace WpfCore.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Student> Students { get; set; }

        //Databse Configure:
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection String:
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=StudentWpf;Integrated Security=True;Pooling=False");
        }
    }
}
