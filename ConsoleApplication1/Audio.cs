using System;

namespace TicTacToe
{
    public enum AudioIndex 
    {
        Victory,
        Lose,
        Move,
        PlaceSuccess,
        PlaceFailed,
        debug
    };
    
    public class Audio
    {
        public void Play(AudioIndex _audioIndex) 
        {
            switch(_audioIndex)
            {
                case AudioIndex.Victory:
                    Console.Beep(440, 250); // A_4
                    Console.Beep(659, 450); // E_5
                    break;

                case AudioIndex.Lose:
                    Console.Beep(330, 200); // E_4
                    Console.Beep(220, 250); // A_3
                    break;
                
                case AudioIndex.Move:
                    Console.Beep(262, 200); // C_4
                    break;

                case AudioIndex.PlaceSuccess:
                    Console.Beep(349, 200); // E_4
                    break;

                case AudioIndex.PlaceFailed:
                    Console.Beep(156, 350); // D#_3 / Eb_3
                    break;

                case AudioIndex.debug:
                    Console.Beep(1000, 500); // Debug sound, no need for a specific tone.
                    break;
            }
        }
    }
}