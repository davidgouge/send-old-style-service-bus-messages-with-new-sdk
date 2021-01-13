using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus.InteropExtensions;

namespace NewStyle
{
    class Program
    {
        static void Main(string[] args)
        {
            var queueclient = new QueueClient("Endpoint=sb://dgsbtest.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=vl3JincBHyPe17a+ASP/4v8wNT+zt6U+XMcCwRZmVjE=", "messagequeuetest");

            var serializer = new DataContractSerializer(typeof(string));
            Message message;
            using (var memoryStream = new MemoryStream())
            {
                var binaryDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(memoryStream);
                serializer.WriteObject(binaryDictionaryWriter, "Hello Dave (New Style)");
                binaryDictionaryWriter.Flush();
            
                message = new Message(memoryStream.ToArray());
            }

            queueclient.SendAsync(message).Wait();
        }
    }
}