/**
* @file			StringUtil.h
* @author		yc.jeon
* @date			2025-05-20
* @version		0.0.2
* @brief		std::string Utility
*/

#pragma once
#include "Common.h"
#include <cstring>

namespace esk::util_str
{
    /**
    * @brief        입력된 buffer 배열을 새로 복사하여 반환하는 함수 (new 하기 때문에 delete 필요!!)
    * @param[in]    buffer          복사할 char 배열
    * @return       복사 된 char 배열 (delete 필요)
    */
    inline char* CopyNewCharArray(IN const char* buffer) noexcept
    {
        if (buffer == nullptr)
        {
            return nullptr;
        }

        size_t nSize = ::strlen(buffer);
        char* pCopy = new char[nSize + 1];
        ::strcpy_s(pCopy, nSize + 1, buffer);
        return pCopy;
    }
} // namespace esk::util_str