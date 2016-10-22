using UnityEngine;
using System.Collections;

public class ChangeTagOfChildrenOnStartup : MonoBehaviour {

	Transform[] children;

	void Start () {
	
		children = GetComponentsInChildren<Transform> ();

		foreach (Transform t in children) {

			if (t == transform) {
				continue;
			}

			t.gameObject.tag = "Geometry";

		}

	}

}
