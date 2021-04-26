using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding.PathFinding
{
    class BFSAlgorithm
    {
        public class Point
        {
            public Point(int y, int x) { Y = y; X = x; }
            public int X;
            public int Y;
        }

        bool[,] found;
        Point[,] parent;

        Queue<Point> queue = new Queue<Point>();
        
        public BFSAlgorithm(int size, int startY, int startX)
        {
            found = new bool[size, size];
            parent = new Point[size, size];
            Initialize(startY, startX);
        }

        private void Initialize(int y, int x)
        {
            queue.Enqueue(new Point(y, x));
            found[y, x] = true;
            parent[y, x] = new Point(y, x);
        }

        int[] deltaY = new int[]
        {
            -1   // Up
            , 0  // Left
            , 1  // Down
            , 0  // Right
        };

        int[] deltaX = new int[]
        {
            0    // Up
            , -1 // Left
            , 0  // Down
            , 1  // Right
        };

        public bool HasReserved()
        {
            return queue.Count > 0;
        }

        public int NextY(int y, int direction)
        {
            return y + deltaY[direction];
        }

        public int NextX(int x, int direction)
        {
            return x + deltaX[direction];
        }

        public Point HeadPoint()
        {
            return queue.Dequeue();
        }

        public bool AlreadyVisited(int y, int x)
        {
            return found[y, x];
        }

        public void ReserveAndSetParent(int nextY, int nextX ,int nowY, int nowX)
        {
            ReservePoint(nextY, nextX);
            SetParent(nextY, nextX, nowY, nowX);
        }

        private void ReservePoint(int nextY, int nextX)
        {
            queue.Enqueue(new Point(nextY, nextX));
            found[nextY, nextX] = true;
        }

        private void SetParent(int nextY, int nextX, int nowY, int nowX)
        {
            parent[nextY, nextX] = new Point(nowY, nowX);
        }

        public List<Point> CompletedPath(int destY, int destX)
        {
            List<Point> points = new List<Point>();
            int curY = destY;
            int curX = destX;

            // Destination -> Start 추적.
            while (!IsStartPoint(curY, curX))
            {
                points.Add(new Point(curY, curX));
                Point point = parent[curY, curX];
                curY = point.Y;
                curX = point.X;
            }

            // 시작점 삽입. ( 위에서 추가되지 않음. )
            points.Add(new Point(curY, curX));
            
            // List 순서를 Start -> Destination으로 변환.
            points.Reverse();

            return points;
        }

        private bool IsStartPoint(int y, int x)
        {
            return parent[y, x].Y == y && parent[y, x].X == x;
        }
    }
}
