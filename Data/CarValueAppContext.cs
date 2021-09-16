using CarValueApi.Migrations;
using CarValueApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarValueApi.Data
{
    public class CarValueAppContext : DbContext
    {
        public CarValueAppContext(DbContextOptions<CarValueAppContext> opt) : base (opt)
        {
            
        }
        
        public DbSet<Command> carValueAPI { get; set; }

        public DbSet<signUpModel> signUpTable { get; set; }
        
        



    }
}
