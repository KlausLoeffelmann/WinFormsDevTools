﻿using System.Diagnostics.CodeAnalysis;

namespace DevTools.RuntimeDeploy;

internal partial class WinFormsBuildArtefactsManager
{
    internal class DesktopAssemblyInfo
    {
        [AllowNull]
        public string Name { get; init; }

        [AllowNull]
        public DirectoryInfo Path { get; init; }

        [AllowNull]
        public FileInfo[] AssemblyFiles { get; init; }

        public FileInfo[]? RefAssemblyFiles { get; init; }

        public override string ToString() 
            => $"Directory: {Name} Files: {AssemblyFiles?.Length}";
    }
}
