// ======================================================================================================
// File Name        : DataType.cs
// Project          : CSUtil
// Last Update      : 2025.05.19 - yc.jeon
// ======================================================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace CSUtil
{
    /// <summary>
    /// 데이터 타입 관련 유틸리티
    /// </summary>
    public static class DataType
    {
        /// <summary>
        /// 자료형의 열거형
        /// </summary>
        public enum Types
        {
            Null,       // STR_NULL
            String,     // STR_STRING
            Bool,       // STR_BOOL
            Bit,        // STR_BIT
            Int8,       // STR_INT8
            UInt8,      // STR_UINT8
            Int16,      // STR_INT16
            UInt16,     // STR_UINT16
            Int32,      // STR_INT32
            UInt32,     // STR_UINT32
            Int64,      // STR_INT64
            UInt64,     // STR_UINT64
            Float,      // STR_FLOAT
            Double,     // STR_DOUBLE
            DateTime,   // STR_DATETIME
        }

        /// <summary>
        /// DefinedData를 정의하기 위한 클래스
        /// </summary>
        public struct DataTypeCls : IEquatable<DataTypeCls>
        {
            /// <summary>
            /// 자료형의 Types (enum)
            /// </summary>
            public Types Type { get; }
            /// <summary>
            /// 자료형의 Types (String)
            /// </summary>
            public string TypeToString { get; }
            /// <summary>
            /// 자료형의 바이트 크기
            /// </summary>
            public int ByteSize { get; }
            /// <summary>
            /// 자료형의 실제 Type(typeof)
            /// </summary>
            public Type RealType { get; }
            /// <summary>
            /// 해당 자료형의 OPC-UA BuiltIn Type
            /// </summary>
            public int OpcUaBuiltInType { get; }
            /// <summary>
            /// 해당 자료형의 최소값
            /// </summary>
            public double MinValue { get; }
            /// <summary>
            /// 해당 자료형의 최대값
            /// </summary>
            public double MaxValue { get; }

            /// <summary>
            /// 생성자
            /// </summary>
            /// <param name="enumType">자료형의 Types (enum)</param>
            /// <param name="byteSize">자료형의 바이트 크기</param>
            /// <param name="realType">자료형의 실제 Type(typeof)</param>
            /// <param name="opcuaBuiltInType">자료형의 OPC-UA에서의 자료형 타입</param>
            /// <param name="minValue">자료형의 최소값</param>
            /// <param name="maxValue">자료형의 최대값</param>
            public DataTypeCls(Types enumType, int byteSize, Type realType, int opcuaBuiltInType, double minValue, double maxValue)
            {
                Type = enumType;
                TypeToString = enumType.ToString();
                ByteSize = byteSize;
                RealType = realType;
                OpcUaBuiltInType = opcuaBuiltInType;
                MinValue = minValue;
                MaxValue = maxValue;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is DataTypeCls equals))
                {
                    return false;
                }

                return Type == equals.Type && ByteSize == equals.ByteSize &&
                    RealType == equals.RealType && OpcUaBuiltInType == equals.OpcUaBuiltInType &&
                    MinValue == equals.MinValue && MaxValue == equals.MaxValue;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Type, TypeToString, ByteSize, RealType, OpcUaBuiltInType, MinValue, MaxValue);
            }

            public static bool operator ==(DataTypeCls left, DataTypeCls right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(DataTypeCls left, DataTypeCls right)
            {
                return !(left == right);
            }

            public bool Equals(DataTypeCls other)
            {
                return Type == other.Type && ByteSize == other.ByteSize &&
                    RealType == other.RealType && OpcUaBuiltInType == other.OpcUaBuiltInType &&
                    MinValue == other.MinValue && MaxValue == other.MaxValue;
            }
        }

        /// <summary>
        /// Double이 지원하는 long의 최대값
        /// </summary>
        public const long SAFE_MAX_LONG_DOUBLE = 9007199254740991L;
        /// <summary>
        /// Double이 지원하는 long의 최소값
        /// </summary>
        public const long SAFE_MIN_LONG_DOUBLE = -9007199254740991L;

        /// <summary>
        /// 정의된 자료형 (실제로 사용할 수 있는)
        /// </summary>
        public static IReadOnlyDictionary<Types, DataTypeCls> DefinedData { get; } = new Dictionary<Types, DataTypeCls>()
        {
            { Types.String, new DataTypeCls(Types.String, 1, typeof(string), 12, 0.0, 0.0) },
            { Types.Bool, new DataTypeCls(Types.Bool, 1, typeof(bool), 1, 0.0, 1.0) },
            { Types.Int16, new DataTypeCls(Types.Int16, 2, typeof(short), 4, short.MinValue, short.MaxValue) },
            { Types.UInt16, new DataTypeCls(Types.UInt16, 2, typeof(ushort), 5, ushort.MinValue, ushort.MaxValue) },
            { Types.Float, new DataTypeCls(Types.Float, 4, typeof(float), 10, float.MinValue, float.MaxValue) },
            { Types.Int32, new DataTypeCls(Types.Int32, 4, typeof(int), 6, int.MinValue, int.MaxValue) },
            { Types.UInt32, new DataTypeCls(Types.UInt32, 4, typeof(uint), 7, uint.MinValue, uint.MaxValue) },
            { Types.Double, new DataTypeCls(Types.Double, 8, typeof(double), 11, double.MinValue, double.MaxValue) },
            { Types.Int64, new DataTypeCls(Types.Int64, 8, typeof(long), 8, SAFE_MIN_LONG_DOUBLE, SAFE_MAX_LONG_DOUBLE) },
            { Types.UInt64, new DataTypeCls(Types.UInt64, 8, typeof(ulong), 9, 0, SAFE_MAX_LONG_DOUBLE) },
            { Types.Bit, new DataTypeCls(Types.Bit, 1, typeof(byte), 3, byte.MinValue, byte.MaxValue) },
            { Types.UInt8, new DataTypeCls(Types.UInt8, 1, typeof(byte), 3, byte.MinValue, byte.MaxValue) },
            { Types.Int8, new DataTypeCls(Types.Int8, 1, typeof(sbyte), 3, sbyte.MinValue, sbyte.MaxValue) },
            { Types.DateTime, new DataTypeCls(Types.DateTime, 8, typeof(DateTime), 13, 0.0, 0.0) },
        };

        /// <summary>
        /// 문자열(String) 데이터를 각자의 형으로 변환하고 object로 반환하는 함수
        /// </summary>
        /// <param name="value">변환할 문자열 데이터</param>
        /// <param name="dataType">변환 타입</param>
        /// <param name="size">원본 데이터의 개수</param>
        /// <param name="token">원본 데이터가 배열인 경우 문자열을 자를 문자 단위</param>
        /// <returns>변환된 값</returns>
        public static object StringToObject(string value, string dataType, int size, char token)
        {
            if (!Enum.TryParse(dataType, out Types type))
            {
                return null;
            }

            return StringToObject(value, type, size, token);
        }

        /// <summary>
        /// 문자열(String) 데이터를 각자의 형으로 변환하고 object로 반환하는 함수
        /// </summary>
        /// <param name="value">변환할 문자열 데이터</param>
        /// <param name="dataType">변환 타입</param>
        /// <param name="size">원본 데이터의 개수</param>
        /// <param name="token">원본 데이터가 배열인 경우 문자열을 자를 문자 단위</param>
        /// <returns>변환된 값</returns>
        public static object StringToObject(string value, Types dataType, int size, char token)
        {
            try
            {
                switch (dataType)
                {
                    case Types.String:
                        return value;
                    case Types.Bool:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new bool[size];
                            }

                            string[] splits = value.Split(token);
                            bool[] datas = new bool[splits.Length];
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                if (bool.TryParse(splits[i], out bool result))
                                {
                                    datas[i] = result;
                                }
                            }
                            return datas;
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return false;
                            }
                            else
                            {
                                try
                                {
                                    return bool.Parse(value);
                                }
                                catch
                                {
                                    return !value.EqualsOrdinal("0") && !value.EqualsOrdinal("0.0");
                                }
                            }
                        }
                    case Types.Bit:
                    case Types.UInt8:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new byte[size];
                            }

                            string[] splits = value.Split(token);
                            byte[] datas = System.Array.ConvertAll(splits, byte.Parse);
                            return datas;
                        }
                        return string.IsNullOrWhiteSpace(value) ? (byte)0 : value.ParseByte();
                    case Types.Int8:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new sbyte[size];
                            }

                            string[] splits = value.Split(token);
                            sbyte[] datas = System.Array.ConvertAll(splits, sbyte.Parse);
                            return datas;
                        }
                        return string.IsNullOrWhiteSpace(value) ? (sbyte)0 : value.ParseSByte();
                    case Types.Int16:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new short[size];
                            }

                            string[] splits = value.Split(token);
                            short[] datas = System.Array.ConvertAll(splits, short.Parse);
                            return datas;
                        }
                        return string.IsNullOrWhiteSpace(value) ? (short)0 : value.ParseShort();
                    case Types.UInt16:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new ushort[size];
                            }

                            string[] splits = value.Split(token);
                            ushort[] datas = System.Array.ConvertAll(splits, ushort.Parse);
                            return datas;
                        }
                        return string.IsNullOrWhiteSpace(value) ? (ushort)0 : value.ParseUShort();
                    case Types.Int32:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new int[size];
                            }

                            string[] splits = value.Split(token);
                            int[] datas = System.Array.ConvertAll(splits, int.Parse);
                            return datas;
                        }
                        return string.IsNullOrWhiteSpace(value) ? 0 : value.ParseInt();
                    case Types.UInt32:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new uint[size];
                            }

                            string[] splits = value.Split(token);
                            uint[] datas = System.Array.ConvertAll(splits, uint.Parse);
                            return datas;
                        }
                        return string.IsNullOrWhiteSpace(value) ? 0 : value.ParseUInt();
                    case Types.Int64:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new long[size];
                            }

                            string[] splits = value.Split(token);
                            long[] datas = System.Array.ConvertAll(splits, long.Parse);
                            return datas;
                        }
                        return string.IsNullOrWhiteSpace(value) ? 0L : value.ParseLong();
                    case Types.UInt64:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new ulong[size];
                            }

                            string[] splits = value.Split(token);
                            ulong[] datas = System.Array.ConvertAll(splits, ulong.Parse);
                            return datas;
                        }
                        return string.IsNullOrWhiteSpace(value) ? 0UL : value.ParseULong();
                    case Types.Float:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new float[size];
                            }

                            string[] splits = value.Split(token);
                            float[] datas = System.Array.ConvertAll(splits, float.Parse);
                            return datas;
                        }
                        return string.IsNullOrWhiteSpace(value) ? 0.0F : value.ParseFloat();
                    case Types.Double:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new double[size];
                            }

                            string[] splits = value.Split(token);
                            double[] datas = System.Array.ConvertAll(splits, double.Parse);
                            return datas;
                        }
                        return string.IsNullOrWhiteSpace(value) ? 0.0 : value.ParseDouble();
                    case Types.DateTime:
                        if (size > 1)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                return new DateTime[size];
                            }

                            string[] splits = value.Split(token);
                            DateTime[] datas = System.Array.ConvertAll(splits, DateTime.Parse);
                            return datas;
                        }
                        return string.IsNullOrWhiteSpace(value) ? DateTime.MinValue : value.ParseDateTime();
                    default:
                        throw new InvalidTypeException("Invalid Types", nameof(dataType));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());

                double dData = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                switch (dataType)
                {
                    case Types.Bit:
                    case Types.UInt8:
                        return Convert.ToByte(dData);
                    case Types.Int8:
                        return Convert.ToSByte(dData);
                    case Types.Int16:
                        return Convert.ToInt16(dData);
                    case Types.UInt16:
                        return Convert.ToUInt16(dData);
                    case Types.Int32:
                        return Convert.ToInt32(dData);
                    case Types.UInt32:
                        return Convert.ToUInt32(dData);
                    case Types.Int64:
                        return Convert.ToInt64(dData);
                    case Types.UInt64:
                        return Convert.ToUInt64(dData);
                    case Types.Float:
                        return Convert.ToSingle(dData);
                    case Types.Double:
                        return Convert.ToDouble(dData);
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// object를 각자의 형에 맞게 문자열(String) 데이터로 반환하는 함수
        /// </summary>
        /// <param name="value">변환 할 Object</param>
        /// <param name="dataType">변환 자료형 타입</param>
        /// <param name="size">원본 데이터의 개수</param>
        /// <param name="token">데이터가 배열일 경우 각 인덱스 사이에 넣을 문자 토큰</param>
        /// <returns>변환된 문자열</returns>
        public static string ObjectToString(object value, string dataType, int size, char token)
        {
            if (!Enum.TryParse(dataType, out Types type))
            {
                return string.Empty;
            }

            return ObjectToString(value, type, size, token);
        }

        /// <summary>
        /// object를 각자의 형에 맞게 문자열(String) 데이터로 반환하는 함수
        /// </summary>
        /// <param name="value">변환 할 Object</param>
        /// <param name="dataType">변환 자료형 타입</param>
        /// <param name="size">원본 데이터의 개수</param>
        /// <param name="token">데이터가 배열일 경우 각 인덱스 사이에 넣을 문자 토큰</param>
        /// <returns>변환된 문자열</returns>
        public static string ObjectToString(object value, Types dataType, int size, char token)
        {
            string result;

            if (value == null)
            {
                return string.Empty;
            }

            if (size == 1)
            {
                result = value.ToString().TrimEnd('\0');
            }
            else
            {
                switch (dataType)
                {
                    case Types.String:
                        {
                            result = value.ToString().TrimEnd('\0');
                        }
                        break;
                    case Types.Bool:
                        {
                            bool[] datas = (bool[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    case Types.Bit:
                    case Types.UInt8:
                        {
                            byte[] datas = (byte[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    case Types.Int8:
                        {
                            sbyte[] datas = (sbyte[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    case Types.Int16:
                        {
                            short[] datas = (short[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    case Types.UInt16:
                        {
                            ushort[] datas = (ushort[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    case Types.Int32:
                        {
                            int[] datas = (int[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    case Types.UInt32:
                        {
                            uint[] datas = (uint[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    case Types.Int64:
                        {
                            long[] datas = (long[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    case Types.UInt64:
                        {
                            ulong[] datas = (ulong[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    case Types.Float:
                        {
                            float[] datas = (float[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    case Types.Double:
                        {
                            double[] datas = (double[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    case Types.DateTime:
                        {
                            DateTime[] datas = (DateTime[])value;
                            result = datas.OneLineString(token);
                        }
                        break;
                    default:
                        throw new InvalidTypeException("Invalid Types", nameof(dataType));
                }
            }

            return result;
        }

        /// <summary>
        /// 자료형에 맞는 초기 데이터를 만들어 object 형식으로 반환하는 함수
        /// </summary>
        /// <param name="dataType">데이터 타입</param>
        /// <param name="size">데이터 개수(바이트 길이 아님)</param>
        /// <returns>만들어진 object</returns>
        public static object MakeObject(string dataType, int size)
        {
            if (!Enum.TryParse(dataType, out Types type))
            {
                return null;
            }

            return MakeObject(type, size);
        }
        /// <summary>
        /// 자료형에 맞는 초기 데이터를 만들어 object 형식으로 반환하는 함수
        /// </summary>
        /// <param name="dataType">데이터 타입</param>
        /// <param name="size">데이터 개수(바이트 길이 아님)</param>
        /// <returns>만들어진 object</returns>
        public static object MakeObject(Types dataType, int size)
        {
            if (size <= 0)
            {
                return null;
            }

            object objValue;
            switch (dataType)
            {
                case Types.UInt8:
                case Types.Bit:
                    if (size > 1)
                    {
                        objValue = new byte[size];
                    }
                    else
                    {
                        objValue = (byte)0;
                    }
                    break;
                case Types.Bool:
                    if (size > 1)
                    {
                        objValue = new bool[size];
                    }
                    else
                    {
                        objValue = false;
                    }
                    break;
                case Types.String:
                    objValue = string.Empty;
                    break;
                case Types.Int8:
                    if (size > 1)
                    {
                        objValue = new sbyte[size];
                    }
                    else
                    {
                        objValue = (sbyte)0;
                    }
                    break;
                case Types.Int16:
                    if (size > 1)
                    {
                        objValue = new short[size];
                    }
                    else
                    {
                        objValue = (short)0;
                    }
                    break;
                case Types.UInt16:
                    if (size > 1)
                    {
                        objValue = new ushort[size];
                    }
                    else
                    {
                        objValue = (ushort)0;
                    }
                    break;
                case Types.Int32:
                    if (size > 1)
                    {
                        objValue = new int[size];
                    }
                    else
                    {
                        objValue = 0;
                    }
                    break;
                case Types.UInt32:
                    if (size > 1)
                    {
                        objValue = new uint[size];
                    }
                    else
                    {
                        objValue = (uint)0;
                    }
                    break;
                case Types.Int64:
                    if (size > 1)
                    {
                        objValue = new long[size];
                    }
                    else
                    {
                        objValue = 0L;
                    }
                    break;
                case Types.UInt64:
                    if (size > 1)
                    {
                        objValue = new ulong[size];
                    }
                    else
                    {
                        objValue = 0UL;
                    }
                    break;
                case Types.Float:
                    if (size > 1)
                    {
                        objValue = new float[size];
                    }
                    else
                    {
                        objValue = 0.0F;
                    }
                    break;
                case Types.Double:
                    if (size > 1)
                    {
                        objValue = new double[size];
                    }
                    else
                    {
                        objValue = 0.0;
                    }
                    break;
                case Types.DateTime:
                    if (size > 1)
                    {
                        objValue = new DateTime[size];
                    }
                    else
                    {
                        objValue = DateTime.Now;
                    }
                    break;
                default:
                    throw new InvalidTypeException("Invalid Types", nameof(dataType));
            }

            return objValue;
        }

        /// <summary>
        /// object 타입의 데이터를 다른 타입의 데이터로 변환하는 함수
        /// </summary>
        /// <param name="srcData">원본 데이터</param>
        /// <param name="destType">변환할 타입</param>
        /// <returns>변환한 데이터</returns>
        public static object DataToObject(object srcData, string destType)
        {
            if (!Enum.TryParse(destType, out Types type))
            {
                return null;
            }

            return DataToObject(srcData, type);
        }

        /// <summary>
        /// object 타입의 데이터를 다른 타입의 데이터로 변환하는 함수
        /// </summary>
        /// <param name="srcData">원본 데이터</param>
        /// <param name="destType">변환할 타입</param>
        /// <returns>변환한 데이터</returns>
        public static object DataToObject<T>(T srcData, Types destType)
        {
            if (srcData == null)
            {
                return null;
            }

            try
            {
                switch (destType)
                {
                    case Types.String:
                        return srcData.ToString();
                    case Types.Bool:
                        return Convert.ToBoolean(srcData, CultureInfo.InvariantCulture);
                    case Types.Bit:
                    case Types.UInt8:
                        return Convert.ToByte(srcData, CultureInfo.InvariantCulture);
                    case Types.Int8:
                        return Convert.ToSByte(srcData, CultureInfo.InvariantCulture);
                    case Types.Int16:
                        return Convert.ToInt16(srcData, CultureInfo.InvariantCulture);
                    case Types.UInt16:
                        return Convert.ToUInt16(srcData, CultureInfo.InvariantCulture);
                    case Types.Int32:
                        return Convert.ToInt32(srcData, CultureInfo.InvariantCulture);
                    case Types.UInt32:
                        return Convert.ToUInt32(srcData, CultureInfo.InvariantCulture);
                    case Types.Int64:
                        return Convert.ToInt64(srcData, CultureInfo.InvariantCulture);
                    case Types.UInt64:
                        return Convert.ToUInt64(srcData, CultureInfo.InvariantCulture);
                    case Types.Float:
                        return Convert.ToSingle(srcData, CultureInfo.InvariantCulture);
                    case Types.Double:
                        return Convert.ToDouble(srcData, CultureInfo.InvariantCulture);
                    case Types.DateTime:
                        return Convert.ToDateTime(srcData, CultureInfo.InvariantCulture);
                    default:
                        throw new InvalidTypeException("Invalid Types", nameof(destType));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Convert Exception({srcData}, {destType}): {ex}");
            }

            return null;
        }

        /// <summary>
        /// 입력된 데이터의 범위(<c>min &lt;= data &lt;= max</c>)를 확인하여 데이터를 범위에 맞게 반환하는 함수 <br/>
        /// </summary>
        /// <param name="data">확인할 데이터</param>
        /// <param name="dataType">입력된 데이터의 자료형</param>
        /// <param name="size">데이터 개수</param>
        /// <param name="min">최소값</param>
        /// <param name="max">최대값</param>
        /// <param name="resultData">
        /// 데이터가 <c>min</c>보다 작으면 <c>min</c> 반환 <br/>
        /// 데이터가 범위 안에 들어왔다면 <c>data</c> 반환 <br/>
        /// 데이터가 <c>max</c>보다 크면 <c>max</c> 반환
        /// </param>
        /// <returns>
        /// true: 실행 성공 <br/>
        /// false: 실행 실패 (data == null, Type Convert 실패)
        /// </returns>
        public static bool DataMinMaxCheck(object data, Types dataType, int size, double min, double max, out object resultData)
        {
            if (data == null)
            {
                resultData = null;
                return false;
            }
            bool isNotConvertType = dataType == Types.String || dataType == Types.Bool || dataType == Types.DateTime || size > 1;
            if (isNotConvertType)
            {
                resultData = data;
                return true;
            }

            bool result = true;

            try
            {
                double chkData = Convert.ToDouble(data, CultureInfo.InvariantCulture);
                chkData = chkData < min ? min : chkData;
                chkData = chkData > max ? max : chkData;

                switch (dataType)
                {
                    case Types.Bit:
                    case Types.UInt8:
                        resultData = Convert.ToByte(chkData);
                        break;
                    case Types.Int8:
                        resultData = Convert.ToSByte(chkData);
                        break;
                    case Types.Int16:
                        resultData = Convert.ToInt16(chkData);
                        break;
                    case Types.UInt16:
                        resultData = Convert.ToUInt16(chkData);
                        break;
                    case Types.Int32:
                        resultData = Convert.ToInt32(chkData);
                        break;
                    case Types.UInt32:
                        resultData = Convert.ToUInt32(chkData);
                        break;
                    case Types.Int64:
                        resultData = Convert.ToInt64(chkData);
                        break;
                    case Types.UInt64:
                        resultData = Convert.ToUInt64(chkData);
                        break;
                    case Types.Float:
                        resultData = Convert.ToSingle(chkData);
                        break;
                    case Types.Double:
                        resultData = chkData;
                        break;
                    default:
                        resultData = null;
                        result = false;
                        break;
                }
            }
            catch
            {
                resultData = null;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 입력된 데이터의 범위(<c>min &lt;= data &lt;= max</c>)를 확인하여 데이터를 범위에 맞게 반환하는 함수 <br/>
        /// </summary>
        /// <param name="data">확인할 데이터</param>
        /// <param name="dataType">입력된 데이터의 자료형</param>
        /// <param name="size">데이터 개수</param>
        /// <param name="min">최소값</param>
        /// <param name="max">최대값</param>
        /// <param name="resultData">
        /// 데이터가 <c>min</c>보다 작으면 <c>min</c> 반환 <br/>
        /// 데이터가 범위 안에 들어왔다면 <c>data</c> 반환 <br/>
        /// 데이터가 <c>max</c>보다 크면 <c>max</c> 반환
        /// </param>
        /// <returns>
        /// true: 실행 성공 <br/>
        /// false: 실행 실패 (data == null, Type Convert 실패)
        /// </returns>
        public static bool DataMinMaxCheck(object data, string dataType, int size, double min, double max, out object resultData)
        {
            if (!Enum.TryParse(dataType, out Types type))
            {
                resultData = null;
                return false;
            }

            return DataMinMaxCheck(data, type, size, min, max, out resultData);
        }

        /// <summary>
        /// 입력된 데이터의 범위(<c>min &lt;= data &lt;= max</c>)를 확인하여 데이터를 범위에 맞게 반환하는 함수 <br/>
        /// (단, 데이터가 범위를 벗어났을 경우 defaultValue로 치환함) <br/>
        /// (단, defaultValue가 범위를 벗어났을 경우 min 또는 max로 치환함)
        /// </summary>
        /// <param name="data">확인할 데이터</param>
        /// <param name="dataType">입력된 데이터의 자료형</param>
        /// <param name="size">데이터 개수</param>
        /// <param name="min">최소값</param>
        /// <param name="max">최대값</param>
        /// <param name="defaultValue">기본값</param>
        /// <param name="resultData">
        /// 데이터가 <c>min</c>보다 작으면 <c>defaultValue</c> 반환 <br/>
        /// 데이터가 범위 안에 들어왔다면 <c>data</c> 반환 <br/>
        /// 데이터가 <c>max</c>보다 크면 <c>defaultValue</c> 반환
        /// </param>
        /// <returns>
        /// true: 실행 성공 <br/>
        /// false: 실행 실패 (data == null, Type Convert 실패)
        /// </returns>
        public static bool DataMinMaxCheck(object data, Types dataType, int size, double min, double max, double defaultValue, out object resultData)
        {
            if (data == null)
            {
                resultData = null;
                return false;
            }
            bool isNotConvertType = dataType == Types.String || dataType == Types.Bool || dataType == Types.DateTime || size > 1;
            if (isNotConvertType)
            {
                resultData = data;
                return true;
            }

            if (defaultValue < min ||
                defaultValue > max)
            {
                return DataMinMaxCheck(data, dataType, size, min, max, out resultData);
            }

            bool result = true;

            try
            {
                double chkData = Convert.ToDouble(data, CultureInfo.InvariantCulture);
                chkData = chkData < min ? defaultValue : chkData;
                chkData = chkData > max ? defaultValue : chkData;

                switch (dataType)
                {
                    case Types.Bit:
                    case Types.UInt8:
                        resultData = Convert.ToByte(chkData);
                        break;
                    case Types.Int8:
                        resultData = Convert.ToSByte(chkData);
                        break;
                    case Types.Int16:
                        resultData = Convert.ToInt16(chkData);
                        break;
                    case Types.UInt16:
                        resultData = Convert.ToUInt16(chkData);
                        break;
                    case Types.Int32:
                        resultData = Convert.ToInt32(chkData);
                        break;
                    case Types.UInt32:
                        resultData = Convert.ToUInt32(chkData);
                        break;
                    case Types.Int64:
                        resultData = Convert.ToInt64(chkData);
                        break;
                    case Types.UInt64:
                        resultData = Convert.ToUInt64(chkData);
                        break;
                    case Types.Float:
                        resultData = Convert.ToSingle(chkData);
                        break;
                    case Types.Double:
                        resultData = chkData;
                        break;
                    default:
                        resultData = null;
                        result = false;
                        break;
                }
            }
            catch (Exception)
            {
                resultData = null;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 입력된 데이터의 범위(<c>min &lt;= data &lt;= max</c>)를 확인하여 데이터를 범위에 맞게 반환하는 함수 <br/>
        /// (단, 데이터가 범위를 벗어났을 경우 defaultValue로 치환함) <br/>
        /// (단, defaultValue가 범위를 벗어났을 경우 min 또는 max로 치환함)
        /// </summary>
        /// <param name="data">확인할 데이터</param>
        /// <param name="dataType">입력된 데이터의 자료형</param>
        /// <param name="size">데이터 개수</param>
        /// <param name="min">최소값</param>
        /// <param name="max">최대값</param>
        /// <param name="defaultValue">기본값</param>
        /// <param name="resultData">
        /// 데이터가 <c>min</c>보다 작으면 <c>defaultValue</c> 반환 <br/>
        /// 데이터가 범위 안에 들어왔다면 <c>data</c> 반환 <br/>
        /// 데이터가 <c>max</c>보다 크면 <c>defaultValue</c> 반환
        /// </param>
        /// <returns>
        /// true: 실행 성공 <br/>
        /// false: 실행 실패 (data == null, Type Convert 실패)
        /// </returns>
        public static bool DataMinMaxCheck(object data, string dataType, int size, double min, double max, double defaultValue, out object resultData)
        {
            if (!Enum.TryParse(dataType, out Types type))
            {
                resultData = null;
                return false;
            }

            return DataMinMaxCheck(data, type, size, min, max, defaultValue, out resultData);
        }

        /// <summary>
        /// Object 데이터에 값을 곱하는 함수
        /// </summary>
        /// <param name="data">곱할 데이터</param>
        /// <param name="dataType">object 데이터의 자료형</param>
        /// <param name="size">데이터의 개수(1보다 클 경우 배열로 인식)</param>
        /// <param name="multiply">데이터에 곱할 값</param>
        /// <returns>곱해진 데이터</returns>
        public static object ObjectMultiply(object data, Types dataType, int size, double multiply)
        {
            if (data == null)
            {
                return null;
            }

            object result = data;

            switch (dataType)
            {
                case Types.Int16:
                    {
                        if (size > 1)
                        {
                            object[] convDatas = (object[])data;
                            short[] datas = new short[convDatas.Length];
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToShort(Convert.ToInt16(convDatas[i], CultureInfo.InvariantCulture) * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToShort(Convert.ToInt16(data, CultureInfo.InvariantCulture) * multiply);
                        }
                    }
                    break;
                case Types.UInt16:
                    {
                        if (size > 1)
                        {
                            object[] convDatas = (object[])data;
                            ushort[] datas = new ushort[convDatas.Length];
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToUShort(Convert.ToUInt16(convDatas[i], CultureInfo.InvariantCulture) * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToUShort(Convert.ToUInt16(data, CultureInfo.InvariantCulture) * multiply);
                        }
                    }
                    break;
                case Types.Int32:
                    {
                        if (size > 1)
                        {
                            object[] convDatas = (object[])data;
                            int[] datas = new int[convDatas.Length];
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToInt(Convert.ToInt32(convDatas[i], CultureInfo.InvariantCulture) * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToInt(Convert.ToInt32(data, CultureInfo.InvariantCulture) * multiply);
                        }
                    }
                    break;
                case Types.UInt32:
                    {
                        if (size > 1)
                        {
                            object[] convDatas = (object[])data;
                            uint[] datas = new uint[convDatas.Length];
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToUInt(Convert.ToUInt32(convDatas[i], CultureInfo.InvariantCulture) * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToUInt(Convert.ToUInt32(data, CultureInfo.InvariantCulture) * multiply);
                        }
                    }
                    break;
                case Types.Int64:
                    {
                        if (size > 1)
                        {
                            object[] convDatas = (object[])data;
                            long[] datas = new long[convDatas.Length];
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToLong(Convert.ToInt64(convDatas[i], CultureInfo.InvariantCulture) * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToLong(Convert.ToInt64(data, CultureInfo.InvariantCulture) * multiply);
                        }
                    }
                    break;
                case Types.UInt64:
                    {
                        if (size > 1)
                        {
                            object[] convDatas = (object[])data;
                            ulong[] datas = new ulong[convDatas.Length];
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToULong(Convert.ToUInt64(convDatas[i], CultureInfo.InvariantCulture) * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToULong(Convert.ToUInt64(data, CultureInfo.InvariantCulture) * multiply);
                        }
                    }
                    break;
                case Types.Float:
                    {
                        if (size > 1)
                        {
                            object[] convDatas = (object[])data;
                            float[] datas = new float[convDatas.Length];
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToFloat(Convert.ToSingle(convDatas[i], CultureInfo.InvariantCulture) * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToFloat(Convert.ToSingle(data, CultureInfo.InvariantCulture) * multiply);
                        }
                    }
                    break;
                case Types.Double:
                    {
                        if (size > 1)
                        {
                            object[] convDatas = (object[])data;
                            double[] datas = new double[convDatas.Length];
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = Convert.ToDouble(convDatas[i], CultureInfo.InvariantCulture) * multiply;
                            }
                            result = datas;
                        }
                        else
                        {
                            result = Convert.ToDouble(data, CultureInfo.InvariantCulture) * multiply;
                        }
                    }
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// double을 byte로 변환하는 함수 <br/>
        /// (값이 byte의 최소 범위보다 작으면 byte.MinValue를 반환) <br/>
        /// (값이 byte의 최대 범위보다 크면 byte.MaxValue를 반환)
        /// </summary>
        /// <param name="value">변환할 double 값</param>
        /// <returns>변환한 값</returns>
        public static byte DoubleToByte(double value)
        {
            if (value < byte.MinValue)
            {
                return byte.MinValue;
            }
            else if (value > byte.MaxValue)
            {
                return byte.MaxValue;
            }
            return (byte)value;
        }

        /// <summary>
        /// double을 sbyte로 변환하는 함수 <br/>
        /// (값이 sbyte의 최소 범위보다 작으면 sbyte.MinValue를 반환) <br/>
        /// (값이 sbyte의 최대 범위보다 크면 sbyte.MaxValue를 반환)
        /// </summary>
        /// <param name="value">변환할 double 값</param>
        /// <returns>변환한 값</returns>
        public static sbyte DoubleToSByte(double value)
        {
            if (value < sbyte.MinValue)
            {
                return sbyte.MinValue;
            }
            else if (value > sbyte.MaxValue)
            {
                return sbyte.MaxValue;
            }
            return (sbyte)value;
        }

        /// <summary>
        /// double을 short로 변환하는 함수 <br/>
        /// (값이 short의 최소 범위보다 작으면 short.MinValue를 반환) <br/>
        /// (값이 short의 최대 범위보다 크면 short.MaxValue를 반환)
        /// </summary>
        /// <param name="value">변환할 double 값</param>
        /// <returns>변환한 값</returns>
        public static short DoubleToShort(double value)
        {
            if (value < short.MinValue)
            {
                return short.MinValue;
            }
            else if (value > short.MaxValue)
            {
                return short.MaxValue;
            }
            return (short)value;
        }

        /// <summary>
        /// double을 ushort로 변환하는 함수<br/>
        /// (값이 ushort의 최소 범위보다 작으면 ushort.MinValue를 반환) <br/>
        /// (값이 ushort의 최대 범위보다 크면 ushort.MaxValue를 반환)
        /// </summary>
        /// <param name="value">변환할 double 값</param>
        /// <returns>변환한 값</returns>
        public static ushort DoubleToUShort(double value)
        {
            if (value < ushort.MinValue)
            {
                return ushort.MinValue;
            }
            else if (value > ushort.MaxValue)
            {
                return ushort.MaxValue;
            }
            return (ushort)value;
        }

        /// <summary>
        /// double을 int로 변환하는 함수 <br/>
        /// (값이 int의 최소 범위보다 작으면 int.MinValue를 반환) <br/>
        /// (값이 int의 최대 범위보다 크면 int.MaxValue를 반환)
        /// </summary>
        /// <param name="value">변환할 double 값</param>
        /// <returns>변환한 값</returns>
        public static int DoubleToInt(double value)
        {
            if (value < int.MinValue)
            {
                return int.MinValue;
            }
            else if (value > int.MaxValue)
            {
                return int.MaxValue;
            }
            return (int)value;
        }

        /// <summary>
        /// double을 uint로 변환하는 함수 <br/>
        /// (값이 uint의 최소 범위보다 작으면 uint.MinValue를 반환) <br/>
        /// (값이 uint의 최대 범위보다 크면 uint.MaxValue를 반환)
        /// </summary>
        /// <param name="value">변환할 double 값</param>
        /// <returns>변환한 값</returns>
        public static uint DoubleToUInt(double value)
        {
            if (value < uint.MinValue)
            {
                return uint.MinValue;
            }
            else if (value > uint.MaxValue)
            {
                return uint.MaxValue;
            }
            return (uint)value;
        }

        /// <summary>
        /// double을 long으로 변환하는 함수 <br/>
        /// (값이 long의 최소 범위보다 작으면 long.MinValue를 반환) <br/>
        /// (값이 long의 최대 범위보다 크면 long.MaxValue를 반환)
        /// </summary>
        /// <param name="value">변환할 double 값</param>
        /// <returns>변환한 값</returns>
        public static long DoubleToLong(double value)
        {
            if (value < long.MinValue)
            {
                return long.MinValue;
            }
            else if (value > long.MaxValue)
            {
                return long.MaxValue;
            }
            return (long)value;
        }

        /// <summary>
        /// double을 ulong으로 변환하는 함수 <br/>
        /// (값이 ulong의 최소 범위보다 작으면 ulong.MinValue를 반환) <br/>
        /// (값이 ulong의 최대 범위보다 크면 ulong.MaxValue를 반환)
        /// </summary>
        /// <param name="value">변환할 double 값</param>
        /// <returns>변환한 값</returns>
        public static ulong DoubleToULong(double value)
        {
            if (value < ulong.MinValue)
            {
                return ulong.MinValue;
            }
            else if (value > ulong.MaxValue)
            {
                return ulong.MaxValue;
            }
            return (ulong)value;
        }

        /// <summary>
        /// double을 float로 변환하는 함수 <br/>
        /// (값이 float의 최소 범위보다 작으면 float.MinValue를 반환) <br/>
        /// (값이 float의 최대 범위보다 크면 float.MaxValue를 반환)
        /// </summary>
        /// <param name="value">변환할 double 값</param>
        /// <returns>변환한 값</returns>
        public static float DoubleToFloat(double value)
        {
            if (value < float.MinValue)
            {
                return float.MinValue;
            }
            else if (value > float.MaxValue)
            {
                return float.MaxValue;
            }
            return (float)value;
        }
    }
}
