using Mazes.Wpf.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Mazes.Wpf.Views
{
    public partial class MazesView : Window
    {
        public MazesView()
        {
            InitializeComponent();
            var viewModel = new MazesViewModel();
            DataContext = viewModel;
        }

        private void Dimensions_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Int32.TryParse(e.Text, out int number))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
