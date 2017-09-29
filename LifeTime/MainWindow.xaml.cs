using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Threading;
using System.Data.Entity;
using LifeTime;
using LifeTime.Properties;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LifeTime
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    ///   List<Book> result 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Console.WriteLine(Thread.CurrentThread.Name);
            InitializeComponent();
         
            Task ResultTime = new Task(MethodResultTime);
          ResultTime.Start();
        }
        public void MethodResultTime()
        {
            while(true){
                try
                { 
                    Dispatcher.Invoke(() => DateTimeNow.Content = DateTime.Now.ToString());
                    using (PlanneryDayContext db = new PlanneryDayContext())
                    {
                      
                        var plan = db.PlanneryDays;
                        foreach (var p in plan)
                        {
                            if (p.Time.ToShortTimeString() == DateTime.Now.ToShortTimeString())
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    popup2.IsOpen = true;

                                });
                            }
                        }
                    }
                    Task.Delay(60000);
                }
                catch (TaskCanceledException)
                {

                }
               
            }

          
        }
        public void ViewTask()
        {
            using (PlanneryDayContext db = new PlanneryDayContext())
            {
            
                Dispatcher.Invoke(() =>
                {
                    TaskToday.Items.Clear();
                  
                });
                
             var plannery = db.PlanneryDays;
                foreach (var p in plannery)
                { if (p.Time.ToShortDateString() == DateTime.Now.ToShortDateString()) {
                        Dispatcher.Invoke(() =>
                        {
                            TaskToday.Items.Add(p.Header);



                        });
                    }
                }
                
                

         
            }
               
        }
       

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string Headertask = "";
            string descriptiontask = "";


            DateTime dateTime = new DateTime();
             Headertask = Header.Text;
            double shour = 0;
            double sminutes = 0;
        
            
                shour = double.Parse(TimeHours.Text);
                sminutes = double.Parse(TimeMinutes.Text);
          

        
        
           descriptiontask = Description.Text;
           dateTime = (DateTime)DatePi.SelectedDate;
            dateTime = dateTime.AddHours(shour);
            dateTime = dateTime.AddMinutes(sminutes);


            PlanneryDay PD = new PlanneryDay() { Header = Headertask, Description = descriptiontask };
            PD.Time = dateTime;
            Task task = new Task(()=>Add_Plan(PD));
            task.Start();
       popup1.IsOpen = true;
            Header.Text = null;
            Description.Text = null;
         DatePi.SelectedDate = null;

        }
        private void Add_Plan(PlanneryDay PD)
        {
            using (PlanneryDayContext db = new PlanneryDayContext())
            {
             
                db.PlanneryDays.Add(PD);
                ViewTask();
                db.SaveChanges();
            }

            
        }

        private void TaskToday_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TaskToday_Loaded(object sender, RoutedEventArgs e)
        {
            ViewTask();
        }

        private void Header_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Header.Text = null;
        }

        private void Description_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Description.Text = null;
        }
    }
}
