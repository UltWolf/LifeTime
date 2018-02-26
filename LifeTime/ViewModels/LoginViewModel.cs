using LifeTime.Models;
using LifeTime.Properties;
using LifeTime.Views;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace LifeTime.ViewModels
{
    public class LoginViewModel : User,INotifyPropertyChanged
    {
        public System.Windows.Input.ICommand LoginCommand { get; private set; }
        public System.Windows.Input.ICommand RegisterCommand { get; private set; }
        public Action CloseAction { get; set; }
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);

        }
        public void Login(object obj)
        {

            var User = sqlHelpers.SqlUser.GetUser(Username);
            if (User.Username != "")
            {
                if (User.Password == Password)
                {
                    var mainWindow = new MainWindow(Username);
                    mainWindow.Show();
                    CloseAction();
                }
                else
                {
                    MessageBox.Show("Your password is incorrect");
                }
            }
            else
            {
                MessageBox.Show("We can`t find this user,sorry");
            }
            }
        public void Register(object obj)
        {
            var registerComponent = new Registration();
            registerComponent.Show();
            CloseAction();
        }
    }
}
