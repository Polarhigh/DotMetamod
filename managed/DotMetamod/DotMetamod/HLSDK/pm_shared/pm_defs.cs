using System;
using System.Runtime.InteropServices;

namespace DotMetamod.HLSDK
{
	// MAX_PHYSINFO_STRING 256 - pm_info.h
	// MAX_PHYSENTS 600

	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct physent_t
	{
		public fixed sbyte	name[32];             // Name of model, or "player" or "world".
		public int			player;
		public Vec3			origin;               // Model's origin in world coordinates.
		public void*		model;		          // only for bsp models
		public void*		studiomodel;          // SOLID_BBOX, but studio clip intersections.
		public Vec3			mins, maxs;	          // only for non-bsp models
		public int			info;		          // For client or server to use to identify (index into edicts or cl_entities)
		public Vec3			angles;               // rotated entities need this info for hull testing to work.

		public int			solid;				  // Triggers and func_door type WATER brushes are SOLID_NOT
		public int			skin;                 // BSP Contents for such things like fun_door water brushes.
		public int			rendermode;			  // So we can ignore glass

		// Complex collision detection.
		public float		frame;
		public int			sequence;
		public fixed byte	controller[4];
		public fixed byte	blending[2];

		public int			movetype;
		public int			takedamage;
		public int			blooddecal;
		public int			team;
		public int			classnumber;

		// For mods
		public int			iuser1;
		public int			iuser2;
		public int			iuser3;
		public int			iuser4;
		public float		fuser1;
		public float		fuser2;
		public float		fuser3;
		public float		fuser4;
		public Vec3			vuser1;
		public Vec3			vuser2;
		public Vec3			vuser3;
		public Vec3			vuser4;
	}

	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct playermove_t
	{
		public int			player_index;  // So we don't try to run the PM_CheckStuck nudging too quickly.
		public int			server;        // For debugging, are we running physics code on server side?

		public int			multiplayer;   // 1 == multiplayer server
		public float		time;          // realtime on host, for reckoning duck timing
		public float		frametime;	   // Duration of this frame

		public Vec3			forward, right, up; // Vectors for angles
		// player state
		public Vec3			origin;        // Movement origin.
		public Vec3			angles;        // Movement view angles.
		public Vec3			oldangles;     // Angles before movement view angles were looked at.
		public Vec3			velocity;      // Current movement direction.
		public Vec3			movedir;       // For waterjumping, a forced forward velocity so we can fly over lip of ledge.
		public Vec3			basevelocity;  // Velocity of the conveyor we are standing, e.g.

		// For ducking/dead
		public Vec3			view_ofs;      // Our eye position.
		public float		flDuckTime;    // Time we started duck
		public int			bInDuck;       // In process of ducking or ducked already?

		// For walking/falling
		public int			flTimeStepSound;  // Next time we can play a step sound
		public int			iStepLeft;

		public float		flFallVelocity;
		public Vec3			punchangle;

		public float		flSwimTime;

		public float		flNextPrimaryAttack;

		public int			effects;		// MUZZLE FLASH, e.g.

		public int			flags;         // FL_ONGROUND, FL_DUCKING, etc.
		public int			usehull;       // 0 = regular player hull, 1 = ducked player hull, 2 = point hull
		public float		gravity;       // Our current gravity and friction.
		public float		friction;
		public int			oldbuttons;    // Buttons last usercmd
		public float		waterjumptime; // Amount of time left in jumping out of water cycle.
		public int			dead;          // Are we a dead player?
		public int			deadflag;
		public int			spectator;     // Should we use spectator physics model?
		public int			movetype;      // Our movement type, NOCLIP, WALK, FLY

		public int			onground;
		public int			waterlevel;
		public int			watertype;
		public int			oldwaterlevel;

		public fixed sbyte	sztexturename[256];
		public sbyte		chtexturetype;

		public float		maxspeed;
		public float		clientmaxspeed; // Player specific maxspeed

		// For mods
		public int			iuser1;
		public int			iuser2;
		public int			iuser3;
		public int			iuser4;
		public float		fuser1;
		public float		fuser2;
		public float		fuser3;
		public float		fuser4;
		public Vec3			vuser1;
		public Vec3			vuser2;
		public Vec3			vuser3;
		public Vec3			vuser4;
		// world state
		// Number of entities to clip against.
		public int			numphysent;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst=600)]
		public physent_t[]	physents;
		// Number of momvement entities (ladders)
		public int			nummoveent;
		// just a list of ladders
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=600)]
		public physent_t[]	moveents;	

		// All things being rendered, for tracing against things you don't actually collide with
		public int			numvisent;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst=600)]
		public physent_t[]	visents;

		// input to run through physics.
		public usercmd_t	cmd;

		// Trace results for objects we collided with.
		public int			numtouch;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst=600)]
		public pmtrace_t[]	touchindex;

		public fixed sbyte	physinfo[256]; // Physics info string

		public movevars_t*	movevars;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public Vec3[]		player_mins;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public Vec3[]		player_maxs;

		// Common functions
		IntPtr PM_Info_ValueForKey;
		IntPtr PM_Particle;
		IntPtr PM_TestPlayerPosition;
		IntPtr Con_NPrintf;
		IntPtr Con_DPrintf;
		IntPtr Con_Printf;
		IntPtr Sys_FloatTime;
		IntPtr PM_StuckTouch;
		IntPtr PM_PointContents;
		IntPtr PM_TruePointContents;
		IntPtr PM_HullPointContents;   
		IntPtr PM_PlayerTrace;
		IntPtr PM_TraceLine;
		IntPtr RandomLong;
		IntPtr RandomFloat;
		IntPtr PM_GetModelType;
		IntPtr PM_GetModelBounds;
		IntPtr PM_HullForBsp;
		IntPtr PM_TraceModel;
		IntPtr COM_FileSize;
		IntPtr COM_LoadFile;
		IntPtr COM_FreeFile;
		IntPtr memfgets;

		// Functions
		// Run functions for this frame?
		int	   runfuncs;      
		IntPtr PM_PlaySound;
		IntPtr PM_TraceTexture;
		IntPtr PM_PlaybackEventFull;
	}
}

