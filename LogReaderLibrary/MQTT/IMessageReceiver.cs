using MQTTnet.Client;

namespace SeaBrief.MQTT.Message;

public interface IMessageReceiver {
    Task OnMessage(MqttApplicationMessageReceivedEventArgs arg);
}