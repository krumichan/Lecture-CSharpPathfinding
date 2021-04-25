using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding
{
    class MyLinkedList<T>
    {
        public MyLinkedListNode<T> Head = null; // first Room.
        public MyLinkedListNode<T> Tail = null; // last Room.

        public int Count = 0;

        // O(1)
        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newNode = new MyLinkedListNode<T>();

            // first Node가 null일 경우.
            if (Head == null)
            {
                Head = newNode;
            }

            // last Node가 이미 존재할 경우.
            if (Tail != null)
            {
                // last Node를 교체.
                Tail.Next = newNode;
                newNode.Prev = Tail;
            }

            Tail = newNode;
            Count++;

            return newNode;
        }

        public void Remove(MyLinkedListNode<T> node)
        {
            // room이 first Node일 경우.
            if (Head == node)
            {
                // first Node의 다음 Node가 first가 된다.
                Head = Head.Next;
            }

            // node이 last Node일 경우.
            if (Tail == node)
            {
                // last Node의 다음 Node가 last가 된다.
                Tail = Tail.Prev;
            }


            // (node prev)   (node)   (node next)
            // 前: (node prev).next = (node)
            // 後: (node prev).next = (node next)
            if (node.Prev != null)
            {
                node.Prev.Next = node.Next;
            }

            // (node prev)   (node)   (node next)
            // 前: (node.next).prev = (node)
            // 後: (node.next).prev = (node prev) 
            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
            }
        }
    }
}
