using UnityEngine;
using System.Collections;

public class ItemEnergyShield : Item {
	
	ProjectileBase projectile;

	[SerializeField]
	int healthMax = 10, healthMaxMod, healthCurrent;

	bool currentlyEnabled = false;

	public override void Use () {

		if (stillOnCooldown) {

			return;

		}

		if (projectile == null) {

			int random = (Projectiles.Length > 1) ? Random.Range (0, Projectiles.Length) : 0;

			projectile = Instantiate (Projectiles [random], Player.Current.transform.position, Quaternion.identity) as ProjectileBase;
			projectile.transform.SetParent (transform);

		}

		Enable ();
		StartAttackLength ();


	}

	protected override void Start() {

		base.Start ();

		ResetHealth ();

	}

	protected override void Update() {

		base.Update ();

		if (currentlyEnabled) {
			projectile.transform.position = Player.Current.transform.position;
		}

		if (Player.Current != null) {

			if ((!Player.Current.CHEAT_ENERGY_SHIELD_NO_DURATION_LIMIT && (currentlyEnabled && Time.time > attackLengthTime)) ||
					(!Player.Current.CHEAT_ENERGY_SHIELD_INFINITE_HEALTH && (currentlyEnabled && healthCurrent < 1))) {

				Disable ();
				StartCooldown ();
				ResetHealth ();

			}

		}

	}

	public void DamageHealth(int damageHealthValue) {

		healthCurrent -= damageHealthValue;

	}

	void ResetHealth() {

		healthMaxMod = (int)(healthMax * Player.Current.ItemEnergyShieldDamageMod);
		healthCurrent = healthMaxMod;


	}

	void StartAttackLength() {

		CurrentAttackLength = InitialAttackLength * Player.Current.ItemEnergyShieldAttackLengthMod;
		attackLengthTime = Time.time + CurrentAttackLength;

	}

	void StartCooldown() {

		CurrentCooldown = InitialCooldown * Player.Current.ItemEnergyShieldCooldownMod;
		cooldownTime = Time.time + CurrentCooldown;

	}

	void Enable() {

		currentlyEnabled = true;

		projectile.gameObject.SetActive (currentlyEnabled);

	}

	void Disable() {

		currentlyEnabled = false;
		projectile.gameObject.SetActive (currentlyEnabled);

	}

}