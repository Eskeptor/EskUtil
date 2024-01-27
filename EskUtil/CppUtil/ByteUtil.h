/**
* @file			ByteUtil.h
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		Byte Utility
*/

#pragma once
#include "Common.h"
#include <string>

/**
* @namepsace	EskUtil
*/
namespace EskUtil
{
	namespace Byte
	{
#pragma pack(push, 1)
		/**
		* @author		yc.jeon
		* @brief		BYTE 배열을 bit 단위로 쪼개기위해 사용하는 구조체
		*/
		struct EskUtil_API ByteBit
		{
			union
			{
				BYTE Value;
				struct
				{
					BYTE Bit0 : 1;
					BYTE Bit1 : 1;
					BYTE Bit2 : 1;
					BYTE Bit3 : 1;
					BYTE Bit4 : 1;
					BYTE Bit5 : 1;
					BYTE Bit6 : 1;
					BYTE Bit7 : 1;
				} BitValue;
			};

			ByteBit()
				: Value(0)
			{}

			ByteBit(BYTE byValue)
				: Value(byValue)
			{}

			ByteBit operator-(const ByteBit& in) const
			{
				ByteBit newData(Value - in.Value);
				return newData;
			}

			ByteBit operator+(const ByteBit& in) const
			{
				ByteBit newData(Value + in.Value);
				return newData;
			}

			ByteBit operator*(const ByteBit& in) const
			{
				ByteBit newData(Value * in.Value);
				return newData;
			}

			ByteBit operator/(const ByteBit& in) const
			{
				ByteBit newData(Value / in.Value);
				return newData;
			}

			ByteBit& operator=(const ByteBit& in)
			{
				Value = in.Value;
				return *this;
			}

			ByteBit& operator+=(const ByteBit& in)
			{
				*this = *this + in;
				return *this;
			}

			ByteBit& operator-=(const ByteBit& in)
			{
				*this = *this - in;
				return *this;
			}

			ByteBit& operator*=(const ByteBit& in)
			{
				*this = *this * in;
				return *this;
			}

			ByteBit& operator/=(const ByteBit& in)
			{
				*this = *this / in;
				return *this;
			}

			bool operator==(const ByteBit& in) const
			{
				return Value == in.Value;
			}

			bool operator<(const ByteBit& in) const
			{
				return Value < in.Value;
			}

			bool operator>(const ByteBit& in) const
			{
				return Value > in.Value;
			}
		};
#pragma pack(pop)

#pragma pack(push, 1)
		/**
		* @author		yc.jeon
		* @brief		BYTE 배열을 int16_t로 변환하기 위해 사용하는 구조체
		*/
		struct EskUtil_API ByteInt16
		{
			union
			{
				int16_t Value;						/**< int16_t 형 데이터 */
				ByteBit ByteValue[sizeof(int16_t)];	/**< 원본 BYTE 배열 데이터 */
			};

			ByteInt16()
				: Value(0)
			{}

			ByteInt16(int16_t nValue16)
				: Value(nValue16)
			{}

			ByteInt16 operator-(const ByteInt16& in) const
			{
				ByteInt16 newData(Value - in.Value);
				return newData;
			}

			ByteInt16 operator+(const ByteInt16& in) const
			{
				ByteInt16 newData(Value + in.Value);
				return newData;
			}

			ByteInt16 operator*(const ByteInt16& in) const
			{
				ByteInt16 newData(Value * in.Value);
				return newData;
			}

			ByteInt16 operator/(const ByteInt16& in) const
			{
				ByteInt16 newData(Value / in.Value);
				return newData;
			}

			ByteInt16& operator=(const ByteInt16& in)
			{
				Value = in.Value;
				return *this;
			}

			ByteInt16& operator+=(const ByteInt16& in)
			{
				*this = *this + in;
				return *this;
			}

			ByteInt16& operator-=(const ByteInt16& in)
			{
				*this = *this - in;
				return *this;
			}

			ByteInt16& operator*=(const ByteInt16& in)
			{
				*this = *this * in;
				return *this;
			}

			ByteInt16& operator/=(const ByteInt16& in)
			{
				*this = *this / in;
				return *this;
			}

			bool operator==(const ByteInt16& in) const
			{
				return Value == in.Value;
			}

			bool operator<(const ByteInt16& in) const
			{
				return Value < in.Value;
			}

