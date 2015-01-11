#ifndef _H_CASSEMBLY_
#define _H_CASSEMBLY_

#include <string>

#include <extdll.h>
#include <meta_api.h>

#include <glib.h>
#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>

struct assembly_methods_s
{
	MonoMethod* handlerGiveFnptrsToDll;
	MonoMethod* handlerMeta_Query;
	MonoMethod* handlerMeta_Attach;
	MonoMethod* handlerMeta_Detach;
};

class CAssembly
{
	assembly_methods_s	mMethods;
	std::string			mAssemblyFile;
	std::string			mNamespace;
	std::string			mClass;

public:
	CAssembly(assembly_methods_s methods, std::string assembly_file, std::string handlers_namespace, std::string handlers_class);

	void GiveFnptrsToDll(enginefuncs_t* pengfuncsFromEngine, globalvars_t* pGlobals);
	void Meta_Query(const char* interfaceVersion, plugin_info_t** plinfo, mutil_funcs_t* pMetaUtilFuncs);
	void Meta_Attach(PLUG_LOADTIME now, META_FUNCTIONS* pFunctionTable, meta_globals_t* pMetaGlobals, gamedll_funcs_t* pGamedllFuncs);
	void Meta_Detach(PLUG_LOADTIME now, PL_UNLOAD_REASON reason);

private:
	bool TestHandler(MonoMethod* method, const char* name);
};

#endif // _H_CASSEMBLY_