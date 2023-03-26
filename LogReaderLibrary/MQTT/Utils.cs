using System.Text;
using System.Text.RegularExpressions;
using MQTTnet.Client;

namespace LogReaderLibrary.MQTT;

public static class MQTTUtils
{
    public static Match DeconstructTopic(string regex, string topic)
    {
        return Regex.Match(topic, regex);
    }

    public static byte[] GetPayload(MqttApplicationMessageReceivedEventArgs arg) {
        return arg.ApplicationMessage.Payload;
    }

    public static string GetTopicValue(string key, Match deconstructed) {
        return deconstructed.Groups[key].Value;
    }

    public static string GetCorrelation(MqttApplicationMessageReceivedEventArgs arg) {
        byte[]? bytes = arg.ApplicationMessage.CorrelationData;
        return bytes == null ? "default" : Encoding.UTF8.GetString(bytes);
    }
}