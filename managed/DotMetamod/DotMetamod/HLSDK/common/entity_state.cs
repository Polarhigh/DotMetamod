using System;
using System.Runtime.InteropServices;

namespace DotMetamod.HLSDK
{
	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct entity_state_t
	{
		// Fields which are filled in by routines outside of delta compression
		public int		entityType;
		// Index into cl_entities array for this entity.
		public int		number;      
		public float	msg_time;

		// Message number last time the player/entity state was updated.
		public int		messagenum;		

		// Fields which can be transitted and reconstructed over the network stream
		public Vec3		origin;
		public Vec3		angles;

		public int		modelindex;
		public int		sequence;
		public float	frame;
		public int		colormap;
		public short	skin;
		public short	solid;
		public int		effects;
		public float	scale;

		public byte		eflags;

		// Render information
		public int		rendermode;
		public int		renderamt;
		public color24	rendercolor;
		public int		renderfx;

		public int		movetype;
		public float	animtime;
		public float	framerate;
		public int		body;
		public fixed byte controller[4];
		public fixed byte blending[4];
		public Vec3		velocity;

		// Send bbox down to client for use during prediction.
		public Vec3		mins;    
		public Vec3		maxs;

		public int		aiment;
		// If owned by a player, the index of that player ( for projectiles ).
		public int		owner; 

		// Friction, for prediction.
		public float	friction;       
		// Gravity multiplier
		public float	gravity;		

		// PLAYER SPECIFIC
		public int		team;
		public int		playerclass;
		public int		health;
		public int		spectator;  
		public int		weaponmodel;
		public int		gaitsequence;
		// If standing on conveyor, e.g.
		public Vec3		basevelocity;   
		// Use the crouched hull, or the regular player hull.
		public int		usehull;		
		// Latched buttons last time state updated.
		public int		oldbuttons;     
		// -1 = in air, else pmove entity number
		public int		onground;		
		public int		iStepLeft;
		// How fast we are falling
		public float	flFallVelocity;  

		public float	fov;
		public int		weaponanim;

		// Parametric movement overrides
		public Vec3		startpos;
		public Vec3		endpos;
		public float	impacttime;
		public float	starttime;

		// For mods
		public int		iuser1;
		public int		iuser2;
		public int		iuser3;
		public int		iuser4;
		public float	fuser1;
		public float	fuser2;
		public float	fuser3;
		public float	fuser4;
		public Vec3		vuser1;
		public Vec3		vuser2;
		public Vec3		vuser3;
		public Vec3		vuser4;
	}

	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct clientdata_t
	{
		public Vec3		origin;
		public Vec3		velocity;

		public int		viewmodel;
		public Vec3		punchangle;
		public int		flags;
		public int		waterlevel;
		public int		watertype;
		public Vec3		view_ofs;
		public float	health;

		public int		bInDuck;

		public int		weapons; // remove?

		public int		flTimeStepSound;
		public int		flDuckTime;
		public int		flSwimTime;
		public int		waterjumptime;

		public float	maxspeed;

		public float	fov;
		public int		weaponanim;

		public int		m_iId;
		public int		ammo_shells;
		public int		ammo_nails;
		public int		ammo_cells;
		public int		ammo_rockets;
		public float	m_flNextAttack;

		public int		tfstate;

		public int		pushmsec;

		public int		deadflag;

		public fixed sbyte physinfo[256]; // MAX_PHYSINFO_STRING 256 - pm_info.h

		// For mods
		public int		iuser1;
		public int		iuser2;
		public int		iuser3;
		public int		iuser4;
		public float	fuser1;
		public float	fuser2;
		public float	fuser3;
		public float	fuser4;
		public Vec3		vuser1;
		public Vec3		vuser2;
		public Vec3		vuser3;
		public Vec3		vuser4;
	}
}

