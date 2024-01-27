/**
* @file			CalcUtil.h
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		Calculate Utility
*/

#pragma once
#include "Common.h"

/**
* @namepsace	EskUtil
*/
namespace EskUtil
{
	namespace Calc
	{
		/**
		* @brief		10으로 나누는 함수
		* @param[in]	nDiv		10으로 나눌 데이터
		* @return		10으로 나눈 결과값
		*/
		EXTERN EskUtil_API int32_t Div10(IN const int32_t& nDiv);
		/**
		* @brief		100으로 나누는 함수
		* @param[in]	nDiv		100으로 나눌 데이터
		* @return		100으로 나눈 결과값
		*/
		EXTERN EskUtil_API int32_t Div100(IN const int32_t& nDiv);
		/**
		* @brief		현재 데이터가 타겟 데이터에 오차범위 내에 들어왔는지 확인하는 함수
		* @param[in]	dData		현재 데이터
		* @param[in]	dTarget		타겟 데이터
		* @param[in]	dOffset		오차 범위
		* @param[in]	bIsPercent	오차값이 퍼센트인지 유무 (기본값: false)
		* @return		true: 들어옴, false: 벗어남
		*/
		EXTERN EskUtil_API bool IsOffsetIn(IN const double& dData, IN const double& dTarget, IN const double& dOffset, IN const bool& bIsPercent = false);
	}
}
