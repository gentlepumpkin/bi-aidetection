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
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace AITool
{
    public class MQTTClient
    {
    
        public MQTTClient()
        {

        }

        public async Task<MqttClientPublishResult> PublishAsync(string topic, string payload, bool retain)
        {
            MqttClientPublishResult res = null;
            Stopwatch sw = Stopwatch.StartNew();

            await Task.Run(async () =>
                            {
                                MqttFactory factory = new MqttFactory();

                                IMqttClient mqttClient = null;
                                bool subscribed = false;

                                using (mqttClient = factory.CreateMqttClient())
                                {
                                    try
                                    {




                                        string server = Global.GetWordBetween(AppSettings.Settings.mqtt_serverandport, "", ":");
                                        string port = Global.GetWordBetween(AppSettings.Settings.mqtt_serverandport, ":", "/");
                                        int portint = 0;
                                        if (!string.IsNullOrEmpty(port))
                                            portint = Convert.ToInt32(port);
                                        if (portint == 0 && AppSettings.Settings.mqtt_UseTLS)
                                        {
                                            portint = 8883;
                                        }
                                        else if (portint == 0)
                                        {
                                            portint = 1883;
                                        }

                                        bool IsWebSocket = (AppSettings.Settings.mqtt_serverandport.ToLower().Contains("ws://") ||
                                                            AppSettings.Settings.mqtt_serverandport.ToLower().Contains("/mqtt") ||
                                                            AppSettings.Settings.mqtt_serverandport.ToLower().Contains("wss://"));

                                        bool UseTLS = (AppSettings.Settings.mqtt_UseTLS || portint == 8883 || AppSettings.Settings.mqtt_serverandport.ToLower().Contains("wss://"));

                                        bool UseCreds = (!string.IsNullOrWhiteSpace(AppSettings.Settings.mqtt_username));

                                        IMqttClientOptions options;


                                        //=====================================================================
                                        //Seems like there should be a better way here with this Options builder...
                                        //I dont see an obvious way to directly modify options without the builder
                                        //and I cant seem to put IF statements around each part of the option builder
                                        //parameters.
                                        //=====================================================================



                                        if (UseTLS)
                                        {
                                            if (UseCreds)
                                            {
                                                if (IsWebSocket)
                                                {

                                                    options = new MqttClientOptionsBuilder()
                                                        .WithClientId(AppSettings.Settings.mqtt_clientid)
                                                        .WithWebSocketServer(AppSettings.Settings.mqtt_serverandport)
                                                        .WithCredentials(AppSettings.Settings.mqtt_username, AppSettings.Settings.mqtt_password)
                                                        .WithTls()
                                                        .WithCleanSession()
                                                        .Build();

                                                }
                                                else
                                                {
                                                    options = new MqttClientOptionsBuilder()
                                                        .WithClientId(AppSettings.Settings.mqtt_clientid)
                                                        .WithTcpServer(server, portint)
                                                        .WithCredentials(AppSettings.Settings.mqtt_username, AppSettings.Settings.mqtt_password)
                                                        .WithTls()
                                                        .WithCleanSession()
                                                        .Build();
                                                }

                                            }
                                            else
                                            {
                                                if (IsWebSocket)
                                                {
                                                    options = new MqttClientOptionsBuilder()
                                                        .WithClientId(AppSettings.Settings.mqtt_clientid)
                                                        .WithWebSocketServer(AppSettings.Settings.mqtt_serverandport)
                                                        .WithTls()
                                                        .WithCleanSession()
                                                        .Build();

                                                }
                                                else
                                                {
                                                    options = new MqttClientOptionsBuilder()
                                                        .WithClientId(AppSettings.Settings.mqtt_clientid)
                                                        .WithTcpServer(server, portint)
                                                        .WithTls()
                                                        .WithCleanSession()
                                                        .Build();

                                                }

                                            }

                                        }
                                        else
                                        {
                                            if (UseCreds)
                                            {
                                                if (IsWebSocket)
                                                {
                                                    options = new MqttClientOptionsBuilder()
                                                    .WithClientId(AppSettings.Settings.mqtt_clientid)
                                                    .WithWebSocketServer(AppSettings.Settings.mqtt_serverandport)
                                                    .WithCredentials(AppSettings.Settings.mqtt_username, AppSettings.Settings.mqtt_password)
                                                    .WithCleanSession()
                                                    .Build();

                                                }
                                                else
                                                {
                                                    options = new MqttClientOptionsBuilder()
                                                    .WithClientId(AppSettings.Settings.mqtt_clientid)
                                                    .WithTcpServer(server, portint)
                                                    .WithCredentials(AppSettings.Settings.mqtt_username, AppSettings.Settings.mqtt_password)
                                                    .WithCleanSession()
                                                    .Build();

                                                }

                                            }
                                            else
                                            {
                                                if (IsWebSocket)
                                                {
                                                    options = new MqttClientOptionsBuilder()
                                                    .WithClientId(AppSettings.Settings.mqtt_clientid)
                                                    .WithTcpServer(server, portint)
                                                    .WithCleanSession()
                                                    .Build();

                                                }
                                                else
                                                {
                                                    options = new MqttClientOptionsBuilder()
                                                    .WithClientId(AppSettings.Settings.mqtt_clientid)
                                                    .WithWebSocketServer(AppSettings.Settings.mqtt_serverandport)
                                                    .WithCleanSession()
                                                    .Build();

                                                }
                                            }


                                        }

                                        if (string.IsNullOrWhiteSpace(topic))
                                        {
                                            topic = Guid.NewGuid().ToString();
                                        }

                                        mqttClient.UseDisconnectedHandler(async e =>
                                        {
                                            string excp = "";
                                            if (e.Exception != null)
                                            {
                                                excp = e.Exception.Message;
                                            }
                                            Global.Log($"MQTT: ### DISCONNECTED FROM SERVER ### - Reason: {e.ReasonCode}, ClientWasDisconnected: {e.ClientWasConnected}, {excp}");

                                            //reconnect here if needed?
                                        });


                                        mqttClient.UseApplicationMessageReceivedHandler(async e =>
                                        {
                                            Global.Log($"MQTT: ### RECEIVED APPLICATION MESSAGE ###");
                                            Global.Log($"MQTT: + Topic = {e.ApplicationMessage.Topic}");
                                            Global.Log($"MQTT: + Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                                            Global.Log($"MQTT: + QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                                            Global.Log($"MQTT: + Retain = {e.ApplicationMessage.Retain}");
                                            Global.Log("");

                                        });


                                        mqttClient.UseConnectedHandler(async e =>
                                        {
                                            Global.Log($"MQTT: ### CONNECTED WITH SERVER '{AppSettings.Settings.mqtt_serverandport}' ### - Result: {e.AuthenticateResult.ResultCode}, '{e.AuthenticateResult.ReasonString}'");

                                            // Subscribe to the topic
                                            await mqttClient.SubscribeAsync(topic, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce);
                                            
                                            subscribed = true;

                                            Global.Log($"MQTT: ### SUBSCRIBED to topic '{topic}'");
                                        });


                                        Global.Log($"MQTT: Sending topic '{topic}' with payload '{payload}' to server '{server}:{portint}'...");


                                        MqttClientAuthenticateResult cres = await mqttClient.ConnectAsync(options, CancellationToken.None);

                                        if (cres != null && cres.ResultCode == MqttClientConnectResultCode.Success)
                                        {
                                            MqttApplicationMessage ma = new MqttApplicationMessageBuilder()
                                                                    .WithTopic(topic)
                                                                    .WithPayload(payload)
                                                                    .WithAtLeastOnceQoS()
                                                                    .WithRetainFlag(retain)
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
                                                Global.Log($"MQTT: ...Sent in {sw.ElapsedMilliseconds}ms, Reason: '{res.ReasonCode}' ({Convert.ToInt32(res.ReasonCode)} - '{res.ReasonString}')");
                                            }
                                            else
                                            {
                                                Global.Log($"MQTT: Error sending: ({sw.ElapsedMilliseconds}ms) Reason: '{res.ReasonCode}' ({Convert.ToInt32(res.ReasonCode)} - '{res.ReasonString}')");
                                            }

                                        }
                                        else if (cres != null)
                                        {
                                            Global.Log($"MQTT:  Error connecting: ({sw.ElapsedMilliseconds}ms) Result: '{cres.ResultCode}' - '{cres.ReasonString}'");
                                        }
                                        else
                                        {
                                            Global.Log($"MQTT:  Error connecting: ({sw.ElapsedMilliseconds}ms) cres=null");
                                        }

                                        if (mqttClient != null && mqttClient.IsConnected)
                                        {
                                            if (subscribed)
                                            {
                                                Global.Log($"MQTT: Unsubscribing from topic '{topic}'");
                                                await mqttClient.UnsubscribeAsync(topic);
                                            }
                                            Global.Log($"MQTT: Disconnecting from server.");
                                            await mqttClient.DisconnectAsync();
                                        }



                                    }
                                    catch (Exception ex)
                                    {

                                        Global.Log($"MQTT: Unexpected Error: Topic '{topic}' Payload '{payload}': " + Global.ExMsg(ex));
                                    }
                                    finally
                                    {
                                        if (mqttClient != null && mqttClient.IsConnected)
                                        {
                                            if (subscribed)
                                            {
                                                Global.Log($"MQTT: Unsubscribing from topic '{topic}'");
                                                await mqttClient.UnsubscribeAsync(topic);
                                            }
                                            Global.Log($"MQTT: Disconnecting from server.");
                                            await mqttClient.DisconnectAsync(); 
                                            mqttClient.Dispose();  //using should dispose anyway
                                        }
                                    }
                                }

                            });


            return res;
            

        }

    }
}
