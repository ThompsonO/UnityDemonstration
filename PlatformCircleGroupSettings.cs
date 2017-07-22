using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCircleGroupSettings : MonoBehaviour {
	//If the circle is moving CCW or CW
	public bool CCW = false;

	//Which plane the circle exists in
	public bool xzCir = false;
	public bool xyCir = false;
	public bool yzCir = false;

	//The speed of the platforms in the circle
	public float cirSpeed = 1;

	//The width and height of the circle
	public float width = 5;
	public float height = 5;

	//How fast the platforms move along the third axis
	public float speed = 1;

	//All of the scripts for platform movement in children
	private PlatformGenericBehavior[] scripts;

	//All children of the circle group
	private Transform[] children;

	//How far along the third axis the circle group should move
	private float thirdAxisValue = 0;

	// Use this for initialization
	void Start () {
		//Grabs every platform behavior script
		scripts = GetComponentsInChildren<PlatformGenericBehavior> ();

		children = GetComponentsInChildren<Transform> ();

		//Gets the third axis value based on which circle is selected
		if (xzCir == true) {
			thirdAxisValue = transform.Find ("ThirdAxisValue").transform.position.y;
		}
		else if (xyCir == true) {
			thirdAxisValue = transform.Find ("ThirdAxisValue").transform.position.z;
		}
		else if (yzCir == true) {
			thirdAxisValue = transform.Find ("ThirdAxisValue").transform.position.x;
		}

		//Updates values in children
		for (int i = 0; i < scripts.Length; i++) {
			scripts [i].CCW = CCW;
			scripts [i].xzCir = xzCir;
			scripts [i].xyCir = xyCir;
			scripts [i].yzCir = yzCir;
			scripts [i].cirSpeed = cirSpeed;
			scripts [i].width = width;
			scripts [i].height = height;
			scripts [i].speed = speed;

			scripts [i].CircularGroupStart ();
		}

		for (int i = 0; i < children.Length; i++) {
			//Sets the appropriate game object transform
			if (children [i].name == "Position2") {
				//Sets value based on circular movement selected
				if (xzCir == true) {
					children [i].position = new Vector3 (children [i].position.x, thirdAxisValue, children [i].position.z);
				}
				else if (xyCir == true) {
					children [i].position = new Vector3 (children [i].position.x, children [i].position.y, thirdAxisValue);
				}
				else if (yzCir == true) {
					children [i].position = new Vector3 (thirdAxisValue, children [i].position.y, children [i].position.z);
				}
			}
		}
	}

	//Activates child platforms
	public void Toggle(){
		for (int i = 0; i < scripts.Length; i++) {
			scripts [i].Activate ();
		}
	}
}
