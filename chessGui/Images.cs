using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLog;

namespace chessGui
{
    internal class Images
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            { PieceType.Pawn, LoadImage("src/PawnW.png") },
            { PieceType.Bishop, LoadImage("src/bishop1.png") },
            { PieceType.King, LoadImage("src/king1.png") },
            { PieceType.Knight, LoadImage("src/Knight1.png") },
            { PieceType.Queen, LoadImage("src/queen1.png") },
            { PieceType.Rook, LoadImage("src/rook1.png") }
        };

        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            { PieceType.Pawn, LoadImage("src/PawnB.png") },
            { PieceType.Bishop, LoadImage("src/bishop2.png") },
            { PieceType.King, LoadImage("src/king2.png") },
            { PieceType.Knight, LoadImage("src/Knight2.png") },
            { PieceType.Queen, LoadImage("src/queen2.png") },
            { PieceType.Rook, LoadImage("src/rook2.png") }
        };
        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        public static ImageSource GetImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.white => whiteSources[type],
                Player.black => blackSources[type],
                _ => null
            };
        }
        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null)
            {
                return null;
            }
            return GetImage(piece.Color, piece.Type);
        }
    }
}
