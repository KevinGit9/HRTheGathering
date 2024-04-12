using System;
using HRTheGathering.Board;

class Program
{
    // Authors:
    // Kevin Shi - 1037132
    // Anton Shi - 1033379

    static void Main(string[] args)
    {
        Board board = Board.Instance;

        // Start the game, intented automated play
        //board.StartGame();

        // Small play according to documentation
        board.SmallPlay();
    }
}