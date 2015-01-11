using System;
using System.Runtime.InteropServices;
using DotMetamod.HLSDK;

namespace DotMetamod.Metamod
{
	public enum MetaRes
	{
		Unset = 0,
		Ignored,		// plugin didn't take any action
		Handled,		// plugin did something, but real function should still be called
		Override,		// call real function, but use my return value
		Supercede,		// skip real function; use my return value
	}

	// Variables provided to plugins.
	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct meta_globals_t
	{
		public MetaRes mres;			// writable; plugin's return flag
		public MetaRes prev_mres;		// readable; return flag of the previous plugin called
		public MetaRes status;			// readable; "highest" return flag so far
		public void* orig_ret;			// readable; return value from "real" function
		public void* override_ret;		// readable; return value from overriding/superceding plugin
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct MetaFunctions
	{
		public GetEntityAPIDelegate			GetEntityAPI;
		public GetEntityAPIDelegate			GetEntityAPI_Post;
		public GetEntityAPI2Delegate		GetEntityAPI2;
		public GetEntityAPI2Delegate		GetEntityAPI2_Post;
		public GetNewDllFunctionsDelegate	GetNewDLLFunctions;
		public GetNewDllFunctionsDelegate	GetNewDLLFunctions_Post;
		public GetEngineFunctionsDelegate 	GetEngineFunctions;
		public GetEngineFunctionsDelegate	GetEngineFunctions_Post;
	}

	// Pair of function tables provided by game DLL.
	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct GamedllFuncs
	{
		public DllFunctions*	dllapi_table;
		public NewDllFuncions*	newapi_table;
	}
}