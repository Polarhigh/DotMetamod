using System;
using DotMetamod.HLSDK;
using System.Runtime.InteropServices;
using Mono.Unix;
using System.Text;

namespace DotMetamod
{
	// TODO
	// 1) ref Vec3 ?
	// 2) добавить в Utils метод для удаления квара из кучи
	// 3) long заменить на int

	public unsafe class EnginefuncsWp
	{
		Enginefuncs ef;

		public EnginefuncsWp(IntPtr engineFuncs)
		{
			this.ef = (Enginefuncs)Marshal.PtrToStructure(engineFuncs, typeof(Enginefuncs));
		}

		public int PrecacheModel(string model)
		{
			return ef.PrecacheModel(model);
		}

		public int PrecacheSound(string sound)
		{
			return ef.PrecacheSound(sound);
		}

		public int SetModel(Entity ent, string model)
		{
			return ef.SetModel(ent.UPointer, model);
		}

		public int ModelIndex(string model)
		{
			return ef.ModelIndex(model);
		}

		public int ModelFrames(int model_index)
		{
			return ef.ModelFrames(model_index);
		}

		public unsafe void SetSize(Entity ent, Vec3 min, Vec3 max)
		{
			float[] fmin = min.GetArray();
			float[] fmax = max.GetArray();

			fixed(float* pmin = fmin, pmax = fmax)
			{
				ef.SetSize(ent.UPointer, pmin, pmax);
			}
		}

		public void ChangeLevel(string level)
		{
			ef.ChangeLevel(level, "");
		}

		public void GetSpawnParms(Entity ent)
		{
			ef.GetSpawnParms(ent.UPointer);
		}

		public void SaveSpawnParms(Entity ent)
		{
			ef.SaveSpawnParms(ent.UPointer);
		}

		public unsafe float VecToYaw(Vec3 vec)
		{
			float[] fvec = vec.GetArray();

			fixed(float* pvec = fvec)
			{
				return ef.VecToYaw(pvec);
			}
		}

		public unsafe Vec3 VecToAngles(Vec3 vec)
		{
			float[] fvec = vec.GetArray();
			float[] fangl = new float[3];

			fixed(float* pvec = fvec, pangl = fangl)
			{
				ef.VecToAngles(pvec, pangl);

				return new Vec3(fangl);
			}
		}

		public unsafe void MoveToOrigin(Entity ent, Vec3 goal, float dist, MoveType mt)
		{
			float[] fvec = goal.GetArray();

			fixed(float* pvec = fvec)
			{
				ef.MoveToOrigin(ent.UPointer, pvec, dist, (int)mt);
			}
		}

		public void ChangeYaw(Entity ent)
		{
			ef.ChangeYaw(ent.UPointer);
		}

		public void ChangePitch(Entity ent)
		{
			ef.ChangeYaw(ent.UPointer);
		}

		public Entity FindEntityByString(Entity ent_start, string field, string value)
		{
			return Entity.GetEntity(ef.FindEntityByString(ent_start.UPointer, field, value));
		}

		public int GetEntityIllum(Entity ent)
		{
			return ef.GetEntityIllum(ent.UPointer);
		}

		public unsafe Entity FindEntityInSphere(Entity ent_start, Vec3 origin, float radius)
		{
			float[] fvec = origin.GetArray();

			fixed(float* pvec = fvec)
			{
				return Entity.GetEntity(ef.FindEntityInSphere(ent_start.UPointer, pvec, radius));
			}
		}

		public Entity FindClientInPVS(Entity ent)
		{
			return Entity.GetEntity(ef.FindClientInPVS(ent.UPointer));
		}

		public Entity EntitiesInPVS(Entity pl)
		{
			return Entity.GetEntity(ef.EntitiesInPVS(pl.UPointer));
		}

		public unsafe void MakeVectors(Vec3 vec)
		{
			float[] fvec = vec.GetArray();

			fixed(float* pvec = fvec)
			{
				ef.MakeVectors(pvec);
			}
		}

