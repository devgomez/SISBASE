using Microsoft.Extensions.DependencyInjection;
using SISBase.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SISBase.Presentation.Views
{
    /// <summary>
    /// Lógica de interacción para CategoriesView.xaml
    /// </summary>
    public partial class CategoriesView : UserControl
    {
        public CategoriesView()
        {
            InitializeComponent();
            DataContext = App.Services.GetRequiredService<CategoryViewModel>();
        }
    }
}
