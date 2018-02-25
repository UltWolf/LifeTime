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
using LifeTime.ViewModels;

namespace LifeTime.Views
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
            DataContext = new MainViewModelView();
            Task ResultTime = new Task(MethodResultTime);
            ResultTime.Start();
        }
        public void MethodResultTime()
        {
            while(true){
                try
                { String timeNow = DateTime.Now.ToString().ToUpper();
                    Dispatcher.Invoke(() => DateTimeNow.Text = ("Time now : "+timeNow));
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

                        db.SaveChanges();
                    }
                
                }
                catch (TaskCanceledException)
                {

                }
               
            }

          
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