		public unsafe void AngleVectors(Vec3 vec, out Vec3 forward, out Vec3 right, out Vec3 up)
		{
			float[] fvec = vec.GetArray();
			float[] fwvec = new float[3], rvec = new float[3], upvec = new float[3];

			fixed(float* pvec = fvec, pfwvec = fwvec, prvec = rvec, pupvec = upvec)
			{
				ef.AngleVectors(pvec, pfwvec, prvec, pupvec);

				forward = new Vec3(fwvec);
				right = new Vec3(rvec);
				up = new Vec3(upvec);
			}
		}

		public Entity CreateEntity()
		{
			return Entity.GetEntity(ef.CreateEntity());
		}

		public void RemoveEntity(Entity ent)
		{
			ef.RemoveEntity(ent.UPointer);
		}

		public Entity CreateNamedEntity(int classname)
		{
			return Entity.GetEntity(ef.CreateNamedEntity(classname));
		}

		public void MakeStatic(Entity ent)
		{
			ef.MakeStatic(ent.UPointer);
		}

		public bool EntIsOnFloor(Entity ent)
		{
			return ef.EntityIsOnFloor(ent.UPointer) > 1;
		}

		public enum DropToFloorResult { Allsolid = -1, FloorNotFound, Ok }
		public DropToFloorResult DropToFloor(Entity ent)
		{
			return (DropToFloorResult)ef.DropToFloor(ent.UPointer);
		}

		public bool WalkMove(Entity ent, float yaw, float dist, WalkMoveMode mode)
		{
			return ef.WalkMove(ent.UPointer, yaw, dist, (int)mode) == 1;
		}

		public unsafe void SetOrigin(Entity ent, Vec3 origin)
		{
			float[] fvec = origin.GetArray();

			fixed(float* pvec = fvec)
			{
				ef.SetOrigin(ent.UPointer, pvec);
			}
		}

		public void EmitSound(Entity ent, int channel, string sample, float volume, float attenuation, int flags, int pitch)
		{
			ef.EmitSound(ent.UPointer, channel, sample, volume, attenuation, flags, pitch);
		}

		public unsafe void EmitAmbientSound(Entity ent, Vec3 pos, string sample, float volume, float attenuation, int flags, int pitch)
		{
			float[] fvec = pos.GetArray();

			fixed(float* pvec = fvec)
			{
				ef.EmitAmbientSound(ent.UPointer, pvec, sample, volume, attenuation, flags, pitch);
			}
		}

		public unsafe TraceResult TraceLine(Vec3 v1, Vec3 v2, int noMonsters, Entity skipEnt)
		{
			float[] fvec1 = v1.GetArray();
			float[] fvec2 = v2.GetArray();

			fixed(float* pvec1 = fvec1, pvec2 = fvec2)
			{
				void* resTrace = null;
				ef.TraceLine(pvec1, pvec2, noMonsters, skipEnt.UPointer, (TraceResult*)resTrace);

				return (TraceResult)Marshal.PtrToStructure(new IntPtr(resTrace), typeof(TraceResult));
			}
		}

		public unsafe TraceResult TraceToss(Entity pent, Entity pentToIgnore)
		{
			void* resTrace = null;
			ef.TraceToss(pent.UPointer, pentToIgnore.UPointer, (TraceResult*)resTrace);

			return (TraceResult)Marshal.PtrToStructure(new IntPtr(resTrace), typeof(TraceResult));
		}

		public unsafe int TraceMonsterHull(Entity pEdict, Vec3 v1, Vec3 v2, int fNoMonsters, Entity pentToSkip, out TraceResult ptr)
		{
			float[] fvec1 = v1.GetArray();
			float[] fvec2 = v2.GetArray();

			fixed(float* pvec1 = fvec1, pvec2 = fvec2)
			{
				void* resTrace = null;
				int res = ef.TraceMonsterHull(pEdict.UPointer, pvec1, pvec2, fNoMonsters, pentToSkip.UPointer, (TraceResult*)resTrace);

				ptr = (TraceResult)Marshal.PtrToStructure(new IntPtr(resTrace), typeof(TraceResult));
				return res;
			}
		}

