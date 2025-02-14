﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

namespace lonos.kernel.core
{
    public static class Address
    {
        /// <summary>
        /// The Name of Kernel Entry Method. It's used by the Loader to determine it's address.
        /// </summary>
        public static readonly string KernelEntryName = "System.Void lonos.kernel.core.Start::Main()";

        public const uint InitialStack = 0xA00000; // 10MB (stack grows down)

        private const uint InitialAllocStart = 0x4000000;

        public const uint GCInitialMemory = InitialAllocStart;
        public const uint GCInitialMemorySize = 1024 * 1024 * 3;

        public const uint InitialDynamicPage = InitialAllocStart + GCInitialMemorySize;
        public const uint LoaderBasePhys = 0x00200000;  // 3MB

        public const uint KernelBasePhys = 0x01400000;  // 27MB
        public const uint KernelBaseVirt = 0xC0100000;  // 3GB+1MB
        public const uint KernelElfSection = KernelBasePhys - 0x1000;
        public const uint KernelBootInfo = 0xA00000 + 0x1000;
        public const uint OriginalKernelElfSection = KernelBootInfo + 0x1000;  //17MB

        public const uint ReserveMemory = 0x100000;  // 1MB 
        public const uint MaximumMemory = 0xFFFFFFFF;  // 4GB

        public const uint UnitTestStack = 0x00004000;  // 4KB (stack grows down)
        public const uint UnitTestQueue = 0x01E00000;  // 30MB [Size=2MB] - previous: 5KB [Size=1KB] 0x00005000
        public const uint DebuggerBuffer = 0x00010000;  // 16KB [Size=64KB]
    }
}
