#include "CMonoManager.h"
#include <string>
#include <CAssembly.h>
#include <mono/metadata/mono-debug.h>
#include <INIReader.h>
#include <ini.h>

using namespace std;

extern string gPluginFolder;

static MonoMethod* SearchMethod(MonoClass* klass, const char* methodname)
{
	MonoMethod *m = NULL;
	gpointer iter = NULL;
	while((m = mono_class_get_methods(klass, &iter)))
		if(strcmp(mono_method_get_name(m), methodname) == 0)
			return m;
	return NULL;
}

CMonoManager::CMonoManager(const char* mono_lib, const char* mono_etc)
{
	INIReader config(gPluginFolder + "/config.ini");
	string runtime_version = config.Get("Runtime", "Version", "v4.0");
	bool debug = config.GetBoolean("Runtime", "Debug", false);

	mono_set_dirs(mono_lib, mono_etc);
	if(debug)
	{
		if(config.GetBoolean("Debugging", "Enabled", false))
		{
			char address[INI_MAX_LINE + 46];
			strcpy(address, "--debugger-agent=transport=dt_socket,address=");
			strcat(address, config.Get("Debugging", "Address", "127.0.0.1:10000").c_str());

			char* options[] = { address };

			mono_jit_parse_options(sizeof(options)/sizeof(char*), (char**)options);
		}

		mono_debug_init(MONO_DEBUG_FORMAT_MONO);

		mDomain = mono_jit_init_version("AppDomain", runtime_version.c_str());
		mono_debug_domain_create(mDomain);
	}
	else
		mDomain = mono_jit_init_version("AppDomain", runtime_version.c_str());
}

CMonoManager::~CMonoManager()
{
	for(auto& kv: mAssemblies)
		delete kv.first;

	mono_jit_cleanup(mDomain);
}

CAssembly* CMonoManager::AddAssembly(const char* assembly_file, const char* handlers_namespace, const char* handlers_class)
{
	MonoAssembly* assembly = mono_domain_assembly_open(mDomain, assembly_file);
	if(!assembly)
		return NULL;
	
	MonoImage* image = mono_assembly_get_image(assembly);
	MonoClass* metamod_class = mono_class_from_name(image, handlers_namespace, handlers_class);

	assembly_methods_s methods;

	if(metamod_class)
	{
		auto empty_arr = (MonoArray*)mono_array_new(mDomain, mono_get_string_class(), 0);
		MonoMethod* method_main = SearchMethod(metamod_class, "Main");
		
		if(method_main)
			mono_runtime_exec_main(method_main, empty_arr, NULL);
		else
			printf("[%s] Warning! Method \"Main\" not found in assembly %s\n\
				You may run into a bug https://bugzilla.novell.com/show_bug.cgi?id=668171#c0\n", Plugin_info.name, assembly_file);

		methods.handlerGiveFnptrsToDll 	= SearchMethod(metamod_class, "GiveFnptrsToDll");
		methods.handlerMeta_Query		= SearchMethod(metamod_class, "Meta_Query");
		methods.handlerMeta_Attach		= SearchMethod(metamod_class, "Meta_Attach");
		methods.handlerMeta_Detach		= SearchMethod(metamod_class, "Meta_Detach");
	}
	else
		memset(&methods, 0, sizeof(assembly_methods_s));

	auto assembly_class = new CAssembly(methods, assembly_file, handlers_namespace, handlers_class);
	mAssemblies[assembly_class] = true;

	return assembly_class;
}

MonoDomain* CMonoManager::GetDomain()
{
	return mDomain;
}