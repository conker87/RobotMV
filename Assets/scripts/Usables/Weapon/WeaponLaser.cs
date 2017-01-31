using UnityEngine;
using System.Collections;

public class WeaponLaser : Weapon {

	// TODO: Sort out an animated and wobbly rendered line shader.

	[Header("Laser Settings")]
	public LayerMask geometryLayer;
	public float laserLength = 7f;

	public LineRenderer line;

	bool hasBeenFiring = false;

	[SerializeField]
	float attacksPerAttackLength = 10f, nextTickTime;

	[Header("EXTENDED")]
	[SerializeField] Switch s;
	[SerializeField] Entity e;
	[SerializeField] DoorProjectile d;

	protected override void Start () {

		base.Start ();

		line = GetComponent<LineRenderer> ();
		line.sortingLayerName = "Geometry";
		line.sortingOrder = -1;

	}
		
	public override void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {

		// Prevent the weapon from being used if the Player does not have the Weapon.
		if (!Player.Current.Weapon_Laser) {

			return;

		}

		// If the button to Fire is press down then set the attckLengthTime to the current time plus the current Attack Length modifier.
		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			CurrentAttackLength = InitialAttackLength * Player.Current.Weapon_Laser_AttackLengthMod;
			attackLengthTime = Time.time + CurrentAttackLength;

		}

		// As long as the current time is less than the calculated total attack length modifier then continue to fire the weapon.
		if (Time.time < attackLengthTime) {

			if (InputManager.Current.GetButton("Fire Weapon")) {

				// The Player cannot change their weapon during the firing state, this is less a gameplay issue more an issue with the line renderer.
				Player.Current.CanChangeWeapon = false;
				hasBeenFiring = true;

				RaycastHit2D hit = Physics2D.Raycast (ShootLocationPosition, Direction, laserLength, geometryLayer);

				line.enabled = true;
				line.sortingLayerName = "Projectiles";
				line.SetPosition (0, ShootLocationPosition);

				if (hit.collider != null) {

					line.numPositions = 3;

					// Set an extra position for the laser to .1 units more to make sure the laser looks like it's hitting the collider.
					line.SetPosition (1, hit.point);
					line.SetPosition (2, hit.point + (Direction.normalized * .1f));

					// If the collider of the hit point has a component of Entity then deal damage to the enemy 10 times a period of the total Attack Length.
					if ((e = hit.collider.gameObject.GetComponentInParent<Entity> ()) != null) {

						// Some destructable blocks will have the Entity component which allows the use of Bombs and Mega Bombs on them, these blocks are tagged as
						//	"Geometry"
						if (e.tag != "Geometry") {

							if (Time.time > nextTickTime) {
								
								int currentDamage = Mathf.RoundToInt (InitialDamage * Player.Current.Weapon_Laser_DamageMod);
								e.DamageHealth (currentDamage);

								nextTickTime = Time.time + (CurrentAttackLength / attacksPerAttackLength);

							}

						}

					}

					// If the collider of the hit point has a component of Switch and the level of the Laser is more than/equal to switch level then trigger it.
					if ((s = hit.collider.gameObject.GetComponentInParent<Switch> ()) != null) {

						if (Level >= s.weaponLevel) {
							s.TriggerSwitch ();
						}

					}

					// If the collider of the hit point has a component of Door, specifically the Projectile variant and the level of the Laser is more than/equal
					// 	to door level and it is not currently open then trigger it
					if ((d = hit.collider.gameObject.GetComponent<DoorProjectile> ()) != null) {

						if (Level >= d.doorLevel && d.IsDoorOpen () == false) {

							d.OpenDoor ();

						}

					}

				} else {

					// Without a hit point the laser just has 2 positions, the last one being in the direction of fire times the length of the laser.
					line.numPositions = 2;

					line.SetPosition (1, (Vector2) ShootLocationPosition + (Vector2) (Direction.normalized * laserLength));

				}

			} else {

				// If the laser has been firing then disable the laser, allow the Player to change weapons, add on the cooldown and reset the firing bool.
				if (hasBeenFiring) {

					line.enabled = false;
					Player.Current.CanChangeWeapon = true;

					// Prevent firing again until after cooldown time
					CurrentCooldown = InitialCooldown * Player.Current.Weapon_Laser_CooldownMod;
					cooldownTime = Time.time + InitialCooldown;

					hasBeenFiring = false;

				}

			}

		} else {
			
			// If the laser has been firing then disable the laser, allow the Player to change weapons, add on the cooldown and reset the firing bool.
			if (hasBeenFiring) {

				line.enabled = false;
				Player.Current.CanChangeWeapon = true;

				// Prevent firing again until after cooldown time
				CurrentCooldown = InitialCooldown * Player.Current.Weapon_Laser_CooldownMod;
				cooldownTime = Time.time + InitialCooldown;

				hasBeenFiring = false;

			}

		}



	}

}
