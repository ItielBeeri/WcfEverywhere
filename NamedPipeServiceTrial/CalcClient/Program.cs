using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CommonContracts;

namespace CalcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //user 1:
            ChannelFactory<ICalc> factory1 = new ChannelFactory<ICalc>("calcEP");
            Console.Write("Your username: ");
            string username1 = Console.ReadLine();
            factory1.Endpoint.Behaviors.Add(new CustomEndpointBehavior(new UserContext() { Username = username1 }));
            var calc1 = factory1.CreateChannel();
            
            //user 2:
            ChannelFactory<ICalc> factory2 = new ChannelFactory<ICalc>("calcEP");
            Console.Write("Your username: ");
            string username2 = Console.ReadLine();
            factory2.Endpoint.Behaviors.Add(new CustomEndpointBehavior(new UserContext() { Username = username2 }));
            var calc2 = factory2.CreateChannel();


            bool isUsingChannel1 = true;
            while (true)
            {
                Console.Write("Num 1: ");
                double num1 = double.Parse(Console.ReadLine());
                Console.Write("Num 2: ");
                double num2 = double.Parse(Console.ReadLine());
                double res = (isUsingChannel1 ? calc1 : calc2).Add(num1, num2);
                Console.WriteLine(res);                
            } 
            Console.Read();
        }
    }
}
