using MQTTnet.Client;

namespace LogReaderLibrary.MQTT.Message;

public interface IMessageReceiver {
    Task OnMessage(MqttApplicationMessageReceivedEventArgs arg);
}