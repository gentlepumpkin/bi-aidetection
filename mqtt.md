# MQTT with AI Tool & Blue Iris

Blue Iris [MQTT](https://mqtt.org/) ([ELI5](https://www.reddit.com/r/homeautomation/comments/515fh5/eli5_mqtt/))
support provides a way to [control the system](https://wiki.instar.com/Software/Windows/Blue_Iris_v5/INSTAR_MQTT/#controlling-blueiris-through-mqtt)
without relying on HTTP and URL based authentication.

## Requirements

A MQTT broker running on your local network, https://mosquitto.org/ is a commonly used OSS MQTT Broker.

## Setup

1.  [Configure Blue Iris](https://wiki.instar.com/Software/Windows/Blue_Iris_v5/INSTAR_MQTT/#configuring-the-blueiris-mqtt-service)
    to connect to your MQTT broker.
1.  Configure AI Tool to connect to your MQTT Broker
    * Cameras > [camera] > Action Settings > MQTT Settings
    * ![MQTT Config](https://imgur.com/S4sDjzs.png)
1.  Configure AI Tool to trigger the Camera via MQTT
    * Cameras > [camera] > Action Settings
    * MQTT Trigger Topic: `ai/[camera]/motion | BlueIris/admin`
    * MQTT Trigger Payload: `[detections] | camera=[camera]&trigger&memo=[SummaryNonEscaped]`

NOTE: Use `[SummaryNonEscaped]` instead of `[Summary]` in the BlueIris payload to avoid getting escaped characters in
the Blue Iris UI.

Now when AI Tool triggers it will publish to two MQTT topics `ai/[camera]/motion` and `BlueIris/admin`. BlueIris
subscribes to the `BlueIris/admin` topic and will trigger the named camera with the specified memo. With this setup
you no longer need the URL based integrations with BlueIris.