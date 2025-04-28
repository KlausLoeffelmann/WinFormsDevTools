using System.Diagnostics;
using System.Reflection;

namespace DevTools.RuntimeDeploy
{
    /// <summary>
    /// Manages temporary copies of assemblies for loading and inspection, 
    /// preventing file locks on original assemblies.
    /// </summary>
    internal sealed class AssemblyTempManager : IDisposable
    {
        private readonly List<string> _tempFiles = [];
        private bool _disposed;

        /// <summary>
        /// Creates a temporary copy of an assembly file and loads it.
        /// </summary>
        /// <param name="assemblyPath">Path to the original assembly.</param>
        /// <returns>The loaded assembly, or null if it cannot be loaded.</returns>
        public Assembly? LoadAssemblyFromCopy(string assemblyPath)
        {
            try
            {
                string tempFile = CreateTempCopy(assemblyPath);
                return Assembly.LoadFrom(tempFile);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading assembly from copy: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets assembly name information from a file without locking it.
        /// </summary>
        /// <param name="assemblyPath">Path to the assembly.</param>
        /// <returns>Assembly name information, or null if unavailable.</returns>
        public AssemblyName? GetAssemblyName(string assemblyPath)
        {
            try
            {
                string tempFile = CreateTempCopy(assemblyPath);
                return AssemblyName.GetAssemblyName(tempFile);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting assembly name: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Creates a temporary copy of a file.
        /// </summary>
        /// <param name="filePath">Path to the original file.</param>
        /// <returns>Path to the temporary copy.</returns>
        private string CreateTempCopy(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Assembly file not found", filePath);
            }

            // Create unique temporary file
            string tempFile = Path.Combine(
                Path.GetTempPath(), 
                $"{Path.GetFileNameWithoutExtension(filePath)}_{Guid.NewGuid()}{Path.GetExtension(filePath)}");

            // Copy file content to temp location
            File.Copy(filePath, tempFile, overwrite: true);
            _tempFiles.Add(tempFile);

            return tempFile;
        }

        /// <summary>
        /// Releases all resources used by the AssemblyTempManager.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                // Clean up all temporary files
                foreach (string tempFile in _tempFiles)
                {
                    try
                    {
                        if (File.Exists(tempFile))
                        {
                            File.Delete(tempFile);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error cleaning up temporary file {tempFile}: {ex.Message}");
                    }
                }

                _tempFiles.Clear();
                _disposed = true;
            }
        }
    }
}
