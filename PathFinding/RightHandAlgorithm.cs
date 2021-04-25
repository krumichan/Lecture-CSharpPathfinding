using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding.PathFinding
{
    class RightHandAlgorithm
    {
        private int[] frontY = new int[]
        {
            -1  // Up
            , 0 // Left
            , 1 // Down
            , 0 // Right
        };

        private int[] frontX = new int[]
        {
            0    // Up
            , -1 // Left
            , 0  // Down
            , 1  // Right
        };

        public int[] rightY = new int[]
        {
            0     // Up
            , -1  // Left
            , 0   // Down
            , 1   // Right
        };

        public int[] rightX = new int[]
        {
            1     // Up
            , 0   // Left
            , -1  // Down
            , 0   // Right
        };

        public Direction TurnRight(Direction currentDirection)
        {
            int cur = (int)currentDirection;
            cur = (cur - 1 + 4) % 4;
            return (Direction)cur;
        }

        public Direction TurnLeft(Direction currentDirection)
        {
            int cur = (int)currentDirection;
            cur = (cur + 1 + 4) % 4;
            return (Direction)cur;
        }

        public void MoveFront(Direction direction, ref int y, ref int x)
        {
            y = FrontY(y, direction);
            x = FrontX(x, direction);
        }

        public int FrontY(int y, Direction direction)
        {
            return y + frontY[(int)direction];
        }

        public int FrontX(int x, Direction direction)
        {
            return x + frontX[(int)direction];
        }

        public int RightY(int y, Direction direction)
        {
            return y + rightY[(int)direction];
        }

        public int RightX(int x, Direction direction)
        {
            return x + rightX[(int)direction];
        }
    }
}
