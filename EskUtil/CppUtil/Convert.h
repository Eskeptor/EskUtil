/**
* @file			Convert.h
* @author		yc.jeon (Eskeptor)
* @date			2026-04-21
* @version		0.0.3
* @brief		Convert Utility
*/

#pragma once
#include "Common.h"
#include <string>

namespace esk::gearforge::util::conv
{
	EXTERN EskUtil_API unsigned int HexToDec(const std::string& strHex);
	EXTERN EskUtil_API std::string DecToHex(int nDec);
	EXTERN EskUtil_API unsigned int RGBToHex(int nR, int nG, int nB);
	EXTERN EskUtil_API unsigned int RGBAToHex(int nR, int nG, int nB, int nA);
	EXTERN EskUtil_API void HexToRGB(int nHex, int* nOutR, int* nOutG, int* nOutB);
	//EXTERN EskUtil_API void HexToRGB(const std::string& strHex, int* nOutR, int* nOutG, int* nOutB);
	EXTERN EskUtil_API void RGBToHSV(int nR, int nG, int nB, int* nOutH, int* nOutS, int* nOutV);
	EXTERN EskUtil_API void HexToHSV(int nHex, int* nOutH, int* nOutS, int* nOutV);
	EXTERN EskUtil_API void HSVToRGB(int nH, int nS, int nV, int* nOutR, int* nOutG, int* nOutB);
	EXTERN EskUtil_API unsigned int HSVToHex(int nH, int nS, int nV);
} // namespace esk::util_conv