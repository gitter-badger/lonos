﻿using System;
using Mosa.Runtime.x86;
using System.Runtime.InteropServices;

namespace lonos.kernel.core
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ElfHeader //Size = 52
    {
        public static uint Magic1 = 0x464C457F; //0x7f + "ELF"

        // TODO: Check, why it's not possible to access a fixed buffer.
        // Like: if ( elfHeader->Ident1 != ElfHeader.Magic1) {
        // All kinds of fixing the variable doesn't work.
        // Maybe a mono problem / old .NET Version?
        //public unsafe fixed uint Ident[4]; // Offset = 0, Size = 16

        // Workarround:
        public unsafe uint Ident1;
        public unsafe uint Ident2;
        public unsafe uint Ident3;
        public unsafe uint Ident4;

        public ushort Type;     // Offset = 16, Size = 2
        public ushort Machine;  // Offset = 18, Size = 2
        public uint Version;    // Offset = 20, Size = 4
        public uint Entry;      // Offset = 24, Size = 4
        public uint PhOff;      // Offset = 28, Size = 4
        public uint ShOff;      // Offset = 32, Size = 4
        public uint Flags;      // Offset = 36, Size = 4
        public ushort EhSize;   // Offset = 40, Size = 2
        public ushort PhEntSize;// Offset = 42, Size = 2
        public ushort PhNum;    // Offset = 44, Size = 2
        public ushort ShEntSize;// Offset = 46, Size = 2
        public ushort ShNum;    // Offset = 48, Size = 2
        public ushort ShStrNdx; // Offset = 50, Size = 2
    }

}
