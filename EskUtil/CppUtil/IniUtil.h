/**
* @file			IniUtil.h
* @author		yc.jeon
* @date			2025-05-20
* @version		0.0.2
* @brief		INI Utility
*/

#pragma once
#include "Common.h"

#include <stdio.h>
#include <Windows.h>

namespace esk::util_ini
{
    /**
    * @brief        INI 파일에서 데이터를 받아오는 함수
    * @param[in]    szSection       INI 파일에서 읽어올 데이터의 섹션 이름
    * @param[in]    szKey           INI 파일에서 읽어올 데이터 키의 이름
    * @param[in]    szDefault       INI 파일에서 해당 값이 없을 때 반환해줄 기본값
    * @param[out]   szOutBuffer     INI 파일에서 읽어온 데이터
    * @param[in]    nBufferSize     읽어올 데이터 버퍼의 최대 길이
    * @param[in]    szFileName      INI 파일의 경로 (전체 경로)
    * @return       읽어온 데이터의 길이 (읽어오지 못했다면 0)
    */
    int GetIniString(IN const char* szSection,
        IN const char* szKey,
        IN const char* szDefault,
        OUT char* szOutBuffer,
        IN const int& nBufferSize,
        IN const char* szFileName)
    {
        const int MAX_LENGTH = 512;
        const char CHAR_LF = 0xA;

        char szBuffer[MAX_LENGTH] = { 0, };
        char* pEqual = nullptr;
        char szFormattedSection[MAX_LENGTH] = { 0, };
        int nFormattedSectionLength = 0;
        int nKeyLength = static_cast<int>(::strlen(szKey));
        char* pCh = nullptr;

        FILE* pFile = nullptr;
        errno_t nErrCode = ::fopen_s(&pFile, szFileName, "r");
        if (nErrCode != 0)
        {
            return 0;
        }

        ::sprintf_s(szFormattedSection, MAX_LENGTH, "[%s]", szSection);
        nFormattedSectionLength = static_cast<int>(::strlen(szFormattedSection));

        // Section Check
        do
        {
            if (::fgets(szBuffer, MAX_LENGTH - 1, pFile) == nullptr)
            {
                ::fclose(pFile);
                ::strcpy_s(szOutBuffer, nBufferSize, szDefault);
                return static_cast<int>(::strlen(szOutBuffer));
            }
        } while (::strncmp(szBuffer, szFormattedSection, nFormattedSectionLength));

        // Key Check
        do
        {
            if (::fgets(szBuffer, MAX_LENGTH - 1, pFile) == nullptr)
            {
                ::fclose(pFile);
                ::strcpy_s(szOutBuffer, nBufferSize, szDefault);
                return static_cast<int>(::strlen(szOutBuffer));
            }
        } while (::strncmp(szBuffer, szKey, nKeyLength));

        ::fclose(pFile);

        // Check Equals
        pEqual = ::strchr(szBuffer, '=');
        if (pEqual == nullptr)
        {
            ::strncpy_s(szOutBuffer, nBufferSize, szDefault, nBufferSize);
            return static_cast<int>(::strlen(szOutBuffer));
        }
        ++pEqual;

        // Remove leading spaces
        while (*pEqual &&
            (::isspace(*pEqual) || *pEqual == CHAR_LF))
        {
            ++pEqual;
        }
        if (pEqual == nullptr)
        {
            ::strncpy_s(szOutBuffer, nBufferSize, szDefault, nBufferSize);
            return static_cast<int>(::strlen(szOutBuffer));
        }

        // Remove trailing spaces
        pCh = pEqual;
        while (*pCh)
        {
            ++pCh;
        }

        // Backup and put in nulls if there is a space
        --pCh;
        while (pCh > pEqual)
        {
            if (::isspace(*pCh) ||
                *pEqual == CHAR_LF)
            {
                *pCh = 0;
                --pCh;
            }
            else
            {
                break;
            }
        }

        // Copy up to nBufferSize chars to outBuffer
        ::strncpy_s(szOutBuffer, nBufferSize - 1, pEqual, nBufferSize - 1);

        szOutBuffer[nBufferSize] = '\0';
        return static_cast<int>(::strlen(szOutBuffer));
    }
} // namespace esk::util_ini
