using UnityEngine;
using System.Collections;

public class PowerUpWeaponBasicBlaster_Upgrade : PowerUp {

	[Header("These are MULTIPLICATED modifiers!")]
	public float AttackLengthMod = 2f, CooldownMod = 0.5f, DamageMod = 2f;

	public override void Give() {
		
		// Player.Current.Weapon_BasicBlaster_AttackLengthMod 	*= AttackLegnthMod;
		Player.Current.WeaponBasicBlasterCooldownMod 		*= CooldownMod;
		Player.Current.WeaponBasicBlasterDamageMod		*= DamageMod;

		base.Give ();

	}

}