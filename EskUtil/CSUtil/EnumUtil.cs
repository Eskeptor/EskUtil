// ======================================================================================================
// File Name        : EnumUtil.cs
// Project          : CSUtil
// Last Update      : 2026.04.21 - yc.jeon (Eskeptor)
// ======================================================================================================

using System;
using System.Runtime.CompilerServices;

namespace Esk.GearForge.CSUtil
{
    /// <summary>
    /// Enum 관련 유틸리티
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// 타입의 종류
        /// </summary>
        private enum UnderlyingTypeKind : byte
        {
            SByte,
            Byte,
            Short,
            UShort,
            Int,
            UInt,
            Long,
            ULong
        }
        /// <summary>
        /// 캐싱 타입
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private static class Underlying<T> where T : struct, Enum
        {
            /// <summary>
            /// 원본 타입
            /// </summary>
            public static readonly Type Type;
            /// <summary>
            /// Switch문을 위한 데이터 타입
            /// </summary>
            public static readonly UnderlyingTypeKind Kind;

            static Underlying()
            {
                Type = Enum.GetUnderlyingType(typeof(T));
                if (Type == typeof(sbyte))
                {
                    Kind = UnderlyingTypeKind.SByte;
                }
                else if (Type == typeof(byte))
                {
                    Kind = UnderlyingTypeKind.Byte;
                }
                else if (Type == typeof(short))
                {
                    Kind = UnderlyingTypeKind.Short;
                }
                else if (Type == typeof(ushort))
                {
                    Kind = UnderlyingTypeKind.UShort;
                }
                else if (Type == typeof(int))
                {
                    Kind = UnderlyingTypeKind.Int;
                }
                else if (Type == typeof(uint))
                {
                    Kind = UnderlyingTypeKind.UInt;
                }
                else if (Type == typeof(long))
                {
                    Kind = UnderlyingTypeKind.Long;
                }
                else if (Type == typeof(ulong))
                {
                    Kind = UnderlyingTypeKind.ULong;
                }
                else
                {
                    throw new NotSupportedException("Unsupported enum underlying type.");
                }
            }
        }

        /// <summary>
        /// 타입을 기준으로 캐싱타입을 반환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Type GetUnderlyingType<T>() where T : struct, Enum
        {
            return Underlying<T>.Type;
        }

        /// <summary>
        /// Enum 값을 Int 값으로 변환하는 함수
        /// </summary>
        /// <remarks>
        /// [MOD][2025.12.01 - yc.jeon] 입력된 타입에 따른 변환 추가 <br/>
        /// </remarks>
        public static int EnumToInt<T>(T e) where T : struct, Enum
        {
            switch (Underlying<T>.Kind)
            {
                case UnderlyingTypeKind.Int:
                    return Unsafe.As<T, int>(ref e);
                case UnderlyingTypeKind.UInt:
                    return unchecked((int)Unsafe.As<T, uint>(ref e));
                case UnderlyingTypeKind.Short:
                    return Unsafe.As<T, short>(ref e);
                case UnderlyingTypeKind.UShort:
                    return Unsafe.As<T, ushort>(ref e);
                case UnderlyingTypeKind.SByte:
                    return Unsafe.As<T, sbyte>(ref e);
                case UnderlyingTypeKind.Byte:
                    return Unsafe.As<T, byte>(ref e);
                case UnderlyingTypeKind.Long:
                    return unchecked((int)Unsafe.As<T, long>(ref e));
                case UnderlyingTypeKind.ULong:
                    return unchecked((int)Unsafe.As<T, ulong>(ref e));
                default:
                    throw new NotSupportedException("Unsupported enum underlying type.");
            }
        }

