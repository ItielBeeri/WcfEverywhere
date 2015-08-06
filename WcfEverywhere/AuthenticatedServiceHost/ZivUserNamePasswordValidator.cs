using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Threading;
using System.Security.Principal;

namespace AuthenticatedServiceHost
{
    class ZivUserNamePasswordValidator : UserNamePasswordValidator
    {
        Dictionary<string, string> _users;
        public ZivUserNamePasswordValidator()
        {
            _users = new Dictionary<string, string> { { "u1", "1" }, { "u2", "2" } };
        }
        public override void Validate(string userName, string password)
        {
            if (null == userName || null == password)
            {
                throw new ArgumentNullException();
            }

            if (!(_users.ContainsKey(userName) && _users[userName] == password))
            {
                throw new SecurityTokenException("Unknown Username or Password");
            }

            // CurrentPrincipal overiden later
            //Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);      
        }




    }
}
