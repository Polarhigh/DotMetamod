using System;
using System.Runtime.InteropServices;

namespace DotMetamod.HLSDK
{
	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct globalvars_t
	{
		public float time;
		public float frametime;
		public float force_retouch;
		public int mapname; // string as int
		public int startspot; // string as int
		public float deathmatch;
		public float coop;
		public float teamplay;
		public float serverflags;
		public float found_secrets;
		public Vec3 v_forward;
		public Vec3 v_up;
		public Vec3 v_right;
		public float trace_allsolid;
		public float trace_startsolid;
		public float trace_fraction;
		public Vec3 trace_endpos;
		public Vec3 trace_plane_normal;
		public float trace_plane_dist;
		public edict_t *trace_ent;
		public float trace_inopen;
		public float trace_inwater;
		public int trace_hitgroup;
		public int trace_flags;
		public int msg_entity;
		public int cdAudioTrack;
		public int maxClients;
		public int maxEntities;
		public sbyte* pStringBase;
		public void* pSaveData;
		public Vec3 vecLandmarkOffset;
	}

	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct entvars_t
	{
		public int		classname;	// string
		public int		globalname;	// string
		
		public Vec3		origin;
		public Vec3		oldorigin;
		public Vec3		velocity;
		public Vec3		basevelocity;
		public Vec3		clbasevelocity;	// Base velocity that was passed in to server physics so
		//  client can predict conveyors correctly.  Server zeroes it, so we need to store here, too.
		public Vec3		movedir;

		public Vec3		angles;		// Model angles
		public Vec3		avelocity;	// angle velocity (degrees per second)
		public Vec3		punchangle;	// auto-decaying view angle adjustment
		public Vec3		v_angle;	// Viewing angle (player only)

		// For parametric entities
		public Vec3		endpos;
		public Vec3		startpos;
		public float	impacttime;
		public float	starttime;

		public int		fixangle; // 0:nothing, 1:force view angles, 2:add avelocity
		public float	idealpitch;
		public float	pitch_speed;
		public float	ideal_yaw;
		public float	yaw_speed;

		public int		modelindex;
		public int		model; //string

		public int		viewmodel; // player's viewmodel
		public int		weaponmodel; // what other players see

		public Vec3		absmin; // BB max translated to world coord
		public Vec3		absmax; // BB max translated to world coord
		public Vec3		mins; // local BB min
		public Vec3		maxs; // local BB max
		public Vec3		size; // maxs - mins

		public float	ltime;
		public float	nextthink;

		public int		movetype;
		public int		solid;

		public int		skin;
		public int		body; // sub-model selection for studiomodels
		public int		effects;

		public float	gravity; // % of "normal" gravity
		public float	friction; // inverse elasticity of MOVETYPE_BOUNCE

		public int		light_level;

		public int		sequence; // animation sequence
		public int		gaitsequence; // movement animation sequence for player (0 for none)
		public float	frame; // % playback position in animation sequences (0..255)
		public float	animtime; // world time when frame was set
		public float	framerate; // animation playback rate (-8x to 8x)
		public fixed byte controller[4]; // bone controller setting (0..255)
		public fixed byte blending[2]; // blending amount between sub-sequences (0..255)

		public float	scale; // sprite rendering scale (0..255)

		public int		rendermode;
		public float	renderamt;
		public Vec3		rendercolor;
		public int		renderfx;

		public float	health;
		public float	frags;
		public int		weapons; // bit mask for available weapons
		public float	takedamage;

		public int		deadflag;


		public Vec3		view_ofs; // eye position

		public int		button;
		public int		impulse;

		public edict_t*	chain; // Entity pointer when linked into a linked list
		public edict_t*	dmg_inflictor;
		public edict_t*	enemy;
		public edict_t*	aiment; // entity pointer when MOVETYPE_FOLLOW
		public edict_t*	owner;
		public edict_t*	groundentity;

		public int		spawnflags;
		public int		flags;

		public int		colormap; // lowbyte topcolor, highbyte bottomcolor
		public int		team;

		public float	max_health;
		public float	teleport_time;
		public float	armortype;
		public float	armorvalue;
		public int		waterlevel;
		public int		watertype;

		public int		target; // string
		public int		targetname; // string
		public int		netname; // string
		public int		message; // string

		public float	dmg_take;
		public float	dmg_save;
		public float	dmg;
		public float	dmgtime;

		public int		noise; // string
		public int		noise1; // string
		public int		noise2; // string
		public int		noise3; // string

		public float	speed;
		public float	air_finished;
		public float	pain_finished;
		public float	radsuit_finished;

		public edict_t*	pContainingEntity;

		public int		playerclass;
		public float	maxspeed;

		public float	fov;
		public int		weaponanim;

		public int		pushmsec;

		public int		bInDuck;
		public int		flTimeStepSound;
		public int		flSwimTime;
		public int		flDuckTime;
		public int		iStepLeft;
		public float	flFallVelocity;

		public int		gamestate;

		public int		oldbuttons;

		public int		groupinfo;

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
		public edict_t*	euser1;
		public edict_t*	euser2;
		public edict_t*	euser3;
		public edict_t*	euser4;
	}
}

