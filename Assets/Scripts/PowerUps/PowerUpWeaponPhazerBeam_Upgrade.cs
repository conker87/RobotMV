using UnityEngine;
using System.Collections;

public class PowerUpWeaponPhazerBeam_Upgrade : PowerUp {

	// These are MULTIPLICATED modifiers!
	public float AttackLengthMod = 2f, CooldownMod = 0.5f, DamageMod = 2f;

	public override void Give() {
		
		// Player.Current.Weapon_PhazerBeam_AttackLengthMod 	*= AttackLengthMod;
		Player.Current.Weapon_PhazerBeam_CooldownMod 		*= CooldownMod;
		Player.Current.Weapon_PhazerBeam_DamageMod			*= DamageMod;

		base.Give ();

	}

}