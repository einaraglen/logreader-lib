using System.Text;
using MQTTnet;

namespace LogReaderLibrary.MQTT.Message;

public class MessageBuilder
{
    private MqttApplicationMessageBuilder message = new MqttApplicationMessageBuilder();

    public MessageBuilder WithTopic(string topic)
    {
        this.message.WithTopic(topic);
        return this;
    }

    public MessageBuilder WithCorrelation(string correlation)
    {
        this.message.WithCorrelationData(Encoding.UTF8.GetBytes(correlation));
        return this;
    }

    public MessageBuilder WithPayload(byte[] bytes)
    {
        this.message.WithPayload(bytes);
        return this;
    }

    public async Task Publish()
    {
        await MQTTClientSingleton.Instance.Client.InternalClient.PublishAsync(this.message.Build());
    }
}