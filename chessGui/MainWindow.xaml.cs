using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLog;

namespace chessGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();
        private GmStatus gameState;
        private Position selectedPos = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState = new GmStatus(Player.white, Board.Initial());
            DrawBoard(gameState.Board);
            /*FlipGrid(gameState.CurrentPlayer);*/
        }
        private void InitializeBoard()
        {
            for (int m = 0; m < 8; m++)
            {
                for (int n = 0; n < 8; n++)
                {
                    Image image = new Image();
                    pieceImages[m, n] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[m, n] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }
        private void DrawBoard(Board board)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Piece piece = board[x, y];
                    pieceImages[x, y].Source = Images.GetImage(piece);
                }
            }
        }
        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMenuOnScreen())
            {
                return;
            }

            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if (selectedPos == null)
            {
                OnFromPosSelected(pos);
            }
            else
            {
                OnToPosSelected(pos);
            }
        }
        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }
        private void OnFromPosSelected(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPiece(pos);
            if (moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }
        private void OnToPosSelected(Position pos)
        {
            selectedPos = null;
            HideHighlights();

            if (moveCache.TryGetValue(pos, out Move move))
            {
                if (move.Type == MoveType.pawnPromotion)
                {
                    HandlePromotion(move.FromPos, move.ToPos);
                }
                else
                {
                    HandleMove(move);
                }
            }
        }
        private void HandlePromotion(Position from, Position to)
        {
            pieceImages[to.Row, to.Column].Source = Images.GetImage(gameState.CurrentPlayer, PieceType.Pawn);
            pieceImages[from.Row, from.Column].Source = null;

            pPromotionMenu promMenu = new pPromotionMenu(gameState.CurrentPlayer);
            MenuContainer.Content = promMenu;

            promMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Move promMove = new PawnPromotion(from, to, type);
                HandleMove(promMove);
            };
        }
        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);

            if (gameState.IsGameOver())
            {
                ShowGameOver();
            }
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach (Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }
        }
        private void ShowHighlights()
        {
            Color color = Color.FromArgb(178, 255, 255, 0);

            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);
            }
        }
        private void HideHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }

        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }

        private void ShowGameOver()
        {
            GameOver gameOver = new GameOver(gameState);
            MenuContainer.Content = gameOver;

            gameOver.OptionSelected += option =>
            {
                if (option == Options.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }
        private void RestartGame()
        {
            selectedPos = null;
            HideHighlights();
            moveCache.Clear();
            gameState = new GmStatus(Player.white, Board.Initial());

            DrawBoard(gameState.Board);
            /*SetCursor(gameState.CurrentPlayer);*/
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsMenuOnScreen() && e.Key == Key.Escape)
            {
                ShowPauseMenu();
            }
        }
        private void ShowPauseMenu()
        {
            PauseMenu pauseMenu = new PauseMenu();
            MenuContainer.Content = pauseMenu;

            pauseMenu.OptionSelected += option =>
            {
                MenuContainer.Content = null;

                if (option == Options.Restart)
                {
                    RestartGame();
                }
            };
        }

        /*private void FlipGrid(Player player)
{
   if (player == Player.white)
   {
       // Flip the grid horizontally
       foreach (UIElement element in BoardGrid.Children)
       {
           if (element is FrameworkElement frameworkElement)
           {
               double centerX = frameworkElement.ActualWidth / 2;
               frameworkElement.RenderTransformOrigin = new Point(0.5, 0.5);
               ScaleTransform flipTransform = new ScaleTransform();
               flipTransform.ScaleX = -1;
               flipTransform.CenterX = centerX;
               frameworkElement.RenderTransform = flipTransform;
           }
       }
   }
   else
   {
       // Flip the grid vertically
       foreach (UIElement element in BoardGrid.Children)
       {
           if (element is FrameworkElement frameworkElement)
           {
               double centerY = frameworkElement.ActualHeight / 2;
               frameworkElement.RenderTransformOrigin = new Point(0.5, 0.5);
               ScaleTransform flipTransform = new ScaleTransform();
               flipTransform.ScaleY = -1;
               flipTransform.CenterY = centerY;
               frameworkElement.RenderTransform = flipTransform;
           }
       }
   }
}*/
    }
}