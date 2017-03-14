using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DetectRoomForLoading : MonoBehaviour {

	BoxCollider2D col;
	Transform child;

	// Use this for initialization
	void Start () {

		col = GetComponent<BoxCollider2D> ();
		child = transform.GetChild (0);

		Vector3 middle = new Vector3( (transform.localPosition.x + child.localPosition.x) / 2f , (transform.localPosition.y + child.localPosition.y) / 2f);

		col.offset = middle;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
