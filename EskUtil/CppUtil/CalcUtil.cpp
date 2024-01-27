/**
* @file			CalcUtil.cpp
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		Calculate Utility
*/

#include "CalcUtil.h"

namespace EskUtil
{
	namespace Calc
	{
		/**
		* @brief		10���� ������ �Լ�
		* @param[in]	nDiv		10���� ���� ������
		* @return		10���� ���� �����
		*/
		EXTERN EskUtil_API inline int32_t Div10(IN const int32_t& nDiv)
		{
			int64_t nDivisor = 0x1999999A;
			return (int32_t)((nDivisor * nDiv) >> 32);
		}

		/**
		* @brief		100���� ������ �Լ�
		* @param[in]	nDiv		100���� ���� ������
		* @return		100���� ���� �����
		*/
		EXTERN EskUtil_API inline int32_t Div100(IN const int32_t& nDiv)
		{
			int32_t nDiv10 = Div10(nDiv);
			return Div10(nDiv10);
		}

		/**
		* @brief		���� �����Ͱ� Ÿ�� �����Ϳ� �������� ���� ���Դ��� Ȯ���ϴ� �Լ�
		* @param[in]	dData		���� ������
		* @param[in]	dTarget		Ÿ�� ������
		* @param[in]	dOffset		���� ����
		* @param[in]	bIsPercent	�������� �ۼ�Ʈ���� ���� (�⺻��: false)
		* @return		true: ����, false: ���
		*/
		EXTERN EskUtil_API inline bool IsOffsetIn(IN const double& dData, IN const double& dTarget, IN const double& dOffset, IN const bool& bIsPercent)
		{
			double dDiff = bIsPercent ? dTarget * dOffset : dOffset;
			double dNRange = dTarget - dDiff;
			double dPRange = dTarget + dDiff;

			return dData >= dNRange && dData <= dPRange;
		}
	}
}