using UnityEngine;
using System.Collections;

public class SwitchResetInSeconds : Switch {

	public float resetInSeconds = 5f;
	[SerializeField]
	float resetTime;

	protected override void Update() {

		base.Update ();

		if (Time.time > resetTime && switchState == SwitchState.ON) {

			switchState = SwitchState.OFF;

		}

	}

	protected override void OnTriggerEnter2D(Collider2D other) {

		hit = other.gameObject.GetComponent<Projectile> ();

		if (hit != null && hit.projectileType == ProjectileType.PLAYER && switchState == SwitchState.OFF) {

			switchState = SwitchState.ON;
			resetTime = Time.time + resetInSeconds;

		}

	}

}
