using System.Runtime.Serialization;

namespace Ziv.ServiceModel.Operations
{
    [DataContract]
    public class OperationResult<TResult> 
    {
        public OperationResult(TResult result, OperationStatusInformation info)
        {
            Result = result;
            Info = info;
        }

        [DataMember]
        public OperationStatusInformation Info { get; private set; }

        [DataMember]
        public TResult Result { get; private set; }
    }

    public class OperationResult
    {
        public OperationStatusInformation Info { get; set; }

        public object Result { get; set; }
    }
}