		public unsafe TraceResult TraceHull(Vec3 v1, Vec3 v2, int fNoMonsters, int hullNumber, Entity pentToSkip)
		{
			float[] fvec1 = v1.GetArray();
			float[] fvec2 = v2.GetArray();

			fixed(float* pvec1 = fvec1, pvec2 = fvec2)
			{
				void* resTrace = null;
				ef.TraceHull(pvec1, pvec2, fNoMonsters, hullNumber, pentToSkip.UPointer, (TraceResult*)resTrace);

				return (TraceResult)Marshal.PtrToStructure(new IntPtr(resTrace), typeof(TraceResult)); 
			}
		}

		public unsafe TraceResult TraceModel(Vec3 v1, Vec3 v2, int hullNumber, Entity pent)
		{
			float[] fvec1 = v1.GetArray();
			float[] fvec2 = v2.GetArray();

			fixed(float* pvec1 = fvec1, pvec2 = fvec2)
			{
				void* resTrace = null;
				ef.TraceModel(pvec1, pvec2, hullNumber, pent.UPointer, (TraceResult*)resTrace);

				return (TraceResult)Marshal.PtrToStructure(new IntPtr(resTrace), typeof(TraceResult));
			}
		}

		public unsafe string TraceTexture(Entity pTextureEntity, Vec3 v1, Vec3 v2)
		{
			float[] fvec1 = v1.GetArray();
			float[] fvec2 = v2.GetArray();

			fixed(float* pvec1 = fvec1, pvec2 = fvec2)
			{
				return Marshal.PtrToStringAnsi(ef.TraceTexture(pTextureEntity.UPointer, pvec1, pvec2));
			}
		}

		public unsafe TraceResult TraceSphere(Vec3 v1, Vec3 v2, int fNoMonsters, float radius, Entity pentToSkip)
		{
			float[] fvec1 = v1.GetArray();
			float[] fvec2 = v2.GetArray();

			fixed(float* pvec1 = fvec1, pvec2 = fvec2)
			{
				void* resTrace = null;
				ef.TraceSphere(pvec1, pvec2, fNoMonsters, radius, pentToSkip.UPointer, (TraceResult*)resTrace);

				return (TraceResult)Marshal.PtrToStructure(new IntPtr(resTrace), typeof(TraceResult));
			}
		}

		public unsafe Vec3 GetAimVector(Entity ent, float speed)
		{
			float* res = null;
			ef.GetAimVector(ent.UPointer, speed, res);

			return new Vec3(res);
		}

		public void ServerCommand(string cmd, params object[] param)
		{
			ef.ServerCommand(param.Length > 0 ? string.Format(cmd, param) : cmd);
		}

		public void ServerExecute()
		{
			ef.ServerExecute();
		}

		public void ClientCommand(Entity pEdict, string cmd, params object[] param)
		{
			ef.ClientCommand(pEdict.UPointer, param.Length > 0 ? string.Format(cmd, param) : cmd);
		}

		public unsafe void ParticleEffect(Vec3 org, Vec3 dir, float color, float count)
		{
			float[] forg = org.GetArray();
			float[] fdir = dir.GetArray();

			fixed(float* porg = forg, pdir = fdir)
			{
				ef.ParticleEffect(porg, pdir, color, count);
			}
		}

		public void LightStyle(int style, string val)
		{
			ef.LightStyle(style, val);
		}

		public int DecalIndex(string name)
		{
			return ef.DecalIndex(name);
		}

		public unsafe PointContentsType PointContents(Vec3 rgflVector)
		{
			float[] fvec = rgflVector.GetArray();

			fixed(float* pvec = fvec)
			{
				return (PointContentsType)ef.PointContents(pvec);
			}
		}

