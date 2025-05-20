/**
* @file			ByteUtil.h
* @author		yc.jeon
* @date			2025-05-20
* @version		0.0.2
* @brief		Byte Utility
*/

#pragma once
#include "Common.h"
#include <Windows.h>

namespace esk::util_byte
{
#pragma pack(push, 1)
    /**
    * @author       yc.jeon
    * @brief        int8_t 자료형을 비트 또는 바이트 단위로 표현하기 위한 구조체
    */
    struct ByteInt8
    {
        union
        {
            int8_t Value;
            struct
            {
                int8_t Bit0 : 1;
                int8_t Bit1 : 1;
                int8_t Bit2 : 1;
                int8_t Bit3 : 1;
                int8_t Bit4 : 1;
                int8_t Bit5 : 1;
                int8_t Bit6 : 1;
                int8_t Bit7 : 1;
            } BitValue;
        };

        ByteInt8() noexcept
            : Value(0)
        {
        }

        ByteInt8(int8_t byValue) noexcept
            : Value(byValue)
        {
        }

        ByteInt8 operator-(const ByteInt8& in) const noexcept
        {
            ByteInt8 newData(Value - in.Value);
            return newData;
        }

        ByteInt8 operator+(const ByteInt8& in) const noexcept
        {
            ByteInt8 newData(Value + in.Value);
            return newData;
        }

        ByteInt8 operator*(const ByteInt8& in) const noexcept
        {
            ByteInt8 newData(Value * in.Value);
            return newData;
        }

        ByteInt8 operator/(const ByteInt8& in) const noexcept
        {
            ByteInt8 newData(Value / in.Value);
            return newData;
        }

        ByteInt8& operator=(const ByteInt8& in) noexcept
        {
            Value = in.Value;
            return *this;
        }

        ByteInt8& operator=(const int8_t& in) noexcept
        {
            Value = in;
            return *this;
        }

        ByteInt8& operator+=(const ByteInt8& in) noexcept
        {
            *this = *this + in;
            return *this;
        }

        ByteInt8& operator-=(const ByteInt8& in) noexcept
        {
            *this = *this - in;
            return *this;
        }

        ByteInt8& operator*=(const ByteInt8& in) noexcept
        {
            *this = *this * in;
            return *this;
        }

        ByteInt8& operator/=(const ByteInt8& in) noexcept
        {
            *this = *this / in;
            return *this;
        }

        bool operator==(const ByteInt8& in) const noexcept
        {
            return Value == in.Value;
        }

        bool operator<(const ByteInt8& in) const noexcept
        {
            return Value < in.Value;
        }

        bool operator>(const ByteInt8& in) const noexcept
        {
            return Value > in.Value;
        }

        bool operator<=(const ByteInt8& in) const noexcept
        {
            return Value <= in.Value;
        }

        bool operator>=(const ByteInt8& in) const noexcept
        {
            return Value >= in.Value;
        }
    };
#pragma pack(pop)

#pragma pack(push, 1)
    /**
    * @author        yc.jeon
    * @brief        uint8_t 자료형을 비트 또는 바이트 단위로 표현하기 위한 구조체
    */
    struct ByteUInt8
    {
        union
        {
            uint8_t Value;
            struct
            {
                uint8_t Bit0 : 1;
                uint8_t Bit1 : 1;
                uint8_t Bit2 : 1;
                uint8_t Bit3 : 1;
                uint8_t Bit4 : 1;
                uint8_t Bit5 : 1;
                uint8_t Bit6 : 1;
                uint8_t Bit7 : 1;
            } BitValue;
        };

        ByteUInt8() noexcept
            : Value(0)
        {
        }

        ByteUInt8(uint8_t byValue) noexcept
            : Value(byValue)
        {
        }

        ByteUInt8 operator-(const ByteUInt8& in) const noexcept
        {
            ByteUInt8 newData(Value - in.Value);
            return newData;
        }

        ByteUInt8 operator+(const ByteUInt8& in) const noexcept
        {
            ByteUInt8 newData(Value + in.Value);
            return newData;
        }

