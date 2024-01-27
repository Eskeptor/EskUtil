/**
* @file			StringUtil.h
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		std::string Utility
*/

#pragma once
#include "Common.h"
#include <string>
#include <memory>
#include <stdexcept>

/**
* @namepsace	EskUtil
*/
namespace EskUtil
{
	/**
	* @author		yc.jeon
	* @brief		std::string 관련된 기능을 제공하는 유틸 클래스
	* @details
	*/
	class EskUtil_API CStringUtil
	{
	public:

		/**
		* @brief		std::string의 Format 기능을 첨가한 함수
		* @param[in]	strFormat		format 문자
		* @param[in]	args			format에 들어갈 데이터
		* @return		format이 반영되어 생성된 문자열
		* @see			https://stackoverflow.com/questions/2342162/stdstring-formatting-like-sprintf
		*/
		template <typename ... Args> static std::string StringFormat(IN const std::string& strFormat, IN Args ... args)
		{
			int nSize = std::snprintf(nullptr, 0, strFormat.c_str(), args ...) + 1;
			if (nSize <= 0)
			{
				throw std::runtime_error("Error during formatting.");
			}

			size_t nSizet = static_cast<size_t>(nSize);
			std::unique_ptr<char[]> pBuff(new char[nSizet]);
			std::snprintf(pBuff.get(), nSizet, strFormat.c_str(), args ...);
			return std::string(pBuff.get(), pBuff.get() + nSizet - 1);
		}
	};
}