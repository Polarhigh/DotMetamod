CMAKE_MINIMUM_REQUIRED(VERSION 2.8.4)

################################################
SET(HLSDK_DIR		/home/trofian/Sources/halflife)
SET(METAMOD_DIR		/home/trofian/Sources/metamod-hl1/metamod)
SET(MONO_DIR		/home/trofian/Sources/mono)
################################################

INCLUDE(FindPkgConfig)
pkg_check_modules(GLIB2 glib-2.0)

IF(NOT GLIB2_FOUND)
	MESSAGE(FATAL_ERROR "glib-2.0 not found")
ENDIF()

ADD_DEFINITIONS(-Dlinux -D__linux__ -DLINUX)

ADD_SUBDIRECTORY(src/meta_mono)
ADD_SUBDIRECTORY(src/meta_assembly)
