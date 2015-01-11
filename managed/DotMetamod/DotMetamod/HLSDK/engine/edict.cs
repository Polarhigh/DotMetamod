using System;
using System.Runtime.InteropServices;

namespace DotMetamod.HLSDK
{
	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct edict_t
	{
		public bool		free;
		public int		serialnumber;
		public link_t	area;
		public int		headnode;
		public int		num_leafs;
		public fixed short leafnums[48];
		public float	freetime;
		public void*	pvPrivateData;
		public entvars_t v;
	};
}