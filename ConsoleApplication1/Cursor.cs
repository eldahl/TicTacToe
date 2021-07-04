using System;

namespace TicTacToe
{
    public class Cursor
    {
        private Vector2 cursorLocation = new Vector2(0, 0);

        private int fieldHeight, fieldLength;
        private int startOffsetX, startOffsetY;
        
        public Cursor()
        {
            fieldHeight = 7;
            fieldLength = 8;
            
            startOffsetX = 10;
            startOffsetY = 4;
        }
        public Cursor(int _fieldHeight, int _fieldLength) 
        {
            fieldHeight = _fieldHeight;
            fieldLength = _fieldLength;

            startOffsetX = 5;
            startOffsetY = 4;
        }
        public Cursor(int _fieldHeight, int _fieldLength, int _startOffsetX, int _startOffsetY)
        {
            fieldHeight = _fieldHeight;
            fieldLength = _fieldLength;

            startOffsetX = _startOffsetX;
            startOffsetY = _startOffsetY;
        }


        /// <summary>
        /// Moves the cursor in the supplied direction.
        /// </summary>
        /// <param name="_direction">1: Up, 2: Right, 3: Down, 4: Left</param>
        public void Move(Vector2 _direction)
        {
            // X   -Check if outside of grid-
            if ((cursorLocation.x + _direction.x) > 2)
                cursorLocation.x = 0;
            else if ((cursorLocation.x + _direction.x) < 0)
                cursorLocation.x = 2;
            else
                cursorLocation.x += _direction.x;

            // Y   -Check if outside of grid-
            if ((cursorLocation.y + _direction.y) > 2)
                cursorLocation.y = 0;
            else if ((cursorLocation.y + _direction.y) < 0)
                cursorLocation.y = 2;
            else
                cursorLocation.y += _direction.y;

            Console.SetCursorPosition
                (
                    startOffsetX + cursorLocation.x * fieldLength, 
                    startOffsetY + cursorLocation.y * fieldHeight
                );
        }


        /// <summary>
        /// Sets the cursors location to the given values.
        /// </summary>
        /// <param name="_cord">Location of the cursor.</param>
        public void SetCursorLocation(Vector2 _cord) 
        {
            Console.SetCursorPosition(_cord.x, _cord.y);
        }

        // Sometimes the mouse jumps to the start or end of the console.
        // Small fix to keep the cursor in location.
        public void UpdateCursorLocation() 
        {
            Console.SetCursorPosition
                (
                    startOffsetX + cursorLocation.x * fieldLength, 
                    startOffsetY + cursorLocation.y * fieldHeight
                );
        }

        public Vector2 GetCursorLocation()
        {
            return cursorLocation;
        }

    }
}