        ByteUInt8 operator*(const ByteUInt8& in) const noexcept
        {
            ByteUInt8 newData(Value * in.Value);
            return newData;
        }

        ByteUInt8 operator/(const ByteUInt8& in) const noexcept
        {
            ByteUInt8 newData(Value / in.Value);
            return newData;
        }

        ByteUInt8& operator=(const ByteUInt8& in) noexcept
        {
            Value = in.Value;
            return *this;
        }

        ByteUInt8& operator=(const uint8_t& in) noexcept
        {
            Value = in;
            return *this;
        }

        ByteUInt8& operator+=(const ByteUInt8& in) noexcept
        {
            *this = *this + in;
            return *this;
        }

        ByteUInt8& operator-=(const ByteUInt8& in) noexcept
        {
            *this = *this - in;
            return *this;
        }

        ByteUInt8& operator*=(const ByteUInt8& in) noexcept
        {
            *this = *this * in;
            return *this;
        }

        ByteUInt8& operator/=(const ByteUInt8& in) noexcept
        {
            *this = *this / in;
            return *this;
        }

        bool operator==(const ByteUInt8& in) const noexcept
        {
            return Value == in.Value;
        }

        bool operator<(const ByteUInt8& in) const noexcept
        {
            return Value < in.Value;
        }

        bool operator>(const ByteUInt8& in) const noexcept
        {
            return Value > in.Value;
        }

        bool operator<=(const ByteUInt8& in) const noexcept
        {
            return Value <= in.Value;
        }

        bool operator>=(const ByteUInt8& in) const noexcept
        {
            return Value >= in.Value;
        }
    };
#pragma pack(pop)

#pragma pack(push, 1)
    /**
    * @author        yc.jeon
    * @brief        int16_t 자료형을 비트 또는 바이트 단위로 표현하기 위한 구조체
    */
    struct ByteInt16
    {
        union
        {
            int16_t Value;                            /**< int16_t 형 데이터 */
            ByteUInt8 ByteValue[sizeof(int16_t)];    /**< 원본 바이트 배열 데이터 */
        };

        ByteInt16() noexcept
            : Value(0)
        {
        }

        ByteInt16(int16_t nValue16) noexcept
            : Value(nValue16)
        {
        }

        ByteInt16 operator-(const ByteInt16& in) const noexcept
        {
            ByteInt16 newData(Value - in.Value);
            return newData;
        }

        ByteInt16 operator+(const ByteInt16& in) const noexcept
        {
            ByteInt16 newData(Value + in.Value);
            return newData;
        }

        ByteInt16 operator*(const ByteInt16& in) const noexcept
        {
            ByteInt16 newData(Value * in.Value);
            return newData;
        }

        ByteInt16 operator/(const ByteInt16& in) const noexcept
        {
            ByteInt16 newData(Value / in.Value);
            return newData;
        }

        ByteInt16& operator=(const ByteInt16& in) noexcept
        {
            Value = in.Value;
            return *this;
        }

        ByteInt16& operator=(const int16_t& in) noexcept
        {
            Value = in;
            return *this;
        }

        ByteInt16& operator+=(const ByteInt16& in) noexcept
        {
            *this = *this + in;
            return *this;
        }

        ByteInt16& operator-=(const ByteInt16& in) noexcept
        {
            *this = *this - in;
            return *this;
        }

        ByteInt16& operator*=(const ByteInt16& in) noexcept
        {
            *this = *this * in;
            return *this;
        }

        ByteInt16& operator/=(const ByteInt16& in) noexcept
        {
            *this = *this / in;
            return *this;
        }

        bool operator==(const ByteInt16& in) const noexcept
        {
            return Value == in.Value;
        }

        bool operator<(const ByteInt16& in) const noexcept
        {
            return Value < in.Value;
        }

        bool operator>(const ByteInt16& in) const noexcept
        {
            return Value > in.Value;
        }

        bool operator<=(const ByteInt16& in) const noexcept
        {
            return Value <= in.Value;
        }

