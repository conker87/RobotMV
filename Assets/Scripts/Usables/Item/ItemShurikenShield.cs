using UnityEngine;
using System.Collections;

public class ItemShurikenShield : Item {

	public override void Use () {

		if (Player.Current.ShurikenShield) {

			if (Time.time > nextShotTime && Player.Current.inputManager.GetButtonDown ("Item") && ((Player.Current.EnergyTanks > 1) || (Player.Current.Energy >= EnergyCost))) {

				int random = Random.Range (0, Projectiles.Length);
				Vector3 pos = ((Vector2) transform.position - Player.Current.position).normalized * 1f + Player.Current.position;

				Projectile projectile = Instantiate (Projectiles [random], pos, Quaternion.identity) as Projectile;

				projectile.transform.SetParent (transform);

				nextShotTime = Time.time + AttackSpeed;
				Player.Current.DamageEnergy (EnergyCost);

			}

		}
	}

}