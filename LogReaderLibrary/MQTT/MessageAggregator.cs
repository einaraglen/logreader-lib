using System.Text.RegularExpressions;
using MQTTnet.Client;

namespace SeaBrief.MQTT.Message;
public class MessageAggregator
{
    private readonly Dictionary<string, List<IMessageReceiver>> handlers = new Dictionary<string, List<IMessageReceiver>>();

    public void Subscribe(string topic, IMessageReceiver handler)
    {
        lock (this.handlers)
            if (this.handlers.ContainsKey(topic))
            {
                this.handlers[topic].Add(handler);
            }
            else
            {
                this.handlers.Add(topic, new List<IMessageReceiver> { handler });
            }
    }

    public void Unsubscribe(string topic, IMessageReceiver handler)
    {
        lock (this.handlers)
        {
            if (this.handlers.ContainsKey(topic))
            {
                this.handlers[topic].Remove(handler);
            }
        }
    }

    public Task PublishAsync(MqttApplicationMessageReceivedEventArgs message)
    {
        var tasks = new List<Task>();

        // Important that we spinn up a thread for each handler so they can only block them-selves
        foreach (var handler in this.GetAllMatches(message.ApplicationMessage.Topic))
        {
            tasks.Add(Task.Run(() => handler.OnMessage(message)));
        }

        return Task.CompletedTask;
    }

    private List<IMessageReceiver> GetAllMatches(string topic)
    {
        List<IMessageReceiver> matches = new List<IMessageReceiver>();

        foreach (string key in this.handlers.Keys)
        {
            if (Regex.IsMatch(key, @"^\^?(?=.*\$).*$"))
            {
                Regex regex = new Regex(key);
                if (regex.IsMatch(topic))
                {
                    // if matching then Add to matches
                    matches.AddRange(this.handlers[key]);
                }
            }
            else if (key == topic)
            {
                matches.AddRange(this.handlers[key]);
            }
        }

        return matches;
    }
}