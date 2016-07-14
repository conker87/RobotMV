using UnityEngine;
using System.Collections;

public class DestroyOnHitCollider : MonoBehaviour {

	public string geometryTag = "Geometry";

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == geometryTag) {

			Destroy (gameObject);

		}

	}

}
