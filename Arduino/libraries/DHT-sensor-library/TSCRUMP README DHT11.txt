We'll need the following functions of this library:

- (DHT_Unified dht(DHTPIN, DHTTYPE).temperature().getSensor(sensor_t)     //If we won't use BMP180 for temperature
This will return a value in C (degrees)

- (DHT_Unified dht(DHTPIN, DHTTYPE).humidity().getSensor(sensor_t)
This will return a value in % (percentage)

For chip connections to arduino, go to
https://learn.adafruit.com/dht/connecting-to-a-dhtxx-sensor