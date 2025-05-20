/**
* @file			SwapUtil.h
* @author		yc.jeon
* @date			2025-05-20
* @version		0.0.2
* @brief		Swap Utility
*/

#pragma once
#include "Common.h"

namespace esk::util_swap
{
    /**
    * @brief        두 데이터를 서로 바꾸는 함수
    * @param[in]    pA        바꿀 데이터1
    * @param[in]    pB        바꿀 데이터2
    */
    template <typename T>
    inline static void Swap(IN T* pA, IN T* pB) noexcept
    {
        T temp = (*pA);
        (*pA) = (*pB);
        (*pB) = temp;
    }
} // namespace esk::util_swap