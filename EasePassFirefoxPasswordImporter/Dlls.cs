using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasePassBrowserPasswordImporter
{
    internal static class Dlls
    {
        private static string assemblyFolder = "";

        public static void ToDir(string dir)
        {
            File.WriteAllBytes(dir + "BouncyCastle.Crypto.dll", Properties.Resources.BouncyCastle_Crypto);
            File.WriteAllBytes(dir + "BrowserPasswordLib.dll", Properties.Resources.BrowserPasswordLib);
            File.WriteAllBytes(dir + "EntityFramework.dll", Properties.Resources.EntityFramework);
            File.WriteAllBytes(dir + "EntityFramework.SqlServer.dll", Properties.Resources.EntityFramework_SqlServer);
            File.WriteAllBytes(dir + "Microsoft.Data.Sqlite.dll", Properties.Resources.Microsoft_Data_Sqlite);
            File.WriteAllBytes(dir + "Newtonsoft.Json.dll", Properties.Resources.Newtonsoft_Json);
            File.WriteAllBytes(dir + "SQLitePCLRaw.core.dll", Properties.Resources.SQLitePCLRaw_core);
            File.WriteAllBytes(dir + "System.Buffers.dll", Properties.Resources.System_Buffers);
            File.WriteAllBytes(dir + "System.Memory.dll", Properties.Resources.System_Memory);
            File.WriteAllBytes(dir + "System.Numerics.Vectors.dll", Properties.Resources.System_Numerics_Vectors);
            File.WriteAllBytes(dir + "System.Runtime.CompilerServices.Unsafe.dll", Properties.Resources.System_Runtime_CompilerServices_Unsafe);

            assemblyFolder = dir;

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyPath = System.IO.Path.Combine(assemblyFolder, new AssemblyName(args.Name).Name + ".dll");

            if (System.IO.File.Exists(assemblyPath))
            {
                return Assembly.LoadFrom(assemblyPath);
            }

            return null;
        }
    }
}
