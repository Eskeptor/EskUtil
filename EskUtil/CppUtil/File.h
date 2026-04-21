/**
 * @file	    File.h
 * @author	    yc.jeon (Eskeptor)
 * @date        2026.04.21
 * @version     0.0.3
 */

#pragma once

#include <string>
#include <vector>
#include <fstream>
#include <filesystem>
#ifdef _WIN32
    #ifndef NOMINMAX
        #define NOMINMAX
    #endif
    #include <Windows.h>
#elif __linux__
    #include <unistd.h>
    #include <limits.h>
    #include <libgen.h>
#endif

namespace esk::gearforge::engine::util::file
{
    /**
     * @brief       파일 목록 타입
     */
    enum class eFileListType
    {
        All = 0,            /* 전체 (파일 + 디렉토리) */
        FileOnly,           /* 파일만 */
        DirectoryOnly,      /* 디렉토리만 */
    };

    /**
     * @brief       현재 실행 중인 실행 파일의 경로를 반환하는 함수
     * @return      현재 실행 중인 실행 파일의 경로
     */
    std::string GetExecutablePath();
    /**
     * @brief       파일(pszFilePath)이 위치한 폴더의 경로를 반환하는 함수
     * @param[in]   pszFilePath: 파일의 전체 경로
     * @return      파일(pszFilePath)이 위치한 폴더의 경로
     */
    std::string GetDirPath(const char* pszFilePath);
    /**
     * @brief       현재 실행 중인 실행 파일이 위치한 폴더의 경로를 반환하는 함수
     * @return      현재 실행 중인 실행 파일이 위치한 폴더의 경로
     */
    std::string GetExecutableDir();
    /**
     * @brief       파일에서 데이터를 읽어와서 문자열로 반환하는 함수 (전체 내용)
     * @param[in]   pszFilePath: 파일의 전체 경로
     * @param[in]   pReadText: 읽어온 파일의 내용
     * @return      true: 성공
     * @return      false: 실패
     */
    bool ReadAllText(const char* pszFilePath, std::string* pReadText);
    /**
     * @brief       파일에 문자열 데이터를 쓰는 함수
     * @param[in]   strFilePath: 파일의 전체 경로
     * @param[in]   strWriteData: 쓸 데이터 내용
     * @param[in]   bIsOverwrite: 파일의 내용을 덮어쓸지 유무
     * @return      true: 성공
     * @return      false: 실패
     */
    bool WriteText(const char* pszFilePath, const char* pszWriteData, bool bIsOverwrite = true);
    /**
     * @brief       파일에 산술형 데이터를 이진 파일로 저장하는 함수
     * @tparam      T: 산술형 데이터
     * @param[in]   strFilePath: 파일의 전체 경로
     * @param[in]   writeData: 쓸 데이터
     * @return      true: 성공
     * @return      false: 실패
     */
    template<typename T>
    bool WriteNumeric(const char* pszFilePath, T writeData)
    {
        static_assert(std::is_arithmetic_v<T>, "int, float, double 등 산술형만 가능");
        if (pszFilePath == nullptr)
        {
            return false;
        }

        std::ofstream outStream(pszFilePath, std::ios::binary);
        if (!outStream.is_open())
        {
            return false;
        }

        outStream.write(reinterpret_cast<const char*>(&writeData), sizeof(T));
        return static_cast<bool>(outStream);
    }

