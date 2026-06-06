using MaterialDesignThemes.Wpf;
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

namespace SISBase.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para UCButton.xaml
    /// </summary>
    public partial class UCButton : UserControl
    {
        public UCButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register(
                nameof(ButtonText),
                typeof(string),
                typeof(UCButton),
                new PropertyMetadata("Button"));

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(UCButton));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                nameof(CommandParameter),
                typeof(object),
                typeof(UCButton));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.Register(
                nameof(ButtonWidth),
                typeof(double),
                typeof(UCButton),
                new PropertyMetadata(120.0));

        public double ButtonWidth
        {
            get => (double)GetValue(ButtonWidthProperty);
            set => SetValue(ButtonWidthProperty, value);
        }

        public static readonly DependencyProperty ButtonHeightProperty =
            DependencyProperty.Register(
                nameof(ButtonHeight),
                typeof(double),
                typeof(UCButton),
                new PropertyMetadata(40.0));

        public double ButtonHeight
        {
            get => (double)GetValue(ButtonHeightProperty);
            set => SetValue(ButtonHeightProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
    DependencyProperty.Register(
        nameof(Icon),
        typeof(PackIconKind),
        typeof(UCButton),
        new PropertyMetadata(PackIconKind.None));

        public PackIconKind Icon
        {
            get => (PackIconKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
    }
}
