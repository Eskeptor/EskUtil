/**
* @file			PointerUtil.h
* @author		yc.jeon
* @date			2024-01-27
* @version		0.0.1
* @brief		Pointer Utility
*/

#pragma once
#include "Common.h"

/**
* @namepsace	EskUtil
*/
namespace EskUtil
{
	/**
	* @brief		Pointer 관련된 기능을 제공하는 유틸 클래스
	* @details
	* @author		yc.jeon
	*/
	class EskUtil_API CPointerUtil
	{
	public:
		/**
		* @brief		할당된 포인터를 해제하는 함수
		* @param[in]	ptr		할당 해제할 포인터
		*/
		template <typename T> static void DestroyPointer(IN T* ptr)
		{
			if (ptr != nullptr)
			{
				delete ptr;
				ptr = nullptr;
			}
		}

		/**
		* @brief		동적 할당된 일차 배열을 해제하는 함수
		* @param[in]	ptr		할당 해제할 동적 일차 배열
		*/
		template <typename T> static void DestroyArray(IN T* ptr)
		{
			if (ptr != nullptr)
			{
				delete[] ptr;
				ptr = nullptr;
			}
		}

		/**
		* @brief		동적 할당된 이차 배열을 해제하는 함수
		* @param[in]	ptr		할당 해제할 동적 이차 배열
		* @param[in]	nSize	이차 배열의 길이
		*/
		template <typename T> static void DestroyArray(IN T** ptr, IN int nSize)
		{
			if (ptr != nullptr)
			{
				for (int i = 0; i < nSize; ++i)
				{
					delete[] ptr[i];
				}
				delete[] ptr;
				ptr = nullptr;
			}
		}

		/**
		* @brief		포인터를 생성하는 함수 (단일 혹은 1차원 배열 포인터)
		* @param[in]	nLength		1차원 배열의 길이(1인경우 단일 포인터 생성)
		* @return		생성된 포인터 (단일 포인터 또는 1차원 배열)
		*/
		template <typename T> static T* CreatePointer(IN const int& nLength = 1)
		{
			return nLength == 1 ? new T() : new T[nLength];
		}
	};
}