using System;
using DotMetamod.Metamod;

namespace DotMetamod
{
	public unsafe class MetaGlobalsWp
	{
		protected meta_globals_t* metaGlobals;

		public MetaGlobalsWp(IntPtr metaGlobals)
		{
			this.metaGlobals = (meta_globals_t*)metaGlobals.ToPointer();
		}

		public void SetMetaResult(MetaRes res)
		{
			metaGlobals->mres = res;
		}

		public MetaRes GetResultStatus()
		{
			return metaGlobals->status;
		}

		public MetaRes GetResultPrevious()
		{
			return metaGlobals->prev_mres;
		}

		public IntPtr GetResultOrigRet()
		{
			return new IntPtr(metaGlobals->orig_ret);
		}

		public void ResultOverrireRet(IntPtr val)
		{
			metaGlobals->override_ret = val.ToPointer();
		}
	}
}

