using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.IO;
using System.Xml;
using System.ServiceModel.Channels;
using CommonContracts;

namespace CalcClient
{
    class CustomClientMessageInspector : IClientMessageInspector
    {
        private UserContext _user;
        public CustomClientMessageInspector(UserContext user)
        {
            _user = user;
        }
        
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            request.Properties.Add("UserName", _user.Username);

            Console.WriteLine("Message:");
            Console.WriteLine(request);
            Console.WriteLine();
            MemoryStream ms = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(ms);
            request.WriteMessage(writer); // the message was consumed here
            writer.Flush();
            ms.Position = 0;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ms);
            this.ChangeMessage(xmlDoc);

            //Now recreating the message
            ms = new MemoryStream();
            xmlDoc.Save(ms);
            ms.Position = 0;
            XmlReader reader = XmlReader.Create(ms);
            Message newMessage = Message.CreateMessage(reader, int.MaxValue, request.Version);
            newMessage.Properties.CopyProperties(request.Properties);
            //newMessage.Properties.Add("UserName", _user.Username);
            request = newMessage;

            return null;

            //// Prepare the request message copy to be modified
            //MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            //request = buffer.CreateMessage();

            //ServiceHeader customData = new ServiceHeader();

            //customData.KerberosID = "KerberosID";
            //customData.SiteminderToken = "SiteminderToken";

            //CustomHeader header = new CustomHeader(customData);

            //// Add the custom header to the request.
            //request.Headers.Add(header);

            //return null;


     
            
        }

        private void ChangeMessage(XmlDocument xmlDoc)
        {
            
        }
    }
}
