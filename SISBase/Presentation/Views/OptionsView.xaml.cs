using Microsoft.Extensions.DependencyInjection;
using SISBase.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SISBase.Presentation.Views
{
    /// <summary>
    /// Lógica de interacción para OptionsView.xaml
    /// </summary>
    public partial class OptionsView : UserControl
    {
        public OptionsView()
        {
            InitializeComponent();
            DataContext = App.Services.GetRequiredService<OptionViewModel>();
        }
    }
}
