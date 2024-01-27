// ======================================================================================================
// File Name        : MemoryUtil.cs
// Project          : CSUtil
// Last Update      : 2024.01.27 - yc.jeon
// ======================================================================================================

using System.Text;

namespace CSUtil
{
    /// <summary>
    /// 메모리 관련 유틸리티
    /// </summary>
    namespace MemoryUtil
    {
        /// <summary>
        /// 메모리 블럭
        /// (공유 메모리를 일정 크기로 잘라서 사용)
        /// </summary>
        public class MemoryBlock
        {
            /// <summary>
            /// 메모리 버퍼 (블럭 단위)
            /// </summary>
            public List<byte[]> Buffer { get; private set; }
            /// <summary>
            /// 전체 메모리 크기 (바이트 크기, 블럭화된 메모리 총합)
            /// </summary>
            public int MemorySize { get; private set; }
            /// <summary>
            /// 블럭화 크기 (해당 크기로 메모리를 잘라서 관리)
            /// </summary>
            public int BlockSize { get; private set; }
            /// <summary>
            /// 블럭의 이름 (Log에서 사용)
            /// </summary>
            public string BlockName { get; private set; }
            /// <summary>
            /// Log Manager
            /// </summary>
            private Log4netUtil.LogManager _logManager = null!;

            /// <summary>
            /// 생성자
            /// </summary>
            /// <param name="blockSize">블럭화 크기</param>
            /// <param name="totalByteSize">전체 메모리 크기</param>
            /// <param name="blockName">블럭의 이름(Log에서 사용)</param>
            public MemoryBlock(int blockSize, int totalByteSize, string blockName)
            {
                if (blockSize > totalByteSize)
                {
                    blockSize = totalByteSize;
                }

                BlockSize = blockSize;

                int blockCnt = totalByteSize / blockSize;
                if (blockCnt * blockSize != totalByteSize)
                {
                    ++blockCnt;
                }
                int lastBlockSize = totalByteSize % blockSize;

                Buffer = new List<byte[]>(blockCnt);
                if (blockCnt == 1)
                {
                    Buffer.Add(new byte[blockSize]);
                }
                else
                {
                    for (int i = 0; i < blockCnt - 1; ++i)
                    {
                        Buffer.Add(new byte[blockSize]);
                    }
                    Buffer.Add(new byte[lastBlockSize]);
                }


                MemorySize = totalByteSize;
                BlockName = blockName;

                string logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
                _logManager = new Log4netUtil.LogManager(logPath, BlockName, "", "", new Log4netUtil.LogManager.LogRollingSettting()
                {
                    IsAppendToFile = true,
                    MaxSizeRollBackups = 100,
                    MaximumFileSize = "200KB",
                }, true);
            }

