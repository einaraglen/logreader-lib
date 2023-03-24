using System.Text;
using LogReaderLibrary.MQTT.Message;
using MQTTnet.Client;

namespace LogReaderLibrary.MQTT.Request;

public class RequestBuilder
{
    private string? correlation;
    private string? topic;
    private TimeSpan timeout = TimeSpan.FromMinutes(1);
    private TaskCompletionSource<byte[]> response = new TaskCompletionSource<byte[]>();

    public RequestBuilder WithTopic(String topic)
    {
        this.topic = topic;
        return this;
    }

    public RequestBuilder WithCorrelation(string correlation)
    {
        this.correlation = correlation;
        return this;
    }

    public RequestBuilder WithTimeout(TimeSpan timeout)
    {
        this.timeout = timeout;
        return this;
    }

    private Task OnResponse(MqttApplicationMessageReceivedEventArgs args)
    {
        string responseCorrelation = Encoding.UTF8.GetString(args.ApplicationMessage.CorrelationData);

        if (args.ApplicationMessage.Topic == this.GetResponseTopic() && responseCorrelation.Equals(this.correlation))
        {
            response.SetResult(args.ApplicationMessage.Payload);
        }

        return Task.CompletedTask;
    }

    private string GetResponseTopic()
    {
        return this.topic!.Replace("Request", "Response");
    }

    public async Task<byte[]> Publish(byte[] payload)
    {

        await new MessageBuilder()
                .WithTopic(this.topic!)
                .WithPayload(payload)
                .WithCorrelation(correlation!)
                .Publish();

        MQTTClientSingleton.Instance.Client.ApplicationMessageReceivedAsync += OnResponse;

        Task? waiting = Task.Delay(this.timeout);
        Task? complete = await Task.WhenAny(this.response.Task, waiting);

        MQTTClientSingleton.Instance.Client.ApplicationMessageReceivedAsync += OnResponse;

        if (complete != this.response.Task)
        {
            throw new TimeoutException($"Response for {this.topic} took over 1 min");
        }

        return await response.Task;
    }
}