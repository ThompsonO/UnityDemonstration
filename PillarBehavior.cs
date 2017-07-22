using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarBehavior : MonoBehaviour {

	//Indicates if the pillar is Active
	public bool pillarActive = false;

	//The platforms activated by the pillar
	public GameObject[] platforms;

	//Rotation controlled by titable platforms
	public bool zRot = false;
	public bool yRot = false;
	public bool xRot = false;

	//The location for the floating charge above the pillar
	public Transform chargeLocation;

	//The GameObjects used for the charge when the pillar becomes active and inactive
	public GameObject activeCharge, inactiveCharge;

	//The bars on all sides of the pillar
	public GameObject bar1, bar2;

	//The materials the bars use when the pillar is active/inactive
	public Material activeBar, inactiveBar;

	//The current charge that is atop the pillar
	private GameObject charge;

	// Use this for initialization
	void Start () {
		//If the pillar starts inactive
		if (pillarActive == false) {
			//Instantiates an inactive charge
			charge = Instantiate (inactiveCharge, chargeLocation.position, Quaternion.identity, chargeLocation);

			//Changes the indicator bars on the pillar to the inactive material
			bar1.GetComponent<Renderer> ().material = inactiveBar;
			bar2.GetComponent<Renderer> ().material = inactiveBar;

		}
		//If the pillar starts active
		else if (pillarActive == true) {
			//Instantiates an active charge
			charge = Instantiate (activeCharge, chargeLocation.position, Quaternion.identity, chargeLocation);

			//Changes the indicator bars on the pillar to the active material
			bar1.GetComponent<Renderer> ().material = activeBar;
			bar2.GetComponent<Renderer> ().material = activeBar;

			//Sets all platforms to active
			for (int i = 0; i < platforms.Length; i++) {
				//Sets platforms to active
				if (platforms [i].GetComponent<PlatformGenericBehavior> () != null) {
					platforms [i].GetComponent<PlatformGenericBehavior> ().Activate ();
				}
				//Sets circle groups to active
				else if (platforms [i].GetComponent<PlatformCircleGroupSettings> () != null) {
					platforms [i].GetComponent<PlatformCircleGroupSettings> ().Toggle ();
				}
				//Tilts tiltable platforms
				else if (platforms [i].GetComponent<TiltBlock> () != null) {
					//Rotates the platform along the appropriate axis
					if (zRot == true) {
						platforms [i].GetComponent<TiltBlock> ().RotateZ ();
					}
					if (yRot == true) {
						platforms [i].GetComponent<TiltBlock> ().RotateY ();
					}
					if (xRot == true) {
						platforms [i].GetComponent<TiltBlock> ().RotateX ();
					}
				}
			}
		}
	}

	void OnCollisionEnter(Collision col)
	{
		//Hit with lightning when the pillar is off
		if (col.gameObject.tag == "Lightning" && pillarActive == false) {

			//Sets the pillar to active
			pillarActive = true;

			//Destroys current charge and replaces it with an instance of the active charge
			Destroy (charge);
			charge = Instantiate (activeCharge, chargeLocation.position, Quaternion.identity, chargeLocation);

			//Changes the indicator bars on the pillar to the active material
			bar1.GetComponent<Renderer> ().material = activeBar;
			bar2.GetComponent<Renderer> ().material = activeBar;

			//Sets all platforms to active
			for (int i = 0; i < platforms.Length; i++) {
				//Sets platforms to active
				if (platforms [i].GetComponent<PlatformGenericBehavior> () != null) {
					platforms [i].GetComponent<PlatformGenericBehavior> ().Activate ();
				}
				//Sets circle groups to active
				else if (platforms [i].GetComponent<PlatformCircleGroupSettings> () != null) {
					platforms [i].GetComponent<PlatformCircleGroupSettings> ().Toggle ();
				}
				//Tilts tiltable platforms
				else if (platforms [i].GetComponent<TiltBlock> () != null) {
					//Rotates the platform along the appropriate axis
					if (zRot == true) {
						platforms [i].GetComponent<TiltBlock> ().RotateZ ();
					}
					if (yRot == true) {
						platforms [i].GetComponent<TiltBlock> ().RotateY ();
					}
					if (xRot == true) {
						platforms [i].GetComponent<TiltBlock> ().RotateX ();
					}
				}
			}
		}
		//Hit with lightning when the pillar is on
		else if (col.gameObject.tag == "Lightning" && pillarActive == true) {

			//Sets the pillar to inactive
			pillarActive = false;

			//Destroys current charge and replaces it with an instance of the inactive charge
			Destroy (charge);
			charge = Instantiate (inactiveCharge, chargeLocation.position, Quaternion.identity, chargeLocation);

			//Changes the indicator bars on the pillar to the inactive material
			bar1.GetComponent<Renderer> ().material = inactiveBar;
			bar2.GetComponent<Renderer> ().material = inactiveBar;

			//Sets all platforms to inactive
			for (int i = 0; i < platforms.Length; i++) {
				//Sets platforms to inactive
				if (platforms [i].GetComponent<PlatformGenericBehavior> () != null) {
					platforms [i].GetComponent<PlatformGenericBehavior> ().Activate ();
				}
				//Sets circle groups to inactive
				else if (platforms [i].GetComponent<PlatformCircleGroupSettings> () != null) {
					platforms [i].GetComponent<PlatformCircleGroupSettings> ().Toggle ();
				}
				//Tilts tiltable platforms
				else if (platforms [i].GetComponent<TiltBlock> () != null) {
					//Rotates the platform along the appropriate axis
					if (zRot == true) {
						platforms [i].GetComponent<TiltBlock> ().RotateZ ();
					}
					if (yRot == true) {
						platforms [i].GetComponent<TiltBlock> ().RotateY ();
					}
					if (xRot == true) {
						platforms [i].GetComponent<TiltBlock> ().RotateX ();
					}
				}
			}
		}
	}
}
