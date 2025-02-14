﻿using System;
using Mosa.Runtime;
using Mosa.Kernel.x86;
using Mosa.Runtime.Plug;
using Mosa.Runtime.x86;

namespace lonos.kernel.core
{

    internal static class ManagedMemoy
    {

        public static void InitializeGCMemory()
        {
            // Wipe GCMemory from Bootloader
            Memory.InitialKernelProtect_MakeWritable_BySize(Address.GCInitialMemory, Address.GCInitialMemorySize);
            MemoryOperation.Clear4(Address.GCInitialMemory, Address.GCInitialMemorySize);
        }

        [Plug("Mosa.Runtime.GC::AllocateMemory")]
        static unsafe IntPtr _AllocateMemory(uint size)
        {
            return AllocateMemory(size);
        }

        internal static bool useAllocator;

        static uint nextAddr;
        public static uint AllocationCount;
        static public IntPtr AllocateMemory(uint size)
        {
            AllocationCount++;

            //var col = Screen.column;
            //var row = Screen.row;
            //Screen.column = 0;
            //Screen.Goto(0, 35);
            //Screen.Write("AllocCount: ");
            //Screen.Write(AllocationCount);
            //Screen.Goto(1, 35);
            //Screen.row = row;
            //Screen.column = col;

            if (useAllocator)
                return Memory.Allocate(size, GFP.GFP_KERNEL);

            return AllocateMemory_EarlyBoot(size);
        }

        static IntPtr AllocateMemory_EarlyBoot(uint size)
        {
            var retAddr = nextAddr;
            nextAddr += size;

            return (IntPtr)(((uint)Address.GCInitialMemory) + retAddr);
        }

        public static void DumpToConsoleLine(uint addr, uint length)
        {
            DumpToConsole(addr, length);
            KernelMessage.Write('\n');
        }

        public static void DumpToConsole(uint addr, uint length)
        {
            var sb = new StringBuffer();
            sb.Append("{0:X}+{1:D} ", addr, length);
            KernelMessage.Write(sb);
            sb.Clear();

            for (uint a = addr; a < addr + length; a++)
            {
                sb.Clear();

                if (a != addr)
                    sb.Append(" ");
                var m = Native.Get8(a);
                sb.Append(m, 16, 2);
                KernelMessage.Write(sb);
            }
        }

    }
}
