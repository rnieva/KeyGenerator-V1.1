using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using KeyGenerator.Data;

namespace KeyGenerator.Model
{
    public class DemoContext: DbContext
    {
        public DbSet<DaraReg> DataAll { get; set; } 
    }
}
