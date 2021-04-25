using Lecture_CSharpPathfinding.MakeMaze;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding.PathFinding
{
    class RightHandAlgorithmImpl
    {
        private RightHandAlgorithm _rightHandAlgorithm;
        private Board _board;
        private Player _player;

        public void Initialize(Board board, Player player)
        {
            _rightHandAlgorithm = new RightHandAlgorithm();
            _board = board;
            _player = player;
        }

        public void Finding(List<Position> points) 
        {
            // 목적지에 도착하지 않았으면 반복.
            while (!_board.isDestination(_player.PosY, _player.PosX))
            {
                // 1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인.
                if (MovableRight(_player.PosY, _player.PosX, _player.Dir))
                {
                    // 오른쪽 방향으로 90도 회전.
                    _player.Dir = (int)_rightHandAlgorithm.TurnRight((Direction)_player.Dir);

                    // 앞으로 한 보 전진.
                    MoveFront(_player.Dir, points);
                    
                }
                // 2. 현재 바라보는 방향을 기준으로 전진할 수 있는지 확인.
                else if (MovableFront(_player.PosY, _player.PosX, _player.Dir))
                {
                    // 앞으로 한 보 전진.
                    MoveFront(_player.Dir, points);
                    points.Add(new Position(_player.PosY, _player.PosX));
                }
                else
                {
                    // 왼쪽 방향으로 90도 회전.
                    _player.Dir = (int)_rightHandAlgorithm.TurnLeft((Direction)_player.Dir);
                }
            }
        }

        private void MoveFront(int direction, List<Position> points)
        {
            int y = _player.PosY;
            int x = _player.PosX;
            _rightHandAlgorithm.MoveFront((Direction)direction, ref y, ref x);
            _player.MovePosition(y, x);
            points.Add(new Position(_player.PosY, _player.PosX));
        }

        private bool MovableFront(int y, int x, int direction)
        {
            return _board.Tile[
                        _rightHandAlgorithm.FrontY(y, (Direction)direction)
                        , _rightHandAlgorithm.FrontX(x, (Direction)direction)
                   ] == TileType.Type.Empty;
        }

        private bool MovableRight(int y, int x, int direction)
        {
            return _board.Tile[
                        _rightHandAlgorithm.RightY(y, (Direction)direction)
                        , _rightHandAlgorithm.RightX(x, (Direction)direction)
                   ] == TileType.Type.Empty;
        }
    }
}
