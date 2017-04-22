using UnityEngine;
using System.Collections;

public class PowerUpWeaponBlackHoleBurst_Upgrade : PowerUp {

	// These are MULTIPLICATED modifiers!
	public float AttackLengthMod = 2f, CooldownMod = 0.5f, DamageMod = 2f;

	public override void Give() {
		
		Player.Current.WeaponBlackHoleBurstAttackLengthMod 	*= AttackLengthMod;
		Player.Current.WeaponBlackHoleBurstCooldownMod 		*= CooldownMod;
		Player.Current.WeaponBlackHoleBurstDamageMod			*= DamageMod;

		base.Give ();

	}

}