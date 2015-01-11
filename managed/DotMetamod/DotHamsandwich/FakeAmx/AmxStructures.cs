using System;
using System.Runtime.InteropServices;

namespace DotHamsandwich.FakeAmxNs
{
	internal unsafe delegate int AmxNativeDelegate(AMX* amx, int* paramsArr);
	internal unsafe delegate int AmxCallbackDelegate(AMX* amx, int index, int* result, int paramsArr);
	internal unsafe delegate int AmxDebugDelegate(AMX* amx);

	internal enum AmxFlag : short
	{
		Debug	 = 0x02,			/* symbolic info. available */
		Compact	 = 0x04,			/* compact encoding */
		ByteOpc	 = 0x08, 			/* opcode is a byte (not a cell) */
		NoChecks = 0x10, 			/* no array bounds checking; no STMT opcode */
		NtvReg	 = 0x1000,			/* all native functions are registered */
		JITC	 = 0x2000,			/* abstract machine is JIT compiled */
		Browse	 = 0x4000,			/* busy browsing */
		//Reloc	 = 0x8000			/* jump/call addresses relocated */
	}

	/*struct AMX_FUNCSTUB
	{
		ucell address         PACKED;
		char name[sEXPMAX+1];
	}*/

	[StructLayout (LayoutKind.Sequential, Pack=1)]
	internal struct AMX_FUNCSTUBNT
	{
		public uint address;
		public uint nameofs;		//we need this for amxx to be backwards comaptible
	}

	[StructLayout (LayoutKind.Sequential, Pack=1)]
	internal unsafe struct AMX
	{
		public byte* head;			/* points to the AMX header plus the code, optionally also the data */
		public byte* data;			/* points to separate data+stack+heap, may be NULL */
		AmxCallbackDelegate callback;
		AmxDebugDelegate debug;		/* debug callback */
		/* for external functions a few registers must be accessible from the outside */
		public int cip;				/* instruction pointer: relative to base + amxhdr->cod */
		public int frm;				/* stack frame base: relative to base + amxhdr->dat */
		public int hea;				/* top of the heap: relative to base + amxhdr->dat */
		public int hlw;				/* bottom of the heap: relative to base + amxhdr->dat */
		public int stk;				/* stack pointer: relative to base + amxhdr->dat */
		public int stp;				/* top of the stack: relative to base + amxhdr->dat */
		public int flags;			/* current status, see amx_Flags() */
		/* user data */
		public void* usertags0;
		public void* usertags1;
		public void* usertags2;
		public void* usertags3;
		//okay userdata[3] in AMX Mod X is for the CPlugin * pointer
		//we're also gonna set userdata[2] to a special debug structure
		//lastly, userdata[1] is for opcode_list from amx_BrowseRelocate
		public void* userdata0;
		public void* userdata1;
		public void* userdata2;
		public void* userdata3;
		/* native functions can raise an error */
		public int error;
		/* passing parameters requires a "count" field */
		public int paramcount;
		/* the sleep opcode needs to store the full AMX status */
		public int pri;
		public int alt;
		public int reset_stk;
		public int reset_hea;
		public int sysreq_d;		/* relocated address/value for the SYSREQ.D opcode */
		/* support variables for the JIT */
		public int reloc_size;		/* required temporary buffer for relocations */
		public int code_size;		/* estimated memory footprint of the native code */
	}

	[StructLayout (LayoutKind.Sequential, Pack=1)]
	internal unsafe struct AMX_Header
	{
		public int size;			/* size of the "file" */
		public ushort magic;		/* signature */
		public sbyte file_version;	/* file format version */
		public sbyte amx_version;	/* required version of the AMX */
		public short flags;
		public short defsize;		/* size of a definition record */
		public int cod;				/* initial value of COD - code block */
		public int dat;				/* initial value of DAT - data block */
		public int hea;				/* initial value of HEA - start of the heap */
		public int stp;				/* initial value of STP - stack top */
		public int cip;				/* initial value of CIP - the instruction pointer */
		public int publics;			/* offset to the "public functions" table */
		public int natives;			/* offset to the "native functions" table */
		public int libraries;		/* offset to the table of libraries */
		public int pubvars;			/* the "public variables" table */
		public int tags;			/* the "public tagnames" table */
		public int nametable;		/* name table */
	}

	/* File format version                          Required AMX version
	 *   0 (original version)                       0
	 *   1 (opcodes JUMP.pri, SWITCH and CASETBL)   1
	 *   2 (compressed files)                       2
	 *   3 (public variables)                       2
	 *   4 (opcodes SWAP.pri/alt and PUSHADDR)      4
	 *   5 (tagnames table)                         4
	 *   6 (reformatted header)                     6
	 *   7 (name table, opcodes SYMTAG & SYSREQ.D)  7
	 *   8 (opcode STMT, renewed debug interface)   8
	 */
}

