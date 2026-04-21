/**
* @file			String.h
* @author		yc.jeon (Eskeptor)
* @date			2026-04-21
* @version		0.0.3
* @brief		std::string Utility
*/

#pragma once
#include "Common.h"
#include <cstring>

namespace esk::gearforge::util::str
{
    /**
    * @brief        입력된 buffer 배열을 새로 복사하여 반환하는 함수 (new 하기 때문에 delete 필요!!)
    * @param[in]    buffer          복사할 char 배열
    * @return       복사 된 char 배열 (delete 필요)
    */
    inline char* AllocCopyCString(const char* pszBuffer)
    {
        if (pszBuffer == nullptr)
        {
            return nullptr;
        }

        size_t nSize = ::strlen(pszBuffer);
        char* pCopy = new char[nSize + 1];
        ::memcpy_s(pCopy, nSize + 1, pszBuffer, nSize + 1);
        return pCopy;
    }
} // namespace esk::util_str