using System;

namespace TicTacToe
{
    // Used for keeping track of the icons on the table.
    public enum Icon 
    { 
        X, 
        O, 
        Empty 
    };

    // Passed around when displaying messages to the user.
    public enum Message
    {
        StartPage0,
        StartPage1,
        StartPage2,
        Help,
        XWon,
        OWon,
        Tied,
        Lost
    };

    public struct Vector2
    {
        public int x;
        public int y;

        public Vector2(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
    
    public class Program
    {
        public static bool doProgram = true;

        static void Main(string[] args)
        {
            Game _game = new Game();

            // First the hackish start sequence which ask for the players names.
            _game.DoStartSequence();
            
            // Then onward to the real deal.
            while (doProgram) 
            {
                _game.DoUpdate();
            }
        }
    }
}