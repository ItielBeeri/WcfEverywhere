using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonContracts;
using System.ServiceModel;

namespace CalcService
{
    [ServiceBehavior(InstanceContextMode= InstanceContextMode.PerCall)]
    [InstanceProviderBehavior]
    public class CalculatorService : ICalc
    {
        string _userName;
        static int instances = 0;

        //public CalculatorService()
        //{

        //}

        public CalculatorService(string userName)
        {
            _userName = userName;
            instances++;
            Console.WriteLine("CalculatorService created. Username: "+ userName);
        }

        public double Add(double a, double b)
        {
            Console.WriteLine("{0} sent numbers {1}, {2}", _userName, a,b);
            return a + b;
        }



        public IDataContainer GetData(IDataContainer data)
        {
            return new DataContainer() { Data = data + "ServerData" };
        }
    }
}