		public unsafe void MessageBegin(MessageDestination msg_dest, int msg_type, Vec3 pOrigin, Entity ed)
		{
			float[] fvec = pOrigin.GetArray();

			fixed(float* pvec = fvec)
			{
				ef.MessageBegin((int)msg_dest, msg_type, pvec, ed.UPointer);
			}
		}

		public void MessageEnd()
		{
			ef.MessageEnd();
		}

		public void WriteByte(int iValue)
		{
			ef.WriteByte(iValue);
		}

		// char в c# 2 байта занимает, так что осторожнее с приведением
		public void WriteChar(int iValue)
		{
			ef.WriteChar(iValue);
		}

		public void WriteShort(int iValue)
		{
			ef.WriteShort(iValue);
		}

		public void WriteLong(int iValue)
		{
			ef.WriteLong(iValue);
		}

		public void WriteAngle(float flValue)
		{
			ef.WriteAngle(flValue);
		}

		public void WriteCoord(float flValue)
		{
			ef.WriteCoord(flValue);
		}

		public void WriteString(string sz)
		{
			ef.WriteString(sz);
		}

		// TODO Entity вместо int
		public void WriteEntity(int iValue)
		{
			ef.WriteEntity(iValue);
		}

		// возвращает указатель на структуру в куче
		// память по указателю должна быть очищена вручную
		public unsafe IntPtr CVarRegister(string name, string val)
		{
			cvar_t* cvar = (cvar_t*)UnixMarshal.AllocHeap(sizeof(cvar_t)).ToPointer();
			cvar->name = (sbyte*)UnixMarshal.StringToHeap(name).ToPointer();
			cvar->str = (sbyte*)UnixMarshal.StringToHeap(val).ToPointer();
			cvar->next = (cvar_t*)IntPtr.Zero.ToPointer();

			ef.CVarRegister(cvar);

			return new IntPtr(cvar);
		}

		public float CVarGetFloat(string szVarName)
		{
			return ef.CVarGetFloat(szVarName);
		}

		public string CVarGetString(string szVarName)
		{
			return Marshal.PtrToStringAnsi(ef.CVarGetString(szVarName));
		}

		public void CVarSetFloat(string szVarName, float flValue)
		{
			ef.CVarSetFloat(szVarName, flValue);
		}

		public void CVarSetString(string name, string val)
		{
			ef.CVarSetString(name, val);
		}

		// TODO alerttype
		public void AlertMessage(short alerttype, string szFmt, params object[] param)
		{
			ef.AlertMessage(alerttype, param.Length > 0 ? string.Format(szFmt, param) : szFmt);
		}

		public void EngineFprintf(/*FILE* */IntPtr pfile, string szFmt, params object[] param)
		{
			ef.EngineFprintf(pfile.ToPointer(), param.Length > 0 ? string.Format(szFmt, param) : szFmt);
		}

		public IntPtr PvAllocEntPrivateData(Entity pEdict, long cb)
		{
			return new IntPtr(ef.PvAllocEntPrivateData(pEdict.UPointer, cb));
		}

		public IntPtr PvEntPrivateData(Entity pEdict)
		{
			return new IntPtr(pEdict.UPointer);
		}

		public void FreeEntPrivateData(Entity pEdict)
		{
			ef.FreeEntPrivateData(pEdict.UPointer);
		}

		public string SzFromIndex(int str_index)
		{
			return Marshal.PtrToStringAnsi(ef.SzFromIndex(str_index));
		}

		public int AllocString(string szValue)
		{
			return ef.AllocString(szValue);
		}

		// Это наверное не нужно, TODO проверить делегат
		public /*entvars_t* */ IntPtr GetVarsOfEntDelegate(Entity pEdict)
		{
			return new IntPtr(ef.GetVarsOfEnt(pEdict.UPointer));
		}

		public Entity PEntityOfEntOffset(int iEntOffset)
		{
			return Entity.GetEntity(ef.PEntityOfEntOffset(iEntOffset));
		}

