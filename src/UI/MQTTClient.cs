using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Publishing;
using Newtonsoft.Json;

namespace AITool
{
    public class MQTTClient
    {
    
        public MQTTClient()
        {

        }

        public async Task<MqttClientPublishResult> PublishAsync(string topic, string payload)
        {
            MqttClientPublishResult res = null;
            Stopwatch sw = Stopwatch.StartNew();

            await Task.Run(async () =>
                            {
                                try
                                {
                                    MqttFactory factory = new MqttFactory();

                                    IMqttClient mqttClient = factory.CreateMqttClient();

                                    Uri uri = new Uri(AppSettings.Settings.mqtt_serverandport);

                                    IMqttClientOptions options;

                                    //Seems like there should be a better way here...

                                    if (AppSettings.Settings.mqtt_UseTLS || uri.Port == 8883)
                                    {
                                        if (!string.IsNullOrWhiteSpace(AppSettings.Settings.mqtt_username))
                                        {
                                            options = new MqttClientOptionsBuilder()
                                                .WithClientId(AppSettings.Settings.mqtt_clientid)
                                                .WithTcpServer(uri.Host, uri.Port)
                                                .WithCredentials(AppSettings.Settings.mqtt_username, AppSettings.Settings.mqtt_password)
                                                .WithTls()
                                                .WithCleanSession()
                                                .Build();

                                        }
                                        else
                                        {
                                            options = new MqttClientOptionsBuilder()
                                                .WithClientId(AppSettings.Settings.mqtt_clientid)
                                                .WithTcpServer(uri.Host, uri.Port)
                                                .WithTls()
                                                .WithCleanSession()
                                                .Build();

                                        }

                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(AppSettings.Settings.mqtt_username))
                                        {
                                            options = new MqttClientOptionsBuilder()
                                            .WithClientId(AppSettings.Settings.mqtt_clientid)
                                            .WithTcpServer(uri.Host, uri.Port)
                                            .WithCredentials(AppSettings.Settings.mqtt_username, AppSettings.Settings.mqtt_password)
                                            .WithCleanSession()
                                            .Build();

                                        }
                                        else
                                        {
                                            options = new MqttClientOptionsBuilder()
                                            .WithClientId(AppSettings.Settings.mqtt_clientid)
                                            .WithTcpServer(uri.Host, uri.Port)
                                            .WithCleanSession()
                                            .Build();
                                        }


                                    }

                                    Global.Log($"MQTT:  Sending topic '{topic}' with payload '{payload}' to server '{AppSettings.Settings.mqtt_serverandport}'...");

                                    MqttClientAuthenticateResult cres = await mqttClient.ConnectAsync(options, CancellationToken.None);

                                    if (cres.ResultCode == MqttClientConnectResultCode.Success)
                                    {
                                        MqttApplicationMessage ma = new MqttApplicationMessageBuilder()
                                                                .WithTopic(topic)
                                                                .WithPayload(payload)
                                                                .Build();

                                        res = await mqttClient.PublishAsync(ma, CancellationToken.None);

                                        //Success = 0,
                                        //        NoMatchingSubscribers = 0x10,
                                        //        UnspecifiedError = 0x80,
                                        //        ImplementationSpecificError = 0x83,
                                        //        NotAuthorized = 0x87,
                                        //        TopicNameInvalid = 0x90,
                                        //        PacketIdentifierInUse = 0x91,
                                        //        QuotaExceeded = 0x97,
                                        //        PayloadFormatInvalid = 0x99

                                        if (res.ReasonCode == MqttClientPublishReasonCode.Success)
                                        {
                                            Global.Log($"MQTT:  ...Sent in {sw.ElapsedMilliseconds}ms, Reason: '{res.ReasonString}' ({Convert.ToInt32(res.ReasonCode)})");
                                        }
                                        else
                                        {
                                            Global.Log($"MQTT:  Error sending: ({sw.ElapsedMilliseconds}ms) Reason: '{res.ReasonString}' ({Convert.ToInt32(res.ReasonCode)})");
                                        }

                                    }
                                    else
                                    {
                                        Global.Log($"MQTT:  Error connecting: ({sw.ElapsedMilliseconds}ms) Reason: '{cres.ReasonString}'");

                                    }


                                }
                                catch (Exception ex)
                                {

                                    Global.Log($"MQTT: Unexpected Error: Topic {topic} Payload {payload}: " + Global.ExMsg(ex));
                                }

                            });


            return res;
            

        }

    }
}
