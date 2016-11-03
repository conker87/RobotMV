using UnityEngine;
using System.Collections;
using SmartLocalization;

public class ItemShurikenShield : Item {

	protected override void Update ()
	{
		
		base.Update ();

		UsableName = LanguageManager.Instance.GetTextValue ("ITEM_ShurikenShieldName");
		Description = LanguageManager.Instance.GetTextValue ("ITEM_ShurikenShieldDescription");

	}

	public override void Use () {

		if (Player.Current.inputManager.GetButtonDown ("Item")) {

			if (Time.time > nextShotTime) {

				if (Player.Current.EnergyTanks > 1 || Player.Current.Energy >= EnergyCost) {

					int random = Random.Range (0, Projectiles.Length);
					Vector3 pos = (transform.position - Player.Current.transform.position).normalized * 1f + Player.Current.transform.position;

					Projectile projectile = Instantiate (Projectiles [random], pos, Quaternion.identity) as Projectile;

					projectile.transform.SetParent (transform);

					nextShotTime = Time.time + AttackSpeed;
					Player.Current.DamageEnergy (EnergyCost);

				}

			}

		}

	}

}