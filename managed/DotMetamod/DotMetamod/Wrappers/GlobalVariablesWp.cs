using System;
using DotMetamod.HLSDK;

namespace DotMetamod.Wrappers
{
	public unsafe class GlobalVariablesWp
	{
		protected globalvars_t* globalVariables;

		public GlobalVariablesWp(IntPtr globalVars)
		{
			globalVariables = (globalvars_t*)globalVars.ToPointer();
		}

		public float time
		{
			get { return globalVariables->time; }
			set { globalVariables->time = value; }
		}

		public float frametime
		{
			get { return globalVariables->frametime; }
			set { globalVariables->frametime = value; }
		}

		public float force_retouch
		{
			get { return globalVariables->force_retouch; }
			set { globalVariables->force_retouch = value; }
		}

		// string as int
		public int mapname
		{
			get { return globalVariables->mapname; }
			set { globalVariables->mapname = value; }
		}

		// string as int
		public int startspot
		{
			get { return globalVariables->startspot; }
			set { globalVariables->startspot = value; }
		}

		public float deathmatch
		{
			get { return globalVariables->deathmatch; }
			set { globalVariables->deathmatch = value; }
		}

		public float coop
		{
			get { return globalVariables->coop; }
			set { globalVariables->coop = value; }
		}

		public float teamplay
		{
			get { return globalVariables->teamplay; }
			set { globalVariables->teamplay = value; }
		}

		public float serverflags
		{
			get { return globalVariables->serverflags; }
			set { globalVariables->serverflags = value; }
		}

		public float found_secrets
		{
			get { return globalVariables->found_secrets; }
			set { globalVariables->found_secrets = value; }
		}

		public Vec3 v_forward
		{
			get { return globalVariables->v_forward; }
			set { globalVariables->v_forward = value; }
		}

		public Vec3 v_up
		{
			get { return globalVariables->v_up; }
			set { globalVariables->v_up = value; }
		}

		public Vec3 v_right
		{
			get { return globalVariables->v_right; }
			set { globalVariables->v_right = value; }
		}

		public float trace_allsolid
		{
			get { return globalVariables->trace_allsolid; }
			set { globalVariables->trace_allsolid = value; }
		}

		public float trace_startsolid
		{
			get { return globalVariables->trace_startsolid; }
			set { globalVariables->trace_startsolid = value; }
		}

		public float trace_fraction
		{
			get { return globalVariables->trace_fraction; }
			set { globalVariables->trace_startsolid = value; }
		}

		public Vec3 trace_endpos
		{
			get { return globalVariables->trace_endpos; }
			set { globalVariables->trace_endpos = value; }
		}

		public Vec3 trace_plane_normal
		{
			get { return globalVariables->trace_plane_normal; }
			set { globalVariables->trace_plane_normal = value; }
		}

		public float trace_plane_dist
		{
			get { return globalVariables->trace_plane_dist; }
			set { globalVariables->trace_plane_dist = value; }
		}

		public Entity trace_ent
		{
			get { return Entity.GetEntity(globalVariables->trace_ent); }
			set { globalVariables->trace_ent = (edict_t*)value.Pointer.ToPointer(); }
		}

		public float trace_inopen
		{
			get { return globalVariables->trace_inopen; }
			set { globalVariables->trace_inopen = value; }
		}

		public float trace_inwater
		{
			get { return globalVariables->trace_inwater; }
			set { globalVariables->trace_inwater = value; }
		}

		public int trace_hitgroup
		{
			get { return globalVariables->trace_hitgroup; }
			set { globalVariables->trace_hitgroup = value; }
		}

		public int trace_flags
		{
			get { return globalVariables->trace_flags; }
			set { globalVariables->trace_flags = value; }
		}

		public int msg_entity
		{
			get { return globalVariables->msg_entity; }
			set { globalVariables->msg_entity = value; }
		}

		public int cdAudioTrack
		{
			get { return globalVariables->cdAudioTrack; }
			set { globalVariables->cdAudioTrack = value; }
		}

		public int maxClients
		{
			get { return globalVariables->maxClients; }
			set { globalVariables->maxClients = value; }
		}

		public int maxEntities
		{
			get { return globalVariables->maxEntities; }
			set { globalVariables->maxEntities = value; }
		}

		public IntPtr pStringBase
		{
			get { return new IntPtr((void*)globalVariables->pStringBase); }
		}

		public IntPtr pSaveData
		{
			get { return new IntPtr(globalVariables->pSaveData); }
			set { globalVariables->pSaveData = value.ToPointer(); }
		}

		public Vec3 vecLandmarkOffset
		{
			get { return globalVariables->vecLandmarkOffset; }
			set { globalVariables->vecLandmarkOffset = value; }
		}
	}
}

