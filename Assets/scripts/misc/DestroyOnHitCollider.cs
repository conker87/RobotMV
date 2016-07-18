using UnityEngine;
using System.Collections;

public class DestroyOnHitCollider : MonoBehaviour {

	public string layer;

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.layer == LayerMask.NameToLayer(layer)) {

			Destroy (gameObject);

		}

	}

}
