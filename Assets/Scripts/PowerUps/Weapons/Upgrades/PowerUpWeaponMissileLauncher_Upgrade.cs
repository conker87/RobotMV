using UnityEngine;
using System.Collections;

public class PowerUpWeaponMissileLauncher_Upgrade : PowerUp {

	// These are MULTIPLICATED modifiers!
	public float AttackLengthMod = 2f, CooldownMod = 0.5f, DamageMod = 2f;

	public override void Give() {
		
		Player.Current.WeaponLaserAttackLengthMod 	*= AttackLengthMod;
		Player.Current.WeaponLaserCooldownMod 		*= CooldownMod;
		Player.Current.WeaponLaserDamageMod			*= DamageMod;

		base.Give ();

	}

}