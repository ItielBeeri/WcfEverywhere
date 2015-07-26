using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ServiceUtilities
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