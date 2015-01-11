using System;
using DotMetamod.HLSDK;

namespace DotMetamod.Metamod
{
	public delegate int GetEntityAPIDelegate(ref DllFunctions pFunctionTable, int interfaceVersion);
	public delegate int GetEntityAPI2Delegate(ref DllFunctions pFunctionTable, ref int interfaceVersion);
	public delegate int GetNewDllFunctionsDelegate(ref NewDllFuncions pFunctionTable, ref int interfaceVersion);
}

