package sample;

import org.eclipse.paho.client.mqttv3.*;

import javax.xml.bind.DatatypeConverter;
import java.nio.ByteBuffer;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.util.Calendar;

/**
 * Created by ali on 11/28/16.
 */

//dev eui 00 04 A3 0B 00 1C 04 FE
// app eui 70 B3 D5 7E D0 00 18 F6
//app key  C3485F16C6EFFF94FE9B95AB8E7EDAAE
//netwerk B602CE2BE6B5EF52B0B9C8D8740F70BE
//app session D3D35C1845B657F4490157FC487B831E
/**
 * https://www.thethingsnetwork.org/docs/current/mqtt/
 * https://mosquitto.org/man/mosquitto_sub-1.html
 *  -t, --topic
    The MQTT topic to subscribe to. See mqtt(7) for more information on MQTT topics.
    This option may be repeated to subscribe to multiple topics.

 *  -u, --username
    Provide a username to be used for authenticating with the broker.
    This requires a broker that supports MQTT v3.1. See also the --pw argument.

 *  -P, --pw
    Provide a password to be used for authenticating with the broker.
    Using this argument without also specifying a username is invalid.
    This requires a broker that supports MQTT v3.1. See also the --username option.


 *  -v, --verbose
    Print received messages verbosely.
    With this argument, messages will be printed as "topic payload".
    When this argument is not given, the messages are printed as "payload".
 */






public class PahoMqttClient implements MqttCallback {
    private MqttClient client;
    private boolean error = false;
    private float temperature,temperature1,temperature2, pressure, humidity, brightness;
    private float precipitation = 0;
    private int latitude = 0;
    private int longitude = 0;
    private String mqttData;
    String weatherData;
    boolean newData = false;

    DBmanager dbcon = null;
    Connection conn = null;

    //Default Tscrump Mqtt credentials
    private String clientID = "Tscrump";
    private String serverURI = "tcp://staging.thethingsnetwork.org:1883";
    private String appEUI= "70B3D57ED00016FA";
    private String appKey = "PlGBB/sm6KysRHZik1ot9oFBnSPauMyt7MHJXosW+Wc=";

    public String getClientID() {
        return clientID;
    }

    public String getAppEUI() {
        return appEUI;
    }

    public String getAppKey() {
        return appKey;
    }



    //forTTN
    public PahoMqttClient(String clientID, String appEUI, String appAccessKey){
        try {

            MqttConnectOptions connectOptions = new MqttConnectOptions();
            connectOptions.setUserName(appEUI);
            connectOptions.setPassword(appAccessKey.toCharArray());
            client = new MqttClient(serverURI, clientID);
            client.connect(connectOptions);
        }catch (Exception e){
            error=true;
            System.out.println(e);

        }
    }
    //normal mqtt server
    public PahoMqttClient(String serverURI, String clientID){
        try {
            client = new MqttClient(serverURI, clientID);
            client.connect();
        }catch (MqttException e){
            e.printStackTrace();
        }
    }


    public void publish(String topic, String data){
        /**
         * Topic: Down (Send)

         To send a message to a specific device registered to the application, publish to:
         +/devices/<DeviceEUI>/down

         Or, in this case maybe more clear:
         <AppEUI>/devices/<DeviceEUI>/down

         Message format
         Encoded as JSON string, format your message as:

         {
         "payload": "SGVsbG8gd29ybGQK=",
         "port": 1,
         "ttl": "1h"
         }
         */
        MqttMessage message = new MqttMessage();
        message.setPayload(data.getBytes());
        try {
            client.publish(topic,message);
        } catch (MqttException e) {
            e.printStackTrace();
        }

    }




