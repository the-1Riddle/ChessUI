using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLog;

namespace chessGui
{
    /// <summary>
    /// Interaction logic for gameOver.xaml
    /// </summary>
    public partial class GameOver : UserControl
    {
        public event Action<Options> OptionSelected;
        public GameOver(GmStatus gameState)
        {
            InitializeComponent();

            EndResult result = gameState.EndResult;
            WinnerText.Text = GetWinnerText(result.Winner);
            ReasonText.Text = GetReasonText(result.Reason, gameState.CurrentPlayer);
        }

        private static string GetWinnerText(Player winner)
        {
            return winner switch
            {
                Player.white => "WHITE WON!",
                Player.black => "BLACK WON!",
                _ => "IT'S A DRAW ?!"
            };
        }
        private static string PlayerString(Player player)
        {
            return player switch
            {
                Player.white => "WHITE",
                Player.black => "BLACK",
                _ => ""
            };
        }

        private static string GetReasonText(EndReason reason, Player currentPlayer)
        {
            return reason switch
            {
                EndReason.Stalemate => $"STALEMATE - {PlayerString(currentPlayer)} LOST",
                EndReason.Checkmate => $"CHECKMATE - {PlayerString(currentPlayer)} LOST",
                EndReason.InsufMaterial => "INSUFFICIENT MATERIALS",
                _ => ""
            };
        }
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Options.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Options.Exit);
        }
    }
}
