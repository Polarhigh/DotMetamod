#include <string>
#include <mono_export.h>
#include <CAssembly.h>
#include <INIReader.h>

using namespace std;

plugin_info_t Plugin_info = {META_INTERFACE_VERSION, "(Unknown assembly)", "", "", "", "", "", PT_ANYTIME, PT_ANYPAUSE};


CAssembly*		gpAssebmly;

enginefuncs_t*	gpEngfuncs;
globalvars_t*	gpGlobals;


C_DLLEXPORT void GiveFnptrsToDll(enginefuncs_t* pengfuncsFromEngine, globalvars_t* pGlobals)
{
	gpEngfuncs = pengfuncsFromEngine;
	gpGlobals = pGlobals;
}

C_DLLEXPORT int Meta_Query(const char* interfaceVersion, plugin_info_t** plinfo, mutil_funcs_t* pMetaUtilFuncs)
{
	// hack, in managed dll will be overwritten
	*plinfo = &Plugin_info;

	// get plugin folder
	string plugin_folder = pMetaUtilFuncs->pfnGetPluginPath(PLID);
	plugin_folder.replace(plugin_folder.find_last_of("/"), plugin_folder.length(), ""); // 2-nd param is ok

	// read config
	INIReader config(plugin_folder + "/config.ini");
	string assembly		= config.Get("Entry", "Assembly", "Assembly.dll");
	string anamespace	= config.Get("Entry", "Namespace", "");
	string klass		= config.Get("Entry", "Class", "Metamod");

	// load assembly
	string assembly_path = plugin_folder + "/managed/" + assembly;
	gpAssebmly = Mono_LoadAssembly(assembly_path.c_str(), anamespace.c_str(), klass.c_str());
	
	if(!gpAssebmly)	
	{
		printf("[Assembly Loader] Assembly %s not found\n", assembly_path.c_str());
		return(FALSE);
	}
	
	gpAssebmly->GiveFnptrsToDll(gpEngfuncs, gpGlobals);
	gpAssebmly->Meta_Query(interfaceVersion, plinfo, pMetaUtilFuncs);
	
	return(TRUE);
}

C_DLLEXPORT int Meta_Attach(PLUG_LOADTIME now, META_FUNCTIONS* pFunctionTable, 
	meta_globals_t* pMetaGlobals, gamedll_funcs_t* pGamedllFuncs)
{
	gpAssebmly->Meta_Attach(now, pFunctionTable, pMetaGlobals, pGamedllFuncs);

	return(TRUE);
}

C_DLLEXPORT int Meta_Detach(PLUG_LOADTIME now, PL_UNLOAD_REASON reason)
{
	gpAssebmly->Meta_Detach(now, reason);

	return(TRUE);
}