using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding
{
    class Board
    {
        public int[] _data = new int[25]; // 배열

        // map size는 보통 고정이다. ( 밑 2 data는 보통 맵 만드는데 쓰지 않음. )
        public MyList<int> _data2 = new MyList<int>();  // 동적 배열
        public MyLinkedList<int> _data3 = new MyLinkedList<int>();  // 연결 리스트

        public void Initialize()
        {
            InitData3();
        }

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
        }
    }
}
