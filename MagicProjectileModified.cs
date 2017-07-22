using UnityEngine;
using System.Collections;

namespace MagicArsenal
{
public class MagicProjectileModified : MonoBehaviour
{
//Code block provided with Unity Asset---------------------------------------------------------------------------------------------------------------------------------------------
	public GameObject impactParticle;
	public GameObject projectileParticle;
	public GameObject muzzleParticle;
	public GameObject[] trailParticles;
	[HideInInspector]
	public Vector3 impactNormal; //Used to rotate impactparticle.
	private bool hasCollided = false;
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public bool LeftProjectile = false;
	public bool RightProjectile = false;
	public GameObject player;


//Code block provided with Unity Asset---------------------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
		projectileParticle.transform.parent = transform;
		if (muzzleParticle){
			muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
			Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
		}
	}

	void OnCollisionEnter(Collision hit)
	{
		if (!hasCollided)
		{
			{	
				hasCollided = true;
				//transform.DetachChildren();
				impactParticle = Instantiate (impactParticle, transform.position, Quaternion.FromToRotation (Vector3.up, impactNormal)) as GameObject;
				//Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);

				if (hit.gameObject.tag == "Destructible") { // Projectile will destroy objects tagged as Destructible
					Destroy (hit.gameObject);
				}


				//yield WaitForSeconds (0.05);
				foreach (GameObject trail in trailParticles) {
					GameObject curTrail = transform.Find (projectileParticle.name + "/" + trail.name).gameObject;
					curTrail.transform.parent = null;
					Destroy (curTrail, 3f);
				}
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

				//Lets the player script know the projectile has been destroyed
				if (LeftProjectile == true) {
					player.GetComponent<MagicFireProjectileModified> ().LeftProjectile = false;
					//Creates JumpPad on impact
					if (this.tag == "Jump") {
						player.GetComponent<MagicFireProjectileModified> ().CreateJumpPadLeft ();
					}
					//Creates Platform on impact
					else if (this.tag == "PlatformSpell") {
						player.GetComponent<MagicFireProjectileModified> ().CreatePlatformLeft ();
					}
				}
				else if (RightProjectile == true) {
					player.GetComponent<MagicFireProjectileModified> ().RightProjectile = false;
					//Creates JumpPad on impact
					if (this.tag == "Jump") {
						player.GetComponent<MagicFireProjectileModified> ().CreateJumpPadRight();
					}
					//Creates Platform on impact
					else if (this.tag == "PlatformSpell") {
						player.GetComponent<MagicFireProjectileModified> ().CreatePlatformRight ();
					}
				}

				
//Code block provided with Unity Asset---------------------------------------------------------------------------------------------------------------------------------------------
				Destroy (projectileParticle, 3f);
				Destroy (impactParticle, 5f);
				Destroy (gameObject);
				//projectileParticle.Stop();

				ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem> ();
				//Component at [0] is that of the parent i.e. this object (if there is any)
				for (int i = 1; i < trails.Length; i++) {
					ParticleSystem trail = trails [i];
					if (!trail.gameObject.name.Contains ("Trail"))
						continue;

					trail.transform.SetParent (null);
					Destroy (trail.gameObject, 2);
				}
			}
		}
	}
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		//Allows other scripts to call for the destruction of the projectile
		public void Destruction(){
			impactParticle = Instantiate (impactParticle, transform.position, Quaternion.FromToRotation (Vector3.up, impactNormal)) as GameObject;

			foreach (GameObject trail in trailParticles) {
				GameObject curTrail = transform.Find (projectileParticle.name + "/" + trail.name).gameObject;
				curTrail.transform.parent = null;
				Destroy (curTrail, 3f);
			}

			//Lets the player script know the projectile has been destroyed
			if (LeftProjectile == true) {
				player.GetComponent<MagicFireProjectileModified> ().LeftProjectile = false;
				//Creates JumpPad on impact
				if (this.tag == "Jump") {
					player.GetComponent<MagicFireProjectileModified> ().CreateJumpPadLeft ();
				}
				//Creates Platform on impact
				else if (this.tag == "PlatformSpell") {
					player.GetComponent<MagicFireProjectileModified> ().CreatePlatformLeft ();
				}
			}
			else if (RightProjectile == true) {
				player.GetComponent<MagicFireProjectileModified> ().RightProjectile = false;
				//Creates JumpPad on impact
				if (this.tag == "Jump") {
					player.GetComponent<MagicFireProjectileModified> ().CreateJumpPadRight();
				}
				//Creates Platform on impact
				else if (this.tag == "PlatformSpell") {
					player.GetComponent<MagicFireProjectileModified> ().CreatePlatformRight ();
				}
			}

			Destroy (projectileParticle, 3f);
			Destroy (impactParticle, 5f);
			Destroy (gameObject);
			//projectileParticle.Stop();

			ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem> ();
			//Component at [0] is that of the parent i.e. this object (if there is any)
			for (int i = 1; i < trails.Length; i++) {
				ParticleSystem trail = trails [i];
				if (!trail.gameObject.name.Contains ("Trail"))
					continue;

				trail.transform.SetParent (null);
				Destroy (trail.gameObject, 2);
			}

		}

}
}