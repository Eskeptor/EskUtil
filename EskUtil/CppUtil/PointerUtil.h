/**
* @file			PointerUtil.h
* @author		yc.jeon
* @date			2025-05-20
* @version		0.0.2
* @brief		Pointer Utility
*/

#pragma once
#include "Common.h"

namespace esk::util_pointer
{
/**
* @brief        할당된 포인터를 해제하는 함수
* @param[in]    PTR         할당 해제할 포인터
*/
#define DeletePointer(PTR)          if ((PTR) != nullptr) { delete (PTR); (PTR) = nullptr; }
/**
* @brief        동적 할당된 일차 배열을 해제하는 함수
* @param[in]    ARR         할당 해제할 동적 일차 배열
*/
#define DeleteArray(ARR)            if ((ARR) != nullptr) { delete[] (ARR); (ARR) = nullptr; }
/**
* @brief        동적 할당된 이차 배열을 해제하는 함수
* @param[in]    ARR         할당 해제할 동적 이차 배열
* @param[in]    SIZE        이차 배열의 길이
*/
#define DeleteArray2(ARR, SIZE)     if ((ARR) != nullptr) { for (int i = 0; i < (SIZE); ++i) { delete[] ARR[i]; } delete[] (ARR); (ARR) = nullptr; }

} // namespace esk::util_pointer