			bool operator>(const ByteInt16& in) const
			{
				return Value > in.Value;
			}
		};
#pragma pack(pop)

#pragma pack(push, 1)
		/**
		* @author		yc.jeon
		* @brief		BYTE 배열을 uint16_t로 변환하기 위해 사용하는 구조체
		*/
		struct EskUtil_API ByteUInt16
		{
			union
			{
				uint16_t Value;								/**< uint16_t 형 데이터 */
				ByteBit ByteValue[sizeof(uint16_t)];		/**< 원본 BYTE 배열 데이터 */
			};

			ByteUInt16()
				: Value(0)
			{}

			ByteUInt16(uint16_t unValue16)
				: Value(unValue16)
			{}

			ByteUInt16 operator-(const ByteUInt16& in) const
			{
				ByteUInt16 newData(Value - in.Value);
				return newData;
			}

			ByteUInt16 operator+(const ByteUInt16& in) const
			{
				ByteUInt16 newData(Value + in.Value);
				return newData;
			}

			ByteUInt16 operator*(const ByteUInt16& in) const
			{
				ByteUInt16 newData(Value * in.Value);
				return newData;
			}

			ByteUInt16 operator/(const ByteUInt16& in) const
			{
				ByteUInt16 newData(Value / in.Value);
				return newData;
			}

			ByteUInt16& operator=(const ByteUInt16& in)
			{
				Value = in.Value;
				return *this;
			}

			ByteUInt16& operator+=(const ByteUInt16& in)
			{
				*this = *this + in;
				return *this;
			}

			ByteUInt16& operator-=(const ByteUInt16& in)
			{
				*this = *this - in;
				return *this;
			}

			ByteUInt16& operator*=(const ByteUInt16& in)
			{
				*this = *this * in;
				return *this;
			}

			ByteUInt16& operator/=(const ByteUInt16& in)
			{
				*this = *this / in;
				return *this;
			}

			bool operator==(const ByteUInt16& in) const
			{
				return Value == in.Value;
			}

			bool operator<(const ByteUInt16& in) const
			{
				return Value < in.Value;
			}

			bool operator>(const ByteUInt16& in) const
			{
				return Value > in.Value;
			}
		};
#pragma pack(pop)

#pragma pack(push, 1)
		/**
		* @author		yc.jeon
		* @brief		BYTE 배열을 int32_t로 변환하기 위해 사용하는 구조체
		*/
		struct EskUtil_API ByteInt32
		{
			union
			{
				int32_t Value;						/**< int32_t 형 데이터 */
				int16_t Value16[2];					/**< int16_t 형 데이터 */
				ByteBit ByteValue[sizeof(int32_t)];	/**< 원본 BYTE 배열 데이터 */
			};

			ByteInt32()
				: Value(0)
			{}

			ByteInt32(int32_t nValue32)
				: Value(nValue32)
			{}

			ByteInt32 operator-(const ByteInt32& in) const
			{
				ByteInt32 newData(Value - in.Value);
				return newData;
			}

			ByteInt32 operator+(const ByteInt32& in) const
			{
				ByteInt32 newData(Value + in.Value);
				return newData;
			}

			ByteInt32 operator*(const ByteInt32& in) const
			{
				ByteInt32 newData(Value * in.Value);
				return newData;
			}

			ByteInt32 operator/(const ByteInt32& in) const
			{
				ByteInt32 newData(Value / in.Value);
				return newData;
			}

			ByteInt32& operator=(const ByteInt32& in)
			{
				Value = in.Value;
				return *this;
			}

			ByteInt32& operator+=(const ByteInt32& in)
			{
				*this = *this + in;
				return *this;
			}

			ByteInt32& operator-=(const ByteInt32& in)
			{
				*this = *this - in;
				return *this;
			}

			ByteInt32& operator*=(const ByteInt32& in)
			{
				*this = *this * in;
				return *this;
			}

			ByteInt32& operator/=(const ByteInt32& in)
			{
				*this = *this / in;
				return *this;
			}

			bool operator==(const ByteInt32& in) const
			{
				return Value == in.Value;
			}

			bool operator<(const ByteInt32& in) const
			{
				return Value < in.Value;
			}

