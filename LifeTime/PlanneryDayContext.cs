using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LifeTime.Models;

namespace LifeTime.Properties
{
    class PlanneryDayContext:DbContext
        
    {
        public PlanneryDayContext():base("DbConnection")
        { }
        public DbSet<PlanneryDay> PlanneryDays { get; set; }
    }
}
