using System;
using HRTheGathering.Board;

class Program
{
    static void Main(string[] args)
    {
        Board board = Board.Instance;

        // Start the game
        board.StartGame();
        //board.SmallPlay();
    }
}