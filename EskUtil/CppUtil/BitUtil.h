/**
* @file			BitUtil.h
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		Bit Utility
*/

#pragma once
#include "Common.h"

/**
* @namepsace	EskUtil
*/
namespace EskUtil
{
	namespace Bit
	{
		/**
		* @brief		바이트의 특정 위치의 비트를 0으로 만드는 함수
		* @param[in]	byData		원본 바이트
		* @param[in]	nLoc		0으로 만들 위치 (0~7)
		* @return		결과값(만약 nLoc이 7보다 크면 byData를 반환)
		*/
		EXTERN EskUtil_API BYTE ClearBit(IN const BYTE& byData, IN const int& nLoc);
		/**
		* @brief		바이트의 특정 위치의 비트를 1로 만드는 함수
		* @param[in]	byData		원본 바이트
		* @param[in]	nLoc		1로 만들 위치 (0~7)
		* @return		결과값(만약 nLoc이 7보다 크면 byData를 반환)
		*/
		EXTERN EskUtil_API BYTE SetBit(IN const BYTE& byData, IN const int& nLoc);
		/**
		* @brief		바이트의 특정 위치의 비트를 반전하는 함수
		* @param[in]	byData		원본 바이트
		* @param[in]	nLoc		반전 할 위치 (0~7)
		* @return		결과값(만약 nLoc이 7보다 크면 byData를 반환)
		*/
		EXTERN EskUtil_API BYTE InvertBit(IN const BYTE& byData, IN const int& nLoc);
		/**
		* @brief		바이트의 특정 위치의 비트 반환하는 함수
		* @param[in]	byData		원본 바이트
		* @param[in]	nLoc		반환 할 위치 (0~7)
		* @return		결과값(만약 nLoc이 7보다 크면 false 반환)
		*/
		EXTERN EskUtil_API bool CheckBit(IN const BYTE& byData, IN const int& nLoc);
	}
}