using UnityEngine;
using System.Collections;
using Uniduino;

public class ArduinoTest : MonoBehaviour {

    public Arduino arduino;

    public int pinUp = 2;
    public int pinDown = 3;
    public int pinLeft = 4;
    public int pinRight = 5;
	public int pinBtn1 = 6;
	public int pinBtn2 = 7;

	public int pinBtn1Light = 8;
	public int pinBtn2Light = 9;

    // Use this for initialization
    void Start () {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
	}

    //Method for setting up the different pins
    void ConfigurePins()
    {
        arduino.pinMode(pinUp, PinMode.INPUT);
        arduino.pinMode(pinDown, PinMode.INPUT);
        arduino.pinMode(pinLeft, PinMode.INPUT);
        arduino.pinMode(pinRight, PinMode.INPUT);
		arduino.pinMode(pinBtn1, PinMode.INPUT);

		arduino.pinMode(pinBtn1Light, PinMode.OUTPUT);

        //Only need to activate once for one pin, becuase all pins are on the same Port
        arduino.reportDigital((byte)(pinUp / 8), 1);
        
        }

    void Update()
    {   
        //These controls should be appliable for anything. When we have the real joystick I will make the wiring so it fits
        //Unsure if we should be using Switch cases instead
		if (arduino.digitalRead (pinUp) == 1) {
			Debug.Log ("PinUp");
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1));
		} else if (arduino.digitalRead (pinDown) == 1) {
			Debug.Log ("PinDown");
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -1));
		} else if (arduino.digitalRead (pinLeft) == 1) {
			Debug.Log ("PinLeft");
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-1, 0));
		} else if (arduino.digitalRead (pinRight) == 1) {
			Debug.Log ("PinRight");
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (1, 0));
		} else if (arduino.digitalRead (pinBtn1) == 1) {
			//TODO: Insert boost controls here
		} else if (arduino.digitalRead (pinBtn2) == 1) {
			//TODO: Insert shield controls here
		}

    }

	//Simple method for checking if the diode is working
	IEnumerator Blinky(){
		while (true) {
			arduino.digitalWrite (pinBtn1Light, Arduino.HIGH);
			yield return new WaitForSeconds(1);
			arduino.digitalWrite (pinBtn1Light, Arduino.LOW);
			yield return new WaitForSeconds(1);

		}
	}

			
}
