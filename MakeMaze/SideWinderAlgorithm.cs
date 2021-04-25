using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding.MakeMaze
{
    class SideWinderAlgorithm
    {
        public void make(TileType.Type[,] tiles, int size)
        {
            // 일단 길을 다 막아버림.
            Initialize(tiles, size);

            // 랜덤으로 우측 또는 아래의 길을 뚫는 작업.
            makeRandomPath(tiles, size);
        }
        private void Initialize(TileType.Type[,] tiles, int size)
        {
            for (int y = 0; y < size; ++y)
            {
                for (int x = 0; x < size; ++x)
                {
                    if (isFixedWall(x, y))
                    {
                        tiles[y, x] = TileType.Type.Wall;
                    }
                    else
                    {
                        tiles[y, x] = TileType.Type.Empty;
                    }
                }
            }
        }

        private void makeRandomPath(TileType.Type[,] tiles, int size)
        {
            Random rand = new Random();
            for (int y = 0; y < size; ++y)
            {
                int count = 1;
                for (int x = 0; x < size; ++x)
                {
                    if (isFixedWall(x, y)) continue;
                    if (isLastTileAxis(x, y, size)) continue;

                    if (isLastTileOfAxisY(y, size))
                    {
                        tiles[y, x + 1] = TileType.Type.Empty;
                        continue;
                    }

                    if (isLastTileOfAxisX(x, size))
                    {
                        tiles[y + 1, x] = TileType.Type.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        tiles[y, x + 1] = TileType.Type.Empty;
                        count++;
                    }
                    else
                    {
                        int randomIndex = rand.Next(0, count);
                        tiles[y + 1, x - randomIndex * 2] = TileType.Type.Empty;
                        count = 1;
                    }
                }
            }
        }

        private bool isFixedWall(int x, int y)
        {
            return x % 2 == 0 || y % 2 == 0;
        }

        private bool isLastTileAxis(int x, int y, int size)
        {
            return isLastTileOfAxisY(y, size) && isLastTileOfAxisX(x, size);
        }

        private bool isLastTileOfAxisX(int x, int size)
        {
            return x == size - 2;
        }

        private bool isLastTileOfAxisY(int y, int size)
        {
            return y == size - 2;
        }
    }
}
