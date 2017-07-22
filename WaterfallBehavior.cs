using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicArsenal;

public class WaterfallBehavior : MonoBehaviour {

	//If the waterfall starts frozen
	public bool frozen = false;

	public GameObject waterfall;

	//The waterfall's collider associated with the trigger
	private BoxCollider waterfallCollider;

	//The speed variable for the water animation
	private Red_UVScroller[] speed;

	//The starting speed of the waterfall
	private float[] startSpeed;

	//The particle systems attached to the waterfall
	private ParticleSystem[] particles;

	// Use this for initialization
	void Start () {

		//Grabs the waterfall's collider
		waterfallCollider = transform.parent.gameObject.GetComponentInChildren<BoxCollider>();

		//Grabs all particle systems in the waterfall
		particles = waterfall.GetComponentsInChildren<ParticleSystem>();

		//Grabs the material scrolling scripts on the waterfall
		speed = waterfall.GetComponentsInChildren<Red_UVScroller>();

		//Creates an array the size of the speed array and fills it with the y speed values
		startSpeed = new float[speed.Length];

		for (int i = 0; i < startSpeed.Length; i++) {
			startSpeed [i] = speed [i].speedY;
		}

		//If the waterfall does not start frozen
		if (frozen == false) {
			
			//Disables the collider at start so objects can pass through
			waterfallCollider.enabled = false;

			//Plays the waterfall's sound
			waterfall.GetComponentInChildren<AudioSource> ().Play ();

			//Plays all particle systems
			for (int i = 0; i < particles.Length; i++) {
				particles [i].Play ();
			}

			//Sets scroll speed
			for (int i = 0; i < speed.Length; i++) {
				speed [i].speedY = startSpeed[i];
			}
		}
		//If the waterfall starts frozen
		else if (frozen == true) {

			//Enables the collider at start so objects cannot pass through
			waterfallCollider.enabled = true;

			//Stops the waterfall's sound
			waterfall.GetComponentInChildren<AudioSource> ().Stop ();

			//Stops all particle systems
			for (int i = 0; i < particles.Length; i++) {
				particles [i].Stop ();
			}

			//Sets scroll speed
			for (int i = 0; i < speed.Length; i++) {
				speed [i].speedY = 0;
			}
		}
	}

	void OnTriggerEnter(Collider col){
		
		//When hit with frost spell
		if (col.gameObject.tag == "Frost") {
			//Enables the collider so it is solid
			waterfallCollider.enabled = true;

			//Stops the waterfall's sound
			waterfall.GetComponentInChildren<AudioSource> ().Stop ();

			//Stops all particle systems
			for (int i = 0; i < particles.Length; i++) {
				particles [i].Stop ();
			}

			//Sets scroll speed
			for (int i = 0; i < speed.Length; i++) {
				speed [i].speedY = 0;
			}
		}

		//When hit with fire spell
		if (col.gameObject.tag == "Fire") {
			//Disables the collider so the waterfall can be passed through
			waterfallCollider.enabled = false;

			//Plays the waterfall's sound
			waterfall.GetComponentInChildren<AudioSource> ().Play ();

			//Plays all particle systems
			for (int i = 0; i < particles.Length; i++) {
				particles [i].Play ();
			}

			//Sets scroll speed
			for (int i = 0; i < speed.Length; i++) {
				speed [i].speedY = startSpeed[i];
			}
		}

		//Destroys spells on contact with water
		if (col.gameObject.layer == 8) {
			col.GetComponent<MagicProjectileModified> ().Destruction();
		}
	}
}
