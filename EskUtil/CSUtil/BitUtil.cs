// ======================================================================================================
// File Name        : BitUtil.cs
// Project          : CSUtil
// Last Update      : 2025.05.19 - yc.jeon
// ======================================================================================================

using System.Collections;
using System;

namespace CSUtil
{
    /// <summary>
    /// Bit 관련 유틸리티
    /// </summary>
    public static class BitUtil
    {
        /// <summary>
        /// 해당 데이터의 특정 위치의 비트값을 설정하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <param name="isSet">비트 설정값</param>
        /// <returns>설정한 후의 값</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 데이터의 범위를 벗어났을 때</exception>
        public static byte SetBit(this byte data, int loc, bool isSet)
        {
            if (loc < 0 ||
                loc >= 8)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            return isSet ? (byte)(data | (0x01 << loc)) : (byte)(data & ~(0x01 << loc));
        }

        /// <summary>
        /// 해당 데이터의 특정 위치의 비트값을 설정하는 함수
        /// </summary>
        /// <param name="datas">데이터</param>
        /// <param name="loc">위치</param>
        /// <param name="isSet">비트 설정값</param>
        /// <returns>설정한 후의 값</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 데이터의 범위를 벗어났을 때</exception>
        public static byte[] SetBit(this byte[] datas, int loc, bool isSet)
        {
            BitArray bitArray = new BitArray(datas);
            if (loc < 0 ||
                loc >= bitArray.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }
            bitArray[loc] = isSet;
            byte[] byNew = new byte[datas.Length];
            bitArray.CopyTo(byNew, 0);
            return byNew;
        }

        /// <summary>
        /// 해당 데이터의 특정 위치의 비트값을 설정하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <param name="isSet">비트 설정값</param>
        /// <returns>설정한 후의 값</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 short 범위를 벗어났을 때</exception>
        public static short SetBit(this short data, int loc, bool isSet)
        {
            if (loc < 0 ||
                loc >= 16)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            return isSet ? (short)(data | (short)(0x01 << loc)) : (short)(data & ~(0x01 << loc));
        }

        /// <summary>
        /// 해당 데이터의 특정 위치의 비트값을 설정하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <param name="isSet">비트 설정값</param>
        /// <returns>설정한 후의 값</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 ushort 범위를 벗어났을 때</exception>
        public static ushort SetBit(this ushort data, int loc, bool isSet)
        {
            if (loc < 0 ||
                loc >= 16)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            return isSet ? (ushort)(data | (ushort)(0x01 << loc)) : (ushort)(data & ~(0x01 << loc));
        }

        /// <summary>
        /// 해당 데이터의 특정 위치의 비트값을 설정하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <param name="isSet">비트 설정값</param>
        /// <returns>설정한 후의 값</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 int 범위를 벗어났을 때</exception>
        public static int SetBit(this int data, int loc, bool isSet)
        {
            if (loc < 0 ||
                loc >= 32)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            return isSet ? data | (0x01 << loc) : data & ~(0x01 << loc);
        }

        /// <summary>
        /// 해당 데이터의 특정 위치의 비트값을 설정하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <param name="isSet">비트 설정값</param>
        /// <returns>설정한 후의 값</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 uint 범위를 벗어났을 때</exception>
        public static uint SetBit(this uint data, int loc, bool isSet)
        {
            if (loc < 0 ||
                loc >= 32)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            return isSet ? (uint)(data | (uint)(0x01 << loc)) : (uint)(data & ~(0x01 << loc));
        }

        /// <summary>
        /// 데이터의 특정 비트값이 설정되어있는지 확인하는 함수
        /// </summary>
        /// <param name="datas">데이터</param>
        /// <param name="loc">위치</param>
        /// <returns>비트값이 1일때 true, 아닐때 false</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 데이터의 범위를 벗어났을 때</exception>
        public static bool CheckBit(byte[] datas, int loc)
        {
            BitArray bitArray = new BitArray(datas);
            if (loc < 0 ||
                loc >= bitArray.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }
            return bitArray.Get(loc);
        }

        /// <summary>
        /// 데이터의 특정 비트값이 설정되어있는지 확인하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <returns>비트값이 1일때 true, 아닐때 false</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 int 범위를 벗어났을 때</exception>
        public static bool CheckBit(int data, int loc)
        {
            if (loc < 0 ||
                loc >= 32)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            int nValue = 0x1 << loc;
            return (data & nValue) == nValue;
        }

        /// <summary>
        /// 데이터의 특정 비트값이 설정되어있는지 확인하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <returns>비트값이 1일때 true, 아닐때 false</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 uint 범위를 벗어났을 때</exception>
        public static bool CheckBit(uint data, int loc)
        {
            if (loc < 0 ||
                loc >= 32)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            uint unValue = (uint)(0x1 << loc);
            return (data & unValue) == unValue;
        }

        /// <summary>
        /// 데이터의 특정 비트값이 설정되어있는지 확인하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <returns>비트값이 1일때 true, 아닐때 false</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 byte 범위를 벗어났을 때</exception>
        public static bool CheckBit(byte data, int loc)
        {
            if (loc < 0 ||
                loc >= 8)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            byte byValue = (byte)(0x1 << loc);
            return (data & byValue) == byValue;
        }

        /// <summary>
        /// 데이터의 특정 비트값이 설정되어있는지 확인하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <returns>비트값이 1일때 true, 아닐때 false</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 short 범위를 벗어났을 때</exception>
        public static bool CheckBit(short data, int loc)
        {
            if (loc < 0 ||
                loc >= 16)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            short shValue = (short)(0x1 << loc);
            return (data & shValue) == shValue;
        }

        /// <summary>
        /// 데이터의 특정 비트값이 설정되어있는지 확인하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <returns>비트값이 1일때 true, 아닐때 false</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 ushort 범위를 벗어났을 때</exception>
        public static bool CheckBit(ushort data, int loc)
        {
            if (loc < 0 ||
                loc >= 16)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            ushort ushValue = (ushort)(0x1 << loc);
            return (data & ushValue) == ushValue;
        }

        /// <summary>
        /// 데이터의 특정 비트값이 설정되어있는지 확인하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <returns>비트값이 1일때 true, 아닐때 false</returns>
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 long 범위를 벗어났을 때</exception>
        public static bool CheckBit(long data, int loc)
        {
            if (loc < 0 ||
                loc >= 64)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            long lValue = (long)(0x1 << loc);
            return (data & lValue) == lValue;
        }

        /// <summary>
        /// 데이터의 특정 비트값이 설정되어있는지 확인하는 함수
        /// </summary>
        /// <param name="data">데이터</param>
        /// <param name="loc">위치</param>
        /// <returns>비트값이 1일때 true, 아닐때 false</returns>
        /// <exception cref="ArgumentOutOfRangeException">ulong</exception>
        public static bool CheckBit(ulong data, int loc)
        {
            if (loc < 0 ||
                loc >= 64)
            {
                throw new ArgumentOutOfRangeException(nameof(loc));
            }

            ulong ulValue = (ulong)(0x1 << loc);
            return (data & ulValue) == ulValue;
        }
    }
}
