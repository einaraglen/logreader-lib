using SeaBrief.MQTT.Message;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace SeaBrief.MQTT;

public class MQTTClientSingleton
{
    private static readonly Lazy<MQTTClientSingleton> lazy =
        new Lazy<MQTTClientSingleton>(() => new MQTTClientSingleton());

    private readonly MessageAggregator aggregator = new MessageAggregator();
    private IManagedMqttClient? client;
    private MQTTClientSingleton() { }

    public void Connect(string id, string address, string port, string client_pfx)
    {
        var clientCert = new X509Certificate2(client_pfx);
        MqttClientOptionsBuilder builder =
            new MqttClientOptionsBuilder()
                .WithClientId(id)
                .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500)
                .WithTcpServer(address, Convert.ToInt32(port))
                 .WithTls(new MqttClientOptionsBuilderTlsParameters()
                 {
                     UseTls = true,
                     SslProtocol = System.Security.Authentication.SslProtocols.Tls12,
                     CertificateValidationHandler = (o) =>
                     {
                         return true;
                     },
                     Certificates = new[]{
                     clientCert,
                 }
                 });

        ManagedMqttClientOptions options =
            new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(60))
                .WithClientOptions(builder.Build())
                .Build();

        this.client = new MqttFactory().CreateManagedMqttClient();

        this.LoadHandlers();
        this.client.StartAsync(options).Wait();
    }

    public MQTTClientSingleton AddTopic(string topic)
    {
        this.client.SubscribeAsync(topic).Wait();
        return this;
    }

    public MQTTClientSingleton AddMessageReceiver(string topic, IMessageReceiver receiver)
    {
        this.aggregator.Subscribe(topic, receiver);
        return this;
    }

    public MQTTClientSingleton RemoveMessageReceiver(string topic, IMessageReceiver receiver)
    {
        this.aggregator.Unsubscribe(topic, receiver);
        return this;
    }

    public void Disconnect()
    {
        this.client!.StopAsync().Wait();
    }

    public static MQTTClientSingleton Instance { get { return lazy.Value; } }

    public IManagedMqttClient Client { get { return this.client!; } }

    private void LoadHandlers()
    {
        this.client!.ConnectedAsync += this.OnConnected;
        this.client!.DisconnectedAsync += this.OnDisconected;
        this.client!.ConnectingFailedAsync += this.OnFailure;
        this.client!.ApplicationMessageReceivedAsync += this.OnMessage;
    }

    private async Task OnMessage(MqttApplicationMessageReceivedEventArgs e)
    {
        await this.aggregator.PublishAsync(e);
    }

    private Task OnConnected(MqttClientConnectedEventArgs arg)
    {
        Console.WriteLine($"MQTT Connected");
        return Task.CompletedTask;
    }

    private Task OnDisconected(MqttClientDisconnectedEventArgs arg)
    {
        Console.WriteLine($"MQTT Disconnected \n{arg.Exception}");
        return Task.CompletedTask;
    }

    private Task OnFailure(ConnectingFailedEventArgs arg)
    {
        Console.WriteLine($"MQTT Connection failed check network or broker! \n{arg.Exception}");
        return Task.CompletedTask;
    }
}