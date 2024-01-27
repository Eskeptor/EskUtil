/**
* @file			CalcUtil.cpp
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		Calculate Utility
*/

#include "CalcUtil.h"

namespace EskUtil
{
	namespace Calc
	{
		/**
		* @brief		10으로 나누는 함수
		* @param[in]	nDiv		10으로 나눌 데이터
		* @return		10으로 나눈 결과값
		*/
		EXTERN EskUtil_API inline int32_t Div10(IN const int32_t& nDiv)
		{
			int64_t nDivisor = 0x1999999A;
			return (int32_t)((nDivisor * nDiv) >> 32);
		}

		/**
		* @brief		100으로 나누는 함수
		* @param[in]	nDiv		100으로 나눌 데이터
		* @return		100으로 나눈 결과값
		*/
		EXTERN EskUtil_API inline int32_t Div100(IN const int32_t& nDiv)
		{
			int32_t nDiv10 = Div10(nDiv);
			return Div10(nDiv10);
		}

		/**
		* @brief		현재 데이터가 타겟 데이터에 오차범위 내에 들어왔는지 확인하는 함수
		* @param[in]	dData		현재 데이터
		* @param[in]	dTarget		타겟 데이터
		* @param[in]	dOffset		오차 범위
		* @param[in]	bIsPercent	오차값이 퍼센트인지 유무 (기본값: false)
		* @return		true: 들어옴, false: 벗어남
		*/
		EXTERN EskUtil_API inline bool IsOffsetIn(IN const double& dData, IN const double& dTarget, IN const double& dOffset, IN const bool& bIsPercent)
		{
			double dDiff = bIsPercent ? dTarget * dOffset : dOffset;
			double dNRange = dTarget - dDiff;
			double dPRange = dTarget + dDiff;

			return dData >= dNRange && dData <= dPRange;
		}
	}
}