		public int EntOffsetOfPEntity(Entity pEdict)
		{
			return ef.EntOffsetOfPEntity(pEdict.UPointer);
		}

		public int IndexOfEdict(Entity pEdict)
		{
			return ef.IndexOfEdict(pEdict.UPointer);
		}

		public Entity PEntityOfEntIndex(int index)
		{
			return Entity.GetEntity(ef.PEntityOfEntIndex(index));
		}

		public Entity FindEntityByVars(IntPtr pvars)
		{
			return Entity.GetEntity(ef.FindEntityByVars((entvars_t*)pvars.ToPointer()));
		}

		public Entity FindEntityByVars(entvars_t* pvars)
		{
			return Entity.GetEntity(ef.FindEntityByVars(pvars));
		}

		public IntPtr GetModelPtr(Entity pEdict)
		{
			return new IntPtr(ef.GetModelPtr(pEdict.UPointer));
		}

		public int RegUserMsg(string pszName, int iSize)
		{
			return ef.RegUserMsg(pszName, iSize);
		}

		public void AnimationAutomove(Entity pEdict, float flTime)
		{
			ef.AnimationAutomove(pEdict.UPointer, flTime);
		}

		public void GetBonePosition(Entity pEdict, int iBone, out Vec3 rgflOrigin, out Vec3 rgflAngles)
		{
			float* porigin = null, pangles = null;

			ef.GetBonePosition(pEdict.UPointer, iBone, porigin, pangles);

			rgflOrigin = new Vec3(porigin);
			rgflAngles = new Vec3(pangles);
		}

		public ulong FunctionFromName(string pName)
		{
			return ef.FunctionFromName(pName);
		}

		public string NameForFunction(ulong function)
		{
			return Marshal.PtrToStringAnsi(ef.NameForFunction(function));
		}

		public void ClientPrintf(Entity pEdict, PrintType ptype, string szMsg, params object[] param)
		{
			ef.ClientPrintf(pEdict.UPointer, ptype, param.Length > 0 ? string.Format(szMsg, param) : szMsg);
		}

		public void ServerPrint(string szMsg, params object[] param)
		{
			ef.ServerPrint(param.Length > 0 ? string.Format(szMsg, param) : szMsg);
		}

		public string Cmd_Args()
		{
			return Marshal.PtrToStringAnsi(ef.Cmd_Args());
		}

		public string Cmd_Argv(int argc)
		{
			return Marshal.PtrToStringAnsi(ef.Cmd_Argv(argc));
		}

		public int Cmd_Argc()
		{
			return ef.Cmd_Argc();
		}

		public void GetAttachment(Entity pEdict, int iAttachment, out Vec3 rgflOrigin, out Vec3 rgflAngles)
		{
			float* porigin = null, pangles = null;

			ef.GetAttachment(pEdict.UPointer, iAttachment, porigin, pangles);

			rgflOrigin = new Vec3(porigin);
			rgflAngles = new Vec3(pangles);
		}

		// IntPtr должен быть указателем на ulong
		public void CRC32_Init(IntPtr pulCRC)
		{
			ef.CRC32_Init((uint*)pulCRC.ToPointer());
		}

		public void CRC32_ProcessBuffer(IntPtr pulCRC, IntPtr p, int len)
		{
			ef.CRC32_ProcessBuffer((uint*)pulCRC.ToPointer(), p.ToPointer(), len);
		}

		public void CRC32_ProcessByte(IntPtr pulCRC, byte ch)
		{
			ef.CRC32_ProcessByte((uint*)pulCRC.ToPointer(), ch);
		}

		// TODO проверить делегат, что за long возвращает
		public long CRC32_Final(long pulCRC)
		{
			return ef.CRC32_Final(pulCRC);
		}

		public long RandomLong(long lLow, long lHigh)
		{
			return ef.RandomLong(lLow, lHigh);
		}

		public float RandomFloat(float flLow, float flHigh)
		{
			return ef.RandomFloat(flLow, flHigh);
		}

