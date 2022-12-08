using System;
using System.IO;
using System.Reflection;

namespace DeveReproduceXmlSerializerBug.PathHelpers
{
    public static class FolderHelperMethods
    {
        public static string AssemblyDirectory => Internal_AssemblyDirectory.Value;
        public static string EntryAssemblyDirectory => Internal_EntryAssemblyDirectory.Value;

        private static Lazy<string> Internal_AssemblyDirectory { get; } = new Lazy<string>(() => GetAssemblyDirectory());
        private static Lazy<string> Internal_EntryAssemblyDirectory { get; } = new Lazy<string>(() => GetEntryAssemblyDirectory());

        private static string GetAssemblyDirectory()
        {
            var assembly = typeof(FolderHelperMethods).GetTypeInfo().Assembly;
            var assemblyDir = Path.GetDirectoryName(assembly.Location) ?? "";
            return assemblyDir;
        }

        private static string GetEntryAssemblyDirectory()
        {
            var assembly = Assembly.GetEntryAssembly();
            if (assembly != null)
            {
                var assemblyDir = Path.GetDirectoryName(assembly.Location) ?? "";
                return assemblyDir;
            }
            return "";
        }

        private static string EnsureExists(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }
}
