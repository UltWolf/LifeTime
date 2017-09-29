using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LifeTime
{
   [Table ("TablePlanneryDays")]
    class PlanneryDay
    {
        public PlanneryDay()
        {
        }
        public PlanneryDay(DateTime time, string Header, string Description)
        {

            this.Time = time;
            this.Header = Header;
            this.Description = Description;
        }
        
        public  int Id { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public  string Header { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}
