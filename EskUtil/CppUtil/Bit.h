/**
* @file			Bit.h
* @author		yc.jeon (Eskeptor)
* @date			2026-04-21
* @version		0.0.3
* @brief		Bit Utility
*/

#pragma once
#include "Common.h"

namespace esk::gearforge::util::bit
{
    /**
    * @brief        바이트의 특정 위치의 비트를 0으로 만드는 함수
    * @param[in]    byData      원본 바이트
    * @param[in]    nLoc        0으로 만들 위치 (0~7)
    * @return       결과값(만약 nLoc이 7보다 크면 byData를 반환)
    */
    inline uint8_t ClearBit(uint8_t byData, int nLoc) noexcept
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
    inline uint8_t SetBit(uint8_t byData, int nLoc) noexcept
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
    inline uint8_t InvertBit(uint8_t byData, int nLoc) noexcept
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
    inline bool CheckBit(uint8_t byData, int nLoc) noexcept
    {
        return (nLoc <= 7 && nLoc >= 0) ? byData & (0x1 << nLoc) : false;
    }
} // namespace esk::util_bit