using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CommonContracts
{
    [ServiceContract]
    public interface ICalc
    {
        [OperationContract(IsOneWay=false)]
        double Add(double a, double b);

        [OperationContract]
        IDataContainer GetData(IDataContainer data);
    }
}
