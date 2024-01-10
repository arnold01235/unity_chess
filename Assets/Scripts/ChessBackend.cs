using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    public static int[] board;

     static Board()
    {
        board = new int[64];
    }

    public const string standardStartFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR, w KQkq - 0 1";
    static void loadPositionFromFen(string fen)
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

