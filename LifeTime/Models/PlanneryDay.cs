using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace LifeTime.Models
{
    [Table("TablePlanneryDays")]
    public class PlanneryDay : INotifyPropertyChanged
    {
        #region constructors
        public PlanneryDay()
        {
        }
        public PlanneryDay(DateTime time, string Header, string Description)
        {

            this.Time = time;
            this.Header = Header;
            this.Description = Description;
        }
        #endregion
        #region parametres
        string header = "Please input title ur task";
        string description = "Please input Description ur task";
        string hours;
        string minutes;
        string time = DateTime.Now.ToShortDateString();
        #endregion
        #region propertys
        public int Id { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public string Header{ get { return header;}  set { header = value; OnPropertyChanged("Header"); } }
        public string Description { get { return description; } set{ description = value; OnPropertyChanged("Description"); } }
        public User User { get; set; }
        public bool Done { get; set; }
        [NotMapped]
        public string TimeHours { get { return hours; } set { hours=value; OnPropertyChanged("TimeHours"); } }
        [NotMapped]
        public string TimeMinutes { get { return minutes; } set { minutes = value; OnPropertyChanged("TimeMinutes"); } }
        #endregion
        #region getInformation
        public static ObservableCollection<PlanneryDay> GetTasks()
        {
            using (Properties.PlanneryDayContext db = new Properties.PlanneryDayContext())
            {
                ObservableCollection<PlanneryDay> returnPlannery = new ObservableCollection<PlanneryDay>();
                var plannery = db.PlanneryDays;
                foreach(var p in plannery)
                {

                    if(p.Time.ToShortDateString() == DateTime.Now.ToShortDateString())
                    {
                        returnPlannery.Add(p);
                    }

                }
                return returnPlannery;
            }
        }

        #endregion
        #region eventDelegates
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        #endregion
    }
}
