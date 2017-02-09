using UnityEngine;
using System.Collections;

public class PowerUpWeaponBasicBlaster_ChargedShot_Upgrade : PowerUp {

	// These are MULTIPLICATED modifiers!
	public float AttackLengthMod = 2f, CooldownMod = 0.5f, DamageMod = 2f;

	public override void Give() {
		
		// Player.Current.Weapon_BasicBlaster_AttackLengthMod 	*= AttackLegnthMod;
		// Player.Current.Weapon_BasicBlaster_CooldownMod 		*= CooldownMod;
		Player.Current.Weapon_BasicBlaster_ChargedShot_DamageMod *= DamageMod;

		base.Give ();

	}

}