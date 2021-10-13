using ECommercialDb.Models.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercialDb.Infrastructer.Context
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext()
        {
            Database.Connection.ConnectionString = @"Server=.;Database=ECommercialDb;Integrated Security=True;";
        }

        public DbSet<Category> Categories { get; set; }
    }

}