        bool operator>=(const ByteInt16& in) const noexcept
        {
            return Value >= in.Value;
        }
    };
#pragma pack(pop)

#pragma pack(push, 1)
    /**
    * @author        yc.jeon
    * @brief        uint16_t 자료형을 비트 또는 바이트 단위로 표현하기 위한 구조체
    */
    struct ByteUInt16
    {
        union
        {
            uint16_t Value;                                /**< uint16_t 형 데이터 */
            ByteUInt8 ByteValue[sizeof(uint16_t)];        /**< 원본 바이트 배열 데이터 */
        };

        ByteUInt16() noexcept
            : Value(0)
        {
        }

        ByteUInt16(uint16_t unValue16) noexcept
            : Value(unValue16)
        {
        }

        ByteUInt16 operator-(const ByteUInt16& in) const noexcept
        {
            ByteUInt16 newData(Value - in.Value);
            return newData;
        }

        ByteUInt16 operator+(const ByteUInt16& in) const noexcept
        {
            ByteUInt16 newData(Value + in.Value);
            return newData;
        }

        ByteUInt16 operator*(const ByteUInt16& in) const noexcept
        {
            ByteUInt16 newData(Value * in.Value);
            return newData;
        }

        ByteUInt16 operator/(const ByteUInt16& in) const noexcept
        {
            ByteUInt16 newData(Value / in.Value);
            return newData;
        }

        ByteUInt16& operator=(const ByteUInt16& in) noexcept
        {
            Value = in.Value;
            return *this;
        }

        ByteUInt16& operator=(const uint16_t& in) noexcept
        {
            Value = in;
            return *this;
        }

        ByteUInt16& operator+=(const ByteUInt16& in) noexcept
        {
            *this = *this + in;
            return *this;
        }

        ByteUInt16& operator-=(const ByteUInt16& in) noexcept
        {
            *this = *this - in;
            return *this;
        }

        ByteUInt16& operator*=(const ByteUInt16& in) noexcept
        {
            *this = *this * in;
            return *this;
        }

        ByteUInt16& operator/=(const ByteUInt16& in) noexcept
        {
            *this = *this / in;
            return *this;
        }

        bool operator==(const ByteUInt16& in) const noexcept
        {
            return Value == in.Value;
        }

        bool operator<(const ByteUInt16& in) const noexcept
        {
            return Value < in.Value;
        }

        bool operator>(const ByteUInt16& in) const noexcept
        {
            return Value > in.Value;
        }

        bool operator<=(const ByteUInt16& in) const noexcept
        {
            return Value <= in.Value;
        }

        bool operator>=(const ByteUInt16& in) const noexcept
        {
            return Value >= in.Value;
        }
    };
#pragma pack(pop)

#pragma pack(push, 1)
    /**
    * @author        yc.jeon
    * @brief        int32_t 자료형을 비트 또는 바이트 단위로 표현하기 위한 구조체
    */
    struct ByteInt32
    {
        union
        {
            int32_t Value;                            /**< int32_t 형 데이터 */
            int16_t Value16[2];                        /**< int16_t 형 데이터 */
            ByteUInt8 ByteValue[sizeof(int32_t)];    /**< 원본 바이트 배열 데이터 */
        };

        ByteInt32() noexcept
            : Value(0)
        {
        }

        ByteInt32(int32_t nValue32) noexcept
            : Value(nValue32)
        {
        }

        ByteInt32 operator-(const ByteInt32& in) const noexcept
        {
            ByteInt32 newData(Value - in.Value);
            return newData;
        }

        ByteInt32 operator+(const ByteInt32& in) const noexcept
        {
            ByteInt32 newData(Value + in.Value);
            return newData;
        }

        ByteInt32 operator*(const ByteInt32& in) const noexcept
        {
            ByteInt32 newData(Value * in.Value);
            return newData;
        }

        ByteInt32 operator/(const ByteInt32& in) const noexcept
        {
            ByteInt32 newData(Value / in.Value);
            return newData;
        }

        ByteInt32& operator=(const ByteInt32& in) noexcept
        {
            Value = in.Value;
            return *this;
        }

        ByteInt32& operator=(const int32_t& in) noexcept
        {
            Value = in;
            return *this;
        }

