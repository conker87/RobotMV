using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetLocationBasedOnGameObjectName : MonoBehaviour {

	public float xLocationModifier = 15, yLocationModifier = 8.5f;

	void Start() {

		string name = gameObject.name;
		string[] split = name.Split (',');

		int x = 0, y = 0;

		if (int.TryParse (split [0], out x) && int.TryParse (split [1], out y)) {

			transform.position = new Vector2 (x * xLocationModifier, y * yLocationModifier);

		}

	}

}
