using EasePassExtensibility;
using EasePassBrowserPasswordImporter;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Security.Credentials;

namespace EasePassBrowserPasswordImporter
{
    public class IE10Importer : IPasswordImporter
    {
        public string SourceName => "IE10/Edge";

        public Uri SourceIcon => AboutPage.ByteArrayToUri(Properties.Resources.edge);

        public PasswordItem[] ImportPasswords()
        {
            try
            {
                var result = new List<PasswordItem>();
                var vault = new PasswordVault();
                var credentials = vault.RetrieveAll();
                for (var i = 0; i < credentials.Count; i++)
                {
                    PasswordCredential cred = credentials.ElementAt(i);
                    cred.RetrievePassword();

                    result.Add(new PasswordItem
                    {
                        DisplayName = cred.Resource,
                        Website = cred.Resource,
                        UserName = cred.UserName,
                        Password = cred.Password
                    });
                }
                return result.ToArray();
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
