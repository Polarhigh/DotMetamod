using System;
using DotMetamod.HLSDK;

namespace DotMetamod
{
	public unsafe class Entity
	{
		protected edict_t* edict;
		protected IntPtr edictPtr;

		protected Entity(IntPtr ptr)
		{
			edict = (edict_t*)ptr.ToPointer();
			edictPtr = ptr;
	    }

		public delegate void EntvarModDelegate(ref entvars_t v);
		public void Entvars(EntvarModDelegate handler)
		{
			handler(ref edict->v);
		}

		public IntPtr Pointer
		{
			get { return edictPtr; }
		}

		public edict_t* UPointer
		{
			get { return edict; }
		}

		public static Entity GetEntity(IntPtr edictPtr)
		{
			return new Entity(edictPtr);
		}

		public static Entity GetEntity(edict_t* edict)
		{
			return new Entity(new IntPtr((void*)edict));
		}
	}
}

