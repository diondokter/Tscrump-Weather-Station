#include <Adafruit_Sensor.h>
#include <Adafruit_BMP085_U.h>
#include <DHT.h>
#include <DHT_U.h>

// Set your AppEUI and AppKey
const char *appEui = "70B3D57EF000335E";
const char *appKey = "CC745AC6D0373813AA14466BE367D301";

#define LightPin A0
#define loraSerial Serial
#define debugSerial SerialUSB

#define freqPlan TTN_FP_EU868
#define DHTPIN 4
#define DHTTYPE DHT11
#define SENSORS 5
DHT_Unified dht(DHTPIN, DHTTYPE);
Adafruit_BMP085_Unified bmp = Adafruit_BMP085_Unified(55);


void setup() {
  loraSerial.begin(57600);
  debugSerial.begin(115200);
  delay(10000);
  loraSerial.println("mac set appkey D2800EA3EE7DA994252AEE2944B0A093");
  delay(100);
  loraSerial.println("mac set appeui 70B3D57ED00016FA");
  delay(100);
  loraSerial.println("mac join otaa");
  delay(5000);
  while (loraSerial.available()) {
    debugSerial.write(loraSerial.read());
  }
  // Wait a maximum of 10s for Serial Monitor
  while (!debugSerial && millis() < 10000);
  pinMode(LightPin, INPUT);

  if (!bmp.begin()) {
    debugSerial.println("BMP180 not working");
  }
  sensor_t humidity;
  sensor_t temperature;

  dht.humidity().getSensor(&humidity);
  dht.temperature().getSensor(&temperature);
}

void loop() {
  int tmpSensorArray[5];
  uint16_t light = analogRead(LightPin);
  
  sensors_event_t event;
  dht.temperature().getEvent(&event);
  if (isnan(event.temperature)) {
    debugSerial.println("Error reading temperature!");
  } 
  tmpSensorArray[0] = (int)event.temperature;
  
  dht.humidity().getEvent(&event);
  if (isnan(event.relative_humidity)) {
    debugSerial.println("Error reading humidity!");
  }
  tmpSensorArray[3] = (int)event.relative_humidity;  
  
  // Get humidity event and print its value.
  bmp.getEvent(&event);
  if (isnan(event.pressure)) {
    debugSerial.println("Error reading humidity!");
  }
  tmpSensorArray[2] = (int)event.pressure;
  
  
  float bmpTemperature;
  bmp.getTemperature(&bmpTemperature);

  tmpSensorArray[1] = (int)bmpTemperature;
  
  debugSerial.println("Sending LoRa packet");
  loraSerial.print("mac tx uncnf 123 ");
  for(int i = 0; i < SENSORS; i++){
    loraSerial.print((int)(tmpSensorArray[i]));
    if(i < (SENSORS - 1)){
      loraSerial.print('A');
    }
  }
  loraSerial.println();



  delay(20000);
}
