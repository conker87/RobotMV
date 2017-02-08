using UnityEngine;
using System.Collections;

public class LerpColourOverTime : MonoBehaviour {

	public float duration;
	float t;

	public bool getDurationFromProjectile = false;

	public Color colorFrom, colorTo;

	Renderer r;

	void Start() {

		if (getDurationFromProjectile) {

			duration = GetComponent<ProjectileBase> ().DestroyInSeconds;

		}

		r = GetComponent<SpriteRenderer> ();

	}

	void Update () {

		ChangeColour ();

	}

	void ChangeColour() {

		r.material.color = Color.Lerp (colorFrom, colorTo, t);

		if (t < 1) {
			
			t += Time.deltaTime / duration;

		}

	}

}