        ByteInt32& operator+=(const ByteInt32& in) noexcept
        {
            *this = *this + in;
            return *this;
        }

        ByteInt32& operator-=(const ByteInt32& in) noexcept
        {
            *this = *this - in;
            return *this;
        }

        ByteInt32& operator*=(const ByteInt32& in) noexcept
        {
            *this = *this * in;
            return *this;
        }

        ByteInt32& operator/=(const ByteInt32& in) noexcept
        {
            *this = *this / in;
            return *this;
        }

        bool operator==(const ByteInt32& in) const noexcept
        {
            return Value == in.Value;
        }

        bool operator<(const ByteInt32& in) const noexcept
        {
            return Value < in.Value;
        }

        bool operator>(const ByteInt32& in) const noexcept
        {
            return Value > in.Value;
        }

        bool operator<=(const ByteInt32& in) const noexcept
        {
            return Value <= in.Value;
        }

        bool operator>=(const ByteInt32& in) const noexcept
        {
            return Value >= in.Value;
        }
    };
#pragma pack(pop)

#pragma pack(push, 1)
    /**
    * @author        yc.jeon
    * @brief        uint32_t 자료형을 비트 또는 바이트 단위로 표현하기 위한 구조체
    */
    struct ByteUInt32
    {
        union
        {
            uint32_t Value;                            /**< uint32_t 형 데이터 */
            uint16_t Value16[2];                    /**< uint16_t 형 데이터 */
            ByteUInt8 ByteValue[sizeof(uint32_t)];    /**< 원본 바이트 배열 데이터 */
        };

        ByteUInt32() noexcept
            : Value(0)
        {
        }

        ByteUInt32(uint32_t unValue32) noexcept
            : Value(unValue32)
        {
        }

        ByteUInt32 operator-(const ByteUInt32& in) const noexcept
        {
            ByteUInt32 newData(Value - in.Value);
            return newData;
        }

        ByteUInt32 operator+(const ByteUInt32& in) const noexcept
        {
            ByteUInt32 newData(Value + in.Value);
            return newData;
        }

        ByteUInt32 operator*(const ByteUInt32& in) const noexcept
        {
            ByteUInt32 newData(Value * in.Value);
            return newData;
        }

        ByteUInt32 operator/(const ByteUInt32& in) const noexcept
        {
            ByteUInt32 newData(Value / in.Value);
            return newData;
        }

        ByteUInt32& operator=(const ByteUInt32& in) noexcept
        {
            Value = in.Value;
            return *this;
        }

        ByteUInt32& operator=(const uint32_t& in) noexcept
        {
            Value = in;
            return *this;
        }

        ByteUInt32& operator+=(const ByteUInt32& in) noexcept
        {
            *this = *this + in;
            return *this;
        }

        ByteUInt32& operator-=(const ByteUInt32& in) noexcept
        {
            *this = *this - in;
            return *this;
        }

        ByteUInt32& operator*=(const ByteUInt32& in) noexcept
        {
            *this = *this * in;
            return *this;
        }

        ByteUInt32& operator/=(const ByteUInt32& in) noexcept
        {
            *this = *this / in;
            return *this;
        }

        bool operator==(const ByteUInt32& in) const noexcept
        {
            return Value == in.Value;
        }

        bool operator<(const ByteUInt32& in) const noexcept
        {
            return Value < in.Value;
        }

        bool operator>(const ByteUInt32& in) const noexcept
        {
            return Value > in.Value;
        }

        bool operator<=(const ByteUInt32& in) const noexcept
        {
            return Value <= in.Value;
        }

        bool operator>=(const ByteUInt32& in) const noexcept
        {
            return Value >= in.Value;
        }
    };
#pragma pack(pop)

#pragma pack(push, 1)
    /**
    * @author        yc.jeon
    * @brief        int64_t 자료형을 비트 또는 바이트 단위로 표현하기 위한 구조체
    */
    struct ByteInt64
    {
        union
        {
            int64_t Value;                            /**< int64_t 형 데이터 */
            int32_t Value32[2];                        /**< int32_t 형 데이터 */
            int16_t Value16[4];                        /**< int16_t 형 데이터 */
            ByteUInt8 ByteValue[sizeof(int64_t)];    /**< 원본 바이트 배열 데이터 */
        };

