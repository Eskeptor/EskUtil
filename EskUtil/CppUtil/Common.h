#pragma once

#ifdef ESK_DLL_EXPORTS
	#ifndef EskUtil_API
		#define EskUtil_API __declspec(dllexport)
	#endif
#else
	#ifndef EskUtil_API
		#define EskUtil_API __declspec(dllimport)
	#endif
#endif

#ifdef __cplusplus
	#define EXTERN extern "C"
#else
	#define EXTERN
#endif

#ifndef INT8_MIN
	#include <cstdint>
#endif

#ifndef BYTE
	typedef unsigned char       BYTE;
#endif

#ifndef IN
	#define IN
#endif

#ifndef OUT
	#define OUT
#endif