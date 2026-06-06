using Microsoft.Extensions.DependencyInjection;
using SISBase.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SISBase.Presentation.Views
{
    /// <summary>
    /// Lógica de interacción para UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
            DataContext = App.Services.GetRequiredService<UserViewModel>();
        }
    }
}
