using UnityEngine;
using System.Collections;

public class PowerUpWeaponLaser_Upgrade : PowerUp {

	// These are MULTIPLICATED modifiers!
	public float AttackLengthMod = 2f, CooldownMod = 0.5f, DamageMod = 2f;

	public override void Give() {
		
		Player.Current.Weapon_Laser_AttackLengthMod 	*= AttackLengthMod;
		Player.Current.Weapon_Laser_CooldownMod 		*= CooldownMod;
		Player.Current.Weapon_Laser_DamageMod			*= DamageMod;

		base.Give ();

	}

}