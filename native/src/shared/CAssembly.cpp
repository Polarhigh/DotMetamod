#include <CAssembly.h>
#include <mono_export.h>

CAssembly::CAssembly(assembly_methods_s methods, std::string assembly_file, std::string handlers_namespace, std::string handlers_class)
{
	mMethods = methods;
	mAssemblyFile = assembly_file;
	mNamespace = handlers_namespace;
	mClass = handlers_class;
}

void CAssembly::GiveFnptrsToDll(enginefuncs_t* pengfuncsFromEngine, globalvars_t* pGlobals)
{
	if(!TestHandler(mMethods.handlerGiveFnptrsToDll, "GiveFnptrsToDll"))
		return;

	void* args[2];
	args[0] = &pengfuncsFromEngine;
	args[1] = &pGlobals;
	mono_runtime_invoke(mMethods.handlerGiveFnptrsToDll, NULL, args, NULL);
}

void CAssembly::Meta_Query(const char* interfaceVersion, plugin_info_t** plinfo, mutil_funcs_t* pMetaUtilFuncs)
{
	if(!TestHandler(mMethods.handlerMeta_Query, "Meta_Query"))
		return;

	void* args[3];
	args[0] = mono_string_new(Mono_GetDomain(), interfaceVersion);
	args[1] = &plinfo;
	args[2] = &pMetaUtilFuncs;
	mono_runtime_invoke(mMethods.handlerMeta_Query, NULL, args, NULL);
}

void CAssembly::Meta_Attach(PLUG_LOADTIME now, META_FUNCTIONS* pFunctionTable, meta_globals_t* pMetaGlobals, gamedll_funcs_t* pGamedllFuncs)
{
	if(!TestHandler(mMethods.handlerMeta_Attach, "Meta_Attach"))
		return;

	void* args[4];
	args[0] = &now;
	args[1] = &pFunctionTable;
	args[2] = &pMetaGlobals;
	args[3] = &(pGamedllFuncs->dllapi_table); //@TODO добавить NEW_DLL_FUNCTIONS *newapi_table;
	mono_runtime_invoke(mMethods.handlerMeta_Attach, NULL, args, NULL);
}

void CAssembly::Meta_Detach(PLUG_LOADTIME now, PL_UNLOAD_REASON reason)
{
	if(!TestHandler(mMethods.handlerMeta_Detach, "Meta_Detach"))
		return;

	void* args[2];
	args[0] = &now;
	args[1] = &reason;
	mono_runtime_invoke(mMethods.handlerMeta_Detach, NULL, args, NULL);
}

bool CAssembly::TestHandler(MonoMethod* method, const char* name)
{
	if(!method)
	{
		printf("[Assembly Loader] Method \"%s\" not found in assembly %s\n", (mNamespace + "." + mClass + "." + name).c_str(), mAssemblyFile.c_str());
		return false;
	}

	return true;
}