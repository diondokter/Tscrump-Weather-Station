

import org.json.JSONObject;
import org.thethingsnetwork.data.common.Connection;
import org.thethingsnetwork.data.mqtt.Client;

/**
 *
 * @author Romain Cambier
 */
public class App {

    public static void main(String[] args) throws Exception {
        String region = System.getenv("region");
        String appId = System.getenv("appId");
        String accessKey = System.getenv("accessKey");

        Client client = new Client(region, appId, accessKey);

        client.onMessage(null, "led", (String _devId, Object _data) -> {
            try {
                // Toggle the LED
                JSONObject response = new JSONObject().put("led", !_data.equals("true"));

                /**
                 * If you don't have an encoder payload function:
                 * client.send(_devId, _data.equals("true") ? new byte[]{0x00} : new byte[]{0x01}, 0);
                 */
                System.out.println("Sending: " + response);
                client.send(_devId, response, 0);
            } catch (Exception ex) {
                System.out.println("Response failed: " + ex.getMessage());
            }
        });

        client.onMessage((String devId, Object data) -> System.out.println("Message: " + devId + " " + data));

        client.onActivation((String _devId, JSONObject _data) -> System.out.println("Activation: " + _devId + ", data: " + _data));

        client.onError((Throwable _error) -> System.err.println("error: " + _error.getMessage()));

        client.onConnected((Connection _client) -> System.out.println("connected !"));

        client.start();
    }

}