        /// <summary>
        /// Int 값을 Enum 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">변환할 Int</param>
        /// <returns>변환된 Enum 값</returns>
        /// <remarks>
        /// [MOD][2025.12.01 - yc.jeon] 입력된 타입에 따른 변환 추가 <br/>
        /// </remarks>
        public static T IntToEnum<T>(int value) where T : struct, Enum
        {
            switch (Underlying<T>.Kind)
            {
                case UnderlyingTypeKind.Int:
                    return Unsafe.As<int, T>(ref value);
                case UnderlyingTypeKind.UInt:
                    {
                        uint v = unchecked((uint)value);
                        return Unsafe.As<uint, T>(ref v);
                    }
                case UnderlyingTypeKind.Short:
                    {
                        short v = unchecked((short)value);
                        return Unsafe.As<short, T>(ref v);
                    }
                case UnderlyingTypeKind.UShort:
                    {
                        ushort v = unchecked((ushort)value);
                        return Unsafe.As<ushort, T>(ref v);
                    }
                case UnderlyingTypeKind.SByte:
                    {
                        sbyte v = unchecked((sbyte)value);
                        return Unsafe.As<sbyte, T>(ref v);
                    }
                case UnderlyingTypeKind.Byte:
                    {
                        byte v = unchecked((byte)value);
                        return Unsafe.As<byte, T>(ref v);
                    }
                case UnderlyingTypeKind.Long:
                    {
                        long v = value;
                        return Unsafe.As<long, T>(ref v);
                    }
                case UnderlyingTypeKind.ULong:
                    {
                        ulong v = unchecked((ulong)(uint)value);
                        return Unsafe.As<ulong, T>(ref v);
                    }
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Enum 값을 UInt 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">변환할 Enum</param>
        /// <returns>변환된 UInt 값</returns>
        /// <remarks>
        /// [MOD][2025.12.01 - yc.jeon] 입력된 타입에 따른 변환 추가 <br/>
        /// </remarks>
        public static uint EnumToUInt<T>(T e) where T : struct, Enum
        {
            switch (Underlying<T>.Kind)
            {
                case UnderlyingTypeKind.UInt:
                    return Unsafe.As<T, uint>(ref e);
                case UnderlyingTypeKind.Int:
                    return unchecked((uint)Unsafe.As<T, int>(ref e));
                case UnderlyingTypeKind.UShort:
                    return Unsafe.As<T, ushort>(ref e);
                case UnderlyingTypeKind.Short:
                    return unchecked((uint)Unsafe.As<T, short>(ref e));
                case UnderlyingTypeKind.Byte:
                    return Unsafe.As<T, byte>(ref e);
                case UnderlyingTypeKind.SByte:
                    return unchecked((uint)Unsafe.As<T, sbyte>(ref e));
                case UnderlyingTypeKind.ULong:
                    return unchecked((uint)Unsafe.As<T, ulong>(ref e));
                case UnderlyingTypeKind.Long:
                    return unchecked((uint)Unsafe.As<T, long>(ref e));
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// UInt 값을 Enum 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">변환할 UInt</param>
        /// <returns>변환된 Enum 값</returns>
        /// <remarks>
        /// [MOD][2025.12.01 - yc.jeon] 입력된 타입에 따른 변환 추가 <br/>
        /// </remarks>
        public static T UIntToEnum<T>(uint value) where T : struct, Enum
        {
            switch (Underlying<T>.Kind)
            {
                case UnderlyingTypeKind.UInt:
                    return Unsafe.As<uint, T>(ref value);
                case UnderlyingTypeKind.Int:
                    {
                        int v = unchecked((int)value);
                        return Unsafe.As<int, T>(ref v);
                    }
                case UnderlyingTypeKind.UShort:
                    {
                        ushort v = unchecked((ushort)value);
                        return Unsafe.As<ushort, T>(ref v);
                    }
                case UnderlyingTypeKind.Short:
                    {
                        short v = unchecked((short)value);
                        return Unsafe.As<short, T>(ref v);
                    }
                case UnderlyingTypeKind.Byte:
                    {
                        byte v = unchecked((byte)value);
                        return Unsafe.As<byte, T>(ref v);
                    }
                case UnderlyingTypeKind.SByte:
                    {
                        sbyte v = unchecked((sbyte)value);
                        return Unsafe.As<sbyte, T>(ref v);
                    }
                case UnderlyingTypeKind.ULong:
                    {
                        ulong v = value;
                        return Unsafe.As<ulong, T>(ref v);
                    }
                case UnderlyingTypeKind.Long:
                    {
                        long v = unchecked((long)(ulong)value);
                        return Unsafe.As<long, T>(ref v);
                    }
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Enum 값을 Long 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">변환할 Enum</param>
        /// <returns>변환된 Long 값</returns>
        /// <remarks>
        /// [MOD][2025.12.01 - yc.jeon] 입력된 타입에 따른 변환 추가 <br/>
        /// </remarks>
        public static long EnumToLong<T>(T e) where T : struct, Enum
        {
            switch (Underlying<T>.Kind)
            {
                case UnderlyingTypeKind.Long:
                    return Unsafe.As<T, long>(ref e);
                case UnderlyingTypeKind.ULong:
                    return unchecked((long)Unsafe.As<T, ulong>(ref e));
                case UnderlyingTypeKind.Int:
                    return Unsafe.As<T, int>(ref e);
                case UnderlyingTypeKind.UInt:
                    return Unsafe.As<T, uint>(ref e);
                case UnderlyingTypeKind.Short:
                    return Unsafe.As<T, short>(ref e);
                case UnderlyingTypeKind.UShort:
                    return Unsafe.As<T, ushort>(ref e);
                case UnderlyingTypeKind.SByte:
                    return Unsafe.As<T, sbyte>(ref e);
                case UnderlyingTypeKind.Byte:
                    return Unsafe.As<T, byte>(ref e);
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Long 값을 Enum 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">변환할 Long</param>
        /// <returns>변환된 Enum 값</returns>
        /// <remarks>
        /// [MOD][2025.12.01 - yc.jeon] 입력된 타입에 따른 변환 추가 <br/>
        /// </remarks>
        public static T LongToEnum<T>(long value) where T : struct, Enum
        {
            switch (Underlying<T>.Kind)
            {
                case UnderlyingTypeKind.Long:
                    return Unsafe.As<long, T>(ref value);
                case UnderlyingTypeKind.ULong:
                    {
                        ulong v = unchecked((ulong)value);
                        return Unsafe.As<ulong, T>(ref v);
                    }
                case UnderlyingTypeKind.Int:
                    {
                        int v = unchecked((int)value);
                        return Unsafe.As<int, T>(ref v);
                    }
                case UnderlyingTypeKind.UInt:
                    {
                        uint v = unchecked((uint)value);
                        return Unsafe.As<uint, T>(ref v);
                    }
                case UnderlyingTypeKind.Short:
                    {
                        short v = unchecked((short)value);
                        return Unsafe.As<short, T>(ref v);
                    }
                case UnderlyingTypeKind.UShort:
                    {
                        ushort v = unchecked((ushort)value);
                        return Unsafe.As<ushort, T>(ref v);
                    }
                case UnderlyingTypeKind.SByte:
                    {
                        sbyte v = unchecked((sbyte)value);
                        return Unsafe.As<sbyte, T>(ref v);
                    }
                case UnderlyingTypeKind.Byte:
                    {
                        byte v = unchecked((byte)value);
                        return Unsafe.As<byte, T>(ref v);
                    }
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Enum 값을 ULong 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">변환할 Enum</param>
        /// <returns>변환된 ULong 값</returns>
        /// <remarks>
        /// [MOD][2025.12.01 - yc.jeon] 입력된 타입에 따른 변환 추가 <br/>
        /// </remarks>
        public static ulong EnumToULong<T>(T e) where T : struct, Enum
        {
            switch (Underlying<T>.Kind)
            {
                case UnderlyingTypeKind.ULong:
                    return Unsafe.As<T, ulong>(ref e);
                case UnderlyingTypeKind.Long:
                    return unchecked((ulong)Unsafe.As<T, long>(ref e));
                case UnderlyingTypeKind.UInt:
                    return Unsafe.As<T, uint>(ref e);
                case UnderlyingTypeKind.Int:
                    return unchecked((ulong)Unsafe.As<T, int>(ref e));
                case UnderlyingTypeKind.UShort:
                    return Unsafe.As<T, ushort>(ref e);
                case UnderlyingTypeKind.Short:
                    return unchecked((ulong)Unsafe.As<T, short>(ref e));
                case UnderlyingTypeKind.Byte:
                    return Unsafe.As<T, byte>(ref e);
                case UnderlyingTypeKind.SByte:
                    return unchecked((ulong)Unsafe.As<T, sbyte>(ref e));
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// ULong 값을 Enum 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">변환할 ULong</param>
        /// <returns>변환된 Enum 값</returns>
        /// <remarks>
        /// [MOD][2025.12.01 - yc.jeon] 입력된 타입에 따른 변환 추가 <br/>
        /// </remarks>
        public static T ULongToEnum<T>(ulong value) where T : struct, Enum
        {
            switch (Underlying<T>.Kind)
            {
                case UnderlyingTypeKind.ULong:
                    return Unsafe.As<ulong, T>(ref value);
                case UnderlyingTypeKind.Long:
                    {
                        long v = unchecked((long)value);
                        return Unsafe.As<long, T>(ref v);
                    }
                case UnderlyingTypeKind.UInt:
                    {
                        uint v = unchecked((uint)value);
                        return Unsafe.As<uint, T>(ref v);
                    }
                case UnderlyingTypeKind.Int:
                    {
                        int v = unchecked((int)value);
                        return Unsafe.As<int, T>(ref v);
                    }
                case UnderlyingTypeKind.UShort:
                    {
                        ushort v = unchecked((ushort)value);
                        return Unsafe.As<ushort, T>(ref v);
                    }
                case UnderlyingTypeKind.Short:
                    {
                        short v = unchecked((short)value);
                        return Unsafe.As<short, T>(ref v);
                    }
                case UnderlyingTypeKind.Byte:
                    {
                        byte v = unchecked((byte)value);
                        return Unsafe.As<byte, T>(ref v);
                    }
                case UnderlyingTypeKind.SByte:
                    {
                        sbyte v = unchecked((sbyte)value);
                        return Unsafe.As<sbyte, T>(ref v);
                    }
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// string 값을 Enum 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">변환할 string</param>
        /// <param name="result">변환된 Enum 값</param>
        /// <returns>
        /// true: 변환 성공 <br/>
        /// false: 변환 실패 <br/>
        /// </returns>
        /// <remarks>[NEW][2026.02.11 - yc.jeon]</remarks>
        public static bool StringToEnum<T>(string value, out T result) where T : struct, Enum
        {
            return Enum.TryParse(value, out result);
        }
    }
}
