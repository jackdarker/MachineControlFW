using System;

namespace Maschine1
{
    public class Randomize
    {
            private static Random random = new Random();
            /// <summary>
            /// Erzeugt eine Zufallszahl zwischen 0 und dem gegebenen Wert - 1.
            /// </summary>
            public static int Int(int maximum)
            {
                return random.Next(maximum);
            }
            /// <summary>
            /// Erzeugt eine Zufallszahl zwischen dem kleineren gegebenen Wert und dem
            /// größeren gegebenen Wert - 1.
            /// </summary>
            public static int Int(int minimum, int maximum)
            {
                if (minimum > maximum)
                    return random.Next(maximum, minimum);
                return random.Next(minimum, maximum);
            }

      }
}

