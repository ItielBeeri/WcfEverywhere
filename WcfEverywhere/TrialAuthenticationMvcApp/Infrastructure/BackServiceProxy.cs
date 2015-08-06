using SampleServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Web;
using TrialAuthenticationMvcApp.Models;
using Ziv.ServiceModel.Operations;

namespace TrialAuthenticationMvcApp.Infrastructure
{
    public class BackServiceProxy : IDisposable
    {
        const string PASSWORD_KEY = "Password";
        ChannelFactory<IDoSomethingAuthenticated> _channelFactory;
        public BackServiceProxy()
        {
            _channelFactory = new ChannelFactory<IDoSomethingAuthenticated>("ClientEP");
        }
        internal bool ForwardLoginRequestToService(Login login)
        {
            try
            {
                IDoSomethingAuthenticated channel = CreateChannel(login.Username, login.Password);
                channel.Login();
                HttpContext.Current.Session[PASSWORD_KEY] = login.Password;
                return true;
            }
            catch (MessageSecurityException messageSecurityException)
            {
                return false;
            }
        }

        private IDoSomethingAuthenticated CreateChannel(string userName, string password)
        {
            _channelFactory.Credentials.UserName.UserName = userName;
            _channelFactory.Credentials.UserName.Password = password;
            IDoSomethingAuthenticated channel = _channelFactory.CreateChannel();
            return channel;
        }

        public OperationResult<SomeResult> DoSomething(SomeParameters parmaters)
        {
            string password = (string)HttpContext.Current.Session[PASSWORD_KEY];
            IDoSomethingAuthenticated channel = CreateChannel(HttpContext.Current.User.Identity.Name, password);
            return channel.DoSomethingAuthenticated(parmaters);


        }

        public void Dispose()
        {
            ((IDisposable)_channelFactory).Dispose();
        }

        //public OperationStartInformation DoSomethingAsync(SomeParameters parmaters)
        //{ }

        //public OperationResult<SomeResult> DoSomethingGetResult(Guid guid)
        //{ }

    }
}