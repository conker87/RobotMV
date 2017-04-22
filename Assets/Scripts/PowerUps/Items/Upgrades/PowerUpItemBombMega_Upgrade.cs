using UnityEngine;
using System.Collections;

public class PowerUpItemBombMega_Upgrade : PowerUp {

	// These are MULTIPLICATED modifiers!
	public float AttackLengthMod = 2f, CooldownMod = 0.5f, DamageMod = 2f;

	public override void Give() {

		Player.Current.ItemBombMegaAttackLengthMod 		*= AttackLengthMod;
		Player.Current.ItemBombMegaAttackLengthMod 		*= CooldownMod;
		Player.Current.ItemBombMegaAttackLengthMod		*= DamageMod;

		base.Give ();

	}

}
