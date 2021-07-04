using System;

namespace TicTacToe
{

    /// <summary>
    /// Controls the drawing of the table, on which the game is played.
    /// </summary>
    public class GameFrame
    {
        // Holds the string that will be drawn in the console.
        private string gameTable;

        // Icons used for the game table
        private string[] xIcon = new string[5], oIcon = new string[5];
        private string emptySpace;

        // Buffer for the icons in the table. 
        // If no icons are assigned to a field in the table, it will be filled with emptySpace.
        string[,] gameTableBuffer = new string[9, 5];

        private bool displayMessage = false;
        
        // Is displayed under the game table all the time.
        public string controlInfo = "Press H for help.";
        public string gameInfo;

        // Buffer for storing messages to the display.
        private string msgToDisplay;

        // Holds the player names
        public string[] playerName = new string[2];

        public GameFrame()
        {
            
            // Declare the different Icons which are going to be drawn.
            xIcon[0] = "x   x";
            xIcon[1] = " x x ";
            xIcon[2] = "  x  ";
            xIcon[3] = " x x ";
            xIcon[4] = "x   x";

            oIcon[0] = " OOO ";
            oIcon[1] = "O   O";
            oIcon[2] = "O   O";
            oIcon[3] = "O   O";
            oIcon[4] = " OOO ";

            // Declare empty space for when no icon is present on the game table.
            emptySpace = "     ";

            // Fill each buffer with empty space
            for (int i = 0; i < gameTableBuffer.GetLength(0); i++)
            {
                for (int j = 0; j < gameTableBuffer.GetLength(1); j++)
                {
                    gameTableBuffer[i, j] = emptySpace;
                }
            }

            MakeGameTable();
        }

        // For passing variable from the Game class, to this class.
        public void PassOnPlayerNames(string[] _names) 
        {
            playerName = _names;
            gameInfo = "'" + playerName[0] + "'s turn";
        }


        /// <summary>
        /// Make a new game table. Should be called after a new icon is placed.
        /// </summary>
        /// 
        /// Used when a new icon is inserted into the gameTableBuffer variable.
        public void MakeGameTable()
        {
            // Buffer for each line in the gameTable.
            string[] _tableLineBuffer = new string[15];

            gameTable = "\n       _______ _______ _______ \n";

            for (int i = 0; i < 3; i++)
            {
                gameTable += "      |       |       |       |\n";
                for (int j = 0; j < 5; j++)
                {
                    _tableLineBuffer[i * j] = "      | " + gameTableBuffer[i * 3, j] + " | " + gameTableBuffer[i * 3 + 1, j] + " | " + gameTableBuffer[i * 3 + 2, j] + " |\n";
                    gameTable = gameTable + _tableLineBuffer[i * j];
                }
                
                gameTable += "      |_______|_______|_______|\n";
                
                if (i == 0 && displayMessage)
                {
                    gameTable += "     _|_______|_______|_______|_\n";
                    for (int j = 0; j < 5; j++)
                    {
                        // If somebody won, display in the middle of the gameTable
                        if (i == 0 && j == 0)
                        {
                            _tableLineBuffer[i * j] = msgToDisplay;
                            gameTable = gameTable + _tableLineBuffer[i * j];

                            i = 1;
                            break;
                        }
                    }
                    gameTable += "      |_______|_______|_______|\n";
                }
            }
            if (displayMessage)
                displayMessage = false;

            gameTable += "          " + controlInfo + "\n\n          " + gameInfo + "\n " + "" + "\n";
        }
        

