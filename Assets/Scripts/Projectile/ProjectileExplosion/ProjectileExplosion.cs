using UnityEngine;
using System.Collections;

public class ProjectileExplosion : MonoBehaviour {

	public void OnDisable() {

		Destroy (gameObject);

	}

}
