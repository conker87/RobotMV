using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSharedMaterialRenderColour : MonoBehaviour {

	Renderer rend;
	public Color color;

	// Use this for initialization
	void Start () {
	
		rend = GetComponent<Renderer> ();
		rend. = color;

	}
	
	// Update is called once per frame
	void Update () {

		rend.sharedMaterial.color = color;

	}

}
