using UnityEngine;
using System.Collections;

public class AnimateBombExplosion : MonoBehaviour {

	public float explosionTime = 1.5f;
	public Vector3 originalScale = new Vector3(0.1f, 0.1f, 1f), targetScale = new Vector3(3f, 3f, 3f);

	void Start () {

		StartCoroutine(IncreaseScaleOverTime (explosionTime));

	}
	
	IEnumerator IncreaseScaleOverTime(float time) {

		float currentTime = 0.0f;

		do {
			
			transform.localScale = Vector3.Lerp (originalScale, targetScale, currentTime / time);
			currentTime += Time.deltaTime;

			yield return null;

		} while (currentTime <= time);

		Destroy (gameObject);

	}

}
