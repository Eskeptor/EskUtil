/**
 * @file	    File.cpp
 * @author	    yc.jeon (Eskeptor)
 * @date        2026.04.21
 * @version     0.0.3
 */

#include <sstream>
#include <Windows.h>
#pragma comment(lib, "version.lib")

#include "File.h"

namespace esk::gearforge::engine::util::file
{
    std::string GetExecutablePath()
    {
        std::string strPath = "";

#ifdef _WIN32
        char szBuffer[MAX_PATH]{ 0, };
        DWORD dwLen = ::GetModuleFileNameA(NULL, szBuffer, MAX_PATH);
        if (dwLen == 0 ||
            dwLen == MAX_PATH)
        {
            return "";
        }
        strPath = std::string(szBuffer);
#elif __linux__
        char szBuffer[PATH_MAX]{ 0, };
        ssize_t nLen = readlink("/proc/self/exe", szBuffer, sizeof(szBuffer) - 1);
        if (nLen == -1)
        {
            return "";
        }
        szBuffer[nLen] = '\0';
        strPath = std::string(szBuffer);
#endif
        return strPath;
    }

    std::string GetDirPath(const char* pszFilePath)
    {
        if (pszFilePath == nullptr)
        {
            return "";
        }

        std::string strFilePath(pszFilePath);
#ifdef _WIN32
        size_t nPos = strFilePath.find_last_of("\\/");
        if (nPos != std::string::npos)
        {
            return strFilePath.substr(0, nPos);
        }
#elif __linux__
        size_t nPos = strFilePath.find_last_of("\\/");
        if (nPos != std::string::npos)
        {
            return strFilePath.substr(0, nPos);
        }
#endif
        return "";
    }

    std::string GetExecutableDir()
    {
        std::string strFullPath = GetExecutablePath();
        std::string strDirPath = GetDirPath(strFullPath.c_str());
        return strDirPath;
    }

    bool ReadAllText(const char* pszFilePath, std::string* pReadText)
    {
        if (pszFilePath == nullptr ||
            pReadText == nullptr)
        {
            return false;
        }

        std::ifstream inStream(pszFilePath);
        if (!inStream.is_open())
        {
            return false;
        }

        std::ostringstream ss;
        ss << inStream.rdbuf();
        *pReadText = ss.str();
        return true;
    }

    bool WriteText(const char* pszFilePath, const char* pszWriteData, bool bIsOverwrite/* = true*/)
    {
        if (pszFilePath == nullptr ||
            pszWriteData == nullptr)
        {
            return false;
        }

        int nFlag = bIsOverwrite ? std::ios::trunc : std::ios::app;
        std::ofstream outStream(pszFilePath, nFlag);
        if (!outStream.is_open())
        {
            return false;
        }

        outStream << pszWriteData;
        return true;
    }

    bool CreateFolder(const char* pszFolderPath, std::error_code* pErr)
    {
        if (pszFolderPath == nullptr)
        {
            return false;
        }

        try
        {
            if (std::filesystem::exists(pszFolderPath))
            {
                return true;
            }

            if (std::filesystem::create_directories(pszFolderPath))
            {
                return true;
            }
        }
        catch (const std::filesystem::filesystem_error& e)
        {
            if (pErr != nullptr)
            {
                *pErr = e.code();
            }
        }

        return false;
    }

    bool IsExistFile(const char* pszFilePath)
    {
        bool bIsExist = std::filesystem::exists(pszFilePath);
        return bIsExist;
    }

    bool IsExistFolder(const char* pszFolderPath)
    {
        bool bIsExist = std::filesystem::exists(pszFolderPath) &&
            std::filesystem::is_directory(pszFolderPath);
        return bIsExist;
    }

    bool GetFileList(
        const char* pszDirPath,
        eFileListType eListType,
        std::vector<std::string>* pvFileList)
    {
        if (pvFileList == nullptr)
        {
            return false;
        }
        if (!std::filesystem::exists(pszDirPath) ||
            !std::filesystem::is_directory(pszDirPath))
        {
            return false;
        }
        pvFileList->reserve(16);

        std::error_code err;
        for (const std::filesystem::directory_entry& entry : std::filesystem::directory_iterator(pszDirPath, err))
        {
            if (err)
            {
                continue;
            }

            bool bIsDir = entry.is_directory();
            if ((eListType == eFileListType::All || eListType == eFileListType::FileOnly) &&
                entry.is_regular_file())
            {
                pvFileList->push_back(entry.path().filename().string());
            }
            else if ((eListType == eFileListType::All || eListType == eFileListType::DirectoryOnly) &&
                entry.is_directory())
            {
                pvFileList->push_back(entry.path().filename().string());
            }
        }
        return !pvFileList->empty();
    }

    std::string GetVersion(const char* pszFilePath)
    {
        if (pszFilePath == nullptr ||
            pszFilePath[0] == '\0')
        {
            return "";
        }

        std::string strResult = "";
        DWORD dwVersionHandle = 0;
        DWORD dwVerSize = ::GetFileVersionInfoSizeA(pszFilePath, &dwVersionHandle);
        if (dwVerSize != 0)
        {
            LPBYTE lpBuffer = nullptr;
            UINT nSize = 0;
            LPSTR pVerData = new char[dwVerSize];
            if (::GetFileVersionInfoA(pszFilePath, NULL, dwVerSize, pVerData) &&
                ::VerQueryValueA(pVerData, "\\", (VOID FAR * FAR*) & lpBuffer, &nSize) &&
                nSize)
            {
                VS_FIXEDFILEINFO* pVerInfo = reinterpret_cast<VS_FIXEDFILEINFO*>(lpBuffer);
                if (pVerInfo->dwSignature == 0xFEEF04BD)
                {
                    // 파일 버전
                    strResult = std::format("{}.{}.{}.{}",
                        (pVerInfo->dwFileVersionMS >> 16) & 0xFFFF,
                        (pVerInfo->dwFileVersionMS) & 0xFFFF,
                        (pVerInfo->dwFileVersionLS >> 16) & 0xFFFF,
                        (pVerInfo->dwFileVersionLS) & 0xFFFF);
                }
            }
            delete[] pVerData;
            pVerData = nullptr;
        }
        else
        {
            strResult = "1.0.0.0";
        }

        // 파일 수정 시간 추가 (선택 사항)
        WIN32_FILE_ATTRIBUTE_DATA fileInfo = { 0, };
        if (::GetFileAttributesExA(pszFilePath, GetFileExInfoStandard, &fileInfo))
        {
            FILETIME fileTime = fileInfo.ftLastWriteTime;
            SYSTEMTIME stUTC;
            SYSTEMTIME stLocal;
            ::FileTimeToSystemTime(&fileTime, &stUTC);
            ::SystemTimeToTzSpecificLocalTime(NULL, &stUTC, &stLocal);

            strResult = std::format("{}({}.{:02}.{:02} {:02}:{:02}:{:02})",
                strResult, stLocal.wYear, stLocal.wMonth, stLocal.wDay, stLocal.wHour, stLocal.wMinute, stLocal.wSecond);
        }

        return strResult;
    }
}