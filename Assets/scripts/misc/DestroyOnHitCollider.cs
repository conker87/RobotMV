using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyOnHitCollider : MonoBehaviour {

	public List<string> layers = new List<string>();

	void OnTriggerEnter2D(Collider2D other) {
		
		foreach (string l in layers) {

			if (other.gameObject.layer == LayerMask.NameToLayer (l)) {

				Destroy (other.gameObject);

			}

		}

	}

}
