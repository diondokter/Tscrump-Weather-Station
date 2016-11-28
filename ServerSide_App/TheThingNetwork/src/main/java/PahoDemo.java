
import org.eclipse.paho.client.mqttv3.*;

public class PahoDemo implements MqttCallback{

MqttClient client;

public PahoDemo()  {
}

public static void main(String[] args) {
    new PahoDemo().doDemo();
}

public void doDemo()  {
    try {
        client = new MqttClient("tcp://localhost:1883", "Sending");
        client.connect();
        client.setCallback(this);
        client.subscribe("foo");
        MqttMessage message = new MqttMessage();
        message.setPayload("A single message from my computer fff"
                .getBytes());
        client.publish("foo", message);
        client.disconnect();
    } catch (MqttException e) {
        e.printStackTrace();
    }
}


    public void connectionLost(Throwable throwable) {

    }

    public void messageArrived(String s, MqttMessage mqttMessage) throws Exception {
        System.out.println(mqttMessage);

    }

    public void deliveryComplete(IMqttDeliveryToken iMqttDeliveryToken) {

    }
}