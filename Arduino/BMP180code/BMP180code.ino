/***************************
 * Name: Ali Ammar
 * Description: arduino functions for the BMP180 to measure temperature and air pressure
 * Project: The Tscrump Weather Station
 * Group: Tscrump
 * Date: 19/11/2016
 ***************************/



#include <SFE_BMP180.h>
#include <Wire.h>

SFE_BMP180 pressure;

#define ALTITUDE 0  // altitude of the weather station
double T,P,p0,a;




void temperature(){
  int status;
  
  status = pressure.startTemperature();
  if(status != 0 ){
    // Wait for the measurement to complete:
    delay(status);

    // Retrieve the completed temperature measurement:
    // The Temperature is stored in the variable T.
    status = pressure.getTemperature(T);
    
    // Function returns 1 if successful, 0 if failure.
    if(status != 0){
      Serial.print("temperature: ");
      Serial.print(T,2);
      Serial.print(" deg C, ");
      Serial.print((9.0/5.0)*T+32.0,2);
      Serial.println(" deg F");
      
    }
    else Serial.println("error retrieving temperature measurement\n");
  }
  else Serial.println("error starting temperature measurement\n");
}

void airPressure(){

  // Start a pressure measurement:
  // The parameter is the oversampling setting, from 0 to 3 (highest res, longest wait).
  status = pressure.startPressure(3);
  
  // If request is successful, the number of ms to wait is returned.
  if(status!=0){

    // Wait for the measurement to complete:
    delay(status);


    
        
    // The pressure is now stored in varibel P.
    status = pressure.getPressure(P,T);

    // Function returns 1 if successful, 0 if failure.
    if(status!=0){
      Serial.print("absolute pressure: ");
      Serial.print(P,2);
      Serial.print(" mb, ");
      Serial.print(P*0.0295333727,2);
      Serial.println(" inHg");

      //IN CASE WE NEED THE ABSOLUTE PRESSURE:
      
      // The pressure sensor returns absolute pressure, which varies with altitude.
      // To remove the effects of altitude, use the sealevel function and your current altitude.
      // This number is commonly used in weather reports.
      // Parameters: P = absolute pressure in mb, ALTITUDE = current altitude in m.
      // Result: p0 = sea-level compensated pressure in mb
      p0 = pressure.sealevel(P,ALTITUDE);
      Serial.print("relative (sea-level) pressure: ");
      Serial.print(p0,2);
      Serial.print(" mb, ");
      Serial.print(p0*0.0295333727,2);
      Serial.println(" inHg");

    }
    else Serial.println("error retrieving pressure measurement\n");
  }
  else Serial.println("error starting pressure measurement\n");
}


void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);

  if(pressure.begin()){
    Serial.println("BMP-180 started");
  }
  else{
    Serial.println("Failed to start");
    while(1);
  }

}

void loop() {
  // put your main code here, to run repeatedly:

  temperature();
  airPressure();

}
