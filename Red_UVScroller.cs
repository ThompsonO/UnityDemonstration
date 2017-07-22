//This code was translated from a javascript script that came with a purchased Unity asset into C# by me

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_UVScroller : MonoBehaviour {

	public int targetMaterialSlot = 0;

	//The speed at which the material scrolls
	public float speedY = 0.5f;
	public float speedX = 0;

	//The amount of scrolling necessary since last update
	private float timeWentX = 0;
	private float timeWentY = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//The amount of scrolling the material will need to do
		timeWentY += Time.deltaTime * speedY;
		timeWentX += Time.deltaTime * speedX;

		//Scrolls the texture
		GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", new Vector2 (timeWentX, timeWentY));
	}
}
