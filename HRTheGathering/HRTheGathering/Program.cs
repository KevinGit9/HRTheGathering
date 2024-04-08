using HRTheGathering.Board;
using HRTheGathering.Publishers;

class Program
{
    static void Main(string[] args)
    {
        Board board = Board.Instance;

        // Start the game
        board.StartGame();
    }
}