		public void SetView(Entity pClient, Entity pViewent)
		{
			ef.SetView(pClient.UPointer, pViewent.UPointer);
		}

		public float Time()
		{
			return ef.Time();
		}

		public void CrosshairAngle(Entity pClient, float pitch, float yaw)
		{
			ef.CrosshairAngle(pClient.UPointer, pitch, yaw);
		}

		public /*byte* */ IntPtr LoadFileForMe(string filename, ref int pLength)
		{
			return new IntPtr((void*)ef.LoadFileForMe(filename, ref pLength));
		}

		public void FreeFile(IntPtr buffer)
		{
			ef.FreeFile(buffer.ToPointer());
		}

		public void EndSection(string pszSectionName)
		{
			ef.EndSection(pszSectionName);
		}

		// TODO что возвращает?
		public int CompareFileTime(string filename1, string filename2, ref int iCompare)
		{
			return ef.CompareFileTime(filename1, filename2, ref iCompare);
		}

		public string GetGameDir()
		{
			StringBuilder dir = new StringBuilder(128);
			ef.GetGameDir(dir);

			return dir.ToString();
		}

		//TODO wtf?
		//public unsafe delegate void Cvar_RegisterVariableDelegate(cvar_t* variable);

		public void FadeClientVolume(Entity pEdict, int fadePercent, int fadeOutSeconds, int holdTime, int fadeInSeconds)
		{
			ef.FadeClientVolume(pEdict.UPointer, fadePercent, fadeOutSeconds, holdTime, fadeInSeconds);
		}

		public void SetClientMaxspeed(Entity pEdict, float fNewMaxspeed)
		{
			ef.SetClientMaxspeed(pEdict.UPointer, fNewMaxspeed);
		}

		public Entity CreateFakeClient(string netname)
		{
			return Entity.GetEntity(ef.CreateFakeClient(netname));
		}

		public unsafe void RunPlayerMove(Entity fakeClient, Vec3 viewangles, float forwardmove, float sidemove, float upmove, ushort buttons, byte impulse, byte msec)
		{
			float[] fvec = viewangles.GetArray();

			fixed(float* pvec = fvec)
			{
				ef.RunPlayerMove(fakeClient.UPointer, pvec, forwardmove, sidemove, upmove, buttons, impulse, msec);
			}
		}
		public int NumberOfEntities()
		{
			return ef.NumberOfEntities();
		}

		public IntPtr GetInfoKeyBuffer(Entity ent)
		{
			return ef.GetInfoKeyBuffer(ent.UPointer);
		}

		public string InfoKeyValueDelegate(IntPtr infoBuffer, string key)
		{
			return Marshal.PtrToStringAnsi(ef.InfoKeyValue(infoBuffer, key));
		}

		public void SetKeyValue(IntPtr infobuffer, string key, string value)
		{
			ef.SetKeyValue(infobuffer, key, value);
		}

		public void SetClientKeyValue(int clientIndex, IntPtr infobuffer, string key, string value)
		{
			ef.SetClientKeyValue(clientIndex, infobuffer, key, value);
		}

		public bool IsMapValid(string filename)
		{
			return ef.IsMapValid(filename) == 1;
		}

		// TODO entityIndex -> Entity
		public unsafe void StaticDecal(Vec3 origin, int decalIndex, int entityIndex, int modelIndex)
		{
			float[] forg = origin.GetArray();

			fixed(float* porg = forg)
			{
				ef.StaticDecal(porg, decalIndex, entityIndex, modelIndex);
			}
		}

		public int PrecacheGeneric(string s)
		{
			return ef.PrecacheGeneric(s);
		}

		public int GetPlayerUserId(Entity e)
		{
			return ef.GetPlayerUserId(e.UPointer);
		}

		public unsafe void BuildSoundMsg(Entity entity, int channel, string sample, float volume, float attenuation, int fFlags, int pitch, MessageDestination msg_dest, int msg_type, Vec3 pOrigin, Entity ed)
		{
			float[] forg = pOrigin.GetArray();

			fixed(float* porg = forg)
			{
				ef.BuildSoundMsg(entity.UPointer, channel, sample, volume, attenuation, fFlags, pitch, (int)msg_dest, msg_type, porg, ed.UPointer);
			}
		}

