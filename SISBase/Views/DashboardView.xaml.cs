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
using System.Windows.Media.Animation;

namespace SISBase.Views
{
    /// <summary>
    /// Lógica de interacción para DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        private const double ExpandedSidebarWidth = 254;
        private const double CollapsedSidebarWidth = 84;

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
            ApplySidebarState();
        }

        private void ApplySidebarState()
        {
            var menuButtons = new[] { btnDashboard, btnProducts, btnCustomers, btnRoles, btnUsuarios, btnOpciones };

            if (_collapsed)
            {
                AnimateSidebarWidth(CollapsedSidebarWidth);

                txtLogo.Visibility = Visibility.Collapsed;
                txtLogoSubtitle.Visibility = Visibility.Collapsed;

                txtDashboard.Visibility = Visibility.Collapsed;
                txtProducts.Visibility = Visibility.Collapsed;
                txtCustomers.Visibility = Visibility.Collapsed;
                txtSales.Visibility = Visibility.Collapsed;
                txtPurchases.Visibility = Visibility.Collapsed;
                txtInvoices.Visibility = Visibility.Collapsed;

                txtVersion.Visibility = Visibility.Collapsed;

                foreach (var button in menuButtons)
                {
                    button.Padding = new Thickness(0);
                    button.HorizontalContentAlignment = HorizontalAlignment.Center;
                }
            }
            else
            {
                AnimateSidebarWidth(ExpandedSidebarWidth);

                txtLogo.Visibility = Visibility.Visible;
                txtLogoSubtitle.Visibility = Visibility.Visible;

                txtDashboard.Visibility = Visibility.Visible;
                txtProducts.Visibility = Visibility.Visible;
                txtCustomers.Visibility = Visibility.Visible;
                txtSales.Visibility = Visibility.Visible;
                txtPurchases.Visibility = Visibility.Visible;
                txtInvoices.Visibility = Visibility.Visible;

                txtVersion.Visibility = Visibility.Visible;

                foreach (var button in menuButtons)
                {
                    button.Padding = new Thickness(12, 0, 10, 0);
                    button.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                }
            }
        }

        private void AnimateSidebarWidth(double targetWidth)
        {
            var animation = new DoubleAnimation
            {
                To = targetWidth,
                Duration = TimeSpan.FromMilliseconds(220),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            SidebarHost.BeginAnimation(FrameworkElement.WidthProperty, animation);
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
            txtPageTitle.Text = "Catalogo de Productos";
            MainContent.Content = new ProductCatalogView();
        }

        private void btnRoles_Click(object sender, RoutedEventArgs e)
        {
            SetActiveMenu(btnRoles);
            txtPageTitle.Text = "Roles";
            MainContent.Content = new RolesView();
        }
        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            SetActiveMenu(btnUsuarios);
            txtPageTitle.Text = "Gestión de Usuarios y Roles";
            MainContent.Content = new UsersView();
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            SetActiveMenu(btnOpciones);
            txtPageTitle.Text = "Opciones";
            MainContent.Content = new OptionsView();
        }

    }
}
