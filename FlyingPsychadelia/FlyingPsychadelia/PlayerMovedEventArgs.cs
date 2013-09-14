using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FlyingPsychadelia
{
    public class PlayerMovedEventArgs : EventArgs
    {
        public Vector2 MovementDifference { get; set; }

        public PlayerMovedEventArgs(Vector2 difference)
        {
            MovementDifference = difference;
        }
    }
}
