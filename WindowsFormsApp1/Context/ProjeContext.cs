using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Context
{
    public class ProjeContext : DbContext
    {
        public ProjeContext()
        {
            Database.Connection.ConnectionString = "Server= localhost; Database=EgitimTakip; Integrated Security=True;";
        }
        // uid=id;pwd=password;
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Take> Takes { get; set; }
        
    }
}
