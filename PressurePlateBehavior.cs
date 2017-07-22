using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateBehavior : MonoBehaviour {

	//Indicates if the plate is Active
	public bool plateActive = false;

	//The platforms activated by the pressure plate
	public GameObject[] platforms;

	//Rotation controlled by titable platforms
	public bool zRot = false;
	public bool yRot = false;
	public bool xRot = false;

	//The materials the bars use when the plate is active/inactive
	public Material activePlate, inactivePlate;

	// Use this for initialization
	void Start () {
		//If the plate starts inactive
		if (plateActive == false) {

			//Changes the plate to the inactive material
			GetComponent<Renderer> ().material = inactivePlate;
		}

		//If the plate starts active
		else if (plateActive == true) {

			//Activates the plate's animation
			GetComponentInParent<Animator>().SetBool("Active", true);

			//Changes the plate to the active material
			GetComponent<Renderer> ().material = activePlate;

			//Gets the plate's particle systems
			ParticleSystem plateParticle = GetComponentInChildren<ParticleSystem>();

			//Activates the plate's particle systems
			plateParticle.Play ();

			ParticleSystem[] elements = plateParticle.GetComponentsInChildren<ParticleSystem> ();

			for (int k = 0; k < elements.Length; k++) {
				elements [k].Play ();
			}

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
		//Stone on pressure plate when the plate is off
		if (col.gameObject.tag == "Stone" && plateActive == false) {

			//Sets the plate to active
			plateActive = true;

			//Changes the plate to the active material
			GetComponent<Renderer> ().material = activePlate;

			//Activates the plate's animation
			GetComponentInParent<Animator>().SetBool("Active", true);

			//Gets the plate's particle systems
			ParticleSystem plateParticle = GetComponentInChildren<ParticleSystem>();

			//Activates the plate's particle systems
			plateParticle.Play ();

			ParticleSystem[] elements = plateParticle.GetComponentsInChildren<ParticleSystem> ();
		
			for (int k = 0; k < elements.Length; k++) {
				elements [k].Play ();
			}

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

	void OnCollisionExit(Collision col){
		//Stone leaves pressure plate when the plate is on
		if (col.gameObject.tag == "Stone" && plateActive == true) {

			//Sets the plate to inactive
			plateActive = false;

			//Deactivates the plate's animation
			GetComponentInParent<Animator>().SetBool("Active", false);

			//Gets the plate's particle systems
			ParticleSystem plateParticle = GetComponentInChildren<ParticleSystem>();

			//Deactivates the plate's particle systems
			plateParticle.Stop ();

			ParticleSystem[] elements = plateParticle.GetComponentsInChildren<ParticleSystem> ();

			for (int k = 0; k < elements.Length; k++) {
				elements [k].Stop ();
			}

			//Changes the plate to the inactive material
			GetComponent<Renderer> ().material = inactivePlate;

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
