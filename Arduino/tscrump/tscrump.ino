#include <TheThingsNetwork.h>
#include <Adafruit_Sensor.h>
#include <DHT.h>
#include <DHT_U.h>

// Set your AppEUI and AppKey
const char *appEui = "0000000000000000";
const char *appKey = "00000000000000000000000000000000";

#define LightPin A0
#define loraSerial Serial1
#define debugSerial Serial

#define freqPlan TTN_FP_EU868
#define DHTPIN 2
#define DHTTYPE DHT11

DHT_Unified dht(DHTPIN, DHTTYPE);
Adafruit_BMP085_Unified bmp = Adafruit_BMP085_Unified(55);
TheThingsNetwork ttn(loraSerial, debugSerial, freqPlan);

void setup() {
  loraSerial.begin(57600);
  debugSerial.begin(9600);

  // Wait a maximum of 10s for Serial Monitor
  while (!debugSerial && millis() < 10000);
  pinMode(LightPin, INPUT);
  debugSerial.println("-- STATUS");
  ttn.showStatus();

  debugSerial.println("-- JOIN");
  ttn.join(appEui, appKey);
  ttn.showStatus();
  if(!dht.begin() || !bmp.begin()){
    debugSerial.println("DHT11 or BMP180 not working");
  }  
  sensor_t humidity;
  sensor_t temperature;
  
  dht.humidity().getSensor(&humidity);  
  dht.temperature().getSensor(&temperature);
}

void loop() {
  debugSerial.println("-- LOOP");
  
  uint16_t light = analogRead(LightPin);
  
  sensors_event_t event;  
  dht.temperature().getEvent(&event);
  if (isnan(event.temperature)) {
    debugSerial.println("Error reading temperature!");
  }
  else {
    debugSerial.print("Temperature: ");
    debugSerial.print(event.temperature);
    debugSerial.println(" *C");
  }
  // Get humidity event and print its value.
  dht.humidity().getEvent(&event);
  if (isnan(event.relative_humidity)) {
    debugSerial.println("Error reading humidity!");
  }
  else {
    debugSerial.print("Humidity: ");
    debugSerial.print(event.relative_humidity);
    debugSerial.println("%");
  }
  bmp.getEvent(&event);
  
  
  /*byte payload[6];
  payload[0] = highByte(temperature);
  payload[1] = lowByte(temperature);
  payload[2] = highByte(humidity);
  payload[3] = lowByte(humidity);
  payload[4] = highByte(light);
  payload[5] = lowByte(light);
  
  debugSerial.print("Temperature: ");
  debugSerial.println(temperature);
  debugSerial.print("Humidity: ");
  debugSerial.println(humidity);
  debugSerial.print("Light level: ");
  debugSerial.println(light);

  ttn.sendBytes(payload, sizeof(payload));*/

  delay(20000);
}
