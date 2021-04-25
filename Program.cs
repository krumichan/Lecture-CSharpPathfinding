using System;

namespace Lecture_CSharpPathfinding
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Initialize();

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;
            const char CIRCLE = '\u25cf';

            int lastTick = 0;

            // 게임 로직
            while (true)
            {
                #region 프레임 관리
                // FPS 프레임 ( 60 frame ok, 30 frame no )
                // frame : 이 loop가 1초에 실행되는 횟수

                // system 시작 후 경과된 millisecond.
                int currentTick = System.Environment.TickCount;
                /*int elapsedTick = currentTick - lastTick;*/

                // 경과 시간 < 1/30 second.
                if (currentTick - lastTick < WAIT_TICK)
                {
                    continue;
                }
                lastTick = currentTick;
                #endregion

                // input

                // login

                // rendering

                Console.SetCursorPosition(0, 0);

                for (int i = 0; i < 25; ++i)
                {
                    for (int j = 0; j < 25; ++j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(CIRCLE);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
