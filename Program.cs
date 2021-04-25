using System;

namespace Lecture_CSharpPathfinding
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Player player = new Player();

            int size = 25;
            int destY = size - 2;
            int destX = size - 2;

            board.Initialize(size, destY, destX, player);
            player.Initialize(1, 1, board);

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;

            int lastTick = 0;

            // 게임 로직
            while (true)
            {
                #region 프레임 관리
                // FPS 프레임 ( 60 frame ok, 30 frame no )
                // frame : 이 loop가 1초에 실행되는 횟수

                // system 시작 후 경과된 millisecond.
                int currentTick = Environment.TickCount;
                int deltaTick = currentTick - lastTick;
                
                // 경과 시간 < 1/30 second.
                if (deltaTick < WAIT_TICK)
                    continue;
                
                lastTick = currentTick;
                #endregion

                // input

                // logic
                player.Update(deltaTick);

                // rendering
                Console.SetCursorPosition(0, 0);
                board.Render();
            }
        }
    }
}
