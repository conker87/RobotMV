using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	public GameObject defaultState, offState;

	public SwitchState switchState;

	protected Projectile hit;


	protected virtual void Start() {

		defaultState.SetActive (true);

	}

	protected virtual void Update() {

		if (switchState == SwitchState.ON) {

			defaultState.SetActive (false);
			offState.SetActive (true);

		} else if (switchState == SwitchState.OFF) {
			
			defaultState.SetActive (true);
			offState.SetActive (false);

		}

	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {

		Debug.Log ("Enter");

		hit = other.gameObject.GetComponent<Projectile> ();

		if (hit != null) {

			if (hit.projectileType == ProjectileType.PLAYER) {

				if (switchState == SwitchState.OFF) {

					switchState = SwitchState.ON;

				}

			}

		}

	}

}

public enum SwitchState { ON, OFF };