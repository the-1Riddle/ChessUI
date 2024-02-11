using System.Windows.Controls;
using System.Windows.Input;
using ChessLog;

namespace chessGui
{
    /// <summary>
    /// Interaction logic for pPromotionMenu.xaml
    /// </summary>
    public partial class pPromotionMenu : UserControl
    {
        public event Action<PieceType> PieceSelected;
        public pPromotionMenu(Player player)
        {
            InitializeComponent();

            QueenPng.Source = Images.GetImage(player, PieceType.Queen);
            BishopPng.Source = Images.GetImage(player, PieceType.Bishop);
            RookPng.Source = Images.GetImage(player, PieceType.Rook);
            KnightPng.Source = Images.GetImage(player, PieceType.Knight);
        }


        private void QueenPng_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Queen);
        }

        private void BishopPng_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Bishop);
        }

        private void RookPng_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Rook);
        }

        private void KnightPng_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(PieceType.Knight);
        }
    }
}
