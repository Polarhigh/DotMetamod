using System;
using System.Text;
using System.Runtime.InteropServices;
using DotMetamod.HLSDK;

namespace DotMetamod.HLSDK
{
	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct TraceResult
	{
		int		fAllSolid;			// if true, plane is not valid
		int		fStartSolid;		// if true, the initial point was in a solid area
		int		fInOpen;
		int		fInWater;
		float	flFraction;			// time completed, 1.0 = didn't hit anything
		Vec3	vecEndPos;			// final position
		float	flPlaneDist;
		Vec3	vecPlaneNormal;		// surface normal at impact
		edict_t*pHit;				// entity the surface is on
		int		iHitgroup;			// 0 == generic, non zero is specific body part
	}

	public enum PrintType
	{
		Console,
		Center,
		Chat,
	}


	public enum ForceType
	{
		Exactfile,						// File on client must exactly match server's file
		ModelSamebounds,				// For model files only, the geometry must fit in the same bbox
		ModelSpecifybounds,			// For model files only, the geometry must fit in the specified bbox
		ModelSpecifyboundsIfAvail,	// For Steam model files only, the geometry must fit in the specified bbox (if the file is available)
	}

	[StructLayout (LayoutKind.Sequential)]
	public unsafe struct KeyValueData
	{
		public sbyte*	szClassName;	// in: entity classname
		public sbyte*	szKeyName;		// in: name of key
		public sbyte*	szValue;		// in: value of key
		public int		fHandled;		// out: DLL sets to true if key-value pair was understood
	}

