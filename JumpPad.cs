using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

	//The multiplier for the player velocity
	public float boost = 3;

	void OnTriggerExit(Collider col){
		//Jump pad only works for player
		if(col.transform.gameObject.tag == "Player"){
			//Modifies player velocity by boost variable
			col.transform.gameObject.GetComponent<Rigidbody> ().velocity = col.transform.gameObject.GetComponent<Rigidbody> ().velocity * boost;
		}
	}
}
