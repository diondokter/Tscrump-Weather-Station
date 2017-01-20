The bootloader is the exact same as the Adafruit M0 feather.
You will need to install the SAMD board toolchain from Adafruit in your Arduino IDE to compile and upload.
Instructions can be found [here](https://learn.adafruit.com/adafruit-feather-m0-basic-proto/using-with-arduino-ide).

You will need the following libraries:
- TheThingsNetwork 2.0.0 (2.1+ breaks non-AVR compatibility)
- Adafruit Unified Sensor 1.0.2
- DHT sensor library 1.3.0+
- Adafruit BMP085 Unified 1.0.0+
