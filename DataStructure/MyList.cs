using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture_CSharpPathfinding
{
    class MyList<T>
    {
        const int DEFAULT_SIZE = 1;

        T[] _data = new T[DEFAULT_SIZE];

        public int Count = 0; // 실제 사용중인 데이터 갯수.
        public int Capacity { get { return _data.Length; } } // 예약된 데이터 갯수.

        // O(1)
        // 예외 케이스 : 이사 비용은 무시 ( if문 부분 )
        public void Add(T item)
        {
            // 공간이 충분한가 확인.
            ValidateDataArray(ref _data, Count, Capacity);

            // 공간에 데이터 삽입.
            _data[Count++] = item;
        }

        // O(1)
        // myList[index] = ## 이런식으로 사용하기 위함.
        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        // index에 따라 복잡도가 달라질 경우, 최악의 경우를 생각한다.
        // O(N)
        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; ++i)
            {
                _data[i] = _data[i + 1];
            }

            _data[(Count--) - 1] = default(T);
        }

        private T[] ValidateDataArray(ref T[] _data, int count, int capacity)
        {
            // 공간이 충분한가 확인.
            if (count >= capacity)
            {
                // 공간이 부족하기 때문에 늘려서 확보.
                T[] newArray = new T[Count * 2];
                for (int i = 0; i < Count; ++i)
                {
                    newArray[i] = _data[i];
                }
                _data = newArray;
            }

            return _data;
        }
    }
}