		public bool IsDedicatedServer()
		{
			return ef.IsDedicatedServer() == 1;
		}

		public IntPtr CVarGetPointer(string szVarName)
		{
			return new IntPtr((void*)ef.CVarGetPointer(szVarName));
		}

		public uint GetPlayerWONId(Entity e)
		{
			return ef.GetPlayerWONId(e.UPointer);
		}

		public void Info_RemoveKey(IntPtr s, string key)
		{
			ef.Info_RemoveKey(s, key);
		}

		public string GetPhysicsKeyValue(Entity pClient, string key)
		{
			return Marshal.PtrToStringAnsi(ef.GetPhysicsKeyValue(pClient.UPointer, key));
		}

		public void SetPhysicsKeyValue(Entity pClient, string key, string value)
		{
			ef.SetPhysicsKeyValue(pClient.UPointer, key, value);
		}

		// TODO делегат
		public string GetPhysicsInfoString(Entity pClient)
		{
			return Marshal.PtrToStringAnsi(ef.GetPhysicsInfoString(pClient.Pointer));
		}

		// TODO посмотреть как работает
		public ushort PrecacheEvent(int type, string psz)
		{
			return ef.PrecacheEvent(type, psz);
		}

		// TODO делегат
		public void PlaybackEvent(int flags, Entity pInvoker, ushort eventindex, float delay, Vec3 origin, Vec3 angles, float fparam1, float fparam2, int iparam1, int iparam2, int bparam1, int bparam2)
		{
			float[] forg = origin.GetArray();
			float[] fangl = angles.GetArray();

			fixed(float* porg = forg, pangl = fangl)
			{
				ef.PlaybackEvent(flags, pInvoker.Pointer, eventindex, delay, porg, pangl, fparam1, fparam2, iparam1, iparam2, bparam1, bparam2);
			}
		}

		public unsafe IntPtr SetFatPVS(Vec3 origin)
		{
			float[] fvec = origin.GetArray();

			fixed(float* pvec = fvec)
			{
				return ef.SetFatPVS(pvec);
			}
		}

		public unsafe IntPtr SetFatPAS(Vec3 origin)
		{
			float[] fvec = origin.GetArray();

			fixed(float* pvec = fvec)
			{
				return ef.SetFatPAS(pvec);
			}
		}

		public bool CheckVisibility(Entity entity, IntPtr pset)
		{
			// может быть = 2, если чета-там headnode
			return ef.CheckVisibility(entity.UPointer, pset) > 0;
		}

		public void DeltaSetField(IntPtr pFields, string fieldname)
		{
			ef.DeltaSetField(pFields.ToPointer(), fieldname);
		}

		public void DeltaUnsetField(IntPtr pFields, string fieldname)
		{
			ef.DeltaUnsetField(pFields.ToPointer(), fieldname);
		}

		public delegate void ConditionalencodeDelegateManaged(IntPtr pFields, IntPtr from, IntPtr to);
		public void DeltaAddEncoder(string name, ConditionalencodeDelegateManaged conditionalencode)
		{
			ef.DeltaAddEncoder(name, Marshal.GetFunctionPointerForDelegate(conditionalencode));
		}

		public int GetCurrentPlayer()
		{
			return ef.GetCurrentPlayer();
		}

		// TODO
		public bool CanSkipPlayer(Entity player)
		{
			return ef.CanSkipPlayer(player.UPointer) == 1;
		}

		// TODO
		public bool DeltaFindField(IntPtr pFields, string fieldname)
		{
			return ef.DeltaFindField(pFields.ToPointer(), fieldname) == 1;
		}

		public void DeltaSetFieldByIndex(IntPtr pFields, int fieldNumber)
		{
			ef.DeltaSetFieldByIndex(pFields.ToPointer(), fieldNumber);
		}

