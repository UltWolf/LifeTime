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
using System.Text.RegularExpressions;

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

            InitializeComponent();
         
            Task ResultTime = new Task(MethodResultTime);
          ResultTime.Start();
        }
        public void MethodResultTime()
        {
            while(true){
                try
                { 
                    Dispatcher.Invoke(() => DateTimeNow.Content = "Time now : "+DateTime.Now.ToString());
                    using (PlanneryDayContext db = new PlanneryDayContext())
                    {
                      
                        var plan = db.PlanneryDays;
                        foreach (var p in plan)
                        {
                            if (p.Time.ToShortTimeString() == DateTime.Now.ToShortTimeString())
                            {
                                if (p.Done ==false ) {
                                    p.Done = true;
                                    Console.WriteLine(p.Done);

                                    MediaPlayer sound = new MediaPlayer();
                                    Uri uri = new Uri(@"E:\ding.mp3");
                                    sound.Open(uri);
                                    sound.Play();
                               MessageBox.Show( $"Hey, did u forget about {p.Header}  u mast do something like a {p.Description},Good luck,baddy.");

                                        Console.WriteLine(p.Done);
                                
                                }
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
                            TaskToday.Items.Add($"Header: {p.Header}; Description: {p.Description}; at time:{p.Time} ");



                        });
                    }
                }
                
                

         
            }
               
        }
       

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string Headertask = "";
            string descriptiontask = "";
           Regex regex = new Regex("^[0-9]{1,2}$");

            DateTime dateTime = new DateTime();


            if (Header.Text == null||Header.Text =="")
            {

                MessageBox.Show("You don`t put header yours Task");
            }

            else if (TimeHours.Text == null || TimeHours.Text == "")
            {

                MessageBox.Show("You don`t put hours yours Task");
            } else if (TimeMinutes.Text == null || TimeMinutes.Text == "")
            {

                MessageBox.Show("You don`t put header yours Task");
            }else if (!regex.IsMatch(TimeHours.Text)&&!regex.IsMatch(TimeMinutes.Text))
            {
                MessageBox.Show("You wrote wrong symbols,pls put hours and minutes");
            }
            else
            { 
                Headertask = Header.Text;
                double shour = 0;
                double sminutes = 0;
                shour = double.Parse(TimeHours.Text);
                sminutes = double.Parse(TimeMinutes.Text);
                if (shour < 0 || shour > 23)
                {
                    MessageBox.Show("Please put right number hours. from 0 to 23 hours");
                }
                else if (sminutes < 0 || sminutes > 59)
                {
                    MessageBox.Show("Please put right number minutes. from 0 to 59 hours");
                }
                else
                {
                    descriptiontask = Description.Text;
                    dateTime = (DateTime)DatePi.SelectedDate;
                    dateTime = dateTime.AddHours(shour);
                    dateTime = dateTime.AddMinutes(sminutes);
                    PlanneryDay PD = new PlanneryDay() { Header = Headertask, Description = descriptiontask };
                    PD.Time = dateTime;
                    Task task = new Task(() => Add_Plan(PD));
                    task.Start();
                    popup1.IsOpen = true;
                    Header.Text = "Название задачи";
                    Description.Text = "Описание";
                    DatePi.SelectedDate = null;
                }
            }

           

        }
        private void Add_Plan(PlanneryDay PD)
        {
            using (PlanneryDayContext db = new PlanneryDayContext())
            {
             
                db.PlanneryDays.Add(PD);
                db.SaveChanges();
                ViewTask();
               
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

        private void TimeHours_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TimeHours.Text = null;
        }

      

        private void TimeMinutes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TimeMinutes.Text = null;
        }

        private void PopUpTask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            popup2.IsOpen = false;
        }
    }
}
