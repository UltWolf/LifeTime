using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.Models
{
    public class User: INotifyPropertyChanged
    {
        public int Id { get; set; }

        string username = "Your login";
        public string Username { get { return username; } set { username = value; OnPropertyChanged("Username"); } }

        string password = "Your Password";
        public string Password { get { return password; } set { password = value; OnPropertyChanged("Password"); } }

        #region propertyChanged
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
