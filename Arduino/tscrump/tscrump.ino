#include <Adafruit_Sensor.h>
#include <Adafruit_BMP085_U.h>
#include <DHT.h>
#include <DHT_U.h>
#include <rn2xx3.h>


// Set your AppEUI and AppKey
const char *appEui = "70B3D57ED00016FA";
const char *appKey = "D2800EA3EE7DA994252AEE2944B0A093";

#define LightPin A0
#define loraSerial Serial
#define debugSerial SerialUSB

#define freqPlan TTN_FP_EU868
#define DHTPIN 4
#define DHTTYPE DHT11
#define SENSORS 5

DHT_Unified dht(DHTPIN, DHTTYPE);
Adafruit_BMP085_Unified bmp = Adafruit_BMP085_Unified(55);
rn2xx3 myLora(loraSerial);


void setup() {
  loraSerial.begin(57600);
  debugSerial.begin(57600);
  delay(5000);
  while (loraSerial.available()) {
    debugSerial.write(loraSerial.read());
  }
  while ((!loraSerial) && (millis() < 10000));
  initialize_radio();
  pinMode(LightPin, INPUT);
  if (!bmp.begin()) {
    debugSerial.println("BMP180 not working");
  }
  sensor_t humidity;
  sensor_t temperature;
  dht.humidity().getSensor(&humidity);
  dht.temperature().getSensor(&temperature);
  myLora.tx("TTN Mapper on TTN Custom PCB with SAMD21G18 and RN2483");
}

void loop() {
  float tmpSensorArray[SENSORS];
  tmpSensorArray[4] = analogRead(LightPin);
  sensors_event_t event;
  dht.temperature().getEvent(&event);
  if (isnan(event.temperature)) {
    debugSerial.println("Error reading temperature!");
  }
  tmpSensorArray[0] = event.temperature;
  dht.humidity().getEvent(&event);
  if (isnan(event.relative_humidity)) {
    debugSerial.println("Error reading humidity!");
  }
  tmpSensorArray[3] = event.relative_humidity;

  // Get humidity event and print its value.
  bmp.getEvent(&event);
  if (isnan(event.pressure)) {
    debugSerial.println("Error reading humidity!");
  }
  tmpSensorArray[2] = event.pressure;
  float bmpTemperature;
  bmp.getTemperature(&bmpTemperature);
  tmpSensorArray[1] = bmpTemperature;
  debugSerial.println("TXing");
  String transmitData = String(tmpSensorArray[0], 2) + "," + String(tmpSensorArray[1], 2) + "," + String(tmpSensorArray[2], 2) + "," + String(tmpSensorArray[3], 2) + "," + String(tmpSensorArray[4], 2);
  debugSerial.println(transmitData);
  myLora.tx(transmitData);
  
  delay(60000);
}

void initialize_radio()
{
  delay(100); //wait for the RN2xx3's startup message
  loraSerial.flush();
  //print out the HWEUI so that we can register it via ttnctl
  String hweui = myLora.hweui();
  while(hweui.length() != 16)
  {
    debugSerial.println("Communication with RN2xx3 unsuccessful. Power cycle the board.");
    delay(10000);
    hweui = myLora.hweui();
  }
  debugSerial.println("When using OTAA, register this DevEUI: ");
  debugSerial.println(hweui);
  debugSerial.println("RN2xx3 firmware version:");
  debugSerial.println(myLora.sysver());
  debugSerial.println("Trying to join TTN");
  bool join_result = false;
  join_result = myLora.initOTAA(appEui, appKey);
  while(!join_result)
  {
    debugSerial.println("Unable to join. Are your keys correct, and do you have TTN coverage?");
    delay(60000); //delay a minute before retry
    join_result = myLora.init();
  }
  debugSerial.println("Successfully joined TTN");

}

