using Microsoft.Extensions.DependencyInjection;
using SISBase.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SISBase.Presentation.Views
{
    /// <summary>
    /// Lógica de interacción para RolesView.xaml
    /// </summary>
    public partial class RolesView : UserControl
    {
        public RolesView()
        {
            InitializeComponent();

            DataContext = App.Services.GetRequiredService<RoleViewModel>();
        }
    }
}
