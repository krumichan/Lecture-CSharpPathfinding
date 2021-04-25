using Lecture_CSharpPathfinding.MakeMaze;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding
{
    class Board
    {
        const char CIRCLE = '\u25cf';

        public TileType.Type[,] Tile { get; private set; } // 배열
        public int Size { get; private set; }

        public int DestY { get; private set; }
        public int DestX { get; private set; }

        Player _player;

        public void Initialize(int size, int destY, int destX, Player player)
        {
            if (size % 2 == 0)
            {
                return;
            }

            _player = player;

            Tile = new TileType.Type[size, size];
            Size = size;

            DestY = destY;
            DestX = destX;

            BinaryTreeAlogorithm binaryTreeAlogorithm = new BinaryTreeAlogorithm();
            binaryTreeAlogorithm.make(Tile, Size);
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < Size; ++y)
            {
                for (int x = 0; x < Size; ++x)
                {
                    // 플레이어 좌표를 갖고 와서, 그 좌표랑 현재 y, x가 일치하면 플레이어 전용 색상으로 표시.
                    if (isPlayer(_player, x, y))
                        Console.ForegroundColor = TileType.GetTileColor(TileType.Type.Player);
                    else if (isDestination(y, x))
                        Console.ForegroundColor = TileType.GetTileColor(TileType.Type.Destination);
                    else
                        Console.ForegroundColor = TileType.GetTileColor(Tile[y, x]);

                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        private bool isPlayer(Player _player, int x, int y)
        {
            return y == _player.PosY && x == _player.PosX;
        }

        public bool isDestination(int y, int x)
        {
            return y == DestY && x == DestX;
        }

        public bool MovableTile(int y, int x)
        {
            if (y >= 0 && y < Size && x >= 0 && x < Size)
            {
                return Tile[y, x] == TileType.Type.Empty;
            }

            return false;
        }

        /*// map size는 보통 고정이다. ( 밑 2 data는 보통 맵 만드는데 쓰지 않음. )
        public MyList<int> _data2 = new MyList<int>();  // 동적 배열
        public MyLinkedList<int> _data3 = new MyLinkedList<int>();  // 연결 리스트

        private void InitData3()
        {
            _data3.AddLast(101);
            _data3.AddLast(102);
            MyLinkedListNode<int> node = _data3.AddLast(103);
            _data3.AddLast(104);
            _data3.AddLast(105);

            _data3.Remove(node);
        }

        private void InitData2()
        {
            _data2.Add(101);
            _data2.Add(102);
            _data2.Add(103);
            _data2.Add(104);
            _data2.Add(105);

            int temp = _data2[2];

            _data2.RemoveAt(2);
        }*/
    }
}