	// @TODO 
	// 1) В некоторых местах, там где в нативной сигнатуре в параметрах есть [const] char*, может быть нужно выделять память в куче
	// 2) long заменить на int
	public delegate int PrecacheModelDelegate(string s);
	public delegate int PrecacheSoundDelegate(string s);
	public unsafe delegate int SetModelDelegate(edict_t* e, string m);
	public delegate int ModelIndexDelegate(string m);
	public delegate int ModelFramesDelegate(int modelIndex);
	public unsafe delegate void SetSizeDelegate(edict_t* e, float* rgflMin, float* rgflMax);
	public delegate void ChangeLevelDelegate(string s1, string s2);
	public unsafe delegate void GetSpawnParmsDelegate(edict_t* ent);
	public unsafe delegate void SaveSpawnParmsDelegate(edict_t* ent);
	public unsafe delegate float VecToYawDelegate(float* rgflVector);
	public unsafe delegate void VecToAnglesDelegate(float* rgflVectorIn, float* rgflVectorOut);
	public unsafe delegate void MoveToOriginDelegate(edict_t* ent, float* pflGoal, float dist, int iMoveType);
	public unsafe delegate void ChangeYawDelegate(edict_t* ent);
	public unsafe delegate void ChangePitchDelegate(edict_t* ent);
	public unsafe delegate edict_t* FindEntityByStringDelegate(edict_t* pEdictStartSearchAfter, string pszField, string pszValue);
	public unsafe delegate int GetEntityIllumDelegate(edict_t* pEnt);
	public unsafe delegate edict_t* FindEntityInSphereDelegate(edict_t* pEdictStartSearchAfter, float* org, float rad);
	public unsafe delegate edict_t* FindClientInPVSDelegate(edict_t* pEdict);
	public unsafe delegate edict_t* EntitiesInPVSDelegate(edict_t* pplayer);
	public unsafe delegate void MakeVectorsDelegate(float* rgflVector);
	public unsafe delegate void AngleVectorsDelegate(float* rgflVector, float* forward, float* right, float* up);
	public unsafe delegate edict_t* CreateEntityDelegate();
	public unsafe delegate void RemoveEntityDelegate(edict_t* e);
	public unsafe delegate edict_t* CreateNamedEntityDelegate(int className);
	public unsafe delegate void MakeStaticDelegate(edict_t* ent);
	public unsafe delegate int EntityIsOnFloorDelegate(edict_t* e);
	public unsafe delegate int DropToFloorDelegate(edict_t* e);
	public unsafe delegate int WalkMoveDelegate(edict_t* ent, float yaw, float dist, int iMode);
	public unsafe delegate void SetOriginDelegate(edict_t* e, float* rgflOrigin);
	public unsafe delegate void EmitSoundDelegate(edict_t* entity, int channel, string sample, /*int*/float volume, float attenuation, int fFlags, int pitch);
	public unsafe delegate void EmitAmbientSoundDelegate(edict_t* entity, float* pos, string samp, float vol, float attenuation, int fFlags, int pitch);
	public unsafe delegate void TraceLineDelegate(float* v1, float* v2, int fNoMonsters, edict_t* pentToSkip, TraceResult* ptr);
	public unsafe delegate void TraceTossDelegate(edict_t* pent, edict_t* pentToIgnore, TraceResult* ptr);
	public unsafe delegate int TraceMonsterHullDelegate(edict_t* pEdict, float* v1, float* v2, int fNoMonsters, edict_t* pentToSkip, TraceResult* ptr);
	public unsafe delegate void TraceHullDelegate(float* v1, float* v2, int fNoMonsters, int hullNumber, edict_t* pentToSkip, TraceResult* ptr);
	public unsafe delegate void TraceModelDelegate(float *v1, float* v2, int hullNumber, edict_t* pent, TraceResult* ptr);
	public unsafe delegate IntPtr TraceTextureDelegate(edict_t* pTextureEntity, float* v1, float* v2);
	public unsafe delegate void TraceSphereDelegate(float* v1, float* v2, int fNoMonsters, float radius, edict_t* pentToSkip, TraceResult* ptr);
	public unsafe delegate void GetAimVectorDelegate(edict_t* ent, float speed, float* rgflReturn);
	public delegate void ServerCommandDelegate(string str);
	public delegate void ServerExecuteDelegate();
	public unsafe delegate void ClientCommandDelegate(edict_t* pEdict, string szFmt); // @TODO 3rd NULL ?
	public unsafe delegate void ParticleEffectDelegate(float* org, float* dir, float color, float count);
	public delegate void LightStyleDelegate(int style, string val);
	public delegate int DecalIndexDelegate(string name);
	public unsafe delegate int PointContentsDelegate(float* rgflVector);
	public unsafe delegate void MessageBeginDelegate(int msg_dest, int msg_type, float* pOrigin, edict_t* ed);
	public delegate void MessageEndDelegate();
	public delegate void WriteByteDelegate(int iValue);
	public delegate void WriteCharDelegate(int iValue);
	public delegate void WriteShortDelegate(int iValue);
	public delegate void WriteLongDelegate(int iValue);
	public delegate void WriteAngleDelegate(float flValue);
	public delegate void WriteCoordDelegate(float flValue);
	public delegate void WriteStringDelegate(string sz);
	public delegate void WriteEntityDelegate(int iValue);
	public unsafe delegate void CVarRegisterDelegate(cvar_t* pCvar);
	public delegate float CVarGetFloatDelegate(string szVarName);
	public delegate IntPtr CVarGetStringDelegate(string szVarName);
	public delegate void CVarSetFloatDelegate(string szVarName, float flValue);
	public delegate void CVarSetStringDelegate(string name, string val);
	public delegate void AlertMessageDelegate(short alerttype, string szFmt); // @TODO 3rd NULL ?
	public unsafe delegate void EngineFprintfDelegate(void* pfile, string szFmt); // @TODO 3rd NULL ?
	public unsafe delegate void* PvAllocEntPrivateDataDelegate(edict_t* pEdict, long cb);
	public unsafe delegate void* PvEntPrivateDataDelegate(edict_t* pEdict);
	public unsafe delegate void FreeEntPrivateDataDelegate(edict_t* pEdict);
	public delegate IntPtr SzFromIndexDelegate(int iString);
	public delegate int AllocStringDelegate(string szValue);
	public unsafe delegate entvars_t* GetVarsOfEntDelegate(edict_t* pEdict);
	public unsafe delegate edict_t* PEntityOfEntOffsetDelegate(int iEntOffset);
	public unsafe delegate int EntOffsetOfPEntityDelegate(edict_t* pEdict);
	public unsafe delegate int IndexOfEdictDelegate(edict_t* pEdict);
	public unsafe delegate edict_t* PEntityOfEntIndexDelegate(int index);
	public unsafe delegate edict_t* FindEntityByVarsDelegate(entvars_t* pvars);
	public unsafe delegate void* GetModelPtrDelegate(edict_t* pEdict);
	public delegate int RegUserMsgDelegate(string pszName, int iSize);
	public unsafe delegate void AnimationAutomoveDelegate(edict_t* pEdict, float flTime);
	public unsafe delegate void GetBonePositionDelegate(edict_t* pEdict, int iBone, float* rgflOrigin, float* rgflAngles);
	public delegate ulong FunctionFromNameDelegate(string pName);
	public delegate IntPtr NameForFunctionDelegate(ulong function);
	public unsafe delegate void ClientPrintfDelegate(edict_t* pEdict, PrintType ptype, string szMsg);
	public delegate void ServerPrintDelegate(string szMsg);
	public delegate IntPtr Cmd_ArgsDelegate();
	public delegate IntPtr Cmd_ArgvDelegate(int argc);
	public delegate int Cmd_ArgcDelegate();
	public unsafe delegate void GetAttachmentDelegate(edict_t* pEdict, int iAttachment, float* rgflOrigin, float* rgflAngles);
	public unsafe delegate void CRC32_InitDelegate(uint* pulCRC);
	public unsafe delegate void CRC32_ProcessBufferDelegate(uint* pulCRC, void* p, int len);
	public unsafe delegate void CRC32_ProcessByteDelegate(uint* pulCRC, byte ch);
	public delegate long CRC32_FinalDelegate(long pulCRC);
	public delegate long RandomLongDelegate(long lLow, long lHigh);
	public delegate float RandomFloatDelegate(float flLow, float flHigh);
	public unsafe delegate void SetViewDelegate(edict_t* pClient, edict_t* pViewent);
	public delegate float TimeDelegate();
	public unsafe delegate void CrosshairAngleDelegate(edict_t* pClient, float pitch, float yaw);
	public unsafe delegate byte* LoadFileForMeDelegate(string filename, ref int pLength);
	public unsafe delegate void FreeFileDelegate(void* buffer);
	public delegate void EndSectionDelegate(string pszSectionName);
	public delegate int CompareFileTimeDelegate(string filename1, string filename2, ref int iCompare);
	public delegate void GetGameDirDelegate(StringBuilder directory);
	public unsafe delegate void Cvar_RegisterVariableDelegate(cvar_t* variable);
	public unsafe delegate void FadeClientVolumeDelegate(edict_t* pEdict, int fadePercent, int fadeOutSeconds, int holdTime, int fadeInSeconds);
	public unsafe delegate void SetClientMaxspeedDelegate(edict_t* pEdict, float fNewMaxspeed);
	public unsafe delegate edict_t* CreateFakeClientDelegate(string netname);
	public unsafe delegate void RunPlayerMoveDelegate(edict_t* edict, float* viewangles, float forwardmove, float sidemove, float upmove, ushort buttons, byte impulse, byte msec);
	public delegate int NumberOfEntitiesDelegate();
	public unsafe delegate IntPtr GetInfoKeyBufferDelegate(edict_t* edict);
	public delegate IntPtr InfoKeyValueDelegate(IntPtr infoBuffer, string key);
	public delegate void SetKeyValueDelegate(IntPtr infobuffer, string key, string value);
	public delegate void SetClientKeyValueDelegate(int clientIndex, IntPtr infobuffer, string key, string value);
	public delegate int IsMapValidDelegate(string filename);
	public unsafe delegate void StaticDecalDelegate(float* origin, int decalIndex, int entityIndex, int modelIndex);
	public delegate int PrecacheGenericDelegate(string s);
	public unsafe delegate int GetPlayerUserIdDelegate(edict_t* e);
	public unsafe delegate void BuildSoundMsgDelegate(edict_t* entity, int channel, string sample, /*int*/float volume, float attenuation, int fFlags, int pitch, int msg_dest, int msg_type, float* pOrigin, edict_t* ed);
	public delegate int IsDedicatedServerDelegate();
	public unsafe delegate cvar_t* CVarGetPointerDelegate(string szVarName);
	public unsafe delegate uint GetPlayerWONIdDelegate(edict_t* e);
	public delegate void Info_RemoveKeyDelegate(IntPtr s, string key); //TODO StringBuilder ? 
	public unsafe delegate IntPtr GetPhysicsKeyValueDelegate(edict_t* pClient, string key);
	public unsafe delegate void SetPhysicsKeyValueDelegate(edict_t* pClient, string key, string value);
	public delegate IntPtr GetPhysicsInfoStringDelegate(IntPtr pClient);
	public delegate ushort PrecacheEventDelegate(int type, string psz);
	public unsafe delegate void PlaybackEventDelegate(int flags, IntPtr pInvoker, ushort eventindex, float delay, float* origin, float* angles, float fparam1, float fparam2, int iparam1, int iparam2, int bparam1, int bparam2);
	public unsafe delegate IntPtr SetFatPVSDelegate(float* org);
	public unsafe delegate IntPtr SetFatPASDelegate(float* org);
	public unsafe delegate int CheckVisibilityDelegate(edict_t* edict, IntPtr pset);
	public unsafe delegate void DeltaSetFieldDelegate(void* pFields, string fieldname); //TODO around gc?
	public unsafe delegate void DeltaUnsetFieldDelegate(void* pFields, string fieldname);
	public unsafe delegate void ConditionalencodeDelegate(void* pFields, IntPtr from, IntPtr to);
	public delegate void DeltaAddEncoderDelegate(string name, IntPtr conditionalencode);
	public delegate int GetCurrentPlayerDelegate();
	public unsafe delegate int CanSkipPlayerDelegate(edict_t* player);
	public unsafe delegate int DeltaFindFieldDelegate(void* pFields, string fieldname);
	public unsafe delegate void DeltaSetFieldByIndexDelegate(void* pFields, int fieldNumber);
	public unsafe delegate void DeltaUnsetFieldByIndexDelegate(void* pFields, int fieldNumber);
	public delegate void SetGroupMaskDelegate(int mask, int op);
	public unsafe delegate int CreateInstancedBaselineDelegate(int classname, entity_state_t* baseline);
	public unsafe delegate void Cvar_DirectSetDelegate(cvar_t* var, string value);
	public unsafe delegate void ForceUnmodifiedDelegate(ForceType type, float* mins, float* maxs, string filename);
	public unsafe delegate void GetPlayerStatsDelegate(edict_t* pClient, ref int ping, ref int packet_loss);
	public delegate void functionServerCommand();
	public delegate void AddServerCommandDelegate(string cmd_name, IntPtr functionPointer);
	public delegate int Voice_GetClientListeningDelegate(int iReceiver, int iSender);
	public delegate int Voice_SetClientListeningDelegate(int iReceiver, int iSender, int bListen);
	public unsafe delegate IntPtr GetPlayerAuthIdDelegate(edict_t* playerEntity);
	public unsafe delegate void* SequenceGetDelegate(string fileName, string entryName);
	public unsafe delegate void* SequencePickSentenceDelegate(string groupName, int pickMethod, ref int picked);
	public delegate int GetFileSizeDelegate(string filename);
	public delegate uint GetApproxWavePlayLenDelegate(string filepath);
	public delegate int IsCareerMatchDelegate();
	public delegate int GetLocalizedStringLengthDelegate(string label);
	public delegate void RegisterTutorMessageShownDelegate(int mid);
	public delegate int GetTimesTutorMessageShownDelegate(int mid);
	public delegate void ProcessTutorMessageDecayBufferDelegater(IntPtr buffer, int bufferLength);
	public delegate void ConstructTutorMessageDecayBufferDelegate(IntPtr buffer, int bufferLength);
	public delegate void ResetTutorMessageDecayDataDelegate();
	public unsafe delegate void QueryClientCvarValueDelegate(edict_t* player, string cvarName);
	public unsafe delegate void QueryClientCvarValue2Delegate(edict_t* player, string cvarName, int requestID);

