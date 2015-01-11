using System;
using System.Runtime.InteropServices;

namespace DotMetamod.HLSDK
{
	[StructLayout (LayoutKind.Sequential)]
	public struct pmplane_t
	{
		public Vec3	 normal;
		public float dist;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct pmtrace_t
	{
		public int	allsolid;	      // if true, plane is not valid
		public int	startsolid;	      // if true, the initial point was in a solid area
		public int	inopen, inwater;  // End point is in empty space or in water
		public float fraction;		  // time completed, 1.0 = didn't hit anything
		public Vec3	endpos;			  // final position
		public pmplane_t plane;		  // surface normal at impact
		public int	ent;			  // entity at impact
		public Vec3	deltavelocity;    // Change in player's velocity caused by impact.  
		// Only run on server.
		public int	hitgroup;
	}
}

