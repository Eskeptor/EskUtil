// ======================================================================================================
// File Name        : DataType.cs
// Project          : CSUtil
// Last Update      : 2024.09.16 - yc.jeon
// ======================================================================================================

using System.Diagnostics;

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
            STRING,     // STR_STRING
            BOOL,       // STR_BOOL
            CHAR,       // STR_CHAR
            SHORT,      // STR_SHORT
            USHORT,     // STR_USHORT
            FLOAT,      // STR_FLOAT
            INT,        // STR_INT
            UINT,       // STR_UINT
            DOUBLE,     // STR_DOUBLE
            LONG,       // STR_LONG
            ULONG,      // STR_ULONG
            BIT,        // STR_BIT
            BYTE,       // STR_BYTE
            SBYTE,      // STR_SBYTE
            DATETIME,   // STR_DATETIME
            LIST,       // STR_LIST
        }

        /// <summary>
        /// Types.STRING의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_STRING = "STRING";
        /// <summary>
        /// Types.BOOL의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_BOOL = "BOOL";
        /// <summary>
        /// Types.CHAR의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_CHAR = "CHAR";
        /// <summary>
        /// Types.SHORT의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_SHORT = "SHORT";
        /// <summary>
        /// Types.USHORT의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_USHORT = "USHORT";
        /// <summary>
        /// Types.FLOAT의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_FLOAT = "FLOAT";
        /// <summary>
        /// Types.INT의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_INT = "INT";
        /// <summary>
        /// Types.UINT의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_UINT = "UINT";
        /// <summary>
        /// Types.DOUBLE의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_DOUBLE = "DOUBLE";
        /// <summary>
        /// Types.LONG의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_LONG = "LONG";
        /// <summary>
        /// Types.ULONG의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_ULONG = "ULONG";
        /// <summary>
        /// Types.BIT의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_BIT = "BIT";
        /// <summary>
        /// Types.BYTE의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_BYTE = "BYTE";
        /// <summary>
        /// Types.SBYTE의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_SBYTE = "SBYTE";
        /// <summary>
        /// Types.DATETIME의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_DATETIME = "DATETIME";
        /// <summary>
        /// Types.LIST의 String 타입값 (Upper case)
        /// </summary>
        public const string STR_LIST = "LIST";

        /// <summary>
        /// Types.STRING의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_STRING_L = "string";
        /// <summary>
        /// Types.BOOL의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_BOOL_L = "bool";
        /// <summary>
        /// Types.CHAR의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_CHAR_L = "char";
        /// <summary>
        /// Types.SHORT의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_SHORT_L = "short";
        /// <summary>
        /// Types.USHORT의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_USHORT_L = "ushort";
        /// <summary>
        /// Types.FLOAT의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_FLOAT_L = "float";
        /// <summary>
        /// Types.INT의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_INT_L = "int";
        /// <summary>
        /// Types.UINT의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_UINT_L = "uint";
        /// <summary>
        /// Types.DOUBLE의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_DOUBLE_L = "double";
        /// <summary>
        /// Types.LONG의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_LONG_L = "long";
        /// <summary>
        /// Types.ULONG의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_ULONG_L = "ulong";
        /// <summary>
        /// Types.BIT의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_BIT_L = "bit";
        /// <summary>
        /// Types.BYTE의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_BYTE_L = "byte";
        /// <summary>
        /// Types.SBYTE의 String 타입값 (Lower case);
        /// </summary>
        public const string STR_SBYTE_L = "sbyte";
        /// <summary>
        /// Types.DATETIME의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_DATETIME_L = "datetime";
        /// <summary>
        /// Types.LIST의 String 타입값 (Lower case)
        /// </summary>
        public const string STR_LIST_L = "list";

        /// <summary>
        /// DefinedData를 정의하기 위한 클래스
        /// </summary>
        public struct DataTypeCls
        {
            /// <summary>
            /// 자료형의 Types (enum)
            /// </summary>
            public Types Type { get; private set; }
            /// <summary>
            /// 자료형의 Types (String)
            /// </summary>
            public string TypeToString { get; private set; }
            /// <summary>
            /// 자료형의 바이트 크기
            /// </summary>
            public int ByteSize { get; private set; }
            /// <summary>
            /// 자료형의 실제 Type(typeof)
            /// </summary>
            public Type RealType { get; private set; }
            /// <summary>
            /// 해당 자료형의 최소값
            /// </summary>
            public double MinValue { get; private set; }
            /// <summary>
            /// 해당 자료형의 최대값
            /// </summary>
            public double MaxValue { get; private set; }

            /// <summary>
            /// 생성자
            /// </summary>
            /// <param name="enumType">자료형의 Types (enum)</param>
            /// <param name="byteSize">자료형의 바이트 크기</param>
            /// <param name="realType">자료형의 실제 Type(typeof)</param>
            /// <param name="minValue">자료형의 최소값</param>
            /// <param name="maxValue">자료형의 최대값</param>
            public DataTypeCls(Types enumType, int byteSize, Type realType, double minValue, double maxValue)
            {
                Type = enumType;
                TypeToString = DefinedTypes[enumType].Item1;
                ByteSize = byteSize;
                RealType = realType;
                MinValue = minValue;
                MaxValue = maxValue;
            }
        }

        /// <summary>
        /// 정의된 자료형(string 타입)
        /// </summary>
        public static readonly Dictionary<Types, Tuple<string, int>> DefinedTypes = new Dictionary<Types, Tuple<string, int>>
        {
            { Types.STRING, new Tuple<string, int>(STR_STRING, (int)Types.STRING) },
            { Types.BOOL, new Tuple<string, int>(STR_BOOL, (int)Types.BOOL) },
            { Types.CHAR, new Tuple<string, int>(STR_CHAR, (int)Types.CHAR) },
            { Types.SHORT, new Tuple<string, int>(STR_SHORT, (int)Types.SHORT) },
            { Types.USHORT, new Tuple<string, int>(STR_USHORT, (int)Types.USHORT) },
            { Types.FLOAT, new Tuple<string, int>(STR_FLOAT, (int)Types.FLOAT) },
            { Types.INT, new Tuple<string, int>(STR_INT, (int)Types.INT) },
            { Types.UINT, new Tuple<string, int>(STR_UINT, (int)Types.UINT) },
            { Types.DOUBLE, new Tuple<string, int>(STR_DOUBLE, (int)Types.DOUBLE) },
            { Types.LONG, new Tuple<string, int>(STR_LONG, (int)Types.LONG) },
            { Types.ULONG, new Tuple<string, int>(STR_ULONG, (int)Types.ULONG) },
            { Types.BIT, new Tuple<string, int>(STR_BIT, (int)Types.BIT) },
            { Types.BYTE, new Tuple<string, int>(STR_BYTE, (int)Types.BYTE) },
            { Types.SBYTE, new Tuple<string, int>(STR_SBYTE, (int)Types.SBYTE) },
            { Types.DATETIME, new Tuple<string, int>(STR_DATETIME, (int)Types.DATETIME) },
            { Types.LIST, new Tuple<string, int>(STR_LIST, (int)Types.LIST) },
        };

        /// <summary>
        /// 정의된 자료형 (실제로 사용할 수 있는)
        /// </summary>
        public static readonly Dictionary<string, DataTypeCls> DefinedData = new Dictionary<string, DataTypeCls>
        {
            { DefinedTypes[Types.STRING].Item1, new DataTypeCls(Types.STRING, 1, typeof(string), 0.0, 0.0) },
            { DefinedTypes[Types.BOOL].Item1, new DataTypeCls(Types.BOOL, 1, typeof(bool), 0.0, 1.0) },
            { DefinedTypes[Types.CHAR].Item1, new DataTypeCls(Types.CHAR, 1, typeof(char), char.MinValue, char.MaxValue) },
            { DefinedTypes[Types.SHORT].Item1, new DataTypeCls(Types.SHORT, 2, typeof(short), short.MinValue, short.MaxValue) },
            { DefinedTypes[Types.USHORT].Item1, new DataTypeCls(Types.USHORT, 2, typeof(ushort), ushort.MinValue, ushort.MaxValue) },
            { DefinedTypes[Types.FLOAT].Item1, new DataTypeCls(Types.FLOAT, 4, typeof(float), float.MinValue, float.MaxValue) },
            { DefinedTypes[Types.INT].Item1, new DataTypeCls(Types.INT, 4, typeof(int), int.MinValue, int.MaxValue) },
            { DefinedTypes[Types.UINT].Item1, new DataTypeCls(Types.UINT, 4, typeof(uint), uint.MinValue, uint.MaxValue) },
            { DefinedTypes[Types.DOUBLE].Item1, new DataTypeCls(Types.DOUBLE, 8, typeof(double), double.MinValue, double.MaxValue) },
            { DefinedTypes[Types.LONG].Item1, new DataTypeCls(Types.LONG, 8, typeof(long), long.MinValue, long.MaxValue) },
            { DefinedTypes[Types.ULONG].Item1, new DataTypeCls(Types.ULONG, 8, typeof(ulong), ulong.MinValue, ulong.MaxValue) },
            { DefinedTypes[Types.BIT].Item1, new DataTypeCls(Types.BIT, 1, typeof(byte), byte.MinValue, byte.MaxValue) },
            { DefinedTypes[Types.BYTE].Item1, new DataTypeCls(Types.BYTE, 1, typeof(byte), byte.MinValue, byte.MaxValue) },
            { DefinedTypes[Types.SBYTE].Item1, new DataTypeCls(Types.SBYTE, 1, typeof(sbyte), sbyte.MinValue, sbyte.MaxValue) },
            { DefinedTypes[Types.DATETIME].Item1, new DataTypeCls(Types.DATETIME, 8, typeof(DateTime), 0.0, 0.0) },
            { DefinedTypes[Types.LIST].Item1, new DataTypeCls(Types.LIST, 8, typeof(string), 0.0, 0.0) }
        };

        /// <summary>
        /// 유효한 데이터 타입인지 확인하는 함수
        /// </summary>
        /// <param name="typeName">확인할 데이터 타입</param>
        /// <returns>true: 유효, false: 유효하지 않음</returns>
        public static bool IsValidType(string typeName)
        {
            return DefinedData.ContainsKey(typeName);
        }

        /// <summary>
        /// 문자열(String) 데이터를 각자의 형으로 변환하고 object로 반환하는 함수
        /// </summary>
        /// <param name="value">변환할 문자열 데이터</param>
        /// <param name="dataType">변환 타입</param>
        /// <param name="size">원본 데이터의 개수</param>
        /// <param name="token">원본 데이터가 배열인 경우 문자열을 자를 문자 단위</param>
        /// <returns>변환된 값</returns>
        public static object? StringToObject(string value, string dataType, int size, char token)
        {
            if (!DefinedData.TryGetValue(dataType, out DataTypeCls dataTypeCls))
            {
                return null;
            }

            return StringToObject(value, dataTypeCls.Type, size, token);
        }

        /// <summary>
        /// 문자열(String) 데이터를 각자의 형으로 변환하고 object로 반환하는 함수
        /// </summary>
        /// <param name="value">변환할 문자열 데이터</param>
        /// <param name="dataType">변환 타입</param>
        /// <param name="size">원본 데이터의 개수</param>
        /// <param name="token">원본 데이터가 배열인 경우 문자열을 자를 문자 단위</param>
        /// <returns>변환된 값</returns>
        public static object? StringToObject(string value, Types dataType, int size, char token)
        {
            try
            {
                switch (dataType)
                {
                    case Types.STRING:
                        return value;
                    case Types.BOOL:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new bool[size];
                            }

                            string[] splits = value.Split(token);
                            bool[] datas = splits.Select(ch => ch == "true").ToArray();
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? false : Convert.ToBoolean(value);
                    case Types.BIT:
                    case Types.BYTE:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new byte[size];
                            }

                            string[] splits = value.Split(token);
                            byte[] datas = Array.ConvertAll(splits, Convert.ToByte);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? (byte)0 : Convert.ToByte(value);
                    case Types.SBYTE:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new sbyte[size];
                            }

                            string[] splits = value.Split(token);
                            sbyte[] datas = Array.ConvertAll(splits, Convert.ToSByte);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? (sbyte)0 : Convert.ToSByte(value);
                    case Types.CHAR:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new char[size];
                            }

                            string[] splits = value.Split(token);
                            char[] datas = Array.ConvertAll(splits, Convert.ToChar);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? char.MinValue : Convert.ToChar(value);
                    case Types.SHORT:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new short[size];
                            }

                            string[] splits = value.Split(token);
                            short[] datas = Array.ConvertAll(splits, Convert.ToInt16);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? (short)0 : Convert.ToInt16(value);
                    case Types.USHORT:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new ushort[size];
                            }

                            string[] splits = value.Split(token);
                            ushort[] datas = Array.ConvertAll(splits, Convert.ToUInt16);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? (ushort)0 : Convert.ToUInt16(value);
                    case Types.INT:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new int[size];
                            }

                            string[] splits = value.Split(token);
                            int[] datas = Array.ConvertAll(splits, Convert.ToInt32);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? 0 : Convert.ToInt32(value);
                    case Types.UINT:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new uint[size];
                            }

                            string[] splits = value.Split(token);
                            uint[] datas = Array.ConvertAll(splits, Convert.ToUInt32);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? 0 : Convert.ToUInt32(value);
                    case Types.LONG:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new long[size];
                            }

                            string[] splits = value.Split(token);
                            long[] datas = Array.ConvertAll(splits, Convert.ToInt64);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? 0L : Convert.ToInt64(value);
                    case Types.ULONG:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new ulong[size];
                            }

                            string[] splits = value.Split(token);
                            ulong[] datas = Array.ConvertAll(splits, Convert.ToUInt64);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? 0UL : Convert.ToUInt64(value);
                    case Types.FLOAT:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new float[size];
                            }

                            string[] splits = value.Split(token);
                            float[] datas = Array.ConvertAll(splits, Convert.ToSingle);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? 0.0F : Convert.ToSingle(value);
                    case Types.DOUBLE:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new double[size];
                            }

                            string[] splits = value.Split(token);
                            double[] datas = Array.ConvertAll(splits, Convert.ToDouble);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? 0.0 : Convert.ToDouble(value);
                    case Types.DATETIME:
                        if (size > 1)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return new DateTime[size];
                            }

                            string[] splits = value.Split(token);
                            DateTime[] datas = Array.ConvertAll(splits, DateTime.Parse);
                            return datas;
                        }
                        return string.IsNullOrEmpty(value) ? DateTime.MinValue : DateTime.Parse(value);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine($"StringToObject: {ex}");
