CMAKE_MINIMUM_REQUIRED(VERSION 2.8.4)

SET(PROJECT_NAME meta_monoloader)

SET(LIBRARY_OUTPUT_PATH ${CMAKE_CURRENT_SOURCE_DIR}/../../bin)

PROJECT(${PROJECT_NAME})

INCLUDE_DIRECTORIES(
	../shared
	../inih
	../inih/cpp
	${HLSDK_DIR}/dlls
	${HLSDK_DIR}/engine
	${HLSDK_DIR}/common
	${HLSDK_DIR}/pm_shared
	${HLSDK_DIR}/public
	${METAMOD_DIR}
	${MONO_DIR}/include/mono-2.0
	${GLIB2_INCLUDE_DIRS}
	)

SET(SOURCE_FILES
	main.cpp
	CMonoManager.cpp
	../shared/CAssembly.cpp
	../inih/ini.c
	../inih/cpp/INIReader.cpp
	)

SET_SOURCE_FILES_PROPERTIES(../inih/ini.c PROPERTIES LANGUAGE CXX)
ADD_LIBRARY(${PROJECT_NAME} SHARED ${SOURCE_FILES})

SET(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wall -std=c++11 -fpermissive -m32 -march=i686 -O2")
SET(MONO_LIB -L${MONO_DIR}/lib -Wl,--whole-archive libmono-2.0.a -Wl,--no-whole-archive -lm -lrt -ldl -lpthread)
TARGET_LINK_LIBRARIES(${PROJECT_NAME} ${GLIB2_LIBRARIES} ${MONO_LIB})