        ByteInt64() noexcept
            : Value(0LL)
        {
        }

        ByteInt64(int64_t nValue64) noexcept
            : Value(nValue64)
        {
        }

        ByteInt64 operator-(const ByteInt64& in) const noexcept
        {
            ByteInt64 newData(Value - in.Value);
            return newData;
        }

        ByteInt64 operator+(const ByteInt64& in) const noexcept
        {
            ByteInt64 newData(Value + in.Value);
            return newData;
        }

        ByteInt64 operator*(const ByteInt64& in) const noexcept
        {
            ByteInt64 newData(Value * in.Value);
            return newData;
        }

        ByteInt64 operator/(const ByteInt64& in) const noexcept
        {
            ByteInt64 newData(Value / in.Value);
            return newData;
        }

        ByteInt64& operator=(const ByteInt64& in) noexcept
        {
            Value = in.Value;
            return *this;
        }

        ByteInt64& operator=(const int64_t& in) noexcept
        {
            Value = in;
            return *this;
        }

        ByteInt64& operator+=(const ByteInt64& in) noexcept
        {
            *this = *this + in;
            return *this;
        }

        ByteInt64& operator-=(const ByteInt64& in) noexcept
        {
            *this = *this - in;
            return *this;
        }

        ByteInt64& operator*=(const ByteInt64& in) noexcept
        {
            *this = *this * in;
            return *this;
        }

        ByteInt64& operator/=(const ByteInt64& in) noexcept
        {
            *this = *this / in;
            return *this;
        }

        bool operator==(const ByteInt64& in) const noexcept
        {
            return Value == in.Value;
        }

        bool operator<(const ByteInt64& in) const noexcept
        {
            return Value < in.Value;
        }

        bool operator>(const ByteInt64& in) const noexcept
        {
            return Value > in.Value;
        }

        bool operator<=(const ByteInt64& in) const noexcept
        {
            return Value <= in.Value;
        }

        bool operator>=(const ByteInt64& in) const noexcept
        {
            return Value >= in.Value;
        }
    };
#pragma pack(pop)

#pragma pack(push, 1)
    /**
    * @author        yc.jeon
    * @brief        uint64_t 자료형을 비트 또는 바이트 단위로 표현하기 위한 구조체
    */
    struct ByteUInt64
    {
        union
        {
            uint64_t Value;                            /**< uint64_t 형 데이터 */
            uint32_t Value32[2];                    /**< uint32_t 형 데이터 */
            uint16_t Value16[4];                    /**< uint16_t 형 데이터 */
            ByteUInt8 ByteValue[sizeof(uint64_t)];    /**< 원본 바이트 배열 데이터 */
        };

        ByteUInt64() noexcept
            : Value(0ULL)
        {
        }

        ByteUInt64(uint64_t unValue64) noexcept
            : Value(unValue64)
        {
        }

        ByteUInt64 operator-(const ByteUInt64& in) const noexcept
        {
            ByteUInt64 newData(Value - in.Value);
            return newData;
        }

        ByteUInt64 operator+(const ByteUInt64& in) const noexcept
        {
            ByteUInt64 newData(Value + in.Value);
            return newData;
        }

        ByteUInt64 operator*(const ByteUInt64& in) const noexcept
        {
            ByteUInt64 newData(Value * in.Value);
            return newData;
        }

        ByteUInt64 operator/(const ByteUInt64& in) const noexcept
        {
            ByteUInt64 newData(Value / in.Value);
            return newData;
        }

        ByteUInt64& operator=(const ByteUInt64& in) noexcept
        {
            Value = in.Value;
            return *this;
        }

        ByteUInt64& operator=(const uint64_t& in) noexcept
        {
            Value = in;
            return *this;
        }

        ByteUInt64& operator+=(const ByteUInt64& in) noexcept
        {
            *this = *this + in;
            return *this;
        }

