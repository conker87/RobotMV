using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableInSeconds : MonoBehaviour {

	public bool isOn = false, fadeOut = true;
	public float fadeOutOverTime = 3f;
	float time;

	Text text;
	Outline outline;

	void Start() {

		gameObject.SetActive (false);

		text = GetComponent<Text> ();
		outline = GetComponent<Outline> ();

	}

	// Update is called once per frame
	void Update () {

		if (isOn) {

			if (Time.time > time) {

				isOn = false;

				StopCoroutine(FadeTo(0.0f, fadeOutOverTime));

				if (!fadeOut) {

					transform.parent.gameObject.SetActive (false);

				} else {

					StartCoroutine (FadeTo (0.0f, fadeOutOverTime));

				}
					
			}

		}

	}

	public void Reset(float seconds) {

		gameObject.SetActive (true);
		transform.parent.gameObject.SetActive (true);

		isOn = true;
		time = Time.time + seconds;

	}

	IEnumerator FadeTo(float aValue, float aTime) {
		
		float alpha = text.color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
			
			Color textColor = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(alpha, aValue, t)), 
			outlineColor =  new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, Mathf.Lerp(alpha, aValue, t));
			text.color = textColor;
			outline.effectColor = outlineColor;

			yield return null;

		}

		transform.parent.gameObject.SetActive (false);

	}

}
