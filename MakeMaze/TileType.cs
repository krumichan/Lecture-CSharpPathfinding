using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding.MakeMaze
{
    static class TileType
    {
        public enum Type
        {
            Empty
        , Wall
        , Player
        , Destination
        ,
        }
        
        static public ConsoleColor GetTileColor(Type type)
        {
            switch (type)
            {
                case Type.Empty:
                    return ConsoleColor.Green;

                case Type.Wall:
                    return ConsoleColor.Red;

                case Type.Player:
                    return ConsoleColor.Blue;

                case Type.Destination:
                    return ConsoleColor.Yellow;

                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