			bool operator>(const ByteInt32& in) const
			{
				return Value > in.Value;
			}
		};
#pragma pack(pop)

#pragma pack(push, 1)
		/**
		* @author		yc.jeon
		* @brief		BYTE 배열을 int32_t/float으로 변환하기 위해 사용하는 구조체
		*/
		struct EskUtil_API ByteIntFloat
		{
			union
			{
				int32_t Value;						/**< int32_t 형 데이터 */
				float ValueF;						/**< float 형 데이터 */
				int16_t Value16[2];					/**< int16_t 형 데이터 */
				ByteBit ByteValue[sizeof(int32_t)];	/**< 원본 BYTE 배열 데이터 */
			};

			ByteIntFloat()
				: Value(0)
			{}

			ByteIntFloat(int32_t nValue32)
				: Value(nValue32)
			{}

			ByteIntFloat(float fValue)
				: ValueF(fValue)
			{}

			ByteIntFloat operator-(const ByteIntFloat& in) const
			{
				ByteIntFloat newData(Value - in.Value);
				return newData;
			}

			ByteIntFloat operator+(const ByteIntFloat& in) const
			{
				ByteIntFloat newData(Value + in.Value);
				return newData;
			}

			ByteIntFloat operator*(const ByteIntFloat& in) const
			{
				ByteIntFloat newData(Value * in.Value);
				return newData;
			}

			ByteIntFloat operator/(const ByteIntFloat& in) const
			{
				ByteIntFloat newData(Value / in.Value);
				return newData;
			}

			ByteIntFloat& operator=(const ByteIntFloat& in)
			{
				Value = in.Value;
				return *this;
			}

			ByteIntFloat& operator+=(const ByteIntFloat& in)
			{
				*this = *this + in;
				return *this;
			}

			ByteIntFloat& operator-=(const ByteIntFloat& in)
			{
				*this = *this - in;
				return *this;
			}

			ByteIntFloat& operator*=(const ByteIntFloat& in)
			{
				*this = *this * in;
				return *this;
			}

			ByteIntFloat& operator/=(const ByteIntFloat& in)
			{
				*this = *this / in;
				return *this;
			}

			bool operator==(const ByteIntFloat& in) const
			{
				return Value == in.Value;
			}

			bool operator<(const ByteIntFloat& in) const
			{
				return Value < in.Value;
			}

			bool operator>(const ByteIntFloat& in) const
			{
				return Value > in.Value;
			}
		};
#pragma pack(pop)

#pragma pack(push, 1)
		/**
		* @author		yc.jeon
		* @brief		BYTE 배열을 uint32_t로 변환하기 위해 사용하는 구조체
		*/
		struct EskUtil_API ByteUInt32
		{
			union
			{
				uint32_t Value;							/**< uint32_t 형 데이터 */
				uint16_t Value16[2];					/**< uint16_t 형 데이터 */
				ByteBit ByteValue[sizeof(uint32_t)];	/**< 원본 BYTE 배열 데이터 */
			};

			ByteUInt32()
				: Value(0)
			{}

			ByteUInt32(uint32_t unValue32)
				: Value(unValue32)
			{}

			ByteUInt32 operator-(const ByteUInt32& in) const
			{
				ByteUInt32 newData(Value - in.Value);
				return newData;
			}

			ByteUInt32 operator+(const ByteUInt32& in) const
			{
				ByteUInt32 newData(Value + in.Value);
				return newData;
			}

			ByteUInt32 operator*(const ByteUInt32& in) const
			{
				ByteUInt32 newData(Value * in.Value);
				return newData;
			}

			ByteUInt32 operator/(const ByteUInt32& in) const
			{
				ByteUInt32 newData(Value / in.Value);
				return newData;
			}

			ByteUInt32& operator=(const ByteUInt32& in)
			{
				Value = in.Value;
				return *this;
			}

			ByteUInt32& operator+=(const ByteUInt32& in)
			{
				*this = *this + in;
				return *this;
			}

			ByteUInt32& operator-=(const ByteUInt32& in)
			{
				*this = *this - in;
				return *this;
			}

			ByteUInt32& operator*=(const ByteUInt32& in)
			{
				*this = *this * in;
				return *this;
			}

			ByteUInt32& operator/=(const ByteUInt32& in)
			{
				*this = *this / in;
				return *this;
			}

