#include <dlfcn.h>
#include <string>
#include "CMonoManager.h"

using namespace std;

plugin_info_t Plugin_info = {
	META_INTERFACE_VERSION, // ifvers
	"Mono Loader",	// name
	"1.0",	 		// version
	"2015/01/01",	// date
	"PolarHIGH", 	// aka trofian, author
	"https://github.com/PolarHIGH/DotMetamod", // url
	"MONO",			// logtag, all caps please
	PT_ANYTIME,		// (when) loadable
	PT_STARTUP,		// (when) unloadable
};

CMonoManager* gMonoManager = NULL;
string gPluginFolder;

// blank flank
int GetEntityAPI2(DLL_FUNCTIONS*, int*) { return(TRUE); }
C_DLLEXPORT void GiveFnptrsToDll(enginefuncs_t*, globalvars_t*) { }

C_DLLEXPORT int Meta_Query(const char* interfaceVersion, plugin_info_t** plinfo, mutil_funcs_t* pMetaUtilFuncs)
{
	*plinfo = &Plugin_info;
	
	gPluginFolder = pMetaUtilFuncs->pfnGetPluginPath(PLID);
	gPluginFolder.replace(gPluginFolder.find_last_of("/"), gPluginFolder.length(), ""); // 2-nd param is ok

	dlopen((gPluginFolder + "/mono/lib/libMonoPosixHelper.so").c_str(), RTLD_NOW | RTLD_GLOBAL);
	
	gMonoManager = new CMonoManager((gPluginFolder + "/mono/lib").c_str(), (gPluginFolder + "/mono/etc").c_str());

	return(TRUE);
}

C_DLLEXPORT int Meta_Attach(PLUG_LOADTIME now, META_FUNCTIONS* pFunctionTable, 
	meta_globals_t* pMetaGlobals, gamedll_funcs_t* pGamedllFuncs)
{
	memset(pFunctionTable, 0, sizeof(META_FUNCTIONS));
	pFunctionTable->pfnGetEntityAPI2 = GetEntityAPI2;

	return(TRUE);
}

C_DLLEXPORT int Meta_Detach(PLUG_LOADTIME, PL_UNLOAD_REASON)
{
	delete gMonoManager;

	return(TRUE);
}

C_DLLEXPORT CAssembly* Mono_LoadAssembly(const char* assembly, const char* handlers_namespace, const char* handlers_class)
{
	return gMonoManager->AddAssembly(assembly, handlers_namespace, handlers_class);
}

C_DLLEXPORT MonoDomain* Mono_GetDomain()
{
	return gMonoManager->GetDomain();
}