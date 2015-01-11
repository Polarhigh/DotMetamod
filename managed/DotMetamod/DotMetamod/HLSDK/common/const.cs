using System;
using System.Runtime.InteropServices;

namespace DotMetamod.HLSDK
{
	[StructLayout (LayoutKind.Sequential)]
	public struct color24
	{
		public byte r;
		public byte g;
		public byte b;
	};

	public enum WalkMoveMode
	{
		Normal,
		WorldOnly,
		CheckOnly
	}

	public enum MessageDestination
	{
		Broadcast,		// unreliable to all
		One,			// reliable to one (msg_entity)
		All,			// reliable to all
		Init,			// write to the init string
		PVS,			// Ents in PVS of org
		PAS,			// Ents in PAS of org
		PVS_R,			// Reliable to PVS
		PAS_R,			// Reliable to PAS
		OneUnreliable,	// Send to one client, but don't put in reliable stream, put in unreliable datagram ( could be dropped )
		Spec			// Sends to all spectator proxies
	}

	public enum PointContentsType
	{
		// contents of a spot in the world
		Empty		= -1,
		Solid		= -2,
		Water		= -3,
		Slime		= -4,
		Lava		= -5,
		Sky			= -6,
		/* These additional contents constants are defined in bspfile.h */
		Origin		= -7,		// removed at csg time
		Clip		= -8,		// changed to contents_solid
		Current_0	= -9,
		Current_90	= -10,
		Current_180 = -11,
		Current_270	= -12,
		Current_Up	= -13,
		Current_Down = -14,
		Translucent	= -15,
		/*  */
		Ladder		= -16,

		Flyfield 	= -17,
		GravityFlyfield = -18,
		Fog			= -19
	}

	public enum MoveType
	{
		None = 0,
		Walk = 3,
		Step,
		Fly,
		Toss,
		Push,
		Noclip,
		FlyMissle,
		Bounce,
		BounceMissile,
		Follow,
		Pushstep,
	}

	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct link_t
	{
		public link_t* prev;
		public link_t* next;
	};
}