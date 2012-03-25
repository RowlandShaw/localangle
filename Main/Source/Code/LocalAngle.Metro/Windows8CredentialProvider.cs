using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalAngle.Metro.Lib
{
    class Windows8CredentialProvider
    {
        /**
        const string VAULT_RESOURCE = "[My App] Credentials";
string UserName { get; set; };
string Password { get; set; };
var vault = new PasswordVault();

try
{
   var creds = vault.FindAllByResource(VAULT_RESOURCE).FirstOrDefault();
   if (creds != null)
   {
      UserName = creds.UserName;
      Password = vault.Retrieve(VAULT_RESOURCE, _vaultedUserName).Password;
   }
}
catch(COMException) 
{
   // this exception likely means that no credentials have been stored
}
**/
    }
}
