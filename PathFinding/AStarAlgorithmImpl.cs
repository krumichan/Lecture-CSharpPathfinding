using Lecture_CSharpPathfinding.MakeMaze;
using System;
using System.Collections.Generic;
using System.Text;
using static Lecture_CSharpPathfinding.PathFinding.AStarAlgorithm;

namespace Lecture_CSharpPathfinding.PathFinding
{
    class AStarAlgorithmImpl
    {
        AStarAlgorithm astar;
        private Board _board;
        private Player _player;

        public void Initialize(Board board, Player player)
        {
            astar = new AStarAlgorithm(board.Size);
            _board = board;
            _player = player;

            // 도착 지점 설정.
            astar.SetDestination(_board.DestY, _board.DestX);

            // 초기 시작점 예약.
            astar.SetStart(_player.PosY, _player.PosX);
        }

        public void Finding(List<Position> points)
        {
            while (astar.IsExistsCandidate())
            {
                // 제일 좋은 후보를 탐색.
                PQNode candidate = astar.GetBestCandidate();

                // 동일한 좌표를 여러 경로로 찾아서, 더 빠른 경로로 인해서 이미 방문 (closed) 된 경우 무시.
                if (astar.isClosed(candidate.Y, candidate.X))
                    continue;

                // 방문.
                astar.Visit(candidate.Y, candidate.X);

                // 목적지 도착.
                if (astar.ReachedDestination(candidate.Y, candidate.X))
                    break;

                // 상하좌우 등 이동할 수 있는 좌표인지 확인 후 위치 예약.
                int directionLength = Enum.GetValues(typeof(DirectionEx)).Length;
                for (int direction = 0; direction < directionLength; ++direction)
                {
                    int nextY = astar.NextY(candidate.Y, direction);
                    int nextX = astar.NextX(candidate.X, direction);

                    if (Passing(nextY, nextX))
                        continue;

                    // 비용 계산.
                    int g = astar.CalculationG(candidate.G, direction);
                    int h = astar.CalculationH(nextY, nextX);

                    // 현재 비용이 다른 경로로 도달하는 비용보다 좋지않을 경우.
                    if (!astar.IsFasterThanPrev(nextY, nextX, g + h))
                        continue;

                    // 예약 진행.
                    astar.Reservation(nextY, nextX, g, h);
                    astar.SetParent(nextY, nextX, candidate.Y, candidate.X);
                }
            }

            CompletedPath(points);
        }

        private bool Passing(int nextY, int nextX)
        {
            return
                !_board.MovableTile(nextY, nextX) || // Tile 미존재.
                !MovableNext(nextY, nextX) ||        // 벽.
                astar.AlreadyVisited(nextY, nextX);  // 이미 방문.
        }

        private bool MovableNext(int y, int x)
        {
            return _board.Tile[y, x] != TileType.Type.Wall;
        }

        private void CompletedPath(List<Position> points)
        {
            var completedPath = astar.CompletedPath();
            completedPath.ForEach((point) =>
            {
                points.Add(new Position(point.Y, point.X));
            });
        }
    }
}
