using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> _heap = new List<T>();

        // O(logN)
        public void Push(T data)
        {
            // heap의 맨 끝에 새로운 데이터 삽입.
            _heap.Add(data);

            int now = _heap.Count - 1;

            // 비교 시작
            while (now > 0)
            {
                int next = (now - 1) / 2;
                
                // 비교 결과 위보다 now가 더 작은 경우.
                if (_heap[now].CompareTo(_heap[next]) < 0)
                    break;

                // now와 next의 자리를 변경.
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // now 값을 교체.
                now = next;
            }
        }

        // O(logN)
        public T Pop()
        {
            // 반환할 데이터 저장.
            T ret = _heap[0];

            // 마지막 데이터를 root로 이동.
            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            lastIndex--;

            // 역으로 내려가며 비교한다.
            int now = 0;
            while (true)
            {
                int left = (2 * now) + 1;
                int right = (2 * now) + 2;

                int next = now;

                // 왼쪽이 현재 값보다 클 경우 왼쪽으로 이동.
                if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                    next = left;

                // 오른쪽 값이 현재 값(왼쪽 이동 값 포함)보다 클 경우 오른쪽으로 이동.
                if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                    next = right;

                // 왼쪽/오른쪽 모두 현재 값보다 작은 경우.
                if (next == now)
                    break;

                // 두 값을 교체.
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // 현재 위치를 변경.
                now = next;
            }

            return ret;
        }

        public int Count { get { return _heap.Count; } }
    }
}
