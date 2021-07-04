using System;

namespace TicTacToe 
{
    public class Game 
    {
        GameFrame gameFrame = new GameFrame();
        Input input;
        Cursor cursor = new Cursor();
        Audio audio = new Audio();        

        // True if X's turn, false if O's turn.
        public bool playerXTurn = true;

        public bool displayingMsg = false;

        // Keeps track of which icons are placed on the gametable
        // true: X | false: O | null: empty
        public Icon[,] placedIcons = new Icon[3,3];

        public Icon winner = Icon.Empty;

        public string[] playerName = new string[2];

        public Game()
        {
            // Pass on this instance of the Game class to Input class.
            input = new Input(this);

            // Set entire gametable to empty
            for (int i = 0; i < placedIcons.GetLength(0); i++) 
            {
                for (int j = 0; j < placedIcons.GetLength(1); j++) 
                {
                    placedIcons[i, j] = Icon.Empty;
                }
            }

            // Set the size of the buffer and window, measured in lines.
            Console.SetWindowSize(37, 27);
            Console.SetBufferSize(37, 27);

        }


        public void DoStartSequence() 
        {
            // Welcome message.
            gameFrame.DisplayMsg(Message.StartPage0);
            Console.ReadLine();

            // Get Player X's name
            gameFrame.DisplayMsg(Message.StartPage1);
            cursor.SetCursorLocation(new Vector2(9, 12));
            playerName[0] = Console.ReadLine();

            // Get Player O's name
            gameFrame.DisplayMsg(Message.StartPage2);
            cursor.SetCursorLocation(new Vector2(9, 12));
            playerName[1] = Console.ReadLine();

            gameFrame.PassOnPlayerNames(playerName);

            // Easter bacon always important
            if (playerName[0] == "bacon" || playerName[1] == "bacon") 
            {
                gameFrame.DisplayMsg("BaconBaconBaconBaconBacon");
                Console.ReadLine();
            }
            
            // Redraw the table, to get rid of the last message.
            gameFrame.MakeGameTable();
            gameFrame.DrawGameTable();

        }

        public bool doDraw = true;

        public void DoUpdate() 
        {
            input.CheckInput();

            if (doDraw)
            {
                gameFrame.DrawGameTable();
                cursor.UpdateCursorLocation();
                doDraw = false;
            }
        }


        /// <summary>
        /// Handles what happens when the game is over.
        /// </summary>
        /// <param name="_winnerIcon">The winner icon, or empty if a tie was had.</param>
        private void OnGameOver(Icon _winnerIcon) 
        {
            if (_winnerIcon == Icon.X)
            {
                gameFrame.DisplayMsg(Message.XWon);
                audio.Play(AudioIndex.Victory);
                winner = Icon.X;
                Console.ReadLine();
                RestartGame();
            }
            else if (_winnerIcon == Icon.O)
            {
                gameFrame.DisplayMsg(Message.OWon);
                audio.Play(AudioIndex.Victory);
                winner = Icon.O;
                Console.ReadLine();
                RestartGame();
            }
            else
            {
                gameFrame.DisplayMsg(Message.Tied);
                audio.Play(AudioIndex.Lose);
                Console.ReadLine();
                RestartGame();
            }
        }


        /// <summary>
        /// Places the current players icon in the current location of the cursor.
        /// </summary>
        /// <param name="_icon">Icon to insert</param>
        public void PlaceIcon(Icon _icon)
        {
            if (placedIcons[cursor.GetCursorLocation().x, cursor.GetCursorLocation().y] == Icon.Empty) 
            {
                gameFrame.InsertIcon(_icon, cursor.GetCursorLocation());

                // Keeps track of each placed icon
                placedIcons[cursor.GetCursorLocation().x, cursor.GetCursorLocation().y] = _icon;

                audio.Play(AudioIndex.PlaceSuccess);
                playerXTurn = !playerXTurn;
            }
            else
                audio.Play(AudioIndex.PlaceFailed);

            // Check for win
            
            // Rows
            for (int i = 0; i < placedIcons.GetLength(0); i++) 
            {
                if (placedIcons[i, cursor.GetCursorLocation().y] != _icon)
                    break;
                if (i == placedIcons.GetLength(0) - 1)
                    OnGameOver(_icon);
            }

            // Columns
            for (int i = 0; i < placedIcons.GetLength(1); i++)
            {
                if (placedIcons[cursor.GetCursorLocation().x, i] != _icon)
                    break;
                if (i == placedIcons.GetLength(1) - 1)
                    OnGameOver(_icon);
            }

            // Diagonal from left -> right, up -> down
            for (int i = 0; i < placedIcons.GetLength(1); i++)
            {
                if (placedIcons[i, i] != _icon)
                    break;
                if (i == placedIcons.GetLength(1) - 1)
                    OnGameOver(_icon);
            }

            // Diagonal from left -> right, down -> up
            for (int i = 0; i < placedIcons.GetLength(1); i++)
            {
                if (placedIcons[i, (placedIcons.GetLength(1) - 1) - i] != _icon)
                    break;
                if (i == placedIcons.GetLength(1) - 1)
                    OnGameOver(_icon);
            }

            if (winner == Icon.Empty)
            {
                // Check if the game is a tie
                int _emptySlots = 9;

                for (int i = 0; i < placedIcons.GetLength(0); i++)
                {
                    for (int j = 0; j < placedIcons.GetLength(1); j++)
                    {
                        if (placedIcons[i, j] != Icon.Empty)
                            _emptySlots--;
                    }
                }
                if (_emptySlots == 0)
                    OnGameOver(Icon.Empty);
            }
            
        }


        /// <summary>
        /// Display a help message on the screen.
        /// </summary>
        /// <param name="_state">Whether or not the help message should be displayed.</param>
        public void DisplayHelp(bool _state) 
        {
            if (_state)
            {
                displayingMsg = true;
                gameFrame.DisplayMsg(Message.Help);
                audio.Play(AudioIndex.Lose);
            }
            else 
            {
                displayingMsg = false;
                gameFrame.MakeGameTable();
                gameFrame.DrawGameTable();
            }
        }


        /// <summary>
        /// Moves the player a given distance.
        /// </summary>
        /// <param name="_delta">How far to move from the current location</param>
        public void Move(Vector2 _deltaLocation)
        {
            if (!displayingMsg)
            {
                cursor.Move(_deltaLocation);
                audio.Play(AudioIndex.Move);
            }
        }
        

        /// <summary>
        /// Restarts the game. (duh)
        /// </summary>
        public void RestartGame() 
        {
            for (int i = 0; i < placedIcons.GetLength(0); i++) 
            {
                for (int j = 0; j < placedIcons.GetLength(1); j++)
                {
                    placedIcons[i, j] = Icon.Empty;
                }
            }

            winner = Icon.Empty;
            
            gameFrame.ResetGameTable();
            gameFrame.DrawGameTable();
            
            cursor.UpdateCursorLocation();
        }
    }
}