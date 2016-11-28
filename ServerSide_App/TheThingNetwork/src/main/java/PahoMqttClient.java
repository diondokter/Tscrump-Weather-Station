import org.eclipse.paho.client.mqttv3.*;

/**
 * Created by ali on 11/28/16.
 */
public class PahoMqttClient implements MqttCallback {
    private MqttClient client;

    public PahoMqttClient(String serverURI, String clientID, String appEUI, String appAccessKey){
        try {
            MqttConnectOptions connectOptions = new MqttConnectOptions();
            connectOptions.setUserName(appEUI);
            connectOptions.setPassword(appAccessKey.toCharArray());
            client = new MqttClient(serverURI, clientID);
            client.connect();
        }catch (MqttException e){
            e.printStackTrace();
        }
    }
    public PahoMqttClient(String serverURI, String clientID){
        try {
            client = new MqttClient(serverURI, clientID);
            client.connect();
        }catch (MqttException e){
            e.printStackTrace();
        }
    }


    public void publish(String topic, String data){
        MqttMessage message = new MqttMessage();
        message.setPayload(data.getBytes());
        try {
            client.publish(topic,message);
        } catch (MqttException e) {
            e.printStackTrace();
        }

    }

    public void subscribe(String topic){
        try {
            client.setCallback(this);
            client.subscribe(topic);
        }catch (MqttException e){
            e.printStackTrace();
        }
    }

    public void disconnect(){
        try {
            client.disconnect();
        } catch (MqttException e) {
            e.printStackTrace();
        }
    }

    public void reconnect(){
        try {
            client.reconnect();
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

    public static void main(String[] args){
        PahoMqttClient client = new PahoMqttClient("tcp://localhost:1883","Beerus-sama");
        client.subscribe("food");
        client.publish("food","ali loves food");
    }
}
