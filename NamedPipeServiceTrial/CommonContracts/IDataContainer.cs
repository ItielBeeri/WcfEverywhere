using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace CommonContracts
{
    public interface IDataContainer
    {
        object Data { get; set; }
    }

    public class DataContainer : IDataContainer
    {
        public object Data { get; set; }
    }

    //[DataContract]
    //public class ServiceHeader
    //{
    //    [DataMember]
    //    public string EmployeeID { get; set; }

    //    [DataMember]
    //    public string WindowsLogonID { get; set; }

    //    [DataMember]
    //    public string KerberosID { get; set; }

    //    [DataMember]
    //    public string SiteminderToken { get; set; }
    //}

    //public class CustomHeader : MessageHeader
    //{
    //    private const string CUSTOM_HEADER_NAME = "HeaderName";
    //    private const string CUSTOM_HEADER_NAMESPACE = "YourNameSpace";

    //    private ServiceHeader _customData;

    //    public ServiceHeader CustomData
    //    {
    //        get
    //        {
    //            return _customData;
    //        }
    //    }

    //    public CustomHeader()
    //    {
    //    }

    //    public CustomHeader(ServiceHeader customData)
    //    {
    //        _customData = customData;
    //    }

    //    public override string Name
    //    {
    //        get { return (CUSTOM_HEADER_NAME); }
    //    }

    //    public override string Namespace
    //    {
    //        get { return (CUSTOM_HEADER_NAMESPACE); }
    //    }

    //    protected override void OnWriteHeaderContents(
    //        System.Xml.XmlDictionaryWriter writer, MessageVersion messageVersion)
    //    {
    //        XmlSerializer serializer = new XmlSerializer(typeof(ServiceHeader));
    //        StringWriter textWriter = new StringWriter();
    //        serializer.Serialize(textWriter, _customData);
    //        textWriter.Close();

    //        string text = textWriter.ToString();

    //        writer.WriteElementString(CUSTOM_HEADER_NAME, "Key", text.Trim());
    //    }

    //    public static ServiceHeader ReadHeader(Message request)
    //    {
    //        Int32 headerPosition = request.Headers.FindHeader(CUSTOM_HEADER_NAME, CUSTOM_HEADER_NAMESPACE);
    //        if (headerPosition == -1)
    //            return null;

    //        MessageHeaderInfo headerInfo = request.Headers[headerPosition];

    //        XmlNode[] content = request.Headers.GetHeader<XmlNode[]>(headerPosition);

    //        string text = content[0].InnerText;

    //        XmlSerializer deserializer = new XmlSerializer(typeof(ServiceHeader));
    //        TextReader textReader = new StringReader(text);
    //        ServiceHeader customData = (ServiceHeader)deserializer.Deserialize(textReader);
    //        textReader.Close();

    //        return customData;
    //    }
    //}
}
