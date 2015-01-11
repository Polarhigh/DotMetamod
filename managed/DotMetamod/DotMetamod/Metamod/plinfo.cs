using System;
using System.Runtime.InteropServices;

// metamod/plinfo.h
namespace DotMetamod.Metamod
{
	public enum PluginLoadTime
	{
		Never = 0,
		Startup,
		Changelevel,
		Anytime,
		Anypause
	}

	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct PluginInfo
	{
		public sbyte* ifvers;
		public sbyte* name;
		public sbyte* version;
		public sbyte* date;
		public sbyte* author;
		public sbyte* url;
		public sbyte* logtag;
		public PluginLoadTime loadable;
		public PluginLoadTime unloadable;
	}
}

