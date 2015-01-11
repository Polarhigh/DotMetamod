using System;
using System.Runtime.InteropServices;

namespace DotMetamod.HLSDK
{
	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct cvar_t
	{
		public sbyte*	name;
		public sbyte*	str;
		public int		flags;
		public float	value;
		public cvar_t*	next;
	}
}

