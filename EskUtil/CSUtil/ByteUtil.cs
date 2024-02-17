// ======================================================================================================
// File Name        : ByteUtil.cs
// Project          : CSUtil
// Last Update      : 2024.02.17 - yc.jeon
// ======================================================================================================

namespace CSUtil
{
    /// <summary>
    /// Byte 관련 유틸리티
    /// </summary>
    public static class ByteUtil
    {
        /// <summary>
        /// 바이트 두 개씩 Swap하는 함수
        /// (예: [0] <-> [1], [2] <-> [3])
        /// </summary>
        /// <param name="bytes">Swap할 바이트 데이터</param>
        /// <returns></returns>
        public static byte[] ByteSwap(byte[] bytes)
        {
            if (bytes.Length % 2 != 0)
            {
                throw new ArgumentException("Byte Swap Func byte Array Length Not Matched", nameof(bytes));
            }

            for (int i = 0; i < bytes.Length; i += 2)
            {
                byte tmp = bytes[i];
                bytes[i] = bytes[i + 1];
                bytes[i + 1] = tmp;
            }

            return bytes;
        }

        /// <summary>
        /// Byte 배열을 자르는 함수
        /// (start: 1, end: 3 일때 = [1], [2], [3] 총 3개의 값을 가진 배열이 잘려나옴)
        /// </summary>
        /// <param name="bytes">원본 배열</param>
        /// <param name="start">시작 위치</param>
        /// <param name="end">끝 위치</param>
        /// <returns>잘린 배열</returns>
        /// <exception cref="IndexOutOfRangeException">start나 end가 원본 배열의 길이를 초과했을 경우</exception>
        /// <exception cref="ArgumentException">start가 end보다 큰 경우</exception>
        public static byte[] Slice(this byte[] bytes, int start, int end)
        {
            if (start >= bytes.Length ||
                end >= bytes.Length)
            {
                throw new IndexOutOfRangeException("start or end is over than array length.");
            }

            if (start > end)
            {
                throw new ArgumentException("start is over than end.", nameof(start));
            }

            int length = end - start + 1;
            byte[] slice = new byte[length];
            for (int i = start; i <= end; ++i)
            {
                slice[i] = bytes[i];
            }

            return slice;
        }

        /// <summary>
        /// char형 데이터 배열을 byte형 데이터 배열로 변환하여 넣는 함수
        /// </summary>
        /// <param name="bytes">변환한 데이터가 저장될 byte형 데이터 배열</param>
        /// <param name="values">변환할 데이터</param>
        public unsafe static void SetBytes(ref byte[] bytes, char[] values)
        {
            int typeSize = sizeof(char);
            bytes = new byte[typeSize * values.Length];

            for (int i = 0; i < values.Length; ++i)
            {
                fixed (byte* ptr = &bytes[i * typeSize])
                {
                    *(char*)ptr = values[i];
                }
            }
        }

