using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Switch : MonoBehaviour {

	[Header("Switch Details")]
	[Range(0, 10)]
	public int		weaponLevel;

	[Header("System")]
	public SwitchState switchState;

	protected Projectile hit;
	protected Animator anim;

	protected virtual void Start() {

		anim = GetComponent<Animator> ();

	}

	protected virtual void Update() {

		if (switchState == SwitchState.ON) {

			anim.SetBool ("on", true);

		} else if (switchState == SwitchState.OFF) {
			
			anim.SetBool ("on", false);

		}

	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {

		hit = other.gameObject.GetComponent<Projectile> ();

		if (hit != null && hit.weaponLevel >= weaponLevel) {

			TriggerSwitch ();

		}

	}

	public virtual void TriggerSwitch() {
		
		if (Player.Current.CurrentWeapon.Level >= weaponLevel && Player.Current.CurrentWeapon.projectileType == ProjectileType.PLAYER) {

			if (switchState == SwitchState.OFF) {

				switchState = SwitchState.ON;

			}

		}

	}

}

public enum SwitchState { ON, OFF };