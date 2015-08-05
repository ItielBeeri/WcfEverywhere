using System.Runtime.Serialization;

namespace Ziv.ServiceModel.Operations
{
    [DataContract]
    public class OperationStatusInformation
    {
        public OperationStatusInformation(OperationState state,
            int progress = 0,
            string failureMessage = null)
        {
            State = state;
            Progress = progress;
            FailureMessage = failureMessage;
        }
        [DataMember]
        public OperationState State { get; internal set; }
        [DataMember]
        public int Progress { get; internal set; }
        [DataMember]
        public string FailureMessage { get; internal set; }
    }
}