    /**
     * @brief       파일에서 이진화 된 산술형 데이터를 읽어오는 함수
     * @tparam      T: 산술형 데이터
     * @param[in]   pszFilePath: 파일의 전체 경로
     * @param[out]  pReadData: 읽어온 데이터
     * @return      true: 성공
     * @return      false: 실패
     */
    template<typename T>
    bool ReadNumeric(const char* pszFilePath, T* pReadData)
    {
        static_assert(std::is_arithmetic_v<T>, "int, float, double 등 산술형만 가능");
        if (pszFilePath == nullptr)
        {
            return false;
        }

        std::ifstream inStream(pszFilePath, std::ios::binary);
        if (!inStream.is_open())
        {
            return false;
        }

        inStream.read(reinterpret_cast<char*>(pReadData), sizeof(T));
        return static_cast<bool>(inStream);
    }
    /**
     * @brief       파일에 산술형 데이터를 가진 벡터를 이진 파일로 저장하는 함수
     * @tparam      T: 산술형 데이터
     * @param[in]   pszFilePath: 파일의 전체 경로
     * @param[in]   vData: 쓸 데이터 벡터
     * @return      true: 성공
     * @return      false: 실패
     */
    template<typename T>
    bool WriteVector(const char* pszFilePath, const std::vector<T>& vWriteData)
    {
        static_assert(std::is_arithmetic_v<T>, "int, float, double 등 산술형만 가능");
        if (pszFilePath == nullptr)
        {
            return false;
        }

        std::ofstream outStream(pszFilePath, std::ios::binary);
        if (!outStream.is_open())
        {
            return false;
        }

        size_t nSize = vWriteData.size();
        outStream.write(reinterpret_cast<const char*>(&nSize), sizeof(nSize));
        if (nSize > 0)
        {
            outStream.write(reinterpret_cast<const char*>(vWriteData.data()), sizeof(T) * nSize);
        }
        return static_cast<bool>(outStream);
    }
    /**
     * @brief       파일에서 이진화 된 산술형 데이터를 가진 벡터를 읽어오는 함수
     * @tparam      T: 산술형 데이터
     * @param[in]   pszFilePath: 파일의 전체 경로
     * @param[out]  readData: 읽어온 데이터 벡터
     * @return      true: 성공
     * @return      false: 실패
     */
    template<typename T>
    bool ReadVector(const char* pszFilePath, std::vector<T>* pvReadData)
    {
        static_assert(std::is_arithmetic_v<T>, "int, float, double 등 산술형만 가능");
        if (pszFilePath == nullptr)
        {
            return false;
        }

        std::ifstream inStream(pszFilePath, std::ios::binary);
        if (!inStream.is_open())
        {
            return false;
        }

        size_t nSize = 0;
        inStream.read(reinterpret_cast<char*>(&nSize), sizeof(nSize));
        if (!inStream)
        {
            return false;
        }

        pvReadData->resize(nSize);
        if (nSize > 0)
        {
            inStream.read(reinterpret_cast<char*>(pvReadData->data()), sizeof(T) * nSize);
        }
        return static_cast<bool>(inStream);
    }
    /**
     * @brief       폴더 경로를 확인해서 없으면 생성하는 함수
     * @param[in]   pszFolderPath: 생성할 폴더의 경로 (전체 경로)
     * @param[out]  pErr: 예외가 발생했을 때 예외 코드 
     * @return      true: 생성 성공 혹은 폴더 존재
     * @return      false: 생성 실패
     */
    bool CreateFolder(const char* pszFolderPath, std::error_code* pErr);
    /**
     * @brief       파일이 존재하는지 여부를 반환하는 함수
     * @param[in]   pszFilePath: 파일의 경로 (전체 경로)
     * @return      true: 존재
     * @return      false: 미존재
     */
    bool IsExistFile(const char* pszFilePath);
    /**
     * @brief       폴더가 존재하는지 여부를 반환하는 함수
     * @param[in]   pszFolderPath: 폴더의 경로 (전체 경로)
     * @return      true: 존재
     * @return      false: 미존재
     */
    bool IsExistFolder(const char* pszFolderPath);
    /**
     * @brief       특정 경로에 있는 파일/폴더 목록을 반환하는 함수
     * @param[in]   pszDirPath: 탐색할 경로
     * @param[in]   eListType: 반환할 목록 타입
     * @param[out]  pvFileList: 반환할 파일/폴더 목록
     * @return      true: 성공
     * @return      false: 실패
     */
    bool GetFileList(
        const char* pszDirPath,
        eFileListType eListType,
        std::vector<std::string>* pvFileList);
    /**
     * @brief       특정 경로게 있는 파일의 버전 정보를 반환하는 함수 (Windows에서만 지원)
     * @param[in]   pszFilePath: 파일의 경로 (전체 경로)
     * @return      파일의 버전 정보 문자열
     */
    std::string GetVersion(const char* pszFilePath);
}