	[StructLayout (LayoutKind.Sequential)]
	public struct Enginefuncs
	{
		public PrecacheModelDelegate		PrecacheModel;
		public PrecacheSoundDelegate		PrecacheSound;
		public SetModelDelegate				SetModel;
		public ModelIndexDelegate			ModelIndex;
		public ModelFramesDelegate			ModelFrames;
		public SetSizeDelegate				SetSize;
		public ChangeLevelDelegate			ChangeLevel;
		public GetSpawnParmsDelegate		GetSpawnParms;
		public SaveSpawnParmsDelegate		SaveSpawnParms;
		public VecToYawDelegate				VecToYaw;
		public VecToAnglesDelegate			VecToAngles;
		public MoveToOriginDelegate			MoveToOrigin;
		public ChangeYawDelegate			ChangeYaw;
		public ChangePitchDelegate			ChangePitch;
		public FindEntityByStringDelegate	FindEntityByString;
		public GetEntityIllumDelegate		GetEntityIllum;
		public FindEntityInSphereDelegate	FindEntityInSphere;
		public FindClientInPVSDelegate		FindClientInPVS;
		public EntitiesInPVSDelegate		EntitiesInPVS;
		public MakeVectorsDelegate			MakeVectors;
		public AngleVectorsDelegate			AngleVectors;
		public CreateEntityDelegate			CreateEntity;
		public RemoveEntityDelegate			RemoveEntity;
		public CreateNamedEntityDelegate	CreateNamedEntity;
		public MakeStaticDelegate			MakeStatic;
		public EntityIsOnFloorDelegate		EntityIsOnFloor;
		public DropToFloorDelegate			DropToFloor;
		public WalkMoveDelegate				WalkMove;
		public SetOriginDelegate			SetOrigin;
		public EmitSoundDelegate			EmitSound;
		public EmitAmbientSoundDelegate		EmitAmbientSound;
		public TraceLineDelegate			TraceLine;
		public TraceTossDelegate			TraceToss;
		public TraceMonsterHullDelegate		TraceMonsterHull;
		public TraceHullDelegate			TraceHull;
		public TraceModelDelegate			TraceModel;
		public TraceTextureDelegate			TraceTexture;
		public TraceSphereDelegate			TraceSphere;
		public GetAimVectorDelegate			GetAimVector;
		public ServerCommandDelegate		ServerCommand;
		public ServerExecuteDelegate		ServerExecute;
		public ClientCommandDelegate		ClientCommand;
		public ParticleEffectDelegate		ParticleEffect;
		public LightStyleDelegate			LightStyle;
		public DecalIndexDelegate			DecalIndex;
		public PointContentsDelegate		PointContents;
		public MessageBeginDelegate 		MessageBegin;
		public MessageEndDelegate   		MessageEnd;
		public WriteByteDelegate    		WriteByte;
		public WriteCharDelegate    		WriteChar;
		public WriteShortDelegate   		WriteShort;
		public WriteLongDelegate    		WriteLong;
		public WriteAngleDelegate   		WriteAngle;
		public WriteCoordDelegate   		WriteCoord;
		public WriteStringDelegate  		WriteString;
		public WriteEntityDelegate  		WriteEntity;
		public CVarRegisterDelegate			CVarRegister;
		public CVarGetFloatDelegate			CVarGetFloat;
		public CVarGetStringDelegate		CVarGetString;
		public CVarSetFloatDelegate			CVarSetFloat;
		public CVarSetStringDelegate		CVarSetString;
		public AlertMessageDelegate			AlertMessage;
		public EngineFprintfDelegate		EngineFprintf;
		public PvAllocEntPrivateDataDelegate PvAllocEntPrivateData;
		public PvEntPrivateDataDelegate		PvEntPrivateData;
		public FreeEntPrivateDataDelegate	FreeEntPrivateData;
		public SzFromIndexDelegate			SzFromIndex;
		public AllocStringDelegate			AllocString;
		public GetVarsOfEntDelegate			GetVarsOfEnt;
		public PEntityOfEntOffsetDelegate	PEntityOfEntOffset;
		public EntOffsetOfPEntityDelegate	EntOffsetOfPEntity;
		public IndexOfEdictDelegate			IndexOfEdict;
		public PEntityOfEntIndexDelegate	PEntityOfEntIndex;
		public FindEntityByVarsDelegate		FindEntityByVars;
		public GetModelPtrDelegate			GetModelPtr;
		public RegUserMsgDelegate			RegUserMsg;
		public AnimationAutomoveDelegate	AnimationAutomove;
		public GetBonePositionDelegate		GetBonePosition;
		public FunctionFromNameDelegate		FunctionFromName;
		public NameForFunctionDelegate		NameForFunction;
		public ClientPrintfDelegate			ClientPrintf;
		public ServerPrintDelegate			ServerPrint;
		public Cmd_ArgsDelegate				Cmd_Args;
		public Cmd_ArgvDelegate				Cmd_Argv;
		public Cmd_ArgcDelegate				Cmd_Argc;
		public GetAttachmentDelegate		GetAttachment;
		public CRC32_InitDelegate			CRC32_Init;
		public CRC32_ProcessBufferDelegate	CRC32_ProcessBuffer;
		public CRC32_ProcessByteDelegate	CRC32_ProcessByte;
		public CRC32_FinalDelegate			CRC32_Final;
		public RandomLongDelegate			RandomLong;
		public RandomFloatDelegate			RandomFloat;
		public SetViewDelegate				SetView;
		public TimeDelegate					Time;
		public CrosshairAngleDelegate		CrosshairAngle;
		public LoadFileForMeDelegate		LoadFileForMe;
		public FreeFileDelegate				FreeFile;
		public EndSectionDelegate			EndSection;
		public CompareFileTimeDelegate		CompareFileTime;
		public GetGameDirDelegate			GetGameDir;
		public Cvar_RegisterVariableDelegate Cvar_RegisterVariable;
		public FadeClientVolumeDelegate		FadeClientVolume;
		public SetClientMaxspeedDelegate	SetClientMaxspeed;
		public CreateFakeClientDelegate		CreateFakeClient;
		public RunPlayerMoveDelegate		RunPlayerMove;
		public NumberOfEntitiesDelegate		NumberOfEntities;
		public GetInfoKeyBufferDelegate		GetInfoKeyBuffer;
		public InfoKeyValueDelegate			InfoKeyValue;
		public SetKeyValueDelegate			SetKeyValue;
		public SetClientKeyValueDelegate	SetClientKeyValue;
		public IsMapValidDelegate			IsMapValid;
		public StaticDecalDelegate			StaticDecal;
		public PrecacheGenericDelegate		PrecacheGeneric;
		public GetPlayerUserIdDelegate		GetPlayerUserId;
		public BuildSoundMsgDelegate		BuildSoundMsg;
		public IsDedicatedServerDelegate	IsDedicatedServer;
		public CVarGetPointerDelegate		CVarGetPointer;
		public GetPlayerWONIdDelegate		GetPlayerWONId;
		public Info_RemoveKeyDelegate		Info_RemoveKey;
		public GetPhysicsKeyValueDelegate	GetPhysicsKeyValue;
		public SetPhysicsKeyValueDelegate	SetPhysicsKeyValue;
		public GetPhysicsInfoStringDelegate	GetPhysicsInfoString;
		public PrecacheEventDelegate		PrecacheEvent;
		public PlaybackEventDelegate		PlaybackEvent;
		public SetFatPVSDelegate			SetFatPVS;
		public SetFatPASDelegate			SetFatPAS;
		public CheckVisibilityDelegate		CheckVisibility;
		public DeltaSetFieldDelegate		DeltaSetField;
		public DeltaUnsetFieldDelegate		DeltaUnsetField;
		public DeltaAddEncoderDelegate		DeltaAddEncoder;
		public GetCurrentPlayerDelegate		GetCurrentPlayer;
		public CanSkipPlayerDelegate		CanSkipPlayer;
		public DeltaFindFieldDelegate		DeltaFindField;
		public DeltaSetFieldByIndexDelegate	DeltaSetFieldByIndex;
		public DeltaUnsetFieldByIndexDelegate DeltaUnsetFieldByIndex;
		public SetGroupMaskDelegate			SetGroupMask;
		public CreateInstancedBaselineDelegate CreateInstancedBaseline;
		public Cvar_DirectSetDelegate		Cvar_DirectSet;
		public ForceUnmodifiedDelegate		ForceUnmodified;
		public GetPlayerStatsDelegate		GetPlayerStats;
		public AddServerCommandDelegate		AddServerCommand;
		public Voice_GetClientListeningDelegate	Voice_GetClientListening;
		public Voice_SetClientListeningDelegate	Voice_SetClientListening;
		public GetPlayerAuthIdDelegate		GetPlayerAuthId;
		public SequenceGetDelegate			SequenceGet;
		public SequencePickSentenceDelegate	SequencePickSentence;
		public GetFileSizeDelegate			GetFileSize;
		public GetApproxWavePlayLenDelegate	GetApproxWavePlayLen;
		public IsCareerMatchDelegate		IsCareerMatch;
		public GetLocalizedStringLengthDelegate	GetLocalizedStringLength;
		public RegisterTutorMessageShownDelegate RegisterTutorMessageShown;
		public GetTimesTutorMessageShownDelegate GetTimesTutorMessageShown;
		public ProcessTutorMessageDecayBufferDelegater ProcessTutorMessageDecayBuffer;
		public ConstructTutorMessageDecayBufferDelegate ConstructTutorMessageDecayBuffer;
		public ResetTutorMessageDecayDataDelegate ResetTutorMessageDecayData;
		public QueryClientCvarValueDelegate QueryClientCvarValue;
		public QueryClientCvarValue2Delegate QueryClientCvarValue2;
	}

