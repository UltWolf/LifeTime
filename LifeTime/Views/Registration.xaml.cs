using LifeTime.ViewModels;
using System.Windows;


namespace LifeTime.Views
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            var vm =  new RegistrationViewModel();
            this.DataContext = vm;
            if (vm.CloseAction == null)
            {
                vm.CloseAction =  new System.Action(this.Close);
            }
        }
    }
}
