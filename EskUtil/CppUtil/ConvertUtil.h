/**
* @file			ConvertUtil.h
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		Convert Utility
*/

#pragma once
#include "Common.h"
#include <string>

/**
* @namepsace	EskUtil
*/
namespace EskUtil
{
	namespace Convert
	{
		EXTERN EskUtil_API unsigned int HexToDec(const std::string& strHex);
		EskUtil_API std::string DecToHex(const int& nDec);
		EXTERN EskUtil_API unsigned int RGBToHex(const int& nR, const int& nG, const int& nB);
		EXTERN EskUtil_API unsigned int RGBAToHex(const int& nR, const int& nG, const int& nB, const int& nA);
		EXTERN EskUtil_API void HexToRGB(const int& nHex, int* nOutR, int* nOutG, int* nOutB);
		//EXTERN EskUtil_API void HexToRGB(const std::string& strHex, int* nOutR, int* nOutG, int* nOutB);
		EXTERN EskUtil_API void RGBToHSV(const int& nR, const int& nG, const int& nB, int* nOutH, int* nOutS, int* nOutV);
		EXTERN EskUtil_API void HexToHSV(const int& nHex, int* nOutH, int* nOutS, int* nOutV);
		EXTERN EskUtil_API void HSVToRGB(const int& nH, const int& nS, const int& nV, int* nOutR, int* nOutG, int* nOutB);
		EXTERN EskUtil_API unsigned int HSVToHex(const int& nH, const int& nS, const int& nV);
	}
}