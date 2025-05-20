// ======================================================================================================
// File Name        : RingArray.cs
// Project          : CSUtil
// Last Update      : 2025.05.20 - yc.jeon
// ======================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace CSUtil
{
    public class RingArray<T>
    {
        /// <summary>
        /// 현재 Ring의 인덱스 번호
        /// </summary>
        public int CurrentIndex { get => _curIndex; }
        private int _curIndex;
        /// <summary>
        /// 배열의 크기
        /// </summary>
        public int Size { get; }
        /// <summary>
        /// 인덱스 탐색 (Ring 기준으로 탐색함)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T this[int index]
        {
            get
            {
                int realIndex = CalcIndex(index);
                if (realIndex == -1)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return _datas[realIndex];
            }
        }
        /// <summary>
        /// 실제 데이터 배열
        /// </summary>
        private T[] _datas;
        /// <summary>
        /// Lock Object
        /// </summary>
        private object DataLock { get; } = new object();
        /// <summary>
        /// Ring을 한 바퀴 이상 돌았는지 유무
        /// </summary>
        private bool _isOverFlow;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="size">배열의 크기</param>
        /// <exception cref="ArgumentException">배열의 크기가 0보다 작거나 같을 때</exception>
        public RingArray(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Array size must always be greater than 0.", nameof(size));
            }
            _datas = new T[size];
            Size = size;
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="datas">복사할 원본 배열</param>
        /// <exception cref="ArgumentNullException">복사할 배열이 null인 경우</exception>
        /// <exception cref="ArgumentException">복사할 배열의 크기가 0일 때</exception>
        public RingArray(T[] datas)
        {
            if (datas == null)
            {
                throw new ArgumentNullException(nameof(datas));
            }
            if (datas.Length == 0)
            {
                throw new ArgumentException("Array size must always be greater than 0.", nameof(datas));
            }
            _datas = new T[_datas.Length];
            Size = _datas.Length;
            System.Array.Copy(datas, _datas, _datas.Length);
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="datas">복사할 IEnumerable</param>
        /// <exception cref="ArgumentNullException">복사할 IEnumerable이 null인 경우</exception>
        /// <exception cref="ArgumentException">복사할 IEnumerable의 내부 Element가 0개인 경우</exception>
        public RingArray(IEnumerable<T> datas)
        {
            if (datas == null)
            {
                throw new ArgumentNullException(nameof(datas));
            }
            if (!datas.Any())
            {
                throw new ArgumentException("Array size must always be greater than 0.", nameof(datas));
            }
            _datas = datas.ToArray();
            Size = _datas.Length;
        }
        /// <summary>
        /// 데이터를 배열에 추가하는 함수
        /// </summary>
        /// <param name="value">추가할 데이터</param>
        public void Add(T value)
        {
            lock (DataLock)
            {
                _datas[_curIndex] = value;
                ++_curIndex;

                if (_curIndex >= Size)
                {
                    _curIndex = 0;
                    _isOverFlow = true;
                }
            }
        }
        /// <summary>
        /// 배열을 초기화 하는 함수
        /// </summary>
        public void Clear()
        {
            lock (DataLock)
            {
                _datas = new T[Size];
                _curIndex = 0;
                _isOverFlow = false;
            }
        }
        /// <summary>
        /// Ring 배열을 정렬된 배열(일자로 편 배열)로 반환하는 함수
        /// </summary>
        /// <returns></returns>
        public T[] GetSortedArray()
        {
            lock (DataLock)
            {
                int size = _isOverFlow ? Size : _curIndex;
                T[] sortedArray = new T[size];
                if (_isOverFlow)
                {
                    System.Array.Copy(_datas, _curIndex, sortedArray, 0, size - _curIndex);
                    if (_curIndex > 0)
                    {
                        System.Array.Copy(_datas, 0, sortedArray, size - _curIndex, _curIndex);
                    }
                }
                else
                {
                    System.Array.Copy(_datas, sortedArray, size);
                }

                return sortedArray;
            }
        }

        private int CalcIndex(int index)
        {
            if (index >= Size)
            {
                return -1;
            }

            int realIndex;
            if (_isOverFlow)
            {
                realIndex = index + _curIndex;
                if (realIndex >= Size)
                {
                    realIndex -= Size;
                }
            }
            else
            {
                realIndex = index;
            }

            return realIndex;
        }
    }
}
