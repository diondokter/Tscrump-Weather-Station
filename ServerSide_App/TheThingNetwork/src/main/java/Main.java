/**
 * Created by ali on 11/26/16.
 */

import java.util.function.BiConsumer;
import java.util.function.Consumer;
import org.eclipse.paho.client.mqttv3.MqttClient;
import org.json.JSONObject;
import org.thethingsnetwork.java.app.lib.Client;



public class Main {
    public static void main(String[] args) throws Exception {

        String region = "region";
        String appId ="appId";
        String accessKey = "accessKey";
        Client client= null;
        try {
            client = new Client(region, appId, accessKey);
            client.onMessage(new BiConsumer<String, Object>() {
                public void accept(String devId, Object data) {
                    System.out.println("Message: " + devId + " " + data);
                }
            });

            client.onActivation(new BiConsumer<String, JSONObject>() {
                @Override
                public void accept(String _devId, JSONObject _data) {
                    System.out.println("Activation: " + _devId + ", data: " + _data);
                }
            });

            client.onError(new Consumer<Throwable>() {
                public void accept(Throwable _error) {
                    System.err.println("error: " + _error.getMessage());
                }
            });

            client.onConnected(new Consumer<MqttClient>() {
                public void accept(MqttClient _client) {
                    System.out.println("connected !");
                }
            });

            client.start();
        }catch (Exception e){System.out.println(e);}


    }
}
