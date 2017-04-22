using UnityEngine;
using System.Collections;

public class PowerUpWeaponPhazerBeam_Upgrade : PowerUp {

	[Header("These are MULTIPLICATED modifiers!")]
	public float AttackLengthMod = 2f, CooldownMod = 0.5f, DamageMod = 2f;

	public override void Give() {
		
		// Player.Current.Weapon_PhazerBeam_AttackLengthMod 	*= AttackLengthMod;
		Player.Current.WeaponPhazerBeamCooldownMod 		*= CooldownMod;
		Player.Current.WeaponPhazerBeamDamageMod			*= DamageMod;

		base.Give ();

	}

}