    public void subscribe(String topic){

        /**
         * Topic: Up (Receive)

         To receive messages from all devices registered to the application, subscribe to:
         <AppEUI>/devices/+/up

         Or, since the authentication already limits you to an Application:
         +/devices/+/up

         To get messages for a specific device subscribe to:
         +/devices/<DeviceEUI>/up

         Or if you like to be verbose:
         <AppEUI>/devices/<DeviceEUI>/up
         */

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

    public int baseToint(String base64){
        byte []  x = DatatypeConverter.parseBase64Binary(base64);
        ByteBuffer wrapped = ByteBuffer.wrap(x); // big-endian by default
        int num = wrapped.getShort();
        return num;
    }

    public void messageArrived(String string, MqttMessage mqttMessage) throws Exception {
        //need to be changed
        System.out.println(mqttMessage);
        mqttData = mqttMessage.toString();

        String [] mqttDataArray = mqttData.split("([{}])");


        System.out.println("index2 "+mqttDataArray[2]);

        weatherData = mqttDataArray[2];

        String[] weatherDataArray = weatherData.split(":");
        weatherData = weatherDataArray[1];
        System.out.println(weatherData);
        weatherDataArray = weatherData.split(",");
        String [] temp1 = weatherDataArray[0].split("\"");
        String [] ldr = weatherDataArray[4].split("\"");

        System.out.println(temp1[1]);
        System.out.println(weatherDataArray[1]);
        System.out.println(weatherDataArray[2]);
        System.out.println(weatherDataArray[3]);
        System.out.println(ldr[0]);

        temperature1 = Float.parseFloat(temp1[1]);
        temperature2 = Float.parseFloat(weatherDataArray[1]);
        pressure = Float.parseFloat(weatherDataArray[2]);
        humidity = Float.parseFloat(weatherDataArray[3]);
        brightness = Float.parseFloat(ldr[0]);
        temperature = (temperature1 + temperature2)/2;

        weatherDAO();







    }
    public void deliveryComplete(IMqttDeliveryToken iMqttDeliveryToken) {


    }
    public void hi(){
        System.out.print("ali");
    }

    public String receiveNttTopic(){
        return "+/devices/+/up";
    }

    public String sendNttTopic(){
        return "+/devices/<DeviceEUI>/down";
    }

    public boolean getError(){
        return error;
    }


    public void weatherDAO() {


        try {
            //make a connection with the database
            dbcon = DBmanager.getInstance();
            conn = dbcon.getConnection();

            Statement stmt = conn.createStatement(); //make a Statement object
            Calendar cal = Calendar.getInstance();
            SimpleDateFormat sdft = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
            String time = sdft.format(cal.getTime());



//date is primary key
//            stmt.executeUpdate("INSERT INTO sensor (Date, Temperature, Pressure, Humidity,Brightness,Precipitation,Latitude,longitude) "
//                    +"VALUES (" +time+ ","+temperature+","+pressure+","+humidity+","+brightness+","+precipitation+","+latitude+","+longitude+")");
            stmt.executeUpdate("INSERT INTO sensor (Date, Temperature, Pressure, Humidity,Brightness,Precipitation,Latitude,longitude) "
                    +"VALUES ('2018-05-21 15:00:00',21,1.058,42,58,5,NULL ,NULL )");
            //("INSERT INTO sensor (Date, Temperature, Pressure, Humidity,Brightness,Precipitation,Latitude,longitude) "+"VALUES ('2018-05-21 15:00:00',21,1.058,42,58,5,NULL ,NULL )");

            conn.close();
        } catch (SQLException e) {

            e.printStackTrace();


        } finally {
            dbcon.close();
        }
    }

    public static void main(String[] args){

//Van paul
//        String pid = "me";
//        String eui = "70B3D57ED00018F6";
//        String key = "r7cAAHo0pY17udmgsvIP9HvL1mlmCbzh9kWbOQGKVLs=";
        //tscrump
        String clientID = "Tscrump";
        String appKey = "PlGBB/sm6KysRHZik1ot9oFBnSPauMyt7MHJXosW+Wc=";
        String appEUI= "70B3D57ED00016FA";

        PahoMqttClient ttn = new PahoMqttClient(clientID,appEUI,appKey);
        ttn.subscribe("+/devices/+/up");



    }
}
