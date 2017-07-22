using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlockBehavior : MonoBehaviour {

	public PhysicMaterial ice;
	public PhysicMaterial stone;
	public Material icyStone;
	public Material normal;

	void OnCollisionEnter(Collision col)
	{
		//Hit with a frost spell
		if (col.gameObject.tag == "Frost") {
			//Sets the PhysicMaterial to ice
			GetComponent<BoxCollider>().material = ice;
			GetComponent<Renderer> ().material = icyStone;
		}
		//Hit with a fire spell
		else if (col.gameObject.tag == "Fire") {
			//Sets the PhysicMaterial to stone
			GetComponent<BoxCollider>().material = stone;
			GetComponent<Renderer> ().material = normal;
		}
	}
}
