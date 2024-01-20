using EasePassExtensibility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasePassBrowserPasswordImporter
{
    public class AboutPage : IAboutPlugin // the main code is from: https://github.com/jabiel/BrowserPass/
    {
        public string PluginName => "Browser Password Importer";

        public string PluginDescription => "Imports passwords from common browsers.";

        public string PluginAuthor => "Finn Freitag";

        public string PluginAuthorURL => "https://github.com/finn-freitag";

        public Uri PluginIcon => ByteArrayToUri(Properties.Resources.icon);

        internal static Uri ByteArrayToUri(byte[] bytes)
        {
            string p = Path.GetTempFileName();
            File.WriteAllBytes(p, bytes);
            return new Uri(p);
        }
    }
}
