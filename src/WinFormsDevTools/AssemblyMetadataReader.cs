using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;

namespace WinFormsDevTools;

internal static class AssemblyMetadataReader
{
    public static AssemblyMetadata GetAssemblyMetadata(string assemblyPath)
    {
        FileStream stream = null!;
        PEReader peReader = null!;

        try
        {
            stream = new FileStream(
                assemblyPath, 
                FileMode.Open, 
                FileAccess.Read, 
                FileShare.ReadWrite);

            peReader = new PEReader(stream);

            if (!peReader.HasMetadata)
            {
                throw new InvalidOperationException("No metadata found in the specified assembly.");
            }

            var metadataReader = peReader.GetMetadataReader();
            var assemblyDefinition = metadataReader.GetAssemblyDefinition();

            var assemblyName = metadataReader.GetString(assemblyDefinition.Name);
            var version = assemblyDefinition.Version.ToString();
            var publicKeyToken = BitConverter.ToString(ComputePublicKeyToken(metadataReader.GetBlobBytes(assemblyDefinition.PublicKey))).Replace("-", "").ToLowerInvariant();
            var fileVersion = GetFileVersion(metadataReader);

            return new AssemblyMetadata
            {
                AssemblyName = $"{assemblyName}, Version={version}, PublicKeyToken={publicKeyToken}",
                PublicKeyToken = publicKeyToken,
                AssemblyVersion = version,
                FileVersion = fileVersion
            };

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            peReader?.Dispose();

            stream?.Close();
            stream?.Dispose();
        }
    }

    private static byte[] ComputePublicKeyToken(byte[] publicKey)
    {
        var hash = SHA1.HashData(publicKey);
        return hash.Skip(hash.Length - 8).ToArray();
    }

    private static string GetFileVersion(MetadataReader metadataReader)
    {
        foreach (var handle in metadataReader.CustomAttributes)
        {
            var attribute = metadataReader.GetCustomAttribute(handle);
            var ctorHandle = attribute.Constructor;
            var container = ctorHandle.Kind switch
            {
                HandleKind.MemberReference => metadataReader.GetMemberReference((MemberReferenceHandle)ctorHandle).Parent,
                HandleKind.MethodDefinition => metadataReader.GetMethodDefinition((MethodDefinitionHandle)ctorHandle).GetDeclaringType(),
                _ => default
            };

            if (container.Kind == HandleKind.TypeReference)
            {
                var typeRef = metadataReader.GetTypeReference((TypeReferenceHandle)container);
                var typeName = metadataReader.GetString(typeRef.Name);
                var typeNamespace = metadataReader.GetString(typeRef.Namespace);

                if (typeNamespace == "System.Reflection" && typeName == "AssemblyFileVersionAttribute")
                {
                    var valueBlob = metadataReader.GetBlobReader(attribute.Value);
                    valueBlob.ReadByte(); // Prolog
                    return valueBlob.ReadSerializedString() ?? string.Empty;
                }
            }
        }

        return string.Empty;
    }
}
