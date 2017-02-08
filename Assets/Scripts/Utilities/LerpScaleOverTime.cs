using UnityEngine;
using System.Collections;

public class LerpScaleOverTime : MonoBehaviour {

	public float lerpTime = 1.5f;
	public bool getTimeFromProjectile = false;
	public Vector3 originalScale = new Vector3(0.1f, 0.1f, 1f), targetScale = new Vector3(3f, 3f, 3f);

	void Start () {

		if (getTimeFromProjectile) {

			lerpTime = GetComponent<ProjectileBase> ().DestroyInSeconds;

		}

		StartCoroutine(IncreaseScaleOverTime (lerpTime));

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