            /// <summary>
            /// 메모리 블럭 버퍼를 반환하는 함수
            /// </summary>
            /// <param name="blockIdx">가져올 메모리 블럭의 인덱스</param>
            /// <returns>메모리 블럭 버퍼</returns>
            public byte[]? GetBufferData(int blockIdx)
            {
                if (blockIdx < 0 ||
                    blockIdx >= Buffer.Count)
                {
                    return null;
                }

                return Buffer[blockIdx];
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="datas">바이트 데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(byte[] datas, int position)
            {
                int nPoint;
                int nBlockIdx;
                int nArrIdx;
                for (int i = 0; i < datas.Length; ++i)
                {
                    nPoint = position + i;
                    nBlockIdx = nPoint / BlockSize;
                    nArrIdx = nPoint % BlockSize;

                    Buffer[nBlockIdx][nArrIdx] = datas[i];
                }
                return true;
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="datas">바이트 데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <param name="length">쓸 바이트 데이터의 개수</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(byte[] datas, int position, int length)
            {
                int nPoint;
                int nBlockIdx;
                int nArrIdx;
                for (int i = 0; i < length; ++i)
                {
                    nPoint = position + i;
                    nBlockIdx = nPoint / BlockSize;
                    nArrIdx = nPoint % BlockSize;

                    Buffer[nBlockIdx][nArrIdx] = datas[i];
                }
                return true;
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="data">데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(bool data, int position)
            {
                if (position + sizeof(bool) >= MemorySize)
                {
                    return false;
                }

                byte[] datas = BitConverter.GetBytes(data);
                return Set(datas, position);
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="data">데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(byte data, int position)
            {
                if (position + sizeof(byte) >= MemorySize)
                {
                    return false;
                }

                byte[] datas = new byte[1] { data };
                return Set(datas, position);
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="data">데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(short data, int position)
            {
                if (position + sizeof(short) >= MemorySize)
                {
                    return false;
                }

                byte[] datas = BitConverter.GetBytes(data);
                return Set(datas, position);
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="data">데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(ushort data, int position)
            {
                if (position + sizeof(ushort) >= MemorySize)
                {
                    return false;
                }

                byte[] datas = BitConverter.GetBytes(data);
                return Set(datas, position);
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="data">데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(int data, int position)
            {
                if (position + sizeof(int) >= MemorySize)
                {
                    return false;
                }

                byte[] datas = BitConverter.GetBytes(data);
                return Set(datas, position);
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="data">데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(uint data, int position)
            {
                if (position + sizeof(uint) >= MemorySize)
                {
                    return false;
                }

                byte[] datas = BitConverter.GetBytes(data);
                return Set(datas, position);
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="data">데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns></returns>
            public bool Set(long data, int position)
            {
                if (position + sizeof(long) >= MemorySize)
                {
                    return false;
                }

                byte[] datas = BitConverter.GetBytes(data);
                return Set(datas, position);
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="data">데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(ulong data, int position)
            {
                if (position + sizeof(ulong) >= MemorySize)
                {
                    return false;
                }

                byte[] datas = BitConverter.GetBytes(data);
                return Set(datas, position);
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="data">데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(float data, int position)
            {
                if (position + sizeof(float) >= MemorySize)
                {
                    return false;
                }

                byte[] datas = BitConverter.GetBytes(data);
                return Set(datas, position);
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="data">데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(double data, int position)
            {
                if (position + sizeof(double) >= MemorySize)
                {
                    return false;
                }

                byte[] datas = BitConverter.GetBytes(data);
                return Set(datas, position);
            }

            /// <summary>
            /// 데이터를 메모리에 쓰는 함수
            /// </summary>
            /// <param name="data">데이터</param>
            /// <param name="position">데이터를 쓸 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Set(string data, int position)
            {
                if (position + data.Length >= MemorySize)
                {
                    return false;
                }

                bool result = false;
                try
                {
                    byte[] datas = Encoding.ASCII.GetBytes(data);
                    result = Set(datas, position);
                }
                catch (Exception ex)
                {
                    _logManager.Error(ex.ToString());
                }

                return result;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="datas">받아온 바이트 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <param name="length">바이트 데이터의 길이</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out byte[]? datas, int position, int length)
            {
                if (position + length >= MemorySize)
                {
                    datas = null;
                    return false;
                }

                datas = new byte[length];
                int nPoint;
                int nBlockIdx;
                int nArrIdx;
                for (int i = 0; i < datas.Length; ++i)
                {
                    nPoint = position + i;
                    nBlockIdx = nPoint / BlockSize;
                    nArrIdx = nPoint % BlockSize;

                    datas[i] = Buffer[nBlockIdx][nArrIdx];
                }

                return true;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="datas">받아온 바이트 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(ref byte[] datas, int position)
            {
                if (position + datas.Length >= MemorySize)
                {
                    return false;
                }

                datas = new byte[datas.Length];
                int nPoint;
                int nBlockIdx;
                int nArrIdx;
                for (int i = 0; i < datas.Length; ++i)
                {
                    nPoint = position + i;
                    nBlockIdx = nPoint / BlockSize;
                    nArrIdx = nPoint % BlockSize;

                    datas[i] = Buffer[nBlockIdx][nArrIdx];
                }

                return true;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="data">받아온 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out bool data, int position)
            {
                int size = sizeof(bool);
                data = false;
                if (position + size >= MemorySize)
                {
                    return false;
                }

                bool result = Get(out byte[]? bytes, position, size);
                if (result)
                {
                    try
                    {
                        data = BitConverter.ToBoolean(bytes!, 0);
                    }
                    catch (Exception ex)
                    {
                        _logManager.Error(ex.ToString());
                        result = false;
                    }
                }

                return result;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="data">받아온 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out byte data, int position)
            {
                int size = sizeof(byte);
                data = 0;

                if (position + size >= MemorySize)
                {
                    return false;
                }

                bool result = Get(out byte[]? bytes, position, size);
                if (result)
                {
                    data = bytes![0];
                }

                return result;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="data">받아온 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out short data, int position)
            {
                int size = sizeof(short);
                data = 0;

                if (position + size >= MemorySize)
                {
                    return false;
                }

                bool result = Get(out byte[]? bytes, position, size);
                if (result)
                {
                    try
                    {
                        data = BitConverter.ToInt16(bytes!, 0);
                    }
                    catch (Exception ex)
                    {
                        _logManager.Error(ex.ToString());
                        result = false;
                    }
                }

                return result;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="data">받아온 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out ushort data, int position)
            {
                int size = sizeof(ushort);
                data = 0;

                if (position + size >= MemorySize)
                {
                    return false;
                }

                bool result = Get(out byte[]? bytes, position, size);
                if (result)
                {
                    try
                    {
                        data = BitConverter.ToUInt16(bytes!, 0);
                    }
                    catch (Exception ex)
                    {
                        _logManager.Error(ex.ToString());
                        result = false;
                    }
                }

                return result;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="data">받아온 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out int data, int position)
            {
                int size = sizeof(int);
                data = 0;

                if (position + size >= MemorySize)
                {
                    return false;
                }

                bool result = Get(out byte[]? bytes, position, size);
                if (result)
                {
                    try
                    {
                        data = BitConverter.ToInt32(bytes!, 0);
                    }
                    catch (Exception ex)
                    {
                        _logManager.Error(ex.ToString());
                        result = false;
                    }
                }

                return result;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="data">받아온 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out uint data, int position)
            {
                int size = sizeof(uint);
                data = 0;

                if (position + size >= MemorySize)
                {
                    return false;
                }

                bool result = Get(out byte[]? bytes, position, size);
                if (result)
                {
                    try
                    {
                        data = BitConverter.ToUInt32(bytes!, 0);
                    }
                    catch (Exception ex)
                    {
                        _logManager.Error(ex.ToString());
                        result = false;
                    }
                }

                return result;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="data">받아온 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out long data, int position)
            {
                int size = sizeof(long);
                data = 0;

                if (position + size >= MemorySize)
                {
                    return false;
                }

                bool result = Get(out byte[]? bytes, position, size);
                if (result)
                {
                    try
                    {
                        data = BitConverter.ToInt64(bytes!, 0);
                    }
                    catch (Exception ex)
                    {
                        _logManager.Error(ex.ToString());
                        result = false;
                    }
                }

                return result;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="data">받아온 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out ulong data, int position)
            {
                int size = sizeof(ulong);
                data = 0;

                if (position + size >= MemorySize)
                {
                    return false;
                }

                bool result = Get(out byte[]? bytes, position, size);
                if (result)
                {
                    try
                    {
                        data = BitConverter.ToUInt64(bytes!, 0);
                    }
                    catch (Exception ex)
                    {
                        _logManager.Error(ex.ToString());
                        result = false;
                    }
                }

                return result;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="data">받아온 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out float data, int position)
            {
                int size = sizeof(float);
                data = 0;

                if (position + size >= MemorySize)
                {
                    return false;
                }

                bool result = Get(out byte[]? bytes, position, size);
                if (result)
                {
                    try
                    {
                        data = BitConverter.ToSingle(bytes!, 0);
                    }
                    catch (Exception ex)
                    {
                        _logManager.Error(ex.ToString());
                        result = false;
                    }
                }

                return result;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="data">받아온 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out double data, int position)
            {
                int size = sizeof(double);
                data = 0;

                if (position + size >= MemorySize)
                {
                    return false;
                }

                bool result = Get(out byte[]? bytes, position, size);
                if (result)
                {
                    try
                    {
                        data = BitConverter.ToDouble(bytes!, 0);
                    }
                    catch (Exception ex)
                    {
                        _logManager.Error(ex.ToString());
                        result = false;
                    }
                }

                return result;
            }

            /// <summary>
            /// 데이터를 메모리에서 가져오는 함수
            /// </summary>
            /// <param name="data">받아온 데이터</param>
            /// <param name="position">데이터의 위치</param>
            /// <param name="length">받아올 문자열의 길이</param>
            /// <returns>true: 성공, false: 실패</returns>
            public bool Get(out string data, int position, int length)
            {
                data = string.Empty;

                if (position + length >= MemorySize)
                {
                    return false;
                }

                bool result = Get(out byte[]? bytes, position, length);
                if (result)
                {
                    try
                    {
                        data = Encoding.ASCII.GetString(bytes!);
                    }
                    catch (Exception ex)
                    {
                        _logManager.Error(ex.ToString());
                        result = false;
                    }
                }

                return result;
            }
        }
    }
}
