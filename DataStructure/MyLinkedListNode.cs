using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding
{
    class MyLinkedListNode<T>
    {
        public T Data;

        // 해당 Node을 기준으로
        // 앞/뒤 Node의 reference.
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Prev;
    }
}
