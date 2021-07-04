using System;

namespace TicTacToe
{
    public class Input
    {
        private Game game;
        
        public Input(Game _game) 
        {
            // Get the Game class instance, for function calls.
            game = _game;
        }

        public void CheckInput()
        {
            if (Console.KeyAvailable)
            {
                // Console.ReadKey is given the parameter false, to keep it from displaying the pressed key.
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        game.Move(new Vector2(0, -1));
                        break;

                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        game.Move(new Vector2(1, 0));
                        break;

                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        game.Move(new Vector2(0, 1));
                        break;

                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        game.Move(new Vector2(-1, 0));
                        break;

                    case ConsoleKey.Enter:
                    case ConsoleKey.E:
                        if (!game.displayingMsg)
                        {
                            if (game.playerXTurn)
                                game.PlaceIcon(Icon.X);
                            else
                                game.PlaceIcon(Icon.O);
                        }
                        else 
                        {
                            game.DisplayHelp(false);
                        }

                        game.doDraw = true;
                        break;

                    case ConsoleKey.R:
                        game.RestartGame();
                        break;

                    case ConsoleKey.H:
                        game.DisplayHelp(true);                        
                        break;

                    case ConsoleKey.Escape:
                        // End main program loop
                        Program.doProgram = false;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}