        ByteUInt64& operator-=(const ByteUInt64& in) noexcept
        {
            *this = *this - in;
            return *this;
        }

        ByteUInt64& operator*=(const ByteUInt64& in) noexcept
        {
            *this = *this * in;
            return *this;
        }

        ByteUInt64& operator/=(const ByteUInt64& in) noexcept
        {
            *this = *this / in;
            return *this;
        }

        bool operator==(const ByteUInt64& in) const noexcept
        {
            return Value == in.Value;
        }

        bool operator<(const ByteUInt64& in) const noexcept
        {
            return Value < in.Value;
        }

        bool operator>(const ByteUInt64& in) const noexcept
        {
            return Value > in.Value;
        }

        bool operator<=(const ByteUInt64& in) const noexcept
        {
            return Value <= in.Value;
        }

        bool operator>=(const ByteUInt64& in) const noexcept
        {
            return Value >= in.Value;
        }
    };
#pragma pack(pop)

#pragma pack(push, 1)
    /**
    * @author        yc.jeon
    * @brief        float 자료형을 비트 또는 바이트 단위로 표현하기 위한 구조체
    */
    struct ByteFloat
    {
        union
        {
            float Value;                        /** float 형 데이터 */
            ByteUInt8 ByteValue[sizeof(float)];    /** 원본 바이트 배열 데이터 */
        };

        ByteFloat() noexcept
            : Value(0.0f)
        {
        }

        ByteFloat(float fValue) noexcept
            : Value(fValue)
        {
        }

        ByteFloat operator-(const ByteFloat& in) const noexcept
        {
            ByteFloat newData(Value - in.Value);
            return newData;
        }

        ByteFloat operator+(const ByteFloat& in) const noexcept
        {
            ByteFloat newData(Value + in.Value);
            return newData;
        }

        ByteFloat operator*(const ByteFloat& in) const noexcept
        {
            ByteFloat newData(Value * in.Value);
            return newData;
        }

        ByteFloat operator/(const ByteFloat& in) const noexcept
        {
            ByteFloat newData(Value / in.Value);
            return newData;
        }

        ByteFloat& operator=(const ByteFloat& in) noexcept
        {
            Value = in.Value;
            return *this;
        }

        ByteFloat& operator=(const float& in) noexcept
        {
            Value = in;
            return *this;
        }

        ByteFloat& operator+=(const ByteFloat& in) noexcept
        {
            *this = *this + in;
            return *this;
        }

        ByteFloat& operator-=(const ByteFloat& in) noexcept
        {
            *this = *this - in;
            return *this;
        }

        ByteFloat& operator*=(const ByteFloat& in) noexcept
        {
            *this = *this * in;
            return *this;
        }

        ByteFloat& operator/=(const ByteFloat& in) noexcept
        {
            *this = *this / in;
            return *this;
        }

        bool operator==(const ByteFloat& in) const noexcept
        {
            return Value == in.Value;
        }

        bool operator<(const ByteFloat& in) const noexcept
        {
            return Value < in.Value;
        }

        bool operator>(const ByteFloat& in) const noexcept
        {
            return Value > in.Value;
        }

        bool operator<=(const ByteFloat& in) const noexcept
        {
            return Value <= in.Value;
        }

        bool operator>=(const ByteFloat& in) const noexcept
        {
            return Value >= in.Value;
        }
    };
#pragma pack(pop)

#pragma pack(push, 1)
    /**
    * @author        yc.jeon
    * @brief        double 자료형을 비트 또는 바이트 단위로 표현하기 위한 구조체
    */
    struct ByteDouble
    {
        union
        {
            double Value;                            /** double 형 데이터 */
            ByteUInt8 ByteValue[sizeof(double)];    /** 원본 바이트 배열 데이터 */
        };

        ByteDouble() noexcept
            : Value(0)
        {
        }

        ByteDouble(double dValue) noexcept
            : Value(dValue)
        {
        }

        ByteDouble operator-(const ByteDouble& in) const noexcept
        {
            ByteDouble newData(Value - in.Value);
            return newData;
        }

        ByteDouble operator+(const ByteDouble& in) const noexcept
        {
            ByteDouble newData(Value + in.Value);
            return newData;
        }

