using LifeTime.Models;
using LifeTime.Properties;
using LifeTime.Views;
using System;
using System.Data.SqlClient;
using System.Windows.Input;

namespace LifeTime.ViewModels
{
    class RegistrationViewModel:User
    {
        public ICommand RegisterCommand { get; set; }
        public Action CloseAction { get;set; }
        public RegistrationViewModel()
        {
            RegisterCommand = new RelayCommand(Registration);
        }
        public void Registration(object obj)
        {
            sqlHelpers.SqlUser.RegistrateUser(Username, Password);
            var MainWindow = new MainWindow(Username);
            MainWindow.Show();
            CloseAction();
        }
    }
}