			bool operator==(const ByteUInt32& in) const
			{
				return Value == in.Value;
			}

			bool operator<(const ByteUInt32& in) const
			{
				return Value < in.Value;
			}

			bool operator>(const ByteUInt32& in) const
			{
				return Value > in.Value;
			}
		};
#pragma pack(pop)

#pragma pack(push, 1)
		/**
		* @author		yc.jeon
		* @brief		BYTE 배열을 int64_t으로 변환하기 위해 사용하는 구조체
		*/
		struct EskUtil_API ByteInt64
		{
			union
			{
				int64_t Value;							/**< int64_t 형 데이터 */
				int32_t Value32[2];						/**< int32_t 형 데이터 */
				int16_t Value16[4];						/**< int16_t 형 데이터 */
				ByteBit ByteValue[sizeof(int64_t)];		/**< 원본 BYTE 배열 데이터 */
			};

			ByteInt64()
				: Value(0LL)
			{}

			ByteInt64(int64_t nValue64)
				: Value(nValue64)
			{}

			ByteInt64 operator-(const ByteInt64& in) const
			{
				ByteInt64 newData(Value - in.Value);
				return newData;
			}

			ByteInt64 operator+(const ByteInt64& in) const
			{
				ByteInt64 newData(Value + in.Value);
				return newData;
			}

			ByteInt64 operator*(const ByteInt64& in) const
			{
				ByteInt64 newData(Value * in.Value);
				return newData;
			}

			ByteInt64 operator/(const ByteInt64& in) const
			{
				ByteInt64 newData(Value / in.Value);
				return newData;
			}

			ByteInt64& operator=(const ByteInt64& in)
			{
				Value = in.Value;
				return *this;
			}

			ByteInt64& operator+=(const ByteInt64& in)
			{
				*this = *this + in;
				return *this;
			}

			ByteInt64& operator-=(const ByteInt64& in)
			{
				*this = *this - in;
				return *this;
			}

			ByteInt64& operator*=(const ByteInt64& in)
			{
				*this = *this * in;
				return *this;
			}

			ByteInt64& operator/=(const ByteInt64& in)
			{
				*this = *this / in;
				return *this;
			}

			bool operator==(const ByteInt64& in) const
			{
				return Value == in.Value;
			}

			bool operator<(const ByteInt64& in) const
			{
				return Value < in.Value;
			}

			bool operator>(const ByteInt64& in) const
			{
				return Value > in.Value;
			}
		};
#pragma pack(pop)

#pragma pack(push, 1)
		/**
		* @author		yc.jeon
		* @brief		BYTE 배열을 uint64_t으로 변환하기 위해 사용하는 구조체
		*/
		struct EskUtil_API ByteUInt64
		{
			union
			{
				uint64_t Value;							/**< uint64_t 형 데이터 */
				uint32_t Value32[2];					/**< uint32_t 형 데이터 */
				uint16_t Value16[4];					/**< uint16_t 형 데이터 */
				ByteBit ByteValue[sizeof(uint64_t)];	/**< 원본 BYTE 배열 데이터 */
			};

			ByteUInt64()
				: Value(0ULL)
			{}

			ByteUInt64(uint64_t unValue64)
				: Value(unValue64)
			{}

			ByteUInt64 operator-(const ByteUInt64& in) const
			{
				ByteUInt64 newData(Value - in.Value);
				return newData;
			}

			ByteUInt64 operator+(const ByteUInt64& in) const
			{
				ByteUInt64 newData(Value + in.Value);
				return newData;
			}

			ByteUInt64 operator*(const ByteUInt64& in) const
			{
				ByteUInt64 newData(Value * in.Value);
				return newData;
			}

			ByteUInt64 operator/(const ByteUInt64& in) const
			{
				ByteUInt64 newData(Value / in.Value);
				return newData;
			}

			ByteUInt64& operator=(const ByteUInt64& in)
			{
				Value = in.Value;
				return *this;
			}

			ByteUInt64& operator+=(const ByteUInt64& in)
			{
				*this = *this + in;
				return *this;
			}

			ByteUInt64& operator-=(const ByteUInt64& in)
			{
				*this = *this - in;
				return *this;
			}

			ByteUInt64& operator*=(const ByteUInt64& in)
			{
				*this = *this * in;
				return *this;
			}