        ByteDouble operator*(const ByteDouble& in) const noexcept
        {
            ByteDouble newData(Value * in.Value);
            return newData;
        }

        ByteDouble operator/(const ByteDouble& in) const noexcept
        {
            ByteDouble newData(Value / in.Value);
            return newData;
        }

        ByteDouble& operator=(const ByteDouble& in) noexcept
        {
            Value = in.Value;
            return *this;
        }

        ByteDouble& operator=(const double& in) noexcept
        {
            Value = in;
            return *this;
        }

        ByteDouble& operator+=(const ByteDouble& in) noexcept
        {
            *this = *this + in;
            return *this;
        }

        ByteDouble& operator-=(const ByteDouble& in) noexcept
        {
            *this = *this - in;
            return *this;
        }

        ByteDouble& operator*=(const ByteDouble& in) noexcept
        {
            *this = *this * in;
            return *this;
        }

        ByteDouble& operator/=(const ByteDouble& in) noexcept
        {
            *this = *this / in;
            return *this;
        }

        bool operator==(const ByteDouble& in) const noexcept
        {
            return Value == in.Value;
        }

        bool operator<(const ByteDouble& in) const noexcept
        {
            return Value < in.Value;
        }

        bool operator>(const ByteDouble& in) const noexcept
        {
            return Value > in.Value;
        }

        bool operator<=(const ByteDouble& in) const noexcept
        {
            return Value <= in.Value;
        }

        bool operator>=(const ByteDouble& in) const noexcept
        {
            return Value >= in.Value;
        }
    };
#pragma pack(pop)

    /**
    * @author        yc.jeon
    * @brief        Byte 관련 기능을 제공하는 유틸 클래스
    * @details
    */
    class CByteUtil
    {
    public:
        /**
        * @brief        BYTE 배열을 string 문자열로 변환하는 함수
        * @param[in]    byString        변환할 BYTE 배열
        * @param[in]    nSize            BYTE 배열의 길이
        * @return        변환된 string 문자열
        */
        //static std::string ToString(uint8_t* byString, const int& nSize)
        //{
        //    if (nSize <= 0)
        //    {
        //        throw std::length_error("string size is less than zero.");
        //    }

        //    char* chBuffer = new char[nSize + 1];
        //    memset(chBuffer, 0, nSize + 1);
        //    memcpy_s(chBuffer, sizeof(char) * nSize, byString, sizeof(uint8_t) * nSize);
        //    std::string strByte(chBuffer);
        //    delete[] chBuffer;
        //    chBuffer = nullptr;
        //    return strByte;
        //}
        /**
        * @brief        BYTE 배열을 wstring 문자열로 변환하는 함수
        * @param[in]    byString        변환할 BYTE 배열
        * @param[in]    nSize            BYTE 배열의 길이
        * @return        변환된 wstring 문자열
        */
        //static std::wstring ToWString(uint8_t* byString, const int& nSize)
        //{
        //    if (nSize <= 0)
        //    {
        //        throw std::length_error("string size is less than zero.");
        //    }

        //    std::string strAscii = ToString(byString, nSize);
        //    int size_needed = MultiByteToWideChar(CP_UTF8, 0, &strAscii[0], (int)strAscii.size(), NULL, 0);
        //    std::wstring wstrTo(size_needed, 0);
        //    MultiByteToWideChar(CP_UTF8, 0, &strAscii[0], (int)strAscii.size(), &wstrTo[0], size_needed);
        //    return wstrTo;
        //}

        /**
        * @brief        데이터의 내용이 비어있는지 확인하는 함수
        * @param[in]    pData        확인할 데이터의 포인터
        * @return        true: 비어있음, false: 비어있지 않음
        */
        template <typename T> static bool IsEmpty(const T* pData)
        {
            if (pData == nullptr)
            {
                return false;
            }

            T zeroData;
            ::memset(&zeroData, 0, sizeof(T));

            return ::memcmp(pData, &zeroData, sizeof(T)) == 0;
        }
    };
} // namespace esk::util_byte