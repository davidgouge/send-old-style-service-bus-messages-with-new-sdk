# How to send Old Style (WindowsAzure.ServiceBus) (Binary XML Encoded) messages to ServiceBus using the new SDK (Microsoft.Azure.ServiceBus)


## Old Style
When a message was sent like this using the old `WindowsAzure.ServiceBus` SDK, the message body was Binary XML Encoded.

```
var brokeredMessage = new BrokeredMessage("Hello Dave (Old Style)");
var queueClient = QueueClient.CreateFromConnectionString("connectionstring", "queuename");
queueClient.Send(brokeredMessage);
```

New SDK `Microsoft.Azure.ServiceBus` does not automatically encode in this way (the API accepts a byte[] as the content of a message), so if you need to send a message that the old SDK will understand, you need to manually encode the message like this:

```
var queueclient = new QueueClient("connectionstring", "queuename");

var serializer = new DataContractSerializer(typeof(string));
var memoryStream = new MemoryStream();
var binaryDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(memoryStream);
serializer.WriteObject(binaryDictionaryWriter, "Hello Dave (New Style)");
binaryDictionaryWriter.Flush();

var message = new Message(memoryStream.ToArray());

queueclient.SendAsync(message);
```