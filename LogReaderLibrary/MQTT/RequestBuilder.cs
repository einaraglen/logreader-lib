using LogReaderLibrary.MQTT.Message;
namespace LogReaderLibrary.MQTT.Request;


public class RequestBuilder
{
    private string? correlation;
    private string? topic;
    private TimeSpan timeout = TimeSpan.FromMinutes(1);
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


    private string GetResponseTopic()
    {
        return this.topic!.Replace("Request", "Response");
    }

    public async Task<byte[]> Publish(byte[] payload)
    {
        var receiver = new ResponseReceiver(this.GetResponseTopic(), this.correlation!);

        MQTTClientSingleton.Instance.AddMessageReceiver(receiver);

        try
        {
            await new MessageBuilder()
                            .WithTopic(this.topic!)
                            .WithPayload(payload)
                            .WithCorrelation(correlation!)
                            .Publish()
                            .ConfigureAwait(false);

            Task? waiting = Task.Delay(this.timeout);
            Task? complete = await Task.WhenAny(receiver.response.Task, waiting).ConfigureAwait(false);



            if (complete != receiver.response.Task)
            {
                throw new TimeoutException($"Response for {this.topic} took over 1 min");
            }

            return await receiver.response.Task.ConfigureAwait(false);
        }
        finally
        {
            MQTTClientSingleton.Instance.RemoveMessageReceiver(receiver);
        }
    }
}