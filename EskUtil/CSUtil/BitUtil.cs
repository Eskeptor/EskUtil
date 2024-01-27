// ======================================================================================================
// File Name        : BitUtil.cs
// Project          : CSUtil
// Last Update      : 2024.01.27 - yc.jeon
// ======================================================================================================

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
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 int 범위를 벗어났을 때</exception>
        public static int SetBit(int data, int loc, bool isSet)
        {
            if (loc < 0 ||
                loc >= 32)
            {
                throw new ArgumentOutOfRangeException("nLoc");
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
        /// <exception cref="ArgumentOutOfRangeException">위치 값이 데이터의 범위를 벗어났을 때</exception>
        public static byte SetBit(byte data, int loc, bool isSet)
        {
            if (loc < 0 ||
                loc >= 8)
            {
                throw new ArgumentOutOfRangeException("nLoc");
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
        public static byte[] SetBit(byte[] datas, int loc, bool isSet)
        {
            System.Collections.BitArray bitArray = new System.Collections.BitArray(datas);
            if (loc < 0 ||
                loc >= bitArray.Length)
            {
                throw new ArgumentOutOfRangeException("nLoc");
            }
            bitArray[loc] = isSet;
            byte[] byNew = new byte[datas.Length];
            bitArray.CopyTo(byNew, 0);
            return byNew;
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
            System.Collections.BitArray bitArray = new System.Collections.BitArray(datas);
            if (loc < 0 ||
                loc >= bitArray.Length)
            {
                throw new ArgumentOutOfRangeException("nLoc");
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
                throw new ArgumentOutOfRangeException();
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
                throw new ArgumentOutOfRangeException();
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
                throw new ArgumentOutOfRangeException();
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
                throw new ArgumentOutOfRangeException();
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
                throw new ArgumentOutOfRangeException();
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
                throw new ArgumentOutOfRangeException();
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
                throw new ArgumentOutOfRangeException();
            }

            ulong ulValue = (ulong)(0x1 << loc);
            return (data & ulValue) == ulValue;
        }
    }
}