        /// <summary>
        /// short형 데이터 배열을 byte형 데이터 배열로 변환하여 넣는 함수
        /// </summary>
        /// <param name="bytes">변환한 데이터가 저장될 byte형 데이터 배열</param>
        /// <param name="values">변환할 데이터</param>
        /// <param name="multiple">변환할 때 곱해줄 값(사용하지 않을 때는 0.0)</param>
        public unsafe static void SetBytes(ref byte[] bytes, short[] values, double multiple = 0.0)
        {
            int typeSize = sizeof(short);
            bytes = new byte[typeSize * values.Length];

            if (multiple == 0.0)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(short*)ptr = values[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(short*)ptr = DataType.DoubleToShort(values[i] * multiple);
                    }
                }
            }
        }

        /// <summary>
        /// ushort형 데이터 배열을 byte형 데이터 배열로 변환하여 넣는 함수
        /// </summary>
        /// <param name="bytes">변환한 데이터가 저장될 byte형 데이터 배열</param>
        /// <param name="values">변환할 데이터</param>
        /// <param name="multiple">변환할 때 곱해줄 값(사용하지 않을 때는 0.0)</param>
        public unsafe static void SetBytes(ref byte[] bytes, ushort[] values, double multiple = 0.0)
        {
            int typeSize = sizeof(ushort);
            bytes = new byte[typeSize * values.Length];

            if (multiple == 0.0)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(ushort*)ptr = values[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(ushort*)ptr = DataType.DoubleToUShort(values[i] * multiple);
                    }
                }
            }
        }

        /// <summary>
        /// int형 데이터 배열을 byte형 데이터 배열로 변환하여 넣는 함수
        /// </summary>
        /// <param name="bytes">변환한 데이터가 저장될 byte형 데이터 배열</param>
        /// <param name="values">변환할 데이터</param>
        /// <param name="multiple">변환할 때 곱해줄 값(사용하지 않을 때는 0.0)</param>
        public unsafe static void SetBytes(ref byte[] bytes, int[] values, double multiple = 0.0)
        {
            int typeSize = sizeof(int);
            bytes = new byte[typeSize * values.Length];

            if (multiple == 0.0)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(int*)ptr = values[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(int*)ptr = DataType.DoubleToInt(values[i] * multiple);
                    }
                }
            }
        }

        /// <summary>
        /// uint형 데이터 배열을 byte형 데이터 배열로 변환하여 넣는 함수
        /// </summary>
        /// <param name="bytes">변환한 데이터가 저장될 byte형 데이터 배열</param>
        /// <param name="values">변환할 데이터</param>
        /// <param name="multiple">변환할 때 곱해줄 값(사용하지 않을 때는 0.0)</param>
        public unsafe static void SetBytes(ref byte[] bytes, uint[] values, double multiple = 0.0)
        {
            int typeSize = sizeof(uint);
            bytes = new byte[typeSize * values.Length];

            if (multiple == 0.0)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(uint*)ptr = values[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(uint*)ptr = DataType.DoubleToUInt(values[i] * multiple);
                    }
                }
            }
        }

        /// <summary>
        /// long형 데이터 배열을 byte형 데이터 배열로 변환하여 넣는 함수
        /// </summary>
        /// <param name="bytes">변환한 데이터가 저장될 byte형 데이터 배열</param>
        /// <param name="values">변환할 데이터</param>
        /// <param name="multiple">변환할 때 곱해줄 값(사용하지 않을 때는 0.0)</param>
        public unsafe static void SetBytes(ref byte[] bytes, long[] values, double multiple = 0.0)
        {
            int typeSize = sizeof(long);
            bytes = new byte[typeSize * values.Length];

            if (multiple == 0.0)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(long*)ptr = values[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(long*)ptr = DataType.DoubleToLong(values[i] * multiple);
                    }
                }
            }
        }

        /// <summary>
        /// ulong형 데이터 배열을 byte형 데이터 배열로 변환하여 넣는 함수
        /// </summary>
        /// <param name="bytes">변환한 데이터가 저장될 byte형 데이터 배열</param>
        /// <param name="values">변환할 데이터</param>
        /// <param name="multiple">변환할 때 곱해줄 값(사용하지 않을 때는 0.0)</param>
        public unsafe static void SetBytes(ref byte[] bytes, ulong[] values, double multiple = 0.0)
        {
            int typeSize = sizeof(ulong);
            bytes = new byte[typeSize * values.Length];

            if (multiple == 0.0)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(ulong*)ptr = values[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(ulong*)ptr = DataType.DoubleToULong(values[i] * multiple);
                    }
                }
            }
        }

        /// <summary>
        /// float형 데이터 배열을 byte형 데이터 배열로 변환하여 넣는 함수
        /// </summary>
        /// <param name="bytes">변환한 데이터가 저장될 byte형 데이터 배열</param>
        /// <param name="values">변환할 데이터</param>
        /// <param name="multiple">변환할 때 곱해줄 값(사용하지 않을 때는 0.0)</param>
        public unsafe static void SetBytes(ref byte[] bytes, float[] values, double multiple = 0.0)
        {
            int typeSize = sizeof(float);
            bytes = new byte[typeSize * values.Length];

            if (multiple == 0.0)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(float*)ptr = values[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(float*)ptr = DataType.DoubleToFloat(values[i] * multiple);
                    }
                }
            }
        }

        /// <summary>
        /// double형 데이터 배열을 byte형 데이터 배열로 변환하여 넣는 함수
        /// </summary>
        /// <param name="bytes">변환한 데이터가 저장될 byte형 데이터 배열</param>
        /// <param name="values">변환할 데이터</param>
        /// <param name="multiple">변환할 때 곱해줄 값(사용하지 않을 때는 0.0)</param>
        public unsafe static void SetBytes(ref byte[] bytes, double[] values, double multiple = 0.0)
        {
            int typeSize = sizeof(double);
            bytes = new byte[typeSize * values.Length];

            if (multiple == 0.0)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(double*)ptr = values[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    fixed (byte* ptr = &bytes[i * typeSize])
                    {
                        *(double*)ptr = values[i] * multiple;
                    }
                }
            }
        }
    }
}
