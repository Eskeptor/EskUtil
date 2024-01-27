/**
* @file			SwapUtil.h
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		Swap Utility
*/

#pragma once
#include "Common.h"

/**
* @namepsace	EskUtil
*/
namespace EskUtil
{
	/**
	* @author		yc.jeon
	* @brief		Swap 관련된 기능을 제공하는 유틸 클래스
	* @details
	*/
	class EskUtil_API CSwapUtil
	{
	public:
		/**
		* @brief		두 데이터를 서로 바꾸는 함수
		* @param[in]	pA		바꿀 데이터1
		* @param[in]	pB		바꿀 데이터2
		*/
		template <typename T> inline static void Swap(IN T* pA, IN T* pB)
		{
			T temp = (*pA);
			(*pA) = (*pB);
			(*pB) = temp;
		}
	};
}