			ByteUInt64& operator/=(const ByteUInt64& in)
			{
				*this = *this / in;
				return *this;
			}

			bool operator==(const ByteUInt64& in) const
			{
				return Value == in.Value;
			}

			bool operator<(const ByteUInt64& in) const
			{
				return Value < in.Value;
			}

			bool operator>(const ByteUInt64& in) const
			{
				return Value > in.Value;
			}
		};
#pragma pack(pop)

#pragma pack(push, 1)
		/**
		* @author		yc.jeon
		* @brief		BYTE 배열을 float로 변환하기 위해 사용하는 구조체
		*/
		struct EskUtil_API ByteFloat
		{
			union
			{
				float Value;						/** float 형 데이터 */
				ByteBit ByteValue[sizeof(float)];	/** 원본 BYTE 배열 데이터 */
			};

			ByteFloat()
				: Value(0.0f)
			{}

			ByteFloat(float fValue)
				: Value(fValue)
			{}

			ByteFloat operator-(const ByteFloat& in) const
			{
				ByteFloat newData(Value - in.Value);
				return newData;
			}

			ByteFloat operator+(const ByteFloat& in) const
			{
				ByteFloat newData(Value + in.Value);
				return newData;
			}

			ByteFloat operator*(const ByteFloat& in) const
			{
				ByteFloat newData(Value * in.Value);
				return newData;
			}

			ByteFloat operator/(const ByteFloat& in) const
			{
				ByteFloat newData(Value / in.Value);
				return newData;
			}

			ByteFloat& operator=(const ByteFloat& in)
			{
				Value = in.Value;
				return *this;
			}

			ByteFloat& operator+=(const ByteFloat& in)
			{
				*this = *this + in;
				return *this;
			}

			ByteFloat& operator-=(const ByteFloat& in)
			{
				*this = *this - in;
				return *this;
			}

			ByteFloat& operator*=(const ByteFloat& in)
			{
				*this = *this * in;
				return *this;
			}

			ByteFloat& operator/=(const ByteFloat& in)
			{
				*this = *this / in;
				return *this;
			}

			bool operator==(const ByteFloat& in) const
			{
				return Value == in.Value;
			}

			bool operator<(const ByteFloat& in) const
			{
				return Value < in.Value;
			}

			bool operator>(const ByteFloat& in) const
			{
				return Value > in.Value;
			}
		};
#pragma pack(pop)

#pragma pack(push, 1)
		/**
		* @author		yc.jeon
		* @brief		BYTE 배열을 double로 변환하기 위해 사용하는 구조체
		*/
		struct EskUtil_API ByteDouble
		{
			union
			{
				double Value;						/** double 형 데이터 */
				ByteBit ByteValue[sizeof(double)];	/** 원본 BYTE 배열 데이터 */
			};

			ByteDouble()
				: Value(0)
			{}

			ByteDouble(double dValue)
				: Value(dValue)
			{}

			ByteDouble operator-(const ByteDouble& in) const
			{
				ByteDouble newData(Value - in.Value);
				return newData;
			}

			ByteDouble operator+(const ByteDouble& in) const
			{
				ByteDouble newData(Value + in.Value);
				return newData;
			}

			ByteDouble operator*(const ByteDouble& in) const
			{
				ByteDouble newData(Value * in.Value);
				return newData;
			}

			ByteDouble operator/(const ByteDouble& in) const
			{
				ByteDouble newData(Value / in.Value);
				return newData;
			}

			ByteDouble& operator=(const ByteDouble& in)
			{
				Value = in.Value;
				return *this;
			}

			ByteDouble& operator+=(const ByteDouble& in)
			{
				*this = *this + in;
				return *this;
			}

			ByteDouble& operator-=(const ByteDouble& in)
			{
				*this = *this - in;
				return *this;
			}

			ByteDouble& operator*=(const ByteDouble& in)
			{
				*this = *this * in;
				return *this;
			}

			ByteDouble& operator/=(const ByteDouble& in)
			{
				*this = *this / in;
				return *this;
			}

			bool operator==(const ByteDouble& in) const
			{
				return Value == in.Value;
			}

			bool operator<(const ByteDouble& in) const
			{
				return Value < in.Value;
			}

			bool operator>(const ByteDouble& in) const
			{
				return Value > in.Value;
			}
		};
#pragma pack(pop)
	}
}