using UnityEngine;
using System.Collections;

public class PowerUpItemBomb_Upgrade : PowerUp {

	// These are MULTIPLICATED modifiers!
	public float AttackLengthMod = 2f, CooldownMod = 0.5f, DamageMod = 2f;

	public override void Give() {

		Player.Current.ItemBombAttackLengthMod 		*= AttackLengthMod;
		Player.Current.ItemBombAttackLengthMod 		*= CooldownMod;
		Player.Current.ItemBombAttackLengthMod		*= DamageMod;

		base.Give ();

	}

}
