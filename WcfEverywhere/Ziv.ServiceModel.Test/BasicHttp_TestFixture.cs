using System;
using System.ServiceModel;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleServiceContract;
using SampleServiceImplementation;

namespace Ziv.ServiceModel.Test
{
    [TestClass]
    public class BasicHttp_TestFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            var baseAddresses = new Uri("http://localhost:8180/");
            using (var serviceHost = new ServiceHost(typeof(DoSomethingService), baseAddresses))
            {
                serviceHost.Open();
                using (var client = new DoSomethingClient())
                {
                    IDoSomethingService channel = client.ChannelFactory.CreateChannel();
                    channel.DoSomething(new SomeParameters() { Parameter = 37 });
                }
            }
        }
    }

    class DoSomethingClient : ClientBase<IDoSomethingService>
    {

    }
}