#endif
            }

            return null;
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
            if (!DefinedData.TryGetValue(dataType, out DataTypeCls dataTypeCls))
            {
                return string.Empty;
            }

            return ObjectToString(value, dataTypeCls.Type, size, token);
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
            string result = string.Empty;

            if (value == null)
            {
                return string.Empty;
            }

            if (size == 1)
            {
                result = value.ToString()!.TrimEnd('\0');
            }
            else
            {
                switch (dataType)
                {
                    case Types.STRING:
                        {
                            result = value.ToString()!.TrimEnd('\0');
                            //string[] datas = (string[])value;
                            //result = UtilFunc.OneLineString(datas, token);
                        }
                        break;
                    case Types.BOOL:
                        {
                            bool[] datas = (bool[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.BIT:
                    case Types.BYTE:
                        {
                            byte[] datas = (byte[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.SBYTE:
                        {
                            sbyte[] datas = (sbyte[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.CHAR:
                        {
                            char[] datas = (char[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.SHORT:
                        {
                            short[] datas = (short[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.USHORT:
                        {
                            ushort[] datas = (ushort[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.INT:
                        {
                            int[] datas = (int[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.UINT:
                        {
                            uint[] datas = (uint[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.LONG:
                        {
                            long[] datas = (long[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.ULONG:
                        {
                            ulong[] datas = (ulong[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.FLOAT:
                        {
                            float[] datas = (float[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.DOUBLE:
                        {
                            double[] datas = (double[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
                    case Types.DATETIME:
                        {
                            DateTime[] datas = (DateTime[])value;
                            result = ExtensionUtil.OneLineString(datas, token);
                        }
                        break;
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
        public static object? MakeObject(string dataType, int size)
        {
            if (!DefinedData.TryGetValue(dataType, out DataTypeCls dataTypeCls))
            {
                return null;
            }

            return MakeObject(dataTypeCls.Type, size);
        }
        /// <summary>
        /// 자료형에 맞는 초기 데이터를 만들어 object 형식으로 반환하는 함수
        /// </summary>
        /// <param name="dataType">데이터 타입</param>
        /// <param name="size">데이터 개수(바이트 길이 아님)</param>
        /// <returns>만들어진 object</returns>
        public static object? MakeObject(Types dataType, int size)
        {
            if (size <= 0)
            {
                return null;
            }

            object? objValue = null;
            switch (dataType)
            {
                case Types.BYTE:
                case Types.BIT:
                    objValue = size > 1 ? new byte[size] : (byte)0;
                    break;
                case Types.BOOL:
                    objValue = size > 1 ? new bool[size] : false;
                    break;
                case Types.STRING:
                    objValue = string.Empty;
                    break;
                case Types.SHORT:
                    objValue = size > 1 ? new short[size] : (short)0;
                    break;
                case Types.USHORT:
                    objValue = size > 1 ? new ushort[size] : (ushort)0;
                    break;
                case Types.INT:
                    objValue = size > 1 ? new int[size] : 0;
                    break;
                case Types.UINT:
                    objValue = size > 1 ? new uint[size] : (uint)0;
                    break;
                case Types.LONG:
                    objValue = size > 1 ? new long[size] : 0L;
                    break;
                case Types.ULONG:
                    objValue = size > 1 ? new ulong[size] : 0UL;
                    break;
                case Types.FLOAT:
                    objValue = size > 1 ? new float[size] : 0.0F;
                    break;
                case Types.DOUBLE:
                    objValue = size > 1 ? new double[size] : 0.0;
                    break;
                case Types.DATETIME:
                    objValue = size > 1 ? new DateTime[size] : DateTime.Now;
                    break;
            }

            return objValue;
        }

        /// <summary>
        /// object 타입의 데이터를 다른 타입의 데이터로 변환하는 함수
        /// </summary>
        /// <param name="srcData">원본 데이터</param>
        /// <param name="destType">변환할 타입</param>
        /// <returns>변환한 데이터</returns>
        public static object? DataToObject(object srcData, string destType)
        {
            if (!DefinedData.TryGetValue(destType, out DataTypeCls dataTypeCls))
            {
                return null;
            }

            return DataToObject(srcData, dataTypeCls.Type);
        }

        /// <summary>
        /// object 타입의 데이터를 다른 타입의 데이터로 변환하는 함수
        /// </summary>
        /// <param name="srcData">원본 데이터</param>
        /// <param name="destType">변환할 타입</param>
        /// <returns>변환한 데이터</returns>
        public static object? DataToObject<T>(T srcData, Types destType)
        {
            if (srcData == null)
            {
                return null;
            }

            try
            {
                switch (destType)
                {
                    case Types.STRING:
                        return srcData.ToString();
                    case Types.BOOL:
                        return Convert.ToBoolean(srcData);
                    case Types.BIT:
                    case Types.BYTE:
                        return Convert.ToByte(srcData);
                    case Types.SBYTE:
                        return Convert.ToSByte(srcData);
                    case Types.CHAR:
                        return Convert.ToChar(srcData);
                    case Types.SHORT:
                        return Convert.ToInt16(srcData);
                    case Types.USHORT:
                        return Convert.ToUInt16(srcData);
                    case Types.INT:
                        return Convert.ToInt32(srcData);
                    case Types.UINT:
                        return Convert.ToUInt32(srcData);
                    case Types.LONG:
                        return Convert.ToInt64(srcData);
                    case Types.ULONG:
                        return Convert.ToUInt64(srcData);
                    case Types.FLOAT:
                        return Convert.ToSingle(srcData);
                    case Types.DOUBLE:
                        return Convert.ToDouble(srcData);
                    case Types.DATETIME:
                        return Convert.ToDateTime(srcData);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine($"DataToObject: {ex}");
#endif
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
        public static bool DataMinMaxCheck(object data, Types dataType, int size, double min, double max, out object? resultData)
        {
            if (data == null)
            {
                resultData = null;
                return false;
            }
            if (dataType == Types.STRING ||
                size > 1)
            {
                resultData = data;
                return true;
            }

            bool result = true;

            try
            {
                double chkData = Convert.ToDouble(data);
                chkData = chkData < min ? min : chkData;
                chkData = chkData > max ? max : chkData;

                switch (dataType)
                {
                    case Types.BOOL:
                        resultData = Convert.ToBoolean(chkData);
                        break;
                    case Types.BIT:
                    case Types.BYTE:
                        resultData = Convert.ToByte(chkData);
                        break;
                    case Types.SBYTE:
                        resultData = Convert.ToSByte(chkData);
                        break;
                    case Types.CHAR:
                        resultData = Convert.ToChar(chkData);
                        break;
                    case Types.SHORT:
                        resultData = Convert.ToInt16(chkData);
                        break;
                    case Types.USHORT:
                        resultData = Convert.ToUInt16(chkData);
                        break;
                    case Types.INT:
                        resultData = Convert.ToInt32(chkData);
                        break;
                    case Types.UINT:
                        resultData = Convert.ToUInt32(chkData);
                        break;
                    case Types.LONG:
                        resultData = Convert.ToInt64(chkData);
                        break;
                    case Types.ULONG:
                        resultData = Convert.ToUInt64(chkData);
                        break;
                    case Types.FLOAT:
                        resultData = Convert.ToSingle(chkData);
                        break;
                    case Types.DOUBLE:
                        resultData = chkData;
                        break;
                    case Types.DATETIME:
                        resultData = Convert.ToDateTime(chkData);
                        break;
                    default:
                        resultData = null;
                        result = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                resultData = null;
                result = false;
#if DEBUG
                Debug.WriteLine($"DataMinMaxCheck: {ex}");
#endif
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
        public static bool DataMinMaxCheck(object data, string dataType, int size, double min, double max, out object? resultData)
        {
            if (!DefinedData.TryGetValue(dataType, out DataTypeCls dataTypeCls))
            {
                resultData = null;
                return false;
            }

            return DataMinMaxCheck(data, dataTypeCls.Type, size, min, max, out resultData);
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
        public static bool DataMinMaxCheck(object data, Types dataType, int size, double min, double max, double defaultValue, out object? resultData)
        {
            if (data == null)
            {
                resultData = null;
                return false;
            }
            if (dataType == Types.STRING ||
                size > 1)
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
                double chkData = Convert.ToDouble(data);
                chkData = chkData < min ? defaultValue : chkData;
                chkData = chkData > max ? defaultValue : chkData;

                switch (dataType)
                {
                    case Types.BOOL:
                        resultData = Convert.ToBoolean(chkData);
                        break;
                    case Types.BIT:
                    case Types.BYTE:
                        resultData = Convert.ToByte(chkData);
                        break;
                    case Types.SBYTE:
                        resultData = Convert.ToSByte(chkData);
                        break;
                    case Types.CHAR:
                        resultData = Convert.ToChar(chkData);
                        break;
                    case Types.SHORT:
                        resultData = Convert.ToInt16(chkData);
                        break;
                    case Types.USHORT:
                        resultData = Convert.ToUInt16(chkData);
                        break;
                    case Types.INT:
                        resultData = Convert.ToInt32(chkData);
                        break;
                    case Types.UINT:
                        resultData = Convert.ToUInt32(chkData);
                        break;
                    case Types.LONG:
                        resultData = Convert.ToInt64(chkData);
                        break;
                    case Types.ULONG:
                        resultData = Convert.ToUInt64(chkData);
                        break;
                    case Types.FLOAT:
                        resultData = Convert.ToSingle(chkData);
                        break;
                    case Types.DOUBLE:
                        resultData = chkData;
                        break;
                    case Types.DATETIME:
                        resultData = Convert.ToDateTime(chkData);
                        break;
                    default:
                        resultData = null;
                        result = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                resultData = null;
                result = false;
#if DEBUG
                Debug.WriteLine($"DataMinMaxCheck: {ex}");
#endif
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
        public static bool DataMinMaxCheck(object data, string dataType, int size, double min, double max, double defaultValue, out object? resultData)
        {
            if (!DefinedData.TryGetValue(dataType, out DataTypeCls dataTypeCls))
            {
                resultData = null;
                return false;
            }

            return DataMinMaxCheck(data, dataTypeCls.Type, size, min, max, defaultValue, out resultData);
        }

        /// <summary>
        /// Object 데이터에 값을 곱하는 함수
        /// </summary>
        /// <param name="data">곱할 데이터</param>
        /// <param name="dataType">object 데이터의 자료형</param>
        /// <param name="size">데이터의 개수(1보다 클 경우 배열로 인식)</param>
        /// <param name="multiply">데이터에 곱할 값</param>
        /// <returns>곱해진 데이터</returns>
        public static object? ObjectMultiply(object data, Types dataType, int size, double multiply)
        {
            if (data == null)
            {
                return null;
            }

            object result = data;

            switch (dataType)
            {
                case Types.BYTE:
                    {
                        if (size > 1)
                        {
                            byte[] datas = (byte[])data;
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = Convert.ToByte(datas[i] * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = Convert.ToByte((byte)data * multiply);
                        }
                    }
                    break;
                case Types.SBYTE:
                    {
                        if (size > 1)
                        {
                            sbyte[] datas = (sbyte[])data;
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToSByte(datas[i] * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToSByte((sbyte)data * multiply);
                        }
                    }
                    break;
                case Types.SHORT:
                    {
                        if (size > 1)
                        {
                            short[] datas = (short[])data;
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToShort(datas[i] * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToShort((short)data * multiply);
                        }
                    }
                    break;
                case Types.USHORT:
                    {
                        if (size > 1)
                        {
                            ushort[] datas = (ushort[])data;
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToUShort(datas[i] * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToUShort((ushort)data * multiply);
                        }
                    }
                    break;
                case Types.INT:
                    {
                        if (size > 1)
                        {
                            int[] datas = (int[])data;
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToInt(datas[i] * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToInt((int)data * multiply);
                        }
                    }
                    break;
                case Types.UINT:
                    {
                        if (size > 1)
                        {
                            uint[] datas = (uint[])data;
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToUInt(datas[i] * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToUInt((uint)data * multiply);
                        }
                    }
                    break;
                case Types.LONG:
                    {
                        if (size > 1)
                        {
                            long[] datas = (long[])data;
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToLong(datas[i] * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToLong((long)data * multiply);
                        }
                    }
                    break;
                case Types.ULONG:
                    {
                        if (size > 1)
                        {
                            ulong[] datas = (ulong[])data;
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToULong(datas[i] * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToULong((ulong)data * multiply);
                        }
                    }
                    break;
                case Types.FLOAT:
                    {
                        if (size > 1)
                        {
                            float[] datas = (float[])data;
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = DoubleToFloat(datas[i] * multiply);
                            }
                            result = datas;
                        }
                        else
                        {
                            result = DoubleToFloat((float)data * multiply);
                        }
                    }
                    break;
                case Types.DOUBLE:
                    {
                        if (size > 1)
                        {
                            double[] datas = (double[])data;
                            for (int i = 0; i < datas.Length; ++i)
                            {
                                datas[i] = datas[i] * multiply;
                            }
                            result = datas;
                        }
                        else
                        {
                            result = (double)data * multiply;
                        }
                    }
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