	public delegate void GameDLLInitDelegate();
	public unsafe delegate int DispatchSpawnDelegate(edict_t* pEdect);
	public unsafe delegate void DispatchThinkDelegate(edict_t* pent);
	public unsafe delegate void DispatchUseDelegate(edict_t* pentUsed, edict_t* pentOther);
	public unsafe delegate void DispatchTouchDelegate(edict_t* pentTouched, edict_t* pentOther);
	public unsafe delegate void DispatchBlockedDelegate(edict_t* pentBlocked, edict_t* pentOther);
	public unsafe delegate void DispatchKeyValueDelegate(edict_t* pentKeyvalue, KeyValueData* pkvd);
	// void DispatchSave(edict_t* pent, SAVERESTOREDATA* pSaveData);
	// int DispatchRestore(edict_t* pent, SAVERESTOREDATA* pSaveData, int globalEntity);
	public unsafe delegate void DispatchObjectCollsionBoxDelegate(edict_t* pent);
	// void SaveWriteFields(SAVERESTOREDATA *pSaveData, const char *pname, void *pBaseData, TYPEDESCRIPTION *pFields, int fieldCount);
	// void SaveReadFields(SAVERESTOREDATA *pSaveData, const char *pname, void *pBaseData, TYPEDESCRIPTION *pFields, int fieldCount);
	// void SaveGlobalState(SAVERESTOREDATA *pSaveData);
	// void RestoreGlobalState(SAVERESTOREDATA *pSaveData);
	// void ResetGlobalState(void);
	public unsafe delegate int ClientConnectDelegate(edict_t* pEntity, string pszName, string pszAddress, out StringBuilder szRejectReason);
	public unsafe delegate void ClientDisconnectDelegate(edict_t* pEntity);
	public unsafe delegate void ClientKillDelegate(edict_t* pEntity);
	public unsafe delegate void ClientPutInServerDelegate(edict_t* pEntity);
	public unsafe delegate void DllClientCommandDelegate(edict_t* pEntity);
	public unsafe delegate void ClientUserInfoChangedDelegate(edict_t* pEntity, IntPtr infobuffer);
	public unsafe delegate void ServerActivateDelegate(edict_t* pEdictList, int edictCount, int clientMax);
	public delegate void ServerDeactivateDelegate();
	public unsafe delegate void PlayerPreThinkDelegate(edict_t* pEntity);
	public unsafe delegate void PlayerPostThinkDelegate(edict_t* pEntity);
	public delegate void StartFrameDelegate();
	public delegate void ParmsNewLevelDelegate();
	public delegate void ParmsChangeLevelDelegate();
	public delegate /* char* */ IntPtr GetGameDescriptionDelegate();
	public unsafe delegate void PlayerCustomizationDelegate(edict_t* pEntity, IntPtr pCust);
	public unsafe delegate void SpectatorConnectDelegate(edict_t* pEntity);
	public unsafe delegate void SpectatorDisconnectDelegate(edict_t* pEntity);
	public unsafe delegate void SpectatorThinkDelegate(edict_t* pEntity);
	public delegate void Sys_ErrorDelegate(string error_string);
	public unsafe delegate void PM_MoveDelegate(playermove_t* ppmove, int server);
	public unsafe delegate void PM_InitDelegate(playermove_t* ppmove);
	public delegate sbyte PM_FindTextureTypeDelegate(string name);
	public unsafe delegate void SetupVisibilityDelegate(edict_t* pViewEntity, edict_t* pClient, /*unsigned char ** */ IntPtr pvs, /*unsigned char ** */ IntPtr pas);
	public unsafe delegate void UpdateClientDataDelegate(edict_t* ent, int sendweapons, clientdata_t* cd);
	public unsafe delegate int AddToFullPackDelegate(entity_state_t* state, int e, edict_t* ent, edict_t* host, int hostflags, int player, /*unsigned char ** */ IntPtr pSet);
	public unsafe delegate void CreateBaselineDelegate(int player, int eindex, entity_state_t* baseline, edict_t* entity, int playermodelindex, Vec3 player_mins, Vec3 player_maxs);
	public delegate void RegisterEncodersDelegate();
	public unsafe delegate int GetWeaponDataDelegate(edict_t* player, weapon_data_t* info);
	public unsafe delegate void CmdStartDelegate(edict_t* player, usercmd_t* cmd, uint random_seed);
	public unsafe delegate void CmdEndDelegate(edict_t* player);
	public unsafe delegate int ConnectionlessPacketDelegate(netadr_t* net_from, string args, StringBuilder response_buffer, ref int response_buffer_size);
	public unsafe delegate int GetHullBoundsDelegate(int hullnumber, float* mins, float* maxs);
	public unsafe delegate void CreateInstancedBaselinesDelegate();
	public unsafe delegate int InconsistentFileDelegate(edict_t* player, string filename, StringBuilder disconnect_message); // disconnect_message 256 max
	public unsafe delegate int AllowLagCompensationDelegate();

