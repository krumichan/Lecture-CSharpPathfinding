using Lecture_CSharpPathfinding.PathFinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding
{
    class Player
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        Board _board;

        int _direction = (int)Direction.Up;
        public int Dir
        {
            get { return _direction; }
            set { _direction = value; }
        }

        List<Position> _points;

        public void Initialize(int posY, int posX, Board board)
        {
            PosY = posY;
            PosX = posX;
            
            _board = board;
            
            _points = new List<Position>();

            /*_points.Add(new Position(PosY, PosX));
            RightHandAlgorithmImpl rightHandAlgorithmImpl = new RightHandAlgorithmImpl();
            rightHandAlgorithmImpl.Initialize(_board, this);
            rightHandAlgorithmImpl.Finding(_points);*/

            BFSAlgorithmImpl bfsAlgorithmImpl = new BFSAlgorithmImpl();
            bfsAlgorithmImpl.Initialize(_board, this);
            bfsAlgorithmImpl.Finding(_points);

        }

        const int MOVE_TICK = 10;
        int _sumTick = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
                return;

            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
            }
        }

        public void MovePosition(int y, int x)
        {
            PosY = y;
            PosX = x;
        }
    }
}
