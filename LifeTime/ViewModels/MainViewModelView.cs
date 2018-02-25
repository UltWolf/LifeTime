
using LifeTime.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LifeTime.ViewModels
{
    class MainViewModelView:PlanneryDay,INotifyPropertyChanged
    {
        #region properties and parametres
        PlanneryDay[] tasks;
        public ObservableCollection<PlanneryDay> Tasks { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public PlanneryDay Task
        {
            get => task;
            set
            {
                task = value;
                OnPropertyChanged("task");
            }
        }
        PlanneryDay task;
        
        public MainViewModelView()
        {
            Refresh();
            AddCommand = new RelayCommand(AddTask, CanAddTask);
            RemoveCommand = new RelayCommand(RemoveTask, CanRemoveTask);
        }
        #endregion
        #region AddingTask
        public void AddTask(Object obj)
        {


            DateTime dateTime = new DateTime();
                double shour = 0;
                double sminutes = 0;
                shour = double.Parse(TimeHours);
                sminutes = double.Parse(TimeMinutes);
                    dateTime = Time;
                    dateTime = dateTime.AddHours(shour);
                    dateTime = dateTime.AddMinutes(sminutes);
                    PlanneryDay PD = new PlanneryDay() { Header = Header, Description = Description };
                    PD.Time = dateTime;
                    Task task = new Task(() => Add_Plan(PD));
                    task.Start();
                
            }
        
    private void Add_Plan(PlanneryDay PD)
    {
        using (Properties.PlanneryDayContext db = new Properties.PlanneryDayContext())
        {
            db.PlanneryDays.Add(PD);
            db.SaveChanges();
                Refresh();
        }
    }
    public bool CanAddTask(Object obj)
        {
            return true;//(obj as PlanneryDay != null);
        }
        #endregion
        #region RemovingTask
        public void RemoveTask(object obj)
        {
            using (Properties.PlanneryDayContext db = new Properties.PlanneryDayContext())
            {
                db.PlanneryDays.Remove((PlanneryDay)obj);
                db.SaveChanges();
                Tasks.Remove((PlanneryDay)obj);
            }

            }

        public bool CanRemoveTask(object obj)
        {
            return (obj as PlanneryDay != null);
        }
        #endregion
        #region Refreshtask
        public void Refresh()
        {
            Tasks = new ObservableCollection<PlanneryDay>(GetTasks());
        }
        #endregion
    }
}