﻿using UnityEngine;
using System.Collections;

public class SwitchResetInSeconds : Switch {

	[Header("Reset Details")]
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

		if (hit != null && hit.weaponLevel >= weaponLevel && hit.projectileType == ProjectileType.PLAYER && switchState == SwitchState.OFF) {

			switchState = SwitchState.ON;
			resetTime = Time.time + resetInSeconds;

		}

	}

	public override void TriggerSwitch() {

		if (Player.Current.CurrentWeapon.weaponLevel >= weaponLevel && Player.Current.CurrentWeapon.projectileType == ProjectileType.PLAYER) {

			if (switchState == SwitchState.OFF) {

				switchState = SwitchState.ON;
				resetTime = Time.time + resetInSeconds;

			}

		}

	}
}
