using System.Windows;
using System.Windows.Controls;

namespace chessGui
{
    /// <summary>
    /// Interaction logic for PauseMenu.xaml
    /// </summary>
    public partial class PauseMenu : UserControl
    {
        public event Action<Options> OptionSelected;
        public PauseMenu()
        {
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Options.Continue);
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Options.Restart);
        }
    }
}
