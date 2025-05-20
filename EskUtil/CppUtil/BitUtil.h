/**
* @file			BitUtil.h
* @author		yc.jeon
* @date			2025-05-20
* @version		0.0.2
* @brief		Bit Utility
*/

#pragma once
#include "Common.h"

namespace esk::util_bit
{
    /**
    * @brief        바이트의 특정 위치의 비트를 0으로 만드는 함수
    * @param[in]    byData      원본 바이트
    * @param[in]    nLoc        0으로 만들 위치 (0~7)
    * @return       결과값(만약 nLoc이 7보다 크면 byData를 반환)
    */
    inline uint8_t ClearBit(IN const uint8_t& byData, IN const int& nLoc) noexcept
    {
        if (nLoc > 7 ||
            nLoc < 0)
        {
            return byData;
        }
        uint8_t byResult = byData & ~(0x1 << nLoc);
        return byResult;
    }
    /**
    * @brief        바이트의 특정 위치의 비트를 1로 만드는 함수
    * @param[in]    byData      원본 바이트
    * @param[in]    nLoc        1로 만들 위치 (0~7)
    * @return       결과값(만약 nLoc이 7보다 크면 byData를 반환)
    */
    inline uint8_t SetBit(IN const uint8_t& byData, IN const int& nLoc) noexcept
    {
        if (nLoc > 7 ||
            nLoc < 0)
        {
            return byData;
        }
        uint8_t byResult = byData | (0x1 << nLoc);
        return byResult;
    }
    /**
    * @brief        바이트의 특정 위치의 비트를 반전하는 함수
    * @param[in]    byData      원본 바이트
    * @param[in]    nLoc        반전 할 위치 (0~7)
    * @return       결과값(만약 nLoc이 7보다 크면 byData를 반환)
    */
    inline uint8_t InvertBit(IN const uint8_t& byData, IN const int& nLoc) noexcept
    {
        if (nLoc > 7 ||
            nLoc < 0)
        {
            return byData;
        }
        uint8_t byResult = byData ^ (0x1 << nLoc);
        return byResult;
    }
    /**
    * @brief        바이트의 특정 위치의 비트 반환하는 함수
    * @param[in]    byData      원본 바이트
    * @param[in]    nLoc        반환 할 위치 (0~7)
    * @return       결과값(만약 nLoc이 7보다 크면 false 반환)
    */
    inline bool CheckBit(IN const uint8_t& byData, IN const int& nLoc) noexcept
    {
        return (nLoc <= 7 && nLoc >= 0) ? byData & (0x1 << nLoc) : false;
    }
} // namespace esk::util_bit