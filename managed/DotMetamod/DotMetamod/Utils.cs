using System;
using System.Runtime.InteropServices;
using Mono.Unix;
using DotMetamod.Metamod;

namespace DotMetamod
{
	public unsafe static class Utils
	{
		public static IntPtr AllocPluginInfo(string ifvers, string name, string version, string date, string author, string url, string logtag, PluginLoadTime load, PluginLoadTime unload)
		{
			PluginInfo* pl = (PluginInfo*)UnixMarshal.AllocHeap(sizeof(PluginInfo)).ToPointer();
			pl->ifvers		= (sbyte*)UnixMarshal.StringToHeap(ifvers).ToPointer();
			pl->name		= (sbyte*)UnixMarshal.StringToHeap(name).ToPointer();
			pl->version		= (sbyte*)UnixMarshal.StringToHeap(version).ToPointer();
			pl->date		= (sbyte*)UnixMarshal.StringToHeap(date).ToPointer();
			pl->author		= (sbyte*)UnixMarshal.StringToHeap(author).ToPointer();
			pl->url			= (sbyte*)UnixMarshal.StringToHeap(url).ToPointer();
			pl->logtag		= (sbyte*)UnixMarshal.StringToHeap(logtag).ToPointer();
			pl->loadable	= load;
			pl->unloadable	= unload;

			return new IntPtr((void*)pl);
		}

		public static void FreePluginInfo(IntPtr plInfo)
		{
			PluginInfo* pl = (PluginInfo*)plInfo.ToPointer();
			UnixMarshal.FreeHeap(new IntPtr((void*)pl->ifvers));
			UnixMarshal.FreeHeap(new IntPtr((void*)pl->name));
			UnixMarshal.FreeHeap(new IntPtr((void*)pl->version));
			UnixMarshal.FreeHeap(new IntPtr((void*)pl->date));
			UnixMarshal.FreeHeap(new IntPtr((void*)pl->author));
			UnixMarshal.FreeHeap(new IntPtr((void*)pl->url));
			UnixMarshal.FreeHeap(new IntPtr((void*)pl->logtag));
			UnixMarshal.FreeHeap(plInfo);
		}
	}
}
