using Microsoft.Extensions.DependencyInjection;
using SISBase.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SISBase.Presentation.Views
{
    /// <summary>
    /// Lógica de interacción para UnitsView.xaml
    /// </summary>
    public partial class UnitsView : UserControl
    {
        public UnitsView()
        {
            InitializeComponent();
            DataContext = App.Services.GetRequiredService<UnitViewModel>();
        }
    }
}
