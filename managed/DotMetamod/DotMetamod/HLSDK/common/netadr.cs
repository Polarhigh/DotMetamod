using System;
using System.Runtime.InteropServices;

namespace DotMetamod.HLSDK
{
	public enum netadrtype_t
	{
		NA_UNUSED,
		NA_LOOPBACK,
		NA_BROADCAST,
		NA_IP,
		NA_IPX,
		NA_BROADCAST_IPX,
	}

	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct netadr_t
	{
		public netadrtype_t	type;
		public fixed byte		ip[4];
		public fixed byte		ipx[10];
		public ushort			port;
	}
}

