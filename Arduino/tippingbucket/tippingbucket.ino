/***************************
 * Name: Marco Meijer
 * Description: arduino code to keep track of the rain meter, more information at: https://docs.google.com/document/d/1lL3xof9Kub50YlRxNvad5bsnVTUUYBEN0Mo9YKesPtc/edit?usp=sharing
 * Project: The Tscrump Weather Station
 * Group: Tscrump
 * Date: 19/11/2016
 ***************************/

const int  switchPin = 2;    // the pin that the reed switch is attached to 

// Variables will change:
int tipCounter = 0;   // counter for the number of bucket tips
int switchState = 0;         // current state of the switch
int lastSwitchState = 0;     // previous state of the switch

void setup() {
  // initialize the button pin as a input:
  pinMode(switchPin, INPUT);
}

void loop() {
  // read the switch input pin:
  switchState = digitalRead(switchPin);
  // compare the buttonState to its previous state
  if (switchState != lastSwitchState) {
    // if the state has changed, increment the counter
    if (switchState == HIGH) {
      // if the current state is HIGH then the button
      // wend from off to on:
      tipCounter ++;
    }
    // Delay a little bit to avoid bouncing
    delay(20);
  }
  // save the current state as the last state,
  //for next time through the loop
  lastSwitchState = switchState;
}
