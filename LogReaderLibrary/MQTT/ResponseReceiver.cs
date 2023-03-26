
using System.Text;
using LogReaderLibrary.MQTT.Message;
using MQTTnet.Client;

namespace LogReaderLibrary.MQTT.Request;
public class ResponseReceiver : IMessageReceiver
{
    private string correlation;
    private string topic;
    public TaskCompletionSource<byte[]> response = new TaskCompletionSource<byte[]>();

    public ResponseReceiver(string topic, string correlation) {
        this.correlation = correlation;
        this.topic = topic;
    }

    public Task OnMessage(MqttApplicationMessageReceivedEventArgs arg)
    {
        string responseCorrelation = Encoding.UTF8.GetString(arg.ApplicationMessage.CorrelationData);

        if (arg.ApplicationMessage.Topic.Equals(this.topic) && responseCorrelation.Equals(this.correlation))
        {
            response.SetResult(arg.ApplicationMessage.Payload);
        }

        return Task.CompletedTask;
    }
}
