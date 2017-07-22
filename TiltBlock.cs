using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltBlock : MonoBehaviour {

	//The angle values that will be iterated between
	public float[] zVals;
	public float[] yVals;
	public float[] xVals;

	//The speed at which roatation occurs
	public float speed = 1;

	//The index for the corresponding Vals array
	private int zPos = 0;
	private int yPos = 0;
	private int xPos = 0;

	//Used for determining the angle to rotate towards
	private Quaternion zRot;
	private Quaternion yRot;
	private Quaternion xRot;

	void Start(){

		//Sets starting rotation goals
		zRot = Quaternion.AngleAxis (zVals [zPos], (new Vector3 (0, 0, 1)));
		yRot = Quaternion.AngleAxis (yVals [yPos], (new Vector3 (0, 1, 0)));
		xRot = Quaternion.AngleAxis (xVals [xPos], (new Vector3 (1, 0, 0)));
			
		//Sets starting rotation
		transform.eulerAngles = new Vector3(xVals [xPos], yVals [yPos],zVals [zPos]);
	}

	void Update(){
		//If the platform hasn't reached the goal z rotation
		if(transform.rotation.z != zVals[zPos]){
			//Rotates along the z axis
			transform.rotation = Quaternion.RotateTowards(transform.rotation, zRot, speed*Time.deltaTime);
		}

		//If the platform hasn't reached the goal y rotation
		if(transform.rotation.y != yVals[yPos]){
			//Rotates along the y axis
			transform.rotation = Quaternion.RotateTowards(transform.rotation, yRot, speed*Time.deltaTime);
		}

		//If the platform hasn't reached the goal x rotation
		if(transform.rotation.x != xVals[xPos]){
			//Rotates along the x axis
			transform.rotation = Quaternion.RotateTowards(transform.rotation, xRot, speed*Time.deltaTime);
		}
	}

	//Rotates the platform on the Z axis
	public void RotateZ(){
		//Increments zPos
		zPos++;

		if (zPos >= zVals.Length) {
			zPos = 0;
		}

		//Sets the goal rotation
		zRot = Quaternion.AngleAxis (zVals [zPos], (new Vector3 (0, 0, 1)));
	}

	//Rotates the platform on the Y axis
	public void RotateY(){
		//Increments yPos
		yPos++;

		if (yPos >= yVals.Length) {
			yPos = 0;
		}

		//Sets the goal rotation
		yRot = Quaternion.AngleAxis (yVals [yPos], (new Vector3 (0, 1, 0)));
	}

	//Rotates the platform on the X axis
	public void RotateX(){
		//Increments xPos
		xPos++;

		if (xPos >= xVals.Length) {
			xPos = 0;
		}

		//Sets the goal rotation
		xRot = Quaternion.AngleAxis (xVals [xPos], (new Vector3 (1, 0, 0)));
	}

	void OnTriggerEnter(Collider col){
		//Platform becomes parent of objects that collide with it
		col.transform.parent = gameObject.transform;
	}

	void OnTriggerExit(Collider col){
		//Sets parent back to self when object leaves platform
		col.transform.parent = null;

	}
}
