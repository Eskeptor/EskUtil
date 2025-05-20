/**
* @file			CalcUtil.h
* @author		yc.jeon
* @date			2025-05-20
* @version		0.0.2
* @brief		Calculate Utility
*/

#pragma once
#include "Common.h"

#include <cmath>
#include <limits>

namespace esk::util_calc
{
    /**
    * @brief        10으로 나누는 함수
    * @param[in]    nDiv            10으로 나눌 데이터
    * @return       10으로 나눈 결과값 (정수)
    */
    inline int32_t Div10(IN const int32_t& nDiv) noexcept
    {
        int64_t nDivisor = 0x1999999A;    // 10의 역수
        int32_t nResult = static_cast<int32_t>(((nDivisor * nDiv) >> 32));
        int32_t nReq = nDiv - nResult * 10;
        return (nDiv < 0 && nReq != 0) ? nResult + 1 : nResult;
    }
    /**
    * @brief        100으로 나누는 함수
    * @param[in]    nDiv            100으로 나눌 데이터
    * @return       100으로 나눈 결과값 (정수)
    */
    inline int32_t Div100(IN const int32_t& nDiv) noexcept
    {
        int64_t nDivisor = 0x028F5C29;    // 100의 역수
        int32_t nResult = static_cast<int32_t>(((nDivisor * nDiv) >> 32));
        int32_t nReq = nDiv - nResult * 100;
        return (nDiv < 0 && nReq != 0) ? nResult + 1 : nResult;
    }
    /**
    * @brief        1000으로 나누는 함수
    * @param[in]    nDiv            1000으로 나눌 데이터
    * @return       1000으로 나눈 결과값 (정수)
    */
    inline int32_t Div1000(IN const int32_t& nDiv) noexcept
    {
        int64_t nDivisor = 0x00418937;    // 1000의 역수
        int32_t nResult = static_cast<int32_t>(((nDivisor * nDiv) >> 32));
        int32_t nReq = nDiv - nResult * 1000;
        return (nDiv < 0 && nReq != 0) ? nResult + 1 : nResult;
    }
    /**
    * @brief        현재 데이터가 타겟 데이터에 오차범위(값) 내에 들어왔는지 확인하는 함수
    * @param[in]    dData           현재 데이터
    * @param[in]    dTarget         타겟 데이터
    * @param[in]    dOffset         오차 범위 (값, 음수인 경우 양수로 치환함)
    * @return       true: 들어옴, false: 벗어남
    */
    inline bool IsOffsetInValue(IN const double& dData, IN const double& dTarget, IN const double& dOffset) noexcept
    {
        double dDiff = dOffset > 0.0 ? dOffset : -dOffset;

        double dNRange = dTarget - dDiff;
        double dPRange = dTarget + dDiff;

        return dData >= dNRange && dData <= dPRange;
    }
    /**
    * @brief        현재 데이터가 타겟 데이터에 오차범위(퍼센트) 내에 들어왔는지 확인하는 함수
    * @param[in]    dData           현재 데이터
    * @param[in]    dTarget         타겟 데이터
    * @param[in]    dOffset         오차 범위 (퍼센트: 10% -> 10입력, 음수인 경우 양수로 치환함)
    * @return       true: 들어옴, false: 벗어남
    */
    inline bool IsOffsetInPercent(IN const double& dData, IN const double& dTarget, IN const double& dOffsetPer) noexcept
    {
        double dDiff = dTarget * (dOffsetPer >= 0 ? dOffsetPer * 0.01 : dOffsetPer * -0.01);

        double dNRange = dTarget - dDiff;
        double dPRange = dTarget + dDiff;

        return dData >= dNRange && dData <= dPRange;
    }
    /**
    * @brief        실수의 Equal 연산을 수행하는 함수
    * @param[in]    dNum1           실수1
    * @param[in]    dNum2           실수2
    * @return       true: 오차범위 내에서 같음, false: 오차범위 내에서 다름
    */
    inline bool EpsEquals(IN const double& dNum1, IN const double& dNum2)
    {
        return std::abs(dNum1 - dNum2) < std::numeric_limits<double>::epsilon();
    }
    /**
    * @brief        실수의 Equal 연산을 수행하는 함수
    * @param[in]    fNum1           실수1
    * @param[in]    fNum2           실수2
    * @return       true: 오차범위 내에서 같음, false: 오차범위 내에서 다름
    */
    inline bool EpsEquals(IN const float& fNum1, IN const float& fNum2)
    {
        return std::abs(fNum1 - fNum2) < std::numeric_limits<float>::epsilon();
    }
} // namespace esk::util_calc
