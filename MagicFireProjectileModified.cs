using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace MagicArsenal
{
	public class MagicFireProjectileModified : MonoBehaviour 
	{
		RaycastHit hit;
		public GameObject[] projectiles;

		public GameObject[] spawn;
		public GameObject magicPlatform;
		public GameObject jumpPad;

		public Transform spawnPositionLeft;
		public Transform spawnPositionRight;
		[HideInInspector]
		public bool LeftProjectile = false;
		public bool RightProjectile = false;
		public int currentProjectileLeft = 0;
		public int currentProjectileRight = 0;
		public float speed = 1000;
		private GameObject currentLeftHand;
		private GameObject currentRightHand;
		private GameObject projectileFiredLeft;
		private GameObject projectileFiredRight;
		static private GameObject lightPlatform;
		static private GameObject jumpPadObject;


		void Start () 
		{
			//Spawns a spell charge in each hand of the player
			currentLeftHand = Instantiate(spawn[currentProjectileLeft], spawnPositionLeft.position, Quaternion.identity, spawnPositionLeft);
			currentRightHand = Instantiate(spawn[currentProjectileRight], spawnPositionRight.position, Quaternion.identity, spawnPositionRight);
		}

		void Update () 
		{
			//Cycles spells in left hand
			if (Input.GetKeyDown(KeyCode.Q))
			{
				nextEffectLeft();
			}

			//Cycles spells in right hand
			if (Input.GetKeyDown(KeyCode.E))
			{
				nextEffectRight();
			}

		
			//Primary mouse fire
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				//If there is not currently a left projectile in existence
				if (LeftProjectile == false) {
					//Only fires if pointing at an object
//Code block provided with Unity Asset---------------------------------------------------------------------------------------------------------------------------------------------
					if (!EventSystem.current.IsPointerOverGameObject ()) {
						if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f)) {//Last Value is max distance, change to Mathf.Infinity if limit needs to be removed
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
							//Destroys old magic platform is a new platform projectile is fired
							if (projectiles [currentProjectileLeft].tag == "PlatformSpell") {
								Destroy(lightPlatform);
							}

							//Creates a new projectile
							projectileFiredLeft = Instantiate (projectiles [currentProjectileLeft], spawnPositionLeft.position, Quaternion.identity) as GameObject;
//Code block provided with Unity Asset---------------------------------------------------------------------------------------------------------------------------------------------
							projectileFiredLeft.transform.LookAt (hit.point);
							projectileFiredLeft.GetComponent<Rigidbody> ().AddForce (projectileFiredLeft.transform.forward * speed);
							projectileFiredLeft.GetComponent<MagicProjectileModified> ().impactNormal = hit.normal;
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
							//Sets LeftProjectile to true
							projectileFiredLeft.GetComponent<MagicProjectileModified> ().LeftProjectile = true;
							//Sets the associated player of the projectile to this player
							projectileFiredLeft.GetComponent<MagicProjectileModified> ().player = this.gameObject;
							//Left Projectile exists
							LeftProjectile = true;
						}  
					}
				}
				//Creates a magic platform on second click of platform spell
				else if (projectileFiredLeft.tag == "PlatformSpell") {
					CreatePlatformLeft ();
				}

			}

			//Secondary mouse fire
			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				//If there is not currently a right projectile in existence
				if (RightProjectile == false) {
					//Only fires if pointing at an object
					if (!EventSystem.current.IsPointerOverGameObject ()) {
						if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f)) {
							//Destroys old magic platform is a new platform projectile is fired
							if (projectiles [currentProjectileRight].tag == "PlatformSpell") {
								Destroy(lightPlatform);
							}

							//Creates a new projectile
							projectileFiredRight = Instantiate (projectiles [currentProjectileRight], spawnPositionRight.position, Quaternion.identity) as GameObject;

							projectileFiredRight.transform.LookAt (hit.point);
							projectileFiredRight.GetComponent<Rigidbody> ().AddForce (projectileFiredRight.transform.forward * speed);
							projectileFiredRight.GetComponent<MagicProjectileModified> ().impactNormal = hit.normal;

							//Sets RightProjectile to true
							projectileFiredRight.GetComponent<MagicProjectileModified> ().RightProjectile = true;
							//Sets the associated player of the projectile to this player
							projectileFiredRight.GetComponent<MagicProjectileModified> ().player = this.gameObject;
							//Right Projectile exists
							RightProjectile = true;
						}  
					}
				}
				//Creates a magic platform on second click of platform spell
				else if (projectileFiredRight.tag == "PlatformSpell") {
					CreatePlatformRight ();
				}

			}
				
		}

		//Cycles through spell charges in left hand
		public void nextEffectLeft()
		{
			//Increments projectile index
			if (currentProjectileLeft < projectiles.Length - 1) {
				currentProjectileLeft++;
			}
			//Sets index to 0 when at the end of the array
			else {
				currentProjectileLeft = 0;
			}

			//Destroys left hand charge
			Destroy (currentLeftHand);
			//Creates new left hand charge
			currentLeftHand = Instantiate(spawn[currentProjectileLeft], spawnPositionLeft.position, Quaternion.identity, spawnPositionLeft);
		}

		//Cycles through spell charges in right hand
		public void nextEffectRight()
		{
			//Increments projectile index
			if (currentProjectileRight < projectiles.Length - 1) {
				currentProjectileRight++;
			}
			//Sets index to 0 when at the end of the array
			else {
				currentProjectileRight = 0;
			}

			//Destroys right hand charge
			Destroy (currentRightHand);
			//Creates right hand charge
			currentRightHand = Instantiate(spawn[currentProjectileRight], spawnPositionRight.position, Quaternion.identity, spawnPositionRight);
		}

		//Allows for other scripts to change the speed of spells
//Code block provided with Unity Asset---------------------------------------------------------------------------------------------------------------------------------------------
		public void AdjustSpeed(float newSpeed)
		{
			speed = newSpeed;
		}
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		//Creates jump pad
		public void CreateJumpPadLeft(){
			//Destroys previous jump pad
			Destroy (jumpPadObject);
			//Creates new jump pad
			jumpPadObject = Instantiate (jumpPad);
			//Sets jump pad position to spell collision position
			jumpPadObject.transform.position = projectileFiredLeft.transform.position;
		}

		//Creates jump pad
		public void CreateJumpPadRight(){
			//Destroys previous jump pad
			Destroy (jumpPadObject);
			//Creates new jump pad
			jumpPadObject = Instantiate (jumpPad);
			//Sets jump pad position to spell collision position
			jumpPadObject.transform.position = projectileFiredRight.transform.position;
		}

		//Creates a magic platform
		public void CreatePlatformLeft(){
			//Destroys previous magic platform
			Destroy (lightPlatform);
			//Creates new magic platform
			lightPlatform = Instantiate (magicPlatform);
			//Sets magic platform position to spell collision position
			lightPlatform.transform.position = projectileFiredLeft.transform.position;
		}

		//Creates a magic platform
		public void CreatePlatformRight(){
			//Destroys previous magic platform
			Destroy (lightPlatform);
			//Creates new magic platform
			lightPlatform = Instantiate (magicPlatform);
			//Sets magic platform position to spell collision position
			lightPlatform.transform.position = projectileFiredRight.transform.position;
		}
	}
}