	[StructLayout (LayoutKind.Sequential)]
	public struct DllFunctions
	{
		public GameDLLInitDelegate			GameDLLInit;
		public DispatchSpawnDelegate		DispatchSpawn;
		public DispatchThinkDelegate		DispatchThink;
		public DispatchUseDelegate			DispatchUse;
		public DispatchTouchDelegate		DispatchTouch;
		public DispatchBlockedDelegate		DispatchBlocked;
		public DispatchKeyValueDelegate		DispatchKeyValue;
		public IntPtr						DispatchSave;
		public IntPtr						DispatchRestore;
		public DispatchObjectCollsionBoxDelegate DispatchObjectCollsionBox;
		public IntPtr						SaveWriteFields;
		public IntPtr						SaveReadFields;
		public IntPtr						SaveGlobalState;
		public IntPtr						RestoreGlobalState;
		public IntPtr						ResetGlobalState;
		public ClientConnectDelegate 		ClientConnect;
		public ClientDisconnectDelegate		ClientDisconnect;
		public ClientKillDelegate			ClientKill;
		public ClientPutInServerDelegate	ClientPutInServer;
		public DllClientCommandDelegate		ClientCommand;
		public ClientUserInfoChangedDelegate ClientUserInfoChanged;
		public ServerActivateDelegate		ServerActivate;
		public ServerDeactivateDelegate		ServerDeactivate;
		public PlayerPreThinkDelegate		PlayerPreThink;
		public PlayerPostThinkDelegate		PlayerPostThink;
		public StartFrameDelegate			StartFrame;
		public ParmsNewLevelDelegate		ParmsNewLevel;
		public ParmsChangeLevelDelegate		ParmsChangeLevel;
		public GetGameDescriptionDelegate	GetGameDescription;
		public PlayerCustomizationDelegate	PlayerCustomization;
		public SpectatorConnectDelegate		SpectatorConnect;
		public SpectatorDisconnectDelegate	SpectatorDisconnect;
		public SpectatorThinkDelegate		SpectatorThink;
		public Sys_ErrorDelegate			Sys_Error;
		public PM_MoveDelegate				PM_Move;
		public PM_InitDelegate				PM_Init;
		public PM_FindTextureTypeDelegate	PM_FindTextureType;
		public SetupVisibilityDelegate		SetupVisibility;
		public UpdateClientDataDelegate		UpdateClientData;
		public AddToFullPackDelegate		AddToFullPack;
		public CreateBaselineDelegate		CreateBaseline;
		public RegisterEncodersDelegate		RegisterEncoders;
		public GetWeaponDataDelegate		GetWeaponDataDelegate;
		public CmdStartDelegate				CmdStart;
		public CmdEndDelegate				CmdEnd;
		public ConnectionlessPacketDelegate ConnectionlessPacket;
		public GetHullBoundsDelegate		GetHullBounds;
		public CreateInstancedBaselinesDelegate CreateInstanceBaseline;
		public InconsistentFileDelegate		InconsistentFile;
		public AllowLagCompensationDelegate	AllowLagCompensation;
	}

	public delegate void OnFreeEntPrivateDataDelegate(edict_t pEnt);
	public delegate void GameShutdownDelegate();
	public unsafe delegate int ShouldCollideDelegate(edict_t* pentTouched, edict_t* pentOther);
	public unsafe delegate void CvarValueDelegate(edict_t* pEnt, string value);
	public unsafe delegate void CvarValue2Delegate (edict_t* pEnt, int requestID, string cvarName, string value );

	[StructLayout (LayoutKind.Sequential)]
	public struct NewDllFuncions
	{
		// Called right before the object's memory is freed. 
		// Calls its destructor.
		public OnFreeEntPrivateDataDelegate OnFreeEntPrivateData;
		public GameShutdownDelegate	GameShutdown;
		public ShouldCollideDelegate	ShouldCollide;
		public CvarValueDelegate		CvarValue;
		public CvarValue2Delegate		CvarValue2;
	}
}

