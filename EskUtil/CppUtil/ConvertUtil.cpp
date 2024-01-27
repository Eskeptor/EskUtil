/**
* @file			ConvertUtil.cpp
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		Convert Utility
*/

#include "ConvertUtil.h"
#include <sstream>

#define MAX(X, Y)		((X) > (Y) ? (X) : (Y))
#define MIN(X, Y)		((X) > (Y) ? (Y) : (X))

namespace EskUtil
{
	namespace Convert
	{
		EXTERN EskUtil_API unsigned int HexToDec(const std::string & strHex)
		{
			unsigned int nDec = std::stoul(strHex, nullptr, 16);
			return nDec;
		}

		EskUtil_API std::string DecToHex(const int& nDec)
		{
			std::stringstream stream;
			stream << std::hex << nDec;
			std::string strHex(stream.str());
			return strHex;
		}

		EXTERN EskUtil_API unsigned int RGBToHex(const int& nR, const int& nG, const int& nB)
		{
			if (nR > 255 ||
				nR < 0 ||
				nG > 255 ||
				nG < 0 ||
				nB > 255 ||
				nB < 0)
			{
				return 0;
			}

			return ((nR & 0xFF) << 16) + ((nG & 0xFF) << 8) + (nB & 0xFF);
		}

		EXTERN EskUtil_API unsigned int RGBAToHex(const int& nR, const int& nG, const int& nB, const int& nA)
		{
			if (nR > 255 ||
				nR < 0 ||
				nG > 255 ||
				nG < 0 ||
				nB > 255 ||
				nB < 0)
			{
				return 0;
			}

			return ((nR & 0xFF) << 24) + ((nG & 0xFF) << 16) + ((nB & 0xFF) << 8) + (nA & 0xFF);
		}
		EXTERN EskUtil_API void HexToRGB(const int& nHex, int* nOutR, int* nOutG, int* nOutB)
		{
			*nOutR = ((nHex >> 16) & 0xFF);
			*nOutG = ((nHex >> 8) & 0xFF);
			*nOutB = (nHex & 0xFF);
		}
		//EXTERN EskUtil_API void HexToRGB(const std::string& strHex, int* nOutR, int* nOutG, int* nOutB)
		//{
		//	int nHex = HexToDec(strHex);
		//	HexToRGB(nHex, nOutR, nOutG, nOutB);
		//}
		EXTERN EskUtil_API void RGBToHSV(const int& nR, const int& nG, const int& nB, int* nOutH, int* nOutS, int* nOutV)
		{
			if (nR > 255 ||
				nR < 0 ||
				nG > 255 ||
				nG < 0 ||
				nB > 255 ||
				nB < 0)
			{
				return;
			}

			double dR = nR / 255.0;
			double dG = nG / 255.0;
			double dB = nB / 255.0;
			double dMax = MAX(MAX(dR, dG), dB);
			double dMin = MIN(MIN(dR, dG), dB);
			double dDiff = dMax - dMin;

			if (dDiff > 0.0)
			{
				if (dMax == dR)
				{
					*nOutH = (int)(60.0 * (fmod(((dG - dB) / dDiff), 6)));
				}
				else if (dMax == dG)
				{
					*nOutH = (int)(60.0 * (((dB - dR) / dDiff) + 2.0));
				}
				else if (dMax == dB)
				{
					*nOutH = (int)(60.0 * (((dR - dG) / dDiff) + 4.0));
				}

				if (dMax > 0.0)
				{
					*nOutS = (int)round((dDiff / dMax * 100.0));
				}
				else
				{
					*nOutS = 0;
				}

				*nOutV = (int)round((dMax * 100.0));
			}
			else
			{
				*nOutH = 0;
				*nOutS = 0;
				*nOutV = (int)round(dMax * 100.0);
			}

			if (*nOutH < 0)
			{
				*nOutH = 360 + *nOutH;
			}
		}
		EXTERN EskUtil_API void HexToHSV(const int& nHex, int* nOutH, int* nOutS, int* nOutV)
		{
			int nR = 0;
			int nG = 0;
			int nB = 0;
			HexToRGB(nHex, &nR, &nG, &nB);
			RGBToHSV(nR, nG, nB, nOutH, nOutS, nOutV);
		}
		EXTERN EskUtil_API void HSVToRGB(const int& nH, const int& nS, const int& nV, int* nOutR, int* nOutG, int* nOutB)
		{
			if (nH > 360 ||
				nH < 0 ||
				nS > 100 ||
				nS < 0 ||
				nV > 100 ||
				nV < 0)
			{
				return;
			}

			double dH = (double)nH;
			double dS = (double)nS / 100.0;
			double dV = (double)nV / 100.0;

			double dHH = dH >= 360.0 ? 0.0 : dH / 60.0;
			long lI = (long)dHH;
			double dFF = dHH - lI;
			double dP = dV * (1.0 - dS);
			double dQ = dV * (1.0 - (dS * dFF));
			double dT = dV * (1.0 - (dS * (1.0 - dFF)));

			switch (lI)
			{
				case 0:
					*nOutR = (int)(dV * 255.0);
					*nOutG = (int)(dT * 255.0);
					*nOutB = (int)(dP * 255.0);
					break;
				case 1:
					*nOutR = (int)(dQ * 255.0);
					*nOutG = (int)(dV * 255.0);
					*nOutB = (int)(dP * 255.0);
					break;
				case 2:
					*nOutR = (int)(dP * 255.0);
					*nOutG = (int)(dV * 255.0);
					*nOutB = (int)(dT * 255.0);
					break;
				case 3:
					*nOutR = (int)(dP * 255.0);
					*nOutG = (int)(dQ * 255.0);
					*nOutB = (int)(dV * 255.0);
					break;
				case 4:
					*nOutR = (int)(dT * 255.0);
					*nOutG = (int)(dP * 255.0);
					*nOutB = (int)(dV * 255.0);
					break;
				case 5:
				default:
					*nOutR = (int)(dV * 255.0);
					*nOutG = (int)(dP * 255.0);
					*nOutB = (int)(dQ * 255.0);
					break;
			}
		}
		EXTERN EskUtil_API unsigned int HSVToHex(const int& nH, const int& nS, const int& nV)
		{
			if (nH > 360 ||
				nH < 0 ||
				nS > 100 ||
				nS < 0 ||
				nV > 100 ||
				nV < 0)
			{
				return 0;
			}

			int nR = 0;
			int nG = 0;
			int nB = 0;
			HSVToRGB(nH, nS, nV, &nR, &nG, &nB);

			return RGBToHex(nR, nG, nB);
		}
	}
}