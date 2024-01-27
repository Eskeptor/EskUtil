/**
* @file			BitUtil.cpp
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		Bit Utility
*/

#include "BitUtil.h"

namespace EskUtil
{
	namespace Bit
	{
		/**
		* @brief		����Ʈ�� Ư�� ��ġ�� ��Ʈ�� 0���� ����� �Լ�
		* @param[in]	byData		���� ����Ʈ
		* @param[in]	nLoc		0���� ���� ��ġ (0~7)
		* @return		�����(���� nLoc�� 7���� ũ�� byData�� ��ȯ)
		*/
		EXTERN EskUtil_API inline BYTE ClearBit(IN const BYTE& byData, IN const int& nLoc)
		{
			if (nLoc > 7)
			{
				return byData;
			}
			BYTE byResult = byData & ~(0x1 << nLoc);
			return byResult;
		}

		/**
		* @brief		����Ʈ�� Ư�� ��ġ�� ��Ʈ�� 1�� ����� �Լ�
		* @param[in]	byData		���� ����Ʈ
		* @param[in]	nLoc		1�� ���� ��ġ (0~7)
		* @return		�����(���� nLoc�� 7���� ũ�� byData�� ��ȯ)
		*/
		EXTERN EskUtil_API inline BYTE SetBit(IN const BYTE& byData, IN const int& nLoc)
		{
			if (nLoc > 7)
			{
				return byData;
			}
			BYTE byResult = byData | (0x1 << nLoc);
			return byResult;
		}

		/**
		* @brief		����Ʈ�� Ư�� ��ġ�� ��Ʈ�� �����ϴ� �Լ�
		* @param[in]	byData		���� ����Ʈ
		* @param[in]	nLoc		���� �� ��ġ (0~7)
		* @return		�����(���� nLoc�� 7���� ũ�� byData�� ��ȯ)
		*/
		EXTERN EskUtil_API inline BYTE InvertBit(IN const BYTE& byData, IN const int& nLoc)
		{
			if (nLoc > 7)
			{
				return byData;
			}
			BYTE byResult = byData ^ (0x1 << nLoc);
			return byResult;
		}

		/**
		* @brief		����Ʈ�� Ư�� ��ġ�� ��Ʈ ��ȯ�ϴ� �Լ�
		* @param[in]	byData		���� ����Ʈ
		* @param[in]	nLoc		��ȯ �� ��ġ (0~7)
		* @return		�����(���� nLoc�� 7���� ũ�� false ��ȯ)
		*/
		EXTERN EskUtil_API inline bool CheckBit(IN const BYTE& byData, IN const int& nLoc)
		{
			return nLoc <= 7 ? byData & (0x1 << nLoc) : false;
		}
	}
}