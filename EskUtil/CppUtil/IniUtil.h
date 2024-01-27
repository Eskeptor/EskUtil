/**
* @file			IniUtil.h
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		INI Utility
*/

#pragma once
#include "Common.h"

/**
* @namepsace	EskUtil
*/
namespace EskUtil
{
	namespace INI
	{
		/**
		* @brief		INI 파일에서 데이터를 받아오는 함수
		* @param[in]	szSection		INI 파일에서 읽어올 데이터의 섹션 이름
		* @param[in]	szKey			INI 파일에서 읽어올 데이터 키의 이름
		* @param[in]	szDefault		INI 파일에서 해당 값이 없을 때 반환해줄 기본값
		* @param[out]	szOutBuffer		INI 파일에서 읽어온 데이터
		* @param[in]	nBufferSize		읽어올 데이터 버퍼의 최대 길이
		* @param[in]	szFileName		INI 파일의 경로 (전체 경로)
		* @return		읽어온 데이터의 길이 (읽어오지 못했다면 0)
		*/
		EXTERN EskUtil_API int GetINIString(IN const char* szSection, IN const char* szKey, IN const char* szDefault, OUT char* szOutBuffer, IN const int& nBufferSize, IN const char* szFileName);
	}
}
