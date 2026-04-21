/**
 * @file	    Time.h
 * @author	    yc.jeon (Eskeptor)
 * @date        2026.04.21
 * @version     0.0.3
 */

#pragma once

#include <chrono>
#include <ctime>

namespace esk::gearforge::util::time
{
    /**
     * @brief       현재 시간을 밀리초 단위로 반환하는 함수
     * @return
     */
    inline uint64_t GetCurrentTimeMillis()
    {
        using namespace std::chrono;
        milliseconds ms = duration_cast<milliseconds>(system_clock::now().time_since_epoch());
        return static_cast<uint64_t>(ms.count());
    }

    /**
     * @brief       밀리초 단위의 시간을 tm 구조체와 밀리초 단위로 변환하는 함수
     * @param[in]   nMillis: 밀리초 단위 시간
     * @param[out]  pTm: tm 구조체 포인터
     * @param[out]  pMsec: 밀리초 단위 정수 포인터
     * @return      true: 성공
     * @return      false: 실패
     */
    inline bool ConvMillisToTime(uint64_t nMillis, std::tm* pTm, int32_t* pMsec)
    {
        if (pTm == nullptr || pMsec == nullptr)
        {
            return false;
        }
        std::time_t seconds = static_cast<std::time_t>(nMillis / 1000);
        *pMsec = static_cast<int32_t>(nMillis % 1000);
#ifdef _WIN32
        localtime_s(pTm, &seconds);
#else
        localtime_r(&seconds, pTm);
#endif
        return true;
    }
}
