Обзор
==========================
DotMetamod позволяет подключать библиотеки(сборки) написанные на c# к metamod.

Требования
==========================
Для компиляции понадобятся исходные коды проектов:
* mono
* hlsdk
* metamod

Так же должны быть установлены:
* make
* cmake
* glib2
* gcc

Ко всему прочему должен быть установлен сам метамод. Далее предполагается, что он установлен на вашем сервере в cstrike/addons/metamod.

Компиляция
==========================
- Скомпилируйте mono по инструкции http://www.mono-project.com/docs/compiling-mono/linux/<br>
**ВАЖНО:** Вы должны скомпилировать mono для 32 битной платформы, желательно i686. Если Вы используйте x86_64 дистрибутив, то вам нужно воспользоваться кросскомпилятором.
- Откройте native/CMakeLists.txt и отредактируйте переменные HLSDK_DIR, METAMOD_DIR, MONO_DIR
- Выполните следующие команды относительно папки native
```bash
$ mkdir cmake && cd cmake
$ cmake ../
$ make
```
- Если всё успешно, то в native/bin должны появиться библиотеки libassemblyloader.so и libmeta_monoloader.so

Установка менеджера сборок
==========================
- В папке cstrike/addons вашего hlds сервера создайте папку "mono"
- Скопируйте в нее libmeta_monoloader.so
- Создайте файл config.cfg с содержимым (описание ключей конфигурации будет дальше):
```ini
[Runtime]
Version	= v4.0 # версия рантайма
Debug = false # режим отладки

# работает только если включен Debug
[Debugging]
Enabled = false
Address = 127.0.0.1:10000
```
- Создайте ещё одну папку mono в этой папке и скопируйте в неё папки lib и etc из папки куда вы установили mono по инструкции из п.Компиляция(1)<br>
У вас должна получиться такая иерархия папок и файлов:
```
addons
 |
 +-mono
  |
  +-libmeta_monoloader.so
  +-config.cfg
  +-mono
    |
    +-lib
    |
    +-etc
```
- Добавьте строчку в addons/metamod/plugins.ini:
linux addons/mono/libmeta_monoloader.so

Создание сборки
==========================
- В папке cstrike/addons вашего hlds сервера создайте папку, назовём ее "my_assembly"
- Скопируйте в нее библиотеку libassemblyloader.so, которая была получена в результате компиляции и **ОБЯЗАТЕЛЬНО** переименуйте, например в lib_my_assembly_loader.so
- Создайте в этой же папке файл config.cfg с содержимым
```ini
[Entry]
Assembly	= MyAssembly.dll
Namespace	= GldSrc.Metamod
Class		= Core
```
- Создайте в этой же папке папку managed<br>
У вас должна получиться такая иерархия папок и файлов:
```
addons
 |
 +-my_assembly
   |
   +-lib_my_assembly_loader.so
   +-config.cfg
   +-managed
```
- Добавьте строчку в addons/metamod/plugins.ini: (она должна быть после "linux addons/mono/libmeta_monoloader.so")
linux addons/my_assembly/lib_my_assembly_loader.so
- Теперь нужно создать c# библиотеку, вот пример
```c#
using System;
using System.Runtime.InteropServices;
using DotMetamod;
using DotMetamod.Metamod;
using DotMetamod.HLSDK;
using DotMetamod.Wrappers;

namespace GldSrc.Metamod
{
	public static class Core
	{
		static IntPtr PLID;
		static IntPtr PluginInfo;

		static EnginefuncsWp engineFunctions;
		static GlobalVariablesWp globalVariables;

		static MetaGlobalsWp metaGlobals;

		static void GiveFnptrsToDll(IntPtr pengfuncsFromEngine, IntPtr pGlobals)
		{
			engineFunctions = new EnginefuncsWp(pengfuncsFromEngine);
			globalVariables = new GlobalVariablesWp(pGlobals);
		}

		static void Meta_Query(string interfaceVersion, IntPtr plinfo, IntPtr pMetaUtilFuncs)
		{
			PluginInfo = Utils.AllocPluginInfo(
				"5:13",				// ifvers
				"Test plugin",		// name
				"0.1",				// version
				"2015/01/01",		// date
				"Polarhigh",		// author
				"https://github.com/PolarHIGH/DotMetamod",	// url
				"TEST",				// logtag
				PluginLoadTime.Anytime, // load
				PluginLoadTime.Anypause	// unload
			);

			Marshal.WriteIntPtr(plinfo, PluginInfo);
			PLID = PluginInfo;
		}

		static unsafe void Meta_Attach(PluginLoadTime now, IntPtr pFunctionTable, IntPtr pMetaGlobals, IntPtr pGamedllFuncs)
		{
			metaGlobals = new MetaGlobalsWp(pMetaGlobals);

			Marshal.StructureToPtr(new MetaFunctions {
				GetEntityAPI2 = (ref DllFunctions table, ref int interfaceVersion) => {
					table.ClientCommand = (edict_t* pEntity) => {
						if(engineFunctions.Cmd_Argv(0)[0] == 'h')
						{
							Entity ent = Entity.GetEntity(pEntity);
							ent.Entvars((ref entvars_t v)  => v.velocity += new Vec3(0f, 0f, 350f));
						}

						metaGlobals.SetMetaResult(MetaRes.Ignored);
					};

					return 0;
				},
				
				GetEngineFunctions_Post = (ref Enginefuncs table, ref int interfaceVersion) => {
					table.MessageBegin = (int msg_dest, int msg_type, float* pOrigin, edict_t* ed) => {
						Vec3 o = pOrigin != null ? new Vec3(pOrigin) : new Vec3();

						Console.WriteLine("dest: {0} type: {1} org: {2} edict: {3}", msg_dest, msg_type, o, ed != null ? engineFunctions.SzFromIndex(ed->v.classname) : "null");
						
						metaGlobals.SetMetaResult(MetaRes.Ignored);
					};

					return 0;
				}
			}, pFunctionTable, false);
		}

		static void Meta_Detach(PluginLoadTime now, int reason)
		{
			Utils.FreePluginInfo(PluginInfo);
		}

		private static void Main(string[] args) { }
	}
}
```
- Скомпилируйте и положите в "addons/my_assembly/managed", имя библиотеки должно совпадать с тем что указано в config.cfg в ключе Assembly

Отладка в Monodevelop
==========================
