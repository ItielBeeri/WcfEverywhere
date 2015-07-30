using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonContracts;
using CalcService;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(CalculatorService));
            foreach (ChannelDispatcher channelDispatcher in host.ChannelDispatchers)
            {
                foreach (var endPoint in channelDispatcher.Endpoints)
                {
                    endPoint.DispatchRuntime.InstanceProvider = new CustomInstanceProvider();
                }
            }
            host.Open();
            try
            {
                Console.WriteLine("Server running...");
                Console.Read();
            }
            finally
            {
                host.Close();
            }
        }
    }
}
