using System.Runtime.Serialization;

namespace Ziv.ServiceModel.Operations
{
    /// <summary>
    /// Represents the sate of an operation running at a remote server
    /// </summary>
    [DataContract]
    public enum OperationState
    {
        [EnumMember]
        Started,
        [EnumMember]
        CompletedSucessfully,
        [EnumMember]
        Failed,
        /// <summary>
        /// Operation has been signaled to cancel but has not yest halted.
        /// </summary>
        [EnumMember]
        CancelationPending,
        [EnumMember]
        Canceled,
        [EnumMember]
        NoFound
    }
}
