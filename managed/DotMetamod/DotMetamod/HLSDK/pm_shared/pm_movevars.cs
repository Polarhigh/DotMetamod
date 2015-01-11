using System;
using System.Runtime.InteropServices;

namespace DotMetamod.HLSDK
{
	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct movevars_t
	{
		public float gravity;           // Gravity for map
		public float stopspeed;         // Deceleration when not moving
		public float maxspeed;          // Max allowed speed
		public float spectatormaxspeed;
		public float accelerate;        // Acceleration factor
		public float airaccelerate;     // Same for when in open air
		public float wateraccelerate;   // Same for when in water
		public float friction;          
		public float edgefriction;	    // Extra friction near dropofs 
		public float waterfriction;     // Less in water
		public float entgravity;        // 1.0
		public float bounce;            // Wall bounce value. 1.0
		public float stepsize;          // sv_stepsize;
		public float maxvelocity;       // maximum server velocity.
		public float zmax;			    // Max z-buffer range (for GL)
		public float waveHeight;	    // Water wave height (for GL)
		int	footsteps;       		    // Play footstep sounds
		public fixed sbyte skyName[32]; // Name of the sky map
		public float rollangle;
		public float rollspeed;
		public float skycolor_r;		// Sky color
		public float skycolor_g;		// 
		public float skycolor_b;		//
		public float skyvec_x;			// Sky vector
		public float skyvec_y;			// 
		public float skyvec_z;			// 
	};
}

