using MQTTnet.Client;

namespace LogReaderLibrary.MQTT.Message;
public class MessageAggregator
{
    private readonly List<IMessageReceiver> handlers = new List<IMessageReceiver>();

    public void Subscribe(IMessageReceiver handler)
    {
        lock (this.handlers)
        {
            this.handlers.Add(handler);
        }
    }

    public void Unsubscribe(IMessageReceiver handler)
    {
        lock (this.handlers)
        {
            this.handlers.Remove(handler);
        }
    }

    public Task PublishAsync(MqttApplicationMessageReceivedEventArgs message)
    {
        var tasks = new List<Task>();

        // Important that we spinn up a thread for each handler so they can only block them-selves
        foreach (var handler in this.handlers)
        {
            tasks.Add(Task.Run(() => handler.OnMessage(message)));
        }

        return Task.CompletedTask;
    }
}