using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasePassBrowserPasswordImporter
{
    public static class ReflectionHelper
    {
        public static dynamic firefox = null;
        public static dynamic chrome = null;

        public static void LoadBrowserObjects(string file)
        {
            if (firefox == null)
            {
                foreach (Type t in GetLoadableTypes(Assembly.LoadFile(file)))
                {
                    if (t.Name == "FirefoxPassReader")
                    {
                        firefox = GetInstanceOf(t);
                    }
                    if (t.Name == "ChromePassReader")
                    {
                        chrome = GetInstanceOf(t);
                    }
                }
            }
        }

        private static object GetInstanceOf(Type type)
        {
            try
            {
                ConstructorInfo[] cis = type.GetConstructors();
                foreach (ConstructorInfo ci in cis)
                {
                    if (ci.GetParameters().Length == 0)
                    {
                        return type.Assembly.CreateInstance(type.FullName);
                    }
                }
            }
            catch { }
            return null;
        }

        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            try
            {
                List<Type> types = new List<Type>();
                foreach (TypeInfo ti in assembly.DefinedTypes)
                {
                    types.Add(ti.AsType());
                }
                return types;
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}