		public void DeltaUnsetFieldByIndex(IntPtr pFields, int fieldNumber)
		{
			ef.DeltaUnsetFieldByIndex(pFields.ToPointer(), fieldNumber);
		}

		public void SetGroupMask(int mask, int op)
		{
			ef.SetGroupMask(mask, op);
		}

		// TODO врапер мне сюда для entity_state_t
		public int CreateInstancedBaseline(int classname, /*entity_state_t* */ IntPtr baseline)
		{
			return ef.CreateInstancedBaseline(classname, (entity_state_t*)baseline.ToPointer());
		}

		public void Cvar_DirectSet(IntPtr cvar, string value)
		{
			ef.Cvar_DirectSet((cvar_t*)cvar.ToPointer(), value);
		}

		// TODO посмотреть хук
		public void ForceUnmodified(ForceType type, Vec3 mins, Vec3 maxs, string filename)
		{
			float[] fmins = mins.GetArray();
			float[] fmaxs = maxs.GetArray();

			fixed(float* pmins = fmins, pmaxs = fmaxs)
			{
				ef.ForceUnmodified(type, pmins, pmaxs, filename);
			}

		}

		public void GetPlayerStats(Entity pClient, ref int ping, ref int packet_loss)
		{
			ef.GetPlayerStats(pClient.UPointer, ref ping, ref packet_loss);
		}
			
		public void AddServerCommand(string cmd_name, functionServerCommand functionPointer)
		{
			ef.AddServerCommand(cmd_name, Marshal.GetFunctionPointerForDelegate(functionPointer));
		}

		public int Voice_GetClientListening(int iReceiver, int iSender)
		{
			return ef.Voice_GetClientListening(iReceiver, iSender);
		}

		public int Voice_SetClientListening(int iReceiver, int iSender, int bListen)
		{
			return ef.Voice_SetClientListening(iReceiver, iSender, bListen);
		}

		public string GetPlayerAuthId(Entity playerEntity)
		{
			return Marshal.PtrToStringAnsi(ef.GetPlayerAuthId(playerEntity.UPointer));
		}

		public IntPtr SequenceGet(string fileName, string entryName)
		{
			return new IntPtr(ef.SequenceGet(fileName, entryName));
		}

		public IntPtr SequencePickSentence(string groupName, int pickMethod, ref int picked)
		{
			return new IntPtr(ef.SequencePickSentence(groupName, pickMethod, ref picked));
		}

		public int GetFileSize(string filename)
		{
			return ef.GetFileSize(filename);
		}

		public uint GetApproxWavePlayLen(string filepath)
		{
			return ef.GetApproxWavePlayLen(filepath);
		}

		public int IsCareerMatch()
		{
			return ef.IsCareerMatch();
		}

		public int GetLocalizedStringLength(string label)
		{
			return ef.GetLocalizedStringLength(label);
		}

		public void RegisterTutorMessageShown(int mid)
		{
			ef.RegisterTutorMessageShown(mid);
		}

		public int GetTimesTutorMessageShown(int mid)
		{
			return ef.GetTimesTutorMessageShown(mid);
		}

		public void ProcessTutorMessageDecayBuffer(IntPtr buffer, int bufferLength)
		{
			ef.ProcessTutorMessageDecayBuffer(buffer, bufferLength);
		}

		public void ConstructTutorMessageDecayBuffer(IntPtr buffer, int bufferLength)
		{
			ef.ConstructTutorMessageDecayBuffer(buffer, bufferLength);
		}

		public void ResetTutorMessageDecayData()
		{
			ef.ResetTutorMessageDecayData();
		}

		public void QueryClientCvarValue(Entity player, string cvarName)
		{
			ef.QueryClientCvarValue(player.UPointer, cvarName);
		}

		public void QueryClientCvarValue2(Entity player, string cvarName, int requestID)
		{
			ef.QueryClientCvarValue2(player.UPointer, cvarName, requestID);
		}
	}
}