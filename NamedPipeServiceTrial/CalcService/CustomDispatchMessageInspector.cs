using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using CommonContracts;
using System.ServiceModel;

namespace CalcService
{
    class CustomDispatchMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            //// Create a copy of the original message so that we can mess with it.
            //MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            //request = buffer.CreateMessage();
            //Message messageCopy = buffer.CreateMessage();

            //// Read the custom context data from the headers
            //ServiceHeader customData = CustomHeader.ReadHeader(request);

            //// Add an extension to the current operation context so
            //// that our custom context can be easily accessed anywhere.
            //ServerContext customContext = new ServerContext();

            //if (customData != null)
            //{
            //    customContext.KerberosID = customData.KerberosID;
            //    customContext.SiteminderToken = customData.SiteminderToken;
            //}
            //OperationContext.Current.IncomingMessageProperties.Add(
            //         "CurrentContext", customContext);
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
        }
    }
}
