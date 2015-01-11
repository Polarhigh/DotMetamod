#ifndef _H_MONOEXPORT_
#define _H_MONOEXPORT_

#include <glib.h>
#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>

#include <extdll.h>

#include <CAssembly.h>

C_DLLEXPORT CAssembly* Mono_LoadAssembly(const char* assembly, const char* handlers_namespace, const char* handlers_class);
C_DLLEXPORT MonoDomain* Mono_GetDomain();

#endif // _H_MONOEXPORT_