using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class chessBackend : MonoBehaviour {
    private void Start()
    {
         Board.loadPositionFromFen(Board.standardStartFEN);
         spawnBoardGraphic();

    }

    private void spawnPieceGraphicToBoard(char symbol, int pos){
        Board.boardGraphic[pos] = Instantiate(Board.prefabDict[symbol], new Vector2(pos%8+0.5f, pos/8+0.5f), Quaternion.identity);
    }

    private void spawnBoardGraphic(){
        for(int i = 0; i<Board.board.Length; i++){
            int currentPiece = Board.board[i];
            if(currentPiece!=0){
                spawnPieceGraphicToBoard(Board.pieceDict[currentPiece], i);
            }
        }
    }
 
}
public static class Piece
{
    public const int None = 0;
    public const int King = 1;
    public const int Pawn = 2;
    public const int Knight = 3;
    public const int Bishop = 4;
    public const int Rook = 5;
    public const int Queen = 6;

    public const int White = 8;
    public const int Black = 16;

}

public static class Board {

    public static Dictionary<char, GameObject> prefabDict;
    public static Dictionary<int, char> pieceDict;
    public static int[] board;
    public static GameObject[] boardGraphic;
     static Board()
    {
        board = new int[64];
        boardGraphic = new GameObject[64];
        loadPrefabDict();
        loadPieceDict();
    }

    private static void loadPrefabDict(){
        prefabDict = new Dictionary<char, GameObject>()
        {
            ['k'] = Resources.Load<GameObject>("Prefab/Pieces/BlackKing"),
            ['p'] = Resources.Load<GameObject>("Prefab/Pieces/BlackPawn"),
            ['n'] = Resources.Load<GameObject>("Prefab/Pieces/BlackKnight"),
            ['b'] = Resources.Load<GameObject>("Prefab/Pieces/BlackBishop"),
            ['r'] = Resources.Load<GameObject>("Prefab/Pieces/BlackRook"),
            ['q'] = Resources.Load<GameObject>("Prefab/Pieces/BlackQueen"),
            ['K'] = Resources.Load<GameObject>("Prefab/Pieces/WhiteKing"),
            ['P'] = Resources.Load<GameObject>("Prefab/Pieces/WhitePawn"),
            ['N'] = Resources.Load<GameObject>("Prefab/Pieces/WhiteKnight"),
            ['B'] = Resources.Load<GameObject>("Prefab/Pieces/WhiteBishop"),
            ['R'] = Resources.Load<GameObject>("Prefab/Pieces/WhiteRook"),
            ['Q'] = Resources.Load<GameObject>("Prefab/Pieces/WhiteQueen")
        };
    }


    private static void loadPieceDict(){
        pieceDict = new Dictionary<int, char>()
        {
            [Piece.King | Piece.White] = 'K',
            [Piece.Queen | Piece.White] = 'Q',
            [Piece.Rook | Piece.White] = 'R',
            [Piece.Bishop | Piece.White] = 'B',
            [Piece.Knight | Piece.White] = 'N',
            [Piece.Pawn | Piece.White] = 'P',
            [Piece.King | Piece.Black] = 'k',
            [Piece.Queen | Piece.Black] = 'q',
            [Piece.Rook | Piece.Black] = 'r',
            [Piece.Bishop | Piece.Black] = 'b',
            [Piece.Knight | Piece.Black] = 'n',
            [Piece.Pawn | Piece.Black] = 'p'
        };
    }

    public const string standardStartFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR, w KQkq - 0 1";
    public static void loadPositionFromFen(string fen)
    {
        Dictionary<char, int> symbolToPiece = new Dictionary<char, int>()
        {
            ['k'] = Piece.King,
            ['p'] = Piece.Pawn,
            ['n'] = Piece.Knight,
            ['b'] = Piece.Bishop,
            ['r'] = Piece.Rook,
            ['q'] = Piece.Queen
        };

        string fenBoard = fen.Split(',')[0];
        int file = 0, rank = 7;
        foreach(char symbol in fenBoard)
        {
            if (symbol == '/')
            {
                file = 0;
                rank--;
            } else
            {
                if(char.IsDigit(symbol))
                {
                    file += (int)char.GetNumericValue(symbol);
                } else
                {
                    int pieceColor = (char.IsUpper(symbol)) ? Piece.White : Piece.Black;
                    int pieceType = symbolToPiece[char.ToLower(symbol)];
                    board[rank* 8 + file] = pieceColor | pieceType;
                    file++;
                }
            }
        }
    }



}