        /// <summary>
        /// Resets the game table for a new game.
        /// </summary>
        public void ResetGameTable()
        {
            for (int i = 0; i < gameTableBuffer.GetLength(0); i++)
            {
                for (int j = 0; j < gameTableBuffer.GetLength(1); j++)
                {
                    gameTableBuffer[i, j] = emptySpace;
                }
            }

            MakeGameTable();
        }
              
         
        /// <summary>
        /// Used for for game related messages, such as controls, and game conditions.
        /// </summary>
        /// <param name="_msg">Which Message to display.</param>
        public void DisplayMsg(Message _msg)
        {
            displayMessage = true;

            switch (_msg) 
            {
                case Message.StartPage0:
                    msgToDisplay =
                        " ###| Welcome to a magnificent  |###\n" +
                        "  ##|     game of TicTacToe!    |## \n" +
                        "   #|                           |#  \n" +
                        "  ##|  Press enter to continue  |## \n" +
                        " ###|___________________________|###\n";
                    break;

                case Message.StartPage1:
                    msgToDisplay =
                        " ###|  Please enter the first   |###\n" +
                        "  ##|       players name!       |## \n" +
                        "   #|                           |#  \n" +
                        "  ##|   :                       |## \n" +
                        " ###|___________________________|###\n";
                    break;

                case Message.StartPage2:
                    msgToDisplay =
                        " ###|  Please enter the second  |###\n" +
                        "  ##|       players name!       |## \n" +
                        "   #|                           |#  \n" +
                        "  ##|   :                       |## \n" +
                        " ###|___________________________|###\n";
                    break;

                case Message.Help:
                    msgToDisplay =
                        " ###|     Arrow keys to move.   |###\n" +
                        "  ##|  Enter to place the brick |## \n" +
                        "   #|                           |#  \n" +
                        "  ##|  Press Enter to continue. |## \n" +
                        " ###|___________________________|###\n";
                    break;

                case Message.Lost:
                    // Because of 2 player, this is kinda useless.
                    msgToDisplay =
                    " ###|                           |###\n" +
                    "  ##|     You lost the game!    |## \n" +
                    "   #|   _____________________   |#  \n" +
                    "  ##|Press any key to play agian|## \n" +
                    " ###|___________________________|###\n";
                    break;

                case Message.XWon:
                    msgToDisplay =
                        " ###|                           |###\n" +
                        "  ##|  Player 'X' won the game! |## \n" +
                        "   #|   _____________________   |#  \n" +
                        "  ##|Press any key to play agian|## \n" +
                        " ###|___________________________|###\n";
                    break;

                case Message.OWon:
                    msgToDisplay =
                        " ###|                           |###\n" +
                        "  ##|  Player 'O' won the game! |## \n" +
                        "   #|   _____________________   |#  \n" +
                        "  ##|Press any key to play agian|## \n" +
                        " ###|___________________________|###\n";
                    break;

                case Message.Tied:
                    msgToDisplay =
                        " ###|                           |###\n" +
                        "  ##|    The game was a tie!    |## \n" +
                        "   #|   _____________________   |#  \n" +
                        "  ##|Press any key to play agian|## \n" +
                        " ###|___________________________|###\n";
                    break;
            }
            MakeGameTable();
            DrawGameTable();
        }

        /// <summary>
        /// Used for debug / error messages.
        /// </summary>
        /// <param name="_msg">The message to display. Limited at 25 characters.</param>
        public void DisplayMsg(string _msg) 
        {
            displayMessage = true;

            // If the message is longer than 25 characters, cut off the rest.
            if (_msg.Length > 25)
                _msg = _msg.Substring(0, 25);

            int paddingAmount = (25 - _msg.Length) / 2;
            if (_msg.Length % 2 != 1)
                _msg += " ";
                        
            // Add equal amounts of padding on either side of the message, in order to center it.
            for (int i = 0; i < paddingAmount; i++) 
            {
                _msg = " " + _msg + " ";
            }

            msgToDisplay =
                        " ###|                           |###\n" +
                        "  ##| "        + _msg +       " |## \n" +
                        "   #|                           |#  \n" +
                        "  ##|                           |## \n" +
                        " ###|___________________________|###\n";
            MakeGameTable();
            DrawGameTable();
        }


        /// <summary>
        /// Draws the current state of the game table.
        /// </summary>
        public void DrawGameTable()
        {
            // Easy as can be.
            Console.Clear();
            Console.Write(gameTable);
        }


        /// <summary>
        /// Inserts a specific icon in specified location on the game table.
        /// </summary>
        /// <param name="_icon">What icon to insert.</param>
        /// <param name="_tableLocation">Location to insert icon.</param>
        public void InsertIcon(Icon _icon, Vector2 _tableLocation)
        {
            switch(_icon)
            {
                case Icon.X:
                    for (int i = 0; i < 5; i++ )
                    {
                        // Insert the xIcon string array into the gameTableBuffer
                        gameTableBuffer[(_tableLocation.y * 3) + _tableLocation.x , i] = xIcon[i];
                    }
                    
                    gameInfo = "'" + playerName[1] + "'s turn";

                    MakeGameTable();
                    break;

                case Icon.O:
                    for (int i = 0; i < 5; i++ )
                    {
                        // Insert the oIcon string array into the gameTableBuffer
                        gameTableBuffer[(_tableLocation.y * 3) + _tableLocation.x, i] = oIcon[i];
                    }

                    gameInfo = "'" + playerName[0] + "'s turn";
                    
                    MakeGameTable();
                    break;

                case Icon.Empty:
                    for (int i = 0; i < 5; i++ )
                    {
                        // Insert the emptySpace string into the gameTableBuffer
                        gameTableBuffer[(_tableLocation.y * 3) + _tableLocation.x, i] = emptySpace;
                    }
                    MakeGameTable();
                    break;

                default:
                    break;
            
            }
        }
    }
}
