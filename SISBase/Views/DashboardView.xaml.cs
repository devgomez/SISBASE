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
using System.Windows.Shapes;
using SISBase.Views.Controls;
using SISBase.Views.Pages;
using System.Windows.Media;
using System.Windows.Controls;
using SISBase.Presentation.Views;

namespace SISBase.Views
{
    /// <summary>
    /// Lógica de interacción para DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        private bool _collapsed = false;
        private Button? _activeButton;

        public DashboardView()
        {
            InitializeComponent();

            MainContent.Content = new DashboardHomeView();
            SetActiveMenu(btnDashboard);
        }

        private void SetActiveMenu(Button button)
        {
            if (_activeButton != null)
            {
                _activeButton.Background = Brushes.Transparent;
                _activeButton.BorderBrush = Brushes.Transparent;
                _activeButton.BorderThickness = new Thickness(0);
            }

            button.Background =
                new SolidColorBrush(Color.FromRgb(30, 41, 59));

            button.BorderBrush =
                new SolidColorBrush(Color.FromRgb(59, 130, 246));

            button.BorderThickness =
                new Thickness(4, 0, 0, 0);

            _activeButton = button;
        }
        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            _collapsed = !_collapsed;

            if (_collapsed)
            {
                SidebarColumn.Width = new GridLength(70);

                txtLogo.Visibility = Visibility.Collapsed;

                txtDashboard.Visibility = Visibility.Collapsed;
                txtProducts.Visibility = Visibility.Collapsed;
                txtCustomers.Visibility = Visibility.Collapsed;
                txtSales.Visibility = Visibility.Collapsed;
                txtPurchases.Visibility = Visibility.Collapsed;
                txtInvoices.Visibility = Visibility.Collapsed;

                txtVersion.Visibility = Visibility.Collapsed;
            }
            else
            {
                SidebarColumn.Width = new GridLength(250);

                txtLogo.Visibility = Visibility.Visible;

                txtDashboard.Visibility = Visibility.Visible;
                txtProducts.Visibility = Visibility.Visible;
                txtCustomers.Visibility = Visibility.Visible;
                txtSales.Visibility = Visibility.Visible;
                txtPurchases.Visibility = Visibility.Visible;
                txtInvoices.Visibility = Visibility.Visible;

                txtVersion.Visibility = Visibility.Visible;
            }
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            SetActiveMenu(btnDashboard);
            txtPageTitle.Text = "Dashboard";
            MainContent.Content = new DashboardHomeView();
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            SetActiveMenu(btnCustomers);
            txtPageTitle.Text = "Clientes";
            MainContent.Content = new CustomerView();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            SetActiveMenu(btnProducts);
            txtPageTitle.Text = "Productos";
           // MainContent.Content = new ProductView();
        }

        private void btnRoles_Click(object sender, RoutedEventArgs e)
        {
            SetActiveMenu(btnRoles);
            txtPageTitle.Text = "Roles";
            MainContent.Content = new RolesView();
        }
        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            SetActiveMenu(btnRoles);
            txtPageTitle.Text = "Gestión de Usuarios y Roles";
            MainContent.Content = new UsersView();
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            SetActiveMenu(btnRoles);
            txtPageTitle.Text = "Opciones";
            MainContent.Content = new OptionsView();
        }

    }
}
