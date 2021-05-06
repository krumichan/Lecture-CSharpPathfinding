using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Lecture_CSharpPathfinding.PathFinding
{
    class AStarAlgorithm
    {
        public class Point
        {
            public Point(int y, int x) { Y = y; X = x; }
            public int X;
            public int Y;
        }

        public enum DirectionEx
        {
            Up = 0
            , Left = 1
            , Down = 2
            , Right = 3
            , UpLeft = 4
            , DownLeft = 5
            , DownRight = 6
            , UpRight = 7
        }

        // 점수 매기기
        // F = G + H
        // F: 최종 점수 ( 작을수록 좋음, 경로에 따라 다름 )
        // G: 시작점에서 해당 좌표까지 이동하는데 드는 비용 ( 작을수록 좋음, 경로에 따라 다름 )
        // H: 목적지에서 얼마나 가까운가 ( 작을수록 좋음, 고정값. )
        public struct PQNode : IComparable<PQNode>
        {
            public int F;
            public int G;
            public int Y;
            public int X;
            // H = G - F

            public int CompareTo([AllowNull] PQNode other)
            {
                if (F == other.F)
                    return 0;

                return F < other.F ? 1 : -1;
            }
        }

        class PriorityQueue<T> where T : IComparable<T>
        {
            List<T> _heap = new List<T>();

            // O(logN)
            public void Push(T data)
            {
                // heap의 맨 끝에 새로운 데이터 삽입.
                _heap.Add(data);

                int now = _heap.Count - 1;

                // 비교 시작
                while (now > 0)
                {
                    int next = (now - 1) / 2;

                    // 비교 결과 위보다 now가 더 작은 경우.
                    if (_heap[now].CompareTo(_heap[next]) < 0)
                        break;

                    // now와 next의 자리를 변경.
                    T temp = _heap[now];
                    _heap[now] = _heap[next];
                    _heap[next] = temp;

                    // now 값을 교체.
                    now = next;
                }
            }

            // O(logN)
            public T Pop()
            {
                // 반환할 데이터 저장.
                T ret = _heap[0];

                // 마지막 데이터를 root로 이동.
                int lastIndex = _heap.Count - 1;
                _heap[0] = _heap[lastIndex];
                _heap.RemoveAt(lastIndex);
                lastIndex--;

                // 역으로 내려가며 비교한다.
                int now = 0;
                while (true)
                {
                    int left = (2 * now) + 1;
                    int right = (2 * now) + 2;

                    int next = now;

                    // 왼쪽이 현재 값보다 클 경우 왼쪽으로 이동.
                    if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                        next = left;

                    // 오른쪽 값이 현재 값(왼쪽 이동 값 포함)보다 클 경우 오른쪽으로 이동.
                    if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                        next = right;

                    // 왼쪽/오른쪽 모두 현재 값보다 작은 경우.
                    if (next == now)
                        break;

                    // 두 값을 교체.
                    T temp = _heap[now];
                    _heap[now] = _heap[next];
                    _heap[next] = temp;

                    // 현재 위치를 변경.
                    now = next;
                }

                return ret;
            }

            public int Count { get { return _heap.Count; } }
        }

        int[] deltaY = new int[]
        {
            -1   // Up
            , 0  // Left
            , 1  // Down
            , 0  // Right
            , -1 // Up Left
            , 1  // Down Left
            , 1  // Down Right
            , -1 // Up Right
        };

        int[] deltaX = new int[]
        {
            0    // Up
            , -1 // Left
            , 0  // Down
            , 1  // Right
            , -1 // Up Left
            , -1 // Down Left
            , 1  // Down Right
            , 1  // Up Right
        };

        int[] cost = new int[]
        {
            10    // Up
            , 10  // Left
            , 10  // Down
            , 10  // Right
            , 14  // Up Left
            , 14  // Down Left
            , 14  // Down Right
            , 14  // Up Right
        };

        bool[,] closed;
        int[,] open;  // F value.
        Point[,] parent;

        int size;

        int destinationY;
        int destinationX;

        int startY;
        int startX;

        // open에 있는 정보들 중에서, 가장 좋은 후보를 빠르게 호출하기 위한 도구.
        PriorityQueue<PQNode> pq;

        public AStarAlgorithm(int size)
        {
            this.size = size;

            // (y, x) 방문 여부 확인 ( 방문 = closed 상태 )
            closed = new bool[size, size];
            parent = new Point[size, size];
            pq = new PriorityQueue<PQNode>();

            // (y, x) 가는 길을 한 번이라도 발견하였는가.
            // 발견X ⇒ MaxValue
            // 발견O ⇒ F = G + H
            open = new int[size, size];
            for (int y = 0; y < size; ++y)
                for (int x = 0; x < size; ++x)
                    open[y, x] = Int32.MaxValue;
        }

        public int CalculationH(int pointY, int pointX)
        {
            return (Math.Abs(destinationY - pointY) + Math.Abs(destinationX - pointX)) * 10;
        }

        public int CalculationG(int g, int direction)
        {
            return g + cost[direction];
        }

        public void SetDestination(int destY, int destX)
        {
            destinationY = destY;
            destinationX = destX;
        }

        public void SetSize(int size)
        {
            this.size = size;
        }

        public void SetStart(int startY, int startX)
        {
            parent = new Point[size, size];

            this.startY = startY;
            this.startX = startX;

            SetParent(startY, startX, startY, startX);

            // G: 예약하는 좌표가 시작점으로 0.
            Reservation(startY, startX, 0, CalculationH(startY, startX));
        }

        public void SetParent(int nextY, int nextX, int pointY, int pointX)
        {
            parent[nextY, nextX] = new Point(pointY, pointX);
        }

        public int NextY(int y, int direction)
        {
            return y + deltaY[direction];
        }

        public int NextX(int x, int direction)
        {
            return x + deltaX[direction];
        }

        public void Reservation(int pointY, int pointX, int g, int h)
        {
            OpenPoint(pointY, pointX, g, h);
            pq.Push(new PQNode()
            {
                F = g + h
                , G = g
                , Y = pointY
                , X = pointX
            });
        }

        private void OpenPoint(int pointY, int pointX, int g, int h)
        {
            open[pointY, pointX] = g + h;
        }

        public PQNode GetBestCandidate()
        {
            return pq.Pop();
        }

        public bool IsExistsCandidate()
        {
            return pq.Count > 0;
        }

        public bool isClosed(int pointY, int pointX)
        {
            return closed[pointY, pointX];
        }

        public void Visit(int pointY, int pointX)
        {
            closed[pointY, pointX] = true;
        }

        public bool ReachedDestination(int pointY, int pointX)
        {
            return destinationY == pointY && destinationX == pointX;
        }

        public bool AlreadyVisited(int pointY, int pointX)
        {
            return closed[pointY, pointX];
        }

        public bool IsFasterThanPrev(int pointY, int pointX, int f)
        {
            {
                return open[pointY, pointX] >= f;
            }
        }

        public List<Point> CompletedPath()
        {
            List<Point> points = new List<Point>();
            int curY = destinationY;
            int curX = destinationX;

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
