using UnityEngine;
using System.Collections;
using SmartLocalization;

public class ItemShurikenShield : Item {

	protected override void Update ()
	{
		
		base.Update ();

		UsableNameLocalisationID = LanguageManager.Instance.GetTextValue ("ITEM_ShurikenShieldName");
		DescriptionLocalisationID = LanguageManager.Instance.GetTextValue ("ITEM_ShurikenShieldDescription");

	}

	public override void Use () {

		if (Player.Current.inputManager.GetButtonDown ("Item")) {

			if (Time.time > nextShotTime) {

				int random = Random.Range (0, Projectiles.Length);
				Vector3 pos = (transform.position - Player.Current.transform.position).normalized * 1f + Player.Current.transform.position;

				Projectile projectile = Instantiate (Projectiles [random], pos, Quaternion.identity) as Projectile;

				projectile.transform.SetParent (transform);
				projectile.weaponLevel = Level;

				nextShotTime = Time.time + AttackLength;

			}

		}

	}

}