#ifndef _H_CMONOMANAGER_
#define _H_CMONOMANAGER_

#include <string>
#include <map>
#include "CAssembly.h"

// @TODO добавить вывод информации сборках
// 

class CMonoManager
{
	MonoDomain* mDomain;
	std::map<CAssembly*, bool>	mAssemblies;

public:
				CMonoManager(const char* mono_lib, const char* mono_etc);
				~CMonoManager();
	CAssembly*	AddAssembly(const char* assembly_file, const char* handlers_namespace, const char* handlers_class);
	MonoDomain* GetDomain();
};

#endif // _H_CMONOMANAGER_