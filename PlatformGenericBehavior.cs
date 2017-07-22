using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenericBehavior : MonoBehaviour {

	//Positions the platform will move towards
	//Note: For circular movement the 0 index in the positions[] array should be the platform's starting position
	public Transform[] positions;

	//If the platform is active to move
	private bool active = false;

	//The speed of the movement
	public float speed;

	//The position the platform is moving to next
	private int nextPos;

	//The starting position of the platform
	//Used for circular movement
	//Note: For circular movement the 0 index in the positions[] array should be the platform's starting position
	private Transform startPos;

	//Verticle or horizontal circular movement
	//Currently only movement towards the unused axis will be accounted for outside of circular movements
	public bool xzCir = false;
	public bool xyCir = false;
	public bool yzCir = false;

	//Switches the direction of the clockwise movement
	public bool CCW = false;

	//The multiplier that changes the direction of movement for the platform
	private int CCWMultiplier = 1;

	//The number of Pi around a circle to offset the starting position by
	public float numPi = 0;

	//Rotational speed for circular movement
	public float cirSpeed;

	//Circle coordinates for circular transformations
	private float cirX;
	private float cirY;
	private float cirZ;

	//Width and height of circular movement
	//Width prioritizes X axis over Z axis and height is assigned based on which axis remains
	public float width;
	public float height;

	//The material to show the platform is active
	public Material platformActive;

	//The material to show the platform is inactive
	public Material platformInactive;

	//Time counter for circular movement
	private float timeCounter = 0;

	// Use this for initialization
	void Start () {

		//Gets the starting position of the platform
		startPos = positions[0];

		//Starts movement towards position at first index
		nextPos = 0;

		//If circular movement is selected
		if (xzCir || xyCir || yzCir) {
			
			//Sets the CCWMultiplier
			if (CCW == true) {
				CCWMultiplier = 1;
			} else {
				CCWMultiplier = -1;
			}

			//Temporary variables for setting the starting position of the platform
			float xTemp, yTemp, zTemp;

			if (xzCir) {
				//Offsets the starting position of the platform by the numPi entered
				xTemp = startPos.position.x + Mathf.Sin (timeCounter + (Mathf.PI * numPi)) * width * CCWMultiplier;
				yTemp = transform.position.y;
				zTemp = startPos.position.z + -(Mathf.Cos (timeCounter + (Mathf.PI * numPi)) * height) + height;

				//Sets the platforms position
				transform.position = new Vector3 (xTemp, yTemp, zTemp);
			}
			else if (xyCir) {

				//Offsets the starting position of the platform by the numPi entered
				xTemp = startPos.position.x + Mathf.Sin (timeCounter + (Mathf.PI * numPi)) * width * CCWMultiplier;
				yTemp = startPos.position.y + -(Mathf.Cos (timeCounter + (Mathf.PI * numPi)) * height) + height;
				zTemp = transform.position.z;

				//Sets the platforms position
				transform.position = new Vector3 (xTemp, yTemp, zTemp);
			}
			else if (yzCir) {
				//Offsets the starting position of the platform by the numPi entered
				xTemp = transform.position.x;
				yTemp = startPos.position.y + -(Mathf.Sin (timeCounter + (Mathf.PI * numPi)) * height) * CCWMultiplier;
				zTemp = startPos.position.z + -(Mathf.Cos (timeCounter + (Mathf.PI * numPi)) * width) + width;

				//Sets the platforms position
				transform.position = new Vector3 (xTemp, yTemp, zTemp);
			}
		}

		//If platform starts activated
		if (active == true) {
			//Grabs all edges of the platform
			MeshRenderer[] edges = GetComponentsInChildren<MeshRenderer> ();

			for (int i = 1; i < edges.Length; i++) {

				//Lights up platform edges
				edges [i].gameObject.GetComponent<Renderer> ().material = platformActive;

				//Gets particle systems attached to edges
				ParticleSystem[] glow = edges [i].gameObject.GetComponentsInChildren<ParticleSystem> ();

				//Plays particle systems
				for (int j = 0; j < glow.Length; j++) {
					glow [j].Play ();

					ParticleSystem[] elements = glow [j].GetComponentsInChildren<ParticleSystem> ();

					for (int k = 0; k < elements.Length; k++) {
						elements [k].Play ();
					}
				}
			}
		}

	}

	// Update is called once per frame
	void Update () {

		//Enables movement if the platform is active
		if (active) {

			//Non-Circular Movement
			if (xyCir == false && xzCir == false && yzCir == false) {
				
				//Moves between positions
				//If the platform has not arrived at its next position
				if (transform.position != positions [nextPos].position) {
					transform.position = Vector3.MoveTowards (transform.position, positions [nextPos].position, speed * Time.deltaTime);
				}
				//Platform has reached its target position
				else {
					//Increments nextPos to next position
					nextPos++;

					//If nextPos is at the end of the positions array
					if (nextPos == positions.Length) {
						//Resets nextPos to the start of the array
						nextPos = 0;
					}
				}
			}

			//Any circular movement
			else if (xzCir || xyCir || yzCir) {

				//Increment time counter
				timeCounter += Time.deltaTime * cirSpeed;

				//Sets the CCWMultiplier
				if (CCW == true) {
					CCWMultiplier = 1;
				}
				else {
					CCWMultiplier = -1;
				}

				//XZ circular movement
				if (xzCir) {
					//The x value to move towards relative to the platform's starting position
					cirX = startPos.position.x + Mathf.Sin (timeCounter + (Mathf.PI * numPi)) * width * CCWMultiplier;
					//Maintains the y position
					cirY = transform.position.y;
					//The z value to move towards relative to the platform's starting position
					//Add width so the starting position is on the circle
					cirZ = startPos.position.z + -(Mathf.Cos (timeCounter + (Mathf.PI * numPi)) * height) + height;

					//Sets the platform's new position
					transform.position = new Vector3 (cirX, cirY, cirZ);

					//Moves towards the Y direction
					if (transform.position.y != positions [nextPos].position.y) {
						Vector3 newY = new Vector3 (transform.position.x, positions [nextPos].position.y, transform.position.z);

						transform.position = Vector3.MoveTowards (transform.position, newY, speed * Time.deltaTime);
					}
					//Platform reached target Y value
					else {
						//Increments nextPos to next position
						nextPos++;

						//If nextPos is at the end of the positions array
						if (nextPos == positions.Length) {
							//Resets nextPos to the start of the array
							nextPos = 0;
						}
					}
				}

				//XY circular movement
				if (xyCir) {
					//The x value to move towards relative to the platform's starting position
					cirX = startPos.position.x + Mathf.Sin (timeCounter + (Mathf.PI * numPi)) * width * CCWMultiplier;
					//The y value to move towards relative to the platform's starting position
					//Add height so the starting position is the bottom of the circle
					cirY = startPos.position.y + -(Mathf.Cos (timeCounter + (Mathf.PI * numPi)) * height) + height;
					//Maintains the z position
					cirZ = transform.position.z;

					//Sets the platform's new position
					transform.position = new Vector3 (cirX, cirY, cirZ);

					//Moves towards the Z direction
					if (transform.position.z != positions [nextPos].position.z) {
						Vector3 newZ = new Vector3 (transform.position.x, transform.position.y, positions [nextPos].position.z);

						transform.position = Vector3.MoveTowards (transform.position, newZ, speed * Time.deltaTime);
					}
					//Platform reached target Z value
					else {
						//Increments nextPos to next position
						nextPos++;

						//If nextPos is at the end of the positions array
						if (nextPos == positions.Length) {
							//Resets nextPos to the start of the array
							nextPos = 0;
						}
					}
				}

				//YZ circular movement
				if (yzCir) {
					//Maintains the x position
					cirX = transform.position.x;
					//The y value to move towards relative to the platform's starting position
					cirY = startPos.position.y + -(Mathf.Sin (timeCounter + (Mathf.PI * numPi)) * height) * CCWMultiplier;
					//The z value to move towards relative to the platform's starting position
					//Add width so the starting position is on the circle
					cirZ = startPos.position.z + -(Mathf.Cos (timeCounter + (Mathf.PI * numPi)) * width) + width;

					//Sets the platform's new position
					transform.position = new Vector3 (cirX, cirY, cirZ);

					//Moves towards the X direction
					if (transform.position.x != positions [nextPos].position.x) {
						Vector3 newX = new Vector3 (positions [nextPos].position.x, transform.position.y, transform.position.z);

						transform.position = Vector3.MoveTowards (transform.position, newX, speed * Time.deltaTime);
					}
					//Platform reached target Z value
					else {
						//Increments nextPos to next position
						nextPos++;

						//If nextPos is at the end of the positions array
						if (nextPos == positions.Length) {
							//Resets nextPos to the start of the array
							nextPos = 0;
						}
					}
				}
			}

		}
	}

	//Activates/Deactivates the platform
	public void Activate(){
		//Activating the platform
		if (active == false) {

			//Sets active to true
			active = true;

			//Grabs all edges of the platform
			MeshRenderer[] edges = GetComponentsInChildren<MeshRenderer> ();

			for (int i = 1; i < edges.Length; i++) {

				//Lights up platform edges
				edges [i].gameObject.GetComponent<Renderer> ().material = platformActive;

				//Gets particle systems attached to edges
				ParticleSystem[] glow = edges [i].gameObject.GetComponentsInChildren<ParticleSystem> ();

				//Plays particle systems
				for (int j = 0; j < glow.Length; j++) {
					glow [j].Play ();

					ParticleSystem[] elements = glow [j].GetComponentsInChildren<ParticleSystem> ();

					for (int k = 0; k < elements.Length; k++) {
						elements [k].Play ();
					}
				}
			}
		}

		//Deactivating the platform
		else if (active == true) {

			//Sets active to false
			active = false;

			//Grabs all edges of the platform
			MeshRenderer[] edges = GetComponentsInChildren<MeshRenderer> ();

			for (int i = 1; i < edges.Length; i++) {

				//Lights up platform edges
				edges [i].gameObject.GetComponent<Renderer> ().material = platformInactive;

				//Gets particle systems attached to edges
				ParticleSystem[] glow = edges [i].gameObject.GetComponentsInChildren<ParticleSystem> ();

				//Stops particle systems
				for (int j = 0; j < glow.Length; j++) {
					glow [j].Stop ();

					ParticleSystem[] elements = glow [j].GetComponentsInChildren<ParticleSystem> ();

					for (int k = 0; k < elements.Length; k++) {
						elements [k].Stop ();
					}
				}
			}
		}
	}


	void OnTriggerEnter(Collider col){
		
		//Prevents the player or spells from changing the platform's direction
		if (col.gameObject.tag != "Player" && col.gameObject.layer != 8 /*The magic layer*/) {
			//Increments nextPos to next position
			nextPos++;

			//If nextPos is at the end of the positions array
			if (nextPos == positions.Length) {
				//Resets nextPos to the start of the array
				nextPos = 0;
			}
		}
		
		//When the player enters the platform
		if (col.tag == "Player") {
			//Platform becomes parent of objects that collide with it
			col.transform.parent = gameObject.transform;
		}
	}

	void OnTriggerExit(Collider col){

		//When the player leaves the platform
		if (col.transform.gameObject.tag == "Player") {
			//Sets parent back to self when object leaves platform
			col.transform.parent = null;
		}
	}

	public void CircularGroupStart(){

		//Gets the starting position of the platform
		startPos = positions[0];

		//If circular movement is selected
		if (xzCir || xyCir || yzCir) {

			//Sets the CCWMultiplier
			if (CCW == true) {
				CCWMultiplier = 1;
			} else {
				CCWMultiplier = -1;
			}

			//Temporary variables for setting the starting position of the platform
			float xTemp, yTemp, zTemp;

			if (xzCir) {
				//Offsets the starting position of the platform by the numPi entered
				xTemp = startPos.position.x + Mathf.Sin (timeCounter + (Mathf.PI * numPi)) * width * CCWMultiplier;
				yTemp = transform.position.y;
				zTemp = startPos.position.z + -(Mathf.Cos (timeCounter + (Mathf.PI * numPi)) * height) + height;

				//Sets the platforms position
				transform.position = new Vector3 (xTemp, yTemp, zTemp);
			}
			else if (xyCir) {

				//Offsets the starting position of the platform by the numPi entered
				xTemp = startPos.position.x + Mathf.Sin (timeCounter + (Mathf.PI * numPi)) * width * CCWMultiplier;
				yTemp = startPos.position.y + -(Mathf.Cos (timeCounter + (Mathf.PI * numPi)) * height) + height;
				zTemp = transform.position.z;

				//Sets the platforms position
				transform.position = new Vector3 (xTemp, yTemp, zTemp);
			}
			else if (yzCir) {
				//Offsets the starting position of the platform by the numPi entered
				xTemp = transform.position.x;
				yTemp = startPos.position.y + -(Mathf.Sin (timeCounter + (Mathf.PI * numPi)) * height) * CCWMultiplier;
				zTemp = startPos.position.z + -(Mathf.Cos (timeCounter + (Mathf.PI * numPi)) * width) + width;

				//Sets the platforms position
				transform.position = new Vector3 (xTemp, yTemp, zTemp);
			}
		}
	}
}

