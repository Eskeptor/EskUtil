// ======================================================================================================
// File Name        : EnumUtil.cs
// Project          : CSUtil
// Last Update      : 2025.05.19 - yc.jeon
// ======================================================================================================

using System;
using System.Runtime.CompilerServices;

namespace CSUtil
{
    /// <summary>
    /// Enum 관련 유틸리티
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// Enum 값을 Int 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">변환할 Enum</param>
        /// <returns>변환된 Int 값</returns>
        public static int EnumToInt<T>(T e) where T : struct, Enum
        {
            return Unsafe.As<T, int>(ref e);
        }

        /// <summary>
        /// Int 값을 Enum 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">변환할 Int</param>
        /// <returns>변환된 Enum 값</returns>
        public static T IntToEnum<T>(int value) where T : struct, Enum
        {
            return Unsafe.As<int, T>(ref value);
        }

        /// <summary>
        /// Enum 값을 UInt 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">변환할 Enum</param>
        /// <returns>변환된 UInt 값</returns>
        public static uint EnumToUInt<T>(T e) where T : struct, Enum
        {
            return Unsafe.As<T, uint>(ref e);
        }

        /// <summary>
        /// UInt 값을 Enum 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">변환할 UInt</param>
        /// <returns>변환된 Enum 값</returns>
        public static T UIntToEnum<T>(uint value) where T : struct
        {
            return Unsafe.As<uint, T>(ref value);
        }

        /// <summary>
        /// Enum 값을 Long 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">변환할 Enum</param>
        /// <returns>변환된 Long 값</returns>
        public static long EnumToLong<T>(T e) where T : struct, Enum
        {
            return Unsafe.As<T, long>(ref e);
        }

        /// <summary>
        /// Long 값을 Enum 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">변환할 Long</param>
        /// <returns>변환된 Enum 값</returns>
        public static T LongToEnum<T>(long value) where T : struct
        {
            return Unsafe.As<long, T>(ref value);
        }

        /// <summary>
        /// Enum 값을 ULong 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">변환할 Enum</param>
        /// <returns>변환된 ULong 값</returns>
        public static ulong EnumToULong<T>(T e) where T : struct, Enum
        {
            return Unsafe.As<T, ulong>(ref e);
        }

        /// <summary>
        /// ULong 값을 Enum 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">변환할 ULong</param>
        /// <returns>변환된 Enum 값</returns>
        public static T ULongToEnum<T>(ulong value) where T : struct
        {
            return Unsafe.As<ulong, T>(ref value);
        }

        public static T StringToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
