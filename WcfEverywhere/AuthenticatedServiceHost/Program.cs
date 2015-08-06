using SampleServiceImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziv.ServiceModel;

namespace AuthenticatedServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ZivServiceHost host=new ZivServiceHost(typeof(DoSomethingAuthenticatedService)))
            {
                host.Open();
                Console.WriteLine("Server listening...");
                Console.ReadLine();
            }
        }
    }
}
