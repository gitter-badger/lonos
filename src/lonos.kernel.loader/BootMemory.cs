﻿using System;
using Mosa.Runtime;
using Mosa.Kernel.x86;
using Mosa.Runtime.Plug;
using Mosa.Runtime.x86;

namespace lonos.kernel.core
{

    public static class BootMemory
    {

        public static void Setup()
        {
            PageStartAddr = Address.InitialDynamicPage;
            //PageStartAddr = Address.GCInitialMemory;
        }

        [Plug("Mosa.Runtime.GC::AllocateMemory")]
        static unsafe private IntPtr _AllocateMemory(uint size)
        {
            return AllocateMemory(size);
        }

        private static uint nextAddr;
        private static uint cnt;
        static public IntPtr AllocateMemory(uint size)
        {
            cnt++;

            var retAddr = nextAddr;
            nextAddr += size;

            return (IntPtr)(((uint)Address.GCInitialMemory) + retAddr);
        }

        static Addr PageStartAddr;
        public static BootInfoMemory AllocateMemoryMap(USize size, BootInfoMemoryType type)
        {
            var map = new BootInfoMemory();
            map.Start = PageStartAddr;
            map.Size = size;
            map.Type = type;
            PageStartAddr += size;

            KernelMessage.WriteLine("Allocated MemoryMap of Type {0} at {1:X8} with Size {2:X8}", (uint)type, map.Start, map.Size);

            return map;
        }

    }
}
