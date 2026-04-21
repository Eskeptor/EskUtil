// ======================================================================================================
// File Name        : Array.cs
// Project          : CSUtil
// Last Update      : 2026.04.21 - yc.jeon (Eskeptor)
// ======================================================================================================

using System;

namespace Esk.GearForge.CSUtil
{
    public static class Array
    {
#if !NET5_0_OR_GREATER
        /// <summary>
        /// 배열의 값을 특정 값으로 채우는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">배열</param>
        /// <param name="value">채워질 값</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">array가 null인 경우</exception>
        public static T[] Fill<T>(this T[] array, T value)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = value;
            }
            return array;
        }
#endif

        /// <summary>
        /// Byte 배열 두개를 하나로 합치는 함수
        /// </summary>
        /// <param name="first">앞쪽에 들어올 배열</param>
        /// <param name="second">뒤쪽에 들어올 배열</param>
        /// <returns></returns>
        public static byte[] Merge(byte[] first, byte[] second)
        {
            if (first == null)
            {
                return second;
            }
            else if (second == null)
            {
                return first;
            }

            byte[] mergeArray = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, mergeArray, 0, first.Length);
            Buffer.BlockCopy(second, 0, mergeArray, first.Length, second.Length);
            return mergeArray;
        }
        /// <summary>
        /// Byte 배열 두개를 하나로 합치는 함수
        /// </summary>
        /// <param name="first">앞쪽에 들어올 배열</param>
        /// <param name="firstLength">앞쪽에 들어올 배열의 크기</param>
        /// <param name="second">뒤쪽에 들어올 배열</param>
        /// <param name="secondLength">뒤쪽에 들어올 배열의 크기</param>
        /// <returns></returns>
        public static byte[] Merge(byte[] first, int firstLength, byte[] second, int secondLength)
        {
            bool isErrFirst = first == null || firstLength <= 0;
            bool isErrSecond = second == null || secondLength <= 0;
            if (isErrFirst &&
                isErrSecond)
            {
                return null;
            }
            else if (isErrFirst)
            {
                byte[] array = new byte[secondLength];
                Buffer.BlockCopy(second, 0, array, 0, secondLength);
                return array;
            }
            else if (isErrSecond)
            {
                byte[] array = new byte[firstLength];
                Buffer.BlockCopy(first, 0, array, 0, firstLength);
                return array;
            }

            byte[] mergeArray = new byte[firstLength + secondLength];
            Buffer.BlockCopy(first, 0, mergeArray, 0, firstLength);
            Buffer.BlockCopy(second, 0, mergeArray, firstLength, secondLength);
            return mergeArray;
        }
    }
}