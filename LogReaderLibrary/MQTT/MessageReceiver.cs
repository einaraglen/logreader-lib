using System.Text;
using System.Text.RegularExpressions;
using LogReaderLibrary.MQTT.Handler;
using MQTTnet.Client;

namespace LogReaderLibrary.MQTT.Message;

public class MessageReceiver
{
    private string regex;
    private Dictionary<string, IHandler> handlers;
    public MessageReceiver(string regex)
    {
        if (!regex.Contains("endpoint") || !regex.Contains("id"))
        {
            throw new InvalidDataException("MessageReceiver regex needs to contain 'endpoint' and 'id' groups");
        }

        this.regex = regex;
        this.handlers = new Dictionary<string, IHandler>();
    }

    public MessageReceiver WithHandler(string endpoint, IHandler handler)
    {
        this.handlers.Add(endpoint, handler);
        return this;
    }

    public Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs arg)
    {
        Console.WriteLine("Received Message!");

        byte[] bytes = arg.ApplicationMessage.Payload;
        string? correlation = GetCorrelation(arg);
        Match topic = DeconstructTopic(arg.ApplicationMessage.Topic);

        if (topic.Success == false)
        {
            throw new InvalidDataException("Received topic does not match given topic regex");
        }

        string endpoint = topic.Groups["endpoint"].Value;

        if (this.handlers[endpoint] == null)
        {
            throw new InvalidDataException($"Missing handler for topic '{endpoint}'");
        }

        string id = topic.Groups["id"].Value;

        this.handlers[endpoint].OnMessage(id, bytes, correlation);

        return Task.CompletedTask;
    }


    private string? GetCorrelation(MqttApplicationMessageReceivedEventArgs arg)
    {
        byte[] bytes = arg.ApplicationMessage.CorrelationData ?? new byte[0];

        return bytes.Length == 0 ? null : Encoding.UTF8.GetString(bytes);
    }

    private Match DeconstructTopic(string topic)
    {
        return Regex.Match(topic, this.regex);
    }
}