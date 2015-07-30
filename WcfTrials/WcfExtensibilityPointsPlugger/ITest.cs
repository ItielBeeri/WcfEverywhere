using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensibilityPointsPlugger
{
    [ServiceContract]
    public interface ITest
    {
        [OperationContract]
        int Add(int x, int y);
        [OperationContract(IsOneWay = true)]
        void Process(string text);
    }
}
