
using LifeTime.Models;
using LifeTime.Views;
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
    class MainViewModelView : PlanneryDay, INotifyPropertyChanged
    {
        #region properties and parametres
        PlanneryDay[] tasks;
        private string _username;
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged("Username"); } }
        public ObservableCollection<PlanneryDay> Tasks { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand HeaderEmptyCommand{get;private set; }
        public ICommand DescrEmptyCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public Action CloseAction { get; set; }
        //TH TimeHours
        public ICommand THEmptyCommand { get; private set; }
        //TM TimeMinutes
        public ICommand TMEmptyCommand { get; private set; }
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
            HeaderEmptyCommand = new RelayCommand(ClearInputHeader);
            DescrEmptyCommand = new RelayCommand(ClearInputDescr);
            THEmptyCommand = new RelayCommand(ClearInputTH);
            TMEmptyCommand = new RelayCommand(ClearInputTM);
            LogoutCommand = new RelayCommand(LogOut);
        }
        private void LogOut(object obj)
        {
            var loginView = new Login();
            loginView.Show();
            CloseAction();

        }


        #endregion
        #region no optimization clear heading
        public void ClearInputHeader(object obj)
        {
            if (obj as string != null)
            {
                
                Header = String.Empty;
                OnPropertyChanged("Header");
            }
        }
        public void ClearInputDescr(object obj)
        {
            if (obj as string != null)
            {
               
                Description = String.Empty;
                OnPropertyChanged("Description");
            }
        }
        public void ClearInputTH(object obj)
        {
            if (obj as string != null)
            {
          
                TimeHours = String.Empty;
                OnPropertyChanged("TimeHours");
            }
        }
        public void ClearInputTM(object obj)
        {
            if (obj as string != null)
            {
                TimeMinutes = String.Empty;
                OnPropertyChanged("TimeMinutes");
            }
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
            Tasks = new ObservableCollection<PlanneryDay>(/*GetTasks()*/);
        }
        #endregion
    }
}