/**
* @file			Pointer.h
* @author		yc.jeon (Eskeptor)
* @date			2026-04-21
* @version		0.0.3
* @brief		Pointer Utility
*/

#pragma once
#include "Common.h"

namespace esk::gearforge::util::ptr
{
    /**
    * @brief        할당된 포인터를 해제하는 함수
    * @param[in]    PTR         할당 해제할 포인터
    */
#define DeletePointer(PTR)  \
do {                        \
    if ((PTR) != nullptr)   \
    {                       \
        delete (PTR);       \
        (PTR) = nullptr;    \
    }                       \
} while (0)
    /**
    * @brief        동적 할당된 일차 배열을 해제하는 함수
    * @param[in]    ARR         할당 해제할 동적 일차 배열
    */
#define DeleteArray(ARR)    \
do {                        \
    if ((ARR) != nullptr)   \
    {                       \
        delete[] (ARR);     \
        (ARR) = nullptr;    \
	}                       \
} while (0)
    /**
    * @brief        동적 할당된 이차 배열을 해제하는 함수
    * @param[in]    ARR         할당 해제할 동적 이차 배열
    * @param[in]    SIZE        이차 배열의 길이
    */
#define DeleteArray2(ARR, SIZE)             \
do {                                        \
    if ((ARR) != nullptr)                   \
    {                                       \
        for (int i = 0; i < (SIZE); ++i)    \
        {                                   \
            if ((ARR)[i] != nullptr)        \
            {                               \
                delete[] (ARR)[i];          \
                (ARR)[i] = nullptr;         \
            }                               \
        }                                   \
        delete[] (ARR);                     \
        (ARR) = nullptr;                    \
    }                                       \
} while (0)
} // namespace esk::util_pointer