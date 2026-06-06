using Microsoft.Extensions.DependencyInjection;
using SISBase.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SISBase.Presentation.Views
{
    /// <summary>
    /// Lógica de interacción para BrandsView.xaml
    /// </summary>
    public partial class BrandsView : UserControl
    {
        public BrandsView()
        {
            InitializeComponent();
            DataContext = App.Services.GetRequiredService<BrandViewModel>();
        }
    }
}
