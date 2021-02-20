using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Subscribing;
using MQTTnet.Protocol;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static AITool.AITOOL;

namespace AITool
{
    public class MQTTClient : IDisposable
    {
        public bool IsSubscribed = false;
        public bool IsConnected = false;

        int portint = 0;
        string server = "";
        string port = "";
        string LastTopic = "";
        string LastPayload = "";
        bool LastRetain = false;

        MqttFactory factory = null;
        IMqttClient mqttClient = null;
        IMqttClientOptions options = null;
        MqttClientAuthenticateResult cres = null;

        public async void Dispose()
        {

            if (mqttClient != null)
            {

                if (mqttClient.IsConnected )
                {
                    Log($"Debug: MQTT: Disconnecting from server.");
                    try
                    {
                        await mqttClient.DisconnectAsync();
                    }
                    catch (Exception ex)
                    {
                        //dont throw ERROR in the log if fail to disconnect
                        Log($"Debug: MQTT: Could not disconnect from server, got: {ex.Msg()}");
                    }
                }
                else
                {
                    Log($"Debug: MQTT: Already disconnected from server, no need to disconnect.");
                }


                IsConnected = false;

                mqttClient.Dispose();

            }
                        


        }

        public MQTTClient()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            try
            {
                this.factory = new MqttFactory();
                this.mqttClient = factory.CreateMqttClient();
                                               

            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Msg()}");
            }
        }


        public async Task<bool> Connect()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool ret = false;
            try
            {

                this.server = Global.GetWordBetween(AppSettings.Settings.mqtt_serverandport, "", ":");
                this.port = Global.GetWordBetween(AppSettings.Settings.mqtt_serverandport, ":", "/");
                this.portint = 0;

                if (!string.IsNullOrEmpty(this.port))
                    this.portint = Convert.ToInt32(this.port);
                if (this.portint == 0 && AppSettings.Settings.mqtt_UseTLS)
                {
                    this.portint = 8883;
                }
                else if (this.portint == 0)
                {
                    this.portint = 1883;
                }

                bool IsWebSocket = (AppSettings.Settings.mqtt_serverandport.IndexOf("ws://", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    AppSettings.Settings.mqtt_serverandport.IndexOf("/mqtt", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    AppSettings.Settings.mqtt_serverandport.IndexOf("wss://", StringComparison.OrdinalIgnoreCase) >= 0);

                bool UseTLS = (AppSettings.Settings.mqtt_UseTLS || portint == 8883 || AppSettings.Settings.mqtt_serverandport.IndexOf("wss://", StringComparison.OrdinalIgnoreCase) >= 0);

                bool UseCreds = (!string.IsNullOrWhiteSpace(AppSettings.Settings.mqtt_username));



                //=====================================================================
                //Seems like there should be a better way here with this Options builder...
                //I dont see an obvious way to directly modify options without the builder
                //and I cant seem to put IF statements around each part of the option builder
                //parameters.
                //=====================================================================

                var lw = new MqttApplicationMessage()
                {
                    Topic = AppSettings.Settings.mqtt_LastWillTopic,
                    Payload = Encoding.UTF8.GetBytes(AppSettings.Settings.mqtt_LastWillPayload),
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = true
                };


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
                                .WithWillMessage(lw)
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
                                .WithWillMessage(lw)
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
                                .WithWillMessage(lw)
                                .WithCleanSession()
                                .Build();

                        }
                        else
                        {
                            options = new MqttClientOptionsBuilder()
                                .WithClientId(AppSettings.Settings.mqtt_clientid)
                                .WithTcpServer(server, portint)
                                .WithTls()
                                .WithWillMessage(lw)
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
                            .WithWillMessage(lw)
                            .WithCleanSession()
                            .Build();

                        }
                        else
                        {
                            options = new MqttClientOptionsBuilder()
                            .WithClientId(AppSettings.Settings.mqtt_clientid)
                            .WithTcpServer(server, portint)
                            .WithCredentials(AppSettings.Settings.mqtt_username, AppSettings.Settings.mqtt_password)
                            .WithWillMessage(lw)
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
                            .WithWillMessage(lw)
                            .WithCleanSession()
                            .Build();

                        }
                        else
                        {
                            options = new MqttClientOptionsBuilder()
                            .WithClientId(AppSettings.Settings.mqtt_clientid)
                            .WithWebSocketServer(AppSettings.Settings.mqtt_serverandport)
                            .WithWillMessage(lw)
                            .WithCleanSession()
                            .Build();

                        }
                    }


                }

                mqttClient.UseDisconnectedHandler(async e =>
                {
                    IsConnected = false;
                    string excp = "";
                    if (e.Exception != null)
                    {
                        excp = e.Exception.Message;
                    }
                    Log($"Debug: MQTT: ### DISCONNECTED FROM SERVER ### - Reason: {e.ReasonCode}, ClientWasDisconnected: {e.ClientWasConnected}, {excp}");

                    //reconnect here if needed?
                });


                mqttClient.UseApplicationMessageReceivedHandler(async e =>
                {
                    Log($"Debug: MQTT: ### RECEIVED APPLICATION MESSAGE ###");
                    Log($"Debug: MQTT: + Topic = {e.ApplicationMessage.Topic}");
                    Log($"Debug: MQTT: + Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload).Truncate(128,true)}");
                    Log($"Debug: MQTT: + QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                    Log($"Debug: MQTT: + Retain = {e.ApplicationMessage.Retain}");
                    Log("");

                });


                mqttClient.UseConnectedHandler(async e =>
                {
                    IsConnected = true;
                    Log($"Debug: MQTT: ### CONNECTED WITH SERVER '{AppSettings.Settings.mqtt_serverandport}' ### - Result: {e.AuthenticateResult.ResultCode}, '{e.AuthenticateResult.ReasonString}'");
                    

                    MqttApplicationMessage ma = new MqttApplicationMessageBuilder()
                                                    .WithTopic(AppSettings.Settings.mqtt_LastWillTopic)
                                                    .WithPayload(AppSettings.Settings.mqtt_OnlinePayload)
                                                    .WithAtLeastOnceQoS()
                                                    .WithRetainFlag(true)
                                                    .Build();

                    Log($"Debug: MQTT: Sending '{AppSettings.Settings.mqtt_OnlinePayload}' message...");
                    MqttClientPublishResult res = await mqttClient.PublishAsync(ma, CancellationToken.None);

                    //if (!string.IsNullOrWhiteSpace(this.LastTopic))
                    //{
                    //    // Subscribe to the topic
                    //    MqttClientSubscribeResult res = await mqttClient.SubscribeAsync(this.LastTopic, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce);

                    //    IsSubscribed = true;

                    //    Log($"Debug: MQTT: ### SUBSCRIBED to topic '{this.LastTopic}'");
                    //}
                });

                Log($"Debug: MQTT: Connecting to server '{this.server}:{this.portint}'...");


                cres = await mqttClient.ConnectAsync(options, CancellationToken.None);

            }
            catch (Exception ex)
            {

                Log($"Error: {ex.Msg()}");
            }
            return ret;
        }

        public async Task<MqttClientPublishResult> PublishAsync(string topic, string payload, bool retain, ClsImageQueueItem CurImg)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            MqttClientPublishResult res = null;
            Stopwatch sw = Stopwatch.StartNew();

            this.LastRetain = retain;
            this.LastPayload = payload;
            this.LastTopic = topic;
            if (string.IsNullOrWhiteSpace(topic))
            {
                this.LastTopic = Guid.NewGuid().ToString();
            }

            if (!this.IsConnected)
                await this.Connect();

            if (!this.IsConnected)
                return res;

                await Task.Run(async () =>
                            {
                                

                                try
                                {


                                    Log($"Debug: MQTT: Sending topic '{this.LastTopic}' with payload '{this.LastPayload}' to server '{this.server}:{this.portint}'...");

                                    if (cres != null && mqttClient.IsConnected && cres.ResultCode == MqttClientConnectResultCode.Success)
                                    {

                                        IsConnected = true;

                                        MqttApplicationMessage ma;

                                        if (CurImg != null)
                                        {
                                            //using FileStream image_data =  System.IO.File.OpenRead(CurImg.image_path);

                                            ma = new MqttApplicationMessageBuilder()
                                                     .WithTopic(this.LastTopic)
                                                     .WithPayload(CurImg.ToStream())
                                                     .WithAtLeastOnceQoS()
                                                     .WithRetainFlag(this.LastRetain)
                                                     .Build();
                                            
                                            res = await mqttClient.PublishAsync(ma, CancellationToken.None);


                                            if (res.ReasonCode == MqttClientPublishReasonCode.Success)
                                            {
                                                Log($"Debug: MQTT: ...Sent image in {sw.ElapsedMilliseconds}ms, Reason: '{res.ReasonCode}' ({Convert.ToInt32(res.ReasonCode)} - '{res.ReasonString}')");
                                            }
                                            else
                                            {
                                                Log($"Error: MQTT: sending image: ({sw.ElapsedMilliseconds}ms) Reason: '{res.ReasonCode}' ({Convert.ToInt32(res.ReasonCode)} - '{res.ReasonString}')");
                                            }

                                        }
                                        else
                                        {
                                            ma = new MqttApplicationMessageBuilder()
                                                    .WithTopic(this.LastTopic)
                                                    .WithPayload(this.LastPayload)
                                                    .WithAtLeastOnceQoS()
                                                    .WithRetainFlag(this.LastRetain)
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
                                                Log($"Debug: MQTT: ...Sent in {sw.ElapsedMilliseconds}ms, Reason: '{res.ReasonCode}' ({Convert.ToInt32(res.ReasonCode)} - '{res.ReasonString}')");
                                            }
                                            else
                                            {
                                                Log($"Error: MQTT: sending: ({sw.ElapsedMilliseconds}ms) Reason: '{res.ReasonCode}' ({Convert.ToInt32(res.ReasonCode)} - '{res.ReasonString}')");
                                            }

                                        }

                                    }
                                    else if (cres != null)
                                    {
                                        IsConnected = false;
                                        Log($"Error: MQTT: connecting: ({sw.ElapsedMilliseconds}ms) Result: '{cres.ResultCode}' - '{cres.ReasonString}'");
                                    }
                                    else
                                    {
                                        IsConnected = false;
                                        Log($"Error: MQTT: Error connecting: ({sw.ElapsedMilliseconds}ms) cres=null");
                                    }

                                    



                                }
                                catch (Exception ex)
                                {

                                    Log($"Error: MQTT: Unexpected Problem: Topic '{this.LastTopic}' Payload '{this.LastPayload}': " + ex.Msg());
                                }
                                finally
                                {
                                   
                                }

                            });


            return res;


        }

    }
}
