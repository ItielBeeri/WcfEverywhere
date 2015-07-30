using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensibilityPointsPlugger
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://" + Environment.MachineName + ":8000/Service";
            using (ServiceHost host = new ServiceHost(typeof(Service), new Uri(baseAddress)))
            {
                ServiceEndpoint endpoint = host.AddServiceEndpoint(typeof(ITest), new BasicHttpBinding(), "");
                endpoint.Contract.Behaviors.Add(new MyBehavior());
                foreach (OperationDescription operation in endpoint.Contract.Operations)
                {
                    operation.Behaviors.Add(new MyBehavior());
                }

                host.Open();
                ColorConsole.WriteLine("Host opened");

                using (ChannelFactory<ITest> factory = new ChannelFactory<ITest>(new BasicHttpBinding(), new EndpointAddress(baseAddress)))
                {
                    factory.Endpoint.Contract.Behaviors.Add(new MyBehavior());
                    foreach (OperationDescription operation in factory.Endpoint.Contract.Operations)
                    {
                        operation.Behaviors.Add(new MyBehavior());
                    }

                    ITest proxy = factory.CreateChannel();
                    ColorConsole.WriteLine("Calling operation");
                    ColorConsole.WriteLine(proxy.Add(3, 4));

                    ColorConsole.WriteLine("Called operation, calling it again, this time it the service will throw");
                    try
                    {
                        ColorConsole.WriteLine(proxy.Add(0, 0));
                    }
                    catch (Exception e)
                    {
                        ColorConsole.WriteLine(ConsoleColor.Red, "{0}: {1}", e.GetType().Name, e.Message);
                    }

                    ColorConsole.WriteLine("Now calling an OneWay operation");
                    proxy.Process("hello");

                    ((IClientChannel)proxy).Close();
                }
            }

            ColorConsole.WriteLine("Done. Any key to close window.");
            Console.ReadKey();
        }
    }
}
