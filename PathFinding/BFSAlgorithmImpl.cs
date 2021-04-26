using Lecture_CSharpPathfinding.MakeMaze;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding.PathFinding
{
    class BFSAlgorithmImpl
    {
        private BFSAlgorithm bfsAlgorithm;
        private Board _board;
        private Player _player;

        public void Initialize(Board board, Player player)
        {
            bfsAlgorithm = new BFSAlgorithm(board.Size, player.PosY, player.PosX);
            _board = board;
            _player = player;
        }

        public void Finding(List<Position> points)
        {
            while (bfsAlgorithm.HasReserved())
            {
                var position = bfsAlgorithm.HeadPoint();
                int nowY = position.Y;
                int nowX = position.X;

                int directionLength = Enum.GetValues(typeof(Direction)).Length;
                for (int direction = 0; direction < directionLength; ++direction)
                {
                    int nextY = bfsAlgorithm.NextY(nowY, direction);
                    int nextX = bfsAlgorithm.NextX(nowX, direction);

                    if (Passing(nextY, nextX))
                        continue;

                    bfsAlgorithm.ReserveAndSetParent(nextY, nextX, nowY, nowX);
                }
            }

            CompletedPath(points);
        }

        private bool Passing(int nextY, int nextX)
        {
            return
                !_board.MovableTile(nextY, nextX) ||        // Tile 미존재.
                !MovableNext(nextY, nextX) ||               // 벽.
                bfsAlgorithm.AlreadyVisited(nextY, nextX);  // 이미 방문.
        }

        private void CompletedPath(List<Position> points)
        {
            int y = _board.DestY;
            int x = _board.DestX;

            var completedPath = bfsAlgorithm.CompletedPath(_board.DestY, _board.DestX);
            completedPath.ForEach((point) =>
            {
                points.Add(new Position(point.Y, point.X));
            });
        }

        private bool MovableNext(int y, int x)
        {
            return _board.Tile[y, x] != TileType.Type.Wall;
        }
    }
}
