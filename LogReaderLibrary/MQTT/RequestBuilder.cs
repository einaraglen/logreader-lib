using SeaBrief.MQTT.Message;
namespace SeaBrief.MQTT.Request;


public class RequestBuilder
{
    private string correlation = Guid.NewGuid().ToString();
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

    public async Task<byte[]> Publish(byte[] payload)
    {
        var receiver = new ResponseReceiver(MQTTUtils.GetResponseTopic(this.topic!), this.correlation!);

        MQTTClientSingleton.Instance.AddMessageReceiver(receiver);

        try
        {
            await new MessageBuilder()
                            .WithTopic(this.topic!)
                            .WithPayload(payload)
                            .WithCorrelation(correlation)
                            .Publish()
                            .ConfigureAwait(false);

            Task? waiting = Task.Delay(this.timeout);
            Task? complete = await Task.WhenAny(receiver.response.Task, waiting).ConfigureAwait(false);



            if (complete != receiver.response.Task)
            {
                throw new TimeoutException($"Response for {this.topic} took over {this.timeout.Seconds} secounds");
            }

            return await receiver.response.Task.ConfigureAwait(false);
        }
        finally
        {
            MQTTClientSingleton.Instance.RemoveMessageReceiver(receiver);
        }
    }
}