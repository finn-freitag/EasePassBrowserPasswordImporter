using EasePassExtensibility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasePassBrowserPasswordImporter
{
    public class FirefoxImporter : IPasswordImporter
    {
        public string SourceName => "Firefox";

        public Uri SourceIcon => AboutPage.ByteArrayToUri(Properties.Resources.firefox);

        public PasswordItem[] ImportPasswords()
        {
            try
            {
                string p = Path.GetTempPath() + "browser_key_reader\\";
                if (!Directory.Exists(p)) Directory.CreateDirectory(p);
                try
                {
                    Dlls.ToDir(p);
                }
                catch { }

                ReflectionHelper.LoadBrowserObjects(p + "BrowserPasswordLib.dll");

                List<PasswordItem> passwords = new List<PasswordItem>();
                foreach (dynamic credential in ReflectionHelper.firefox.ReadPasswords())
                {
                    PasswordItem item = new PasswordItem();
                    item.DisplayName = credential.Url;
                    item.UserName = credential.Username;
                    item.Password = credential.Password;
                    item.Website = item.DisplayName;
                    passwords.Add(item);
                }
                return passwords.ToArray();
            }
            catch
            {
                return new PasswordItem[0];
            }
        }

        public bool PasswordsAvailable()
        {
            return true;
            //try
            //{
            //    return ImportPasswords().Length > 0;
            //}
            //catch
            //{
            //    return false;
            //}
        }
    }
}
