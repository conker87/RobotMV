using UnityEngine;
using System.Collections;

public class AnimateBombExplosion : MonoBehaviour {

	public float explosionTime = 2f;
	Vector3 originalScale = new Vector3(0.1f, 0.1f, 1f), targetScale = new Vector3(1.5f, 1.5f, 1f);

	float tick = 0.5f, nextTickTime;

	// Use this for initialization
	void Start () {

		StartCoroutine(IncreaseScaleOverTime (explosionTime));

	}
	
	IEnumerator IncreaseScaleOverTime(float time) {

		Vector3 originalScale = new Vector3(0.1f, 0.1f, 1f), targetScale = new Vector3(1.5f, 1.5f, 1f);

		float currentTime = 0.0f;

		do {
			
			transform.localScale = Vector3.Lerp (originalScale, targetScale, currentTime / time);
			currentTime += Time.deltaTime;

			yield return null;

		} while (currentTime <= time);

		Destroy (gameObject);

	}

	void OnTriggerEnter2D(Collider2D other) {

		Debug.Log("HIT SOMETHING -- " + other.gameObject);

		Enemy e;

		if ((e = other.gameObject.GetComponent<Enemy> ()) != null)
		{
			
			e.DamageHealth (10);

		}

	}

}
