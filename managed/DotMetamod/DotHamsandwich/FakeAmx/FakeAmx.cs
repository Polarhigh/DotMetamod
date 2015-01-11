using System;
using System.Runtime.InteropServices;
using Mono.Unix;

namespace DotHamsandwich.FakeAmxNs
{
	internal unsafe class FakeAmx
	{
		const ushort AMX_MAGIC = 0xf1e0;
		const sbyte CUR_FILE_VERSION = 8;	/* current file version; also the current AMX version */
		const sbyte MIN_FILE_VERSION = 6;	/* lowest supported file format version for the current AMX version */
		const sbyte MIN_AMX_VERSION = 8;	/* minimum AMX version needed to support the current file format */
		const int FUNCTION_PCODE_SIZE = 6;

		internal struct AmxFunctionInfo
		{
			public AmxNativeDelegate	Function;
			public string				Name;
		}

		AMX* amxData { get; private set; }

		public FakeAmx(AmxFunctionInfo[] publicFunctions, int heapSize = 4096, int stackSize = 4096)
		{
			int hdrSize = Marshal.SizeOf(typeof(AMX_Header));

			AMX_Header* hdr = (AMX_Header*)Marshal.AllocHGlobal(hdrSize).ToPointer();

			int nametablesize = sizeof(short);
			foreach (var f in publicFunctions) nametablesize += f.Name.Length + 1;

			int sc_dataalign = sizeof(int);
			int padding = (int)(sc_dataalign - (hdrSize + nametablesize) % sc_dataalign);
			if (padding == sc_dataalign)
				padding = 0;
				
			hdr->magic		= AMX_MAGIC;
			hdr->file_version = CUR_FILE_VERSION;
			hdr->amx_version = MIN_AMX_VERSION;
			hdr->flags		= (short)(AmxFlag.NoChecks | AmxFlag.ByteOpc);
			hdr->defsize	= (short)Marshal.SizeOf(typeof(AMX_FUNCSTUBNT));
			hdr->publics	= hdrSize;
			hdr->natives	= hdr->publics + publicFunctions.Length*Marshal.SizeOf(typeof(AMX_FUNCSTUBNT));
			hdr->libraries	= hdr->natives;
			hdr->pubvars	= hdr->libraries; // + numlibraries*sizeof(AMX_FUNCSTUBNT);
			hdr->tags		= hdr->pubvars; // + numpubvars*sizeof(AMX_FUNCSTUBNT);
			hdr->nametable	= hdr->tags; // + numtags*sizeof(AMX_FUNCSTUBNT);
			hdr->cod		= hdr->nametable + nametablesize + padding;
			hdr->dat		= hdr->cod; // + code_idx;
			hdr->hea		= hdr->dat; // + glb_declared*sizeof(cell);
			hdr->stp		= hdr->hea + heapSize*sizeof(int);
			hdr->cip		= -1; // mainaddr;
			hdr->size 		= hdr->hea + publicFunctions.Length*FUNCTION_PCODE_SIZE;

			IntPtr data = Marshal.AllocHGlobal(
				hdr->size - hdrSize	+	// data + code
				heapSize*sizeof(int) +	// heap
				stackSize*sizeof(int)	// stack	
			);

			int tablePos = hdr->publics;
			int codePos = hdr->cod;

			foreach (var f in publicFunctions) {
				//WriteFunctionInTable(data, ref tablePos, codePos, f);
				WriteFunctionPCode(data, ref codePos, f.Function);
			}

			amxData = (AMX*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(AMX))).ToPointer();
			amxData->head = (byte*)hdr;
			amxData->data = (byte*)data.ToPointer();
		}

		void WriteFunctionInTable(IntPtr ptr, ref int pos, int funcAddr, AmxFunctionInfo func)
		{
			/* sci6.c:701
			assert(sym->vclass==sGLOBAL);
			func.address=sym->addr;
			func.nameofs=nameofs;
			#if BYTE_ORDER==BIG_ENDIAN
			align32(&func.address);
			align32(&func.nameofs);
			#endif
			pc_resetbin(fout,hdr.publics+count*sizeof(AMX_FUNCSTUBNT));
			pc_writebin(fout,&func,sizeof func);
			pc_resetbin(fout,nameofs);
			pc_writebin(fout,sym->name,strlen(sym->name)+1);
			nameofs+=strlen(sym->name)+1;
			count++; */
		}

		void WriteFunctionPCode(IntPtr ptr, ref int pos, AmxNativeDelegate func)
		{
			Marshal.WriteByte(ptr, pos, 135); // OP_SYSREQ_D
			pos++;

			Marshal.WriteIntPtr(ptr, pos, Marshal.GetFunctionPointerForDelegate(func));
			pos += 4;

			Marshal.WriteByte(ptr, pos, 120); // OP_HALT
			